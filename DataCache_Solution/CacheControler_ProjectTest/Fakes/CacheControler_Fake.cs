using CacheControler_Project.Classes;
using CacheControler_Project.Enums;
using CacheControler_ProjectTest.Fakes.MoqInterface;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Exceptions;
using ConnectionControler_ProjectTest.Fakes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CacheControler_ProjectTest.Fakes
{
    public class CacheControler_Fake
    {
        private static Mock<ICacheToConnectOps> m_ConnectionControler = new Mock<ICacheToConnectOps>();

        private Dictionary<DSpanGeoReq, CacheHit> cachedConsumption;    // Cached consumption
        private List<AuditRecord> cachedAudit;                          // Cached audit
        private Dictionary<string, string> cachedGeo;                   // Cached geo

        private readonly object cacheLock = new object();               // Consumption mutex
        private List<DSpanGeoReq> orderedCacheOverlayKeys;              //For free space strategy, both lists are alliegned
        private List<int> orderedCacheOverlayHits;
        private readonly int cacheValidPeriod;                          // Cached record is-valid period in hours              
        private readonly int garbageCollectorInterval;                  // Garbage collector awakening interval for clearing outdated cached records
        private DateTime futureCheckpoint;                              // Time when cache records expire

        private readonly int maxAudit;
        public readonly int maxConsumptionRecords;
        private readonly int maxGeo;
        private int freeConsumptionRecords;
        private int freeAuditRecords;
        private int freeGeoRecords;
        private bool dbOnline;
        private bool auditAcceptable;
        private bool geoAcceptable;

        private static bool keepGCThreadAlive = true;

        //For emulating different scenarios
        public static  Mock<ICacheToConnectOps>  Proxy()
        {
            return m_ConnectionControler;
        }
        //

        public CacheControler_Fake()
        {
            cacheValidPeriod = 6;
            garbageCollectorInterval = 100;                                 //Garbage collector awakens 
            maxAudit = 5;                                                   // Emulate with 1 second
            maxConsumptionRecords = 5;                                     
            maxGeo = 5;
            freeConsumptionRecords = maxConsumptionRecords;
            futureCheckpoint = DateTime.Now.AddMilliseconds(cacheValidPeriod * 1000); //Add milliseconds emulate larger interval (3seconds instead of 3 hours)

            cachedConsumption = new Dictionary<DSpanGeoReq, CacheHit>();    // Initially without consumption
            orderedCacheOverlayKeys = new List<DSpanGeoReq>();              // For delete strategy
            orderedCacheOverlayHits = new List<int>();

            cachedAudit = new List<AuditRecord>();                          // In case Remote DB Fails
            cachedGeo = new Dictionary<string, string>();
            dbOnline = true;
            auditAcceptable = true;
            geoAcceptable = true;

            try
            {
                dbOnline = m_ConnectionControler.Object.Echo();                        //Check if Db is 
            }
            catch (DBOfflineException)
            {
                dbOnline = false;
            }

            if (DBOnline)                                                        //If it's online
            {
                List<AuditRecord> candidateAudit;
                Dictionary<string, string> candidateGeo;
                try
                {
                    candidateAudit = m_ConnectionControler.Object.ReadAuditContnet();  //Try to retrive initial data
                    if (candidateAudit.Count > maxAudit)
                        auditAcceptable = false;
                    else
                    {
                        auditAcceptable = true;
                        cachedAudit = candidateAudit;
                        freeAuditRecords = maxAudit - cachedAudit.Count;
                    }

                    candidateGeo = m_ConnectionControler.Object.ReadGeoContent();
                    if (candidateGeo.Count > maxGeo)
                        geoAcceptable = false;
                    else
                    {
                        geoAcceptable = true;
                        cachedGeo = candidateGeo;
                        freeGeoRecords = maxGeo - cachedGeo.Count;
                    }
                }
                catch (DBOfflineException)
                {
                    dbOnline = false;
                }


            }

            Task.Run(() => CacheGarbageCollector());                        // Start background Grabage collector task
        }

        ~CacheControler_Fake()
        {
            keepGCThreadAlive = false;
        }

        public bool DBOnline { get { return dbOnline; } }
        public bool AuditAcceptable { get { return auditAcceptable; } }
        public bool GeoAcceptable { get { return geoAcceptable; } }

        public List<AuditRecord> CachedAudit { get => cachedAudit; }
        public Dictionary<string, string> CachedGeo { get => cachedGeo; }

        public bool DBTryReconnect()
        {
            try
            {
                dbOnline = m_ConnectionControler.Object.TryReconnect();
                if (!m_ConnectionControler.Object.TryReconnect()) return false;
                List<AuditRecord> candidateAudit = m_ConnectionControler.Object.ReadAuditContnet();        //Try to retrive initial data

                if (candidateAudit.Count > maxAudit)
                    auditAcceptable = false;
                else
                {
                    auditAcceptable = true;
                    cachedAudit = candidateAudit;
                    freeAuditRecords = maxAudit - cachedAudit.Count;
                }

                Dictionary<string, string> candidateGeo = m_ConnectionControler.Object.ReadGeoContent();
                if (candidateGeo.Count > maxGeo)
                    geoAcceptable = false;
                else
                {
                    geoAcceptable = true;
                    cachedGeo = candidateGeo;
                    freeGeoRecords = maxGeo - cachedGeo.Count;
                }
            }
            catch (DBOfflineException)
            {
                dbOnline = false;
                return dbOnline;
            }
            dbOnline = true;
            return dbOnline;
        }

        /// 
        /// <param name="geoRecord"></param>
        public EPostGeoEntityStatus AddNewGeoEntity(GeoRecord geoRecord)
        {
            if (freeGeoRecords == 0) return EPostGeoEntityStatus.OutOfMemory;

            if (cachedGeo.ContainsKey(geoRecord.GName) && cachedGeo[geoRecord.GName] == geoRecord.GID)
                return EPostGeoEntityStatus.DBWriteAborted;    // GID already recorded

            try
            {
                if (m_ConnectionControler.Object.GeoEntityWrite(geoRecord))
                {
                    cachedGeo.Add(geoRecord.GName, geoRecord.GID);
                    freeGeoRecords--;
                    dbOnline = true;
                }

            }
            catch (DBOfflineException)
            {
                dbOnline = false;
                return EPostGeoEntityStatus.DBWriteFailed;
            }
            return EPostGeoEntityStatus.Success;
        }

        private void CacheGarbageCollector()    // Task body awakens each minute and delete records 
        {
            int targetIndex;
            DateTime tmp;
            int timeVal;
            while (keepGCThreadAlive)
            {
      
                lock (cacheLock)
                {
                    foreach (var elem in cachedConsumption)
                    {
                        timeVal = DateTime.Compare(elem.Value.HitTime, futureCheckpoint);
                        if (timeVal <= 0)    // Record younger than 3 hours => still valid, if not:
                        {
                            targetIndex = orderedCacheOverlayKeys.IndexOf(elem.Key);          // Find index
                            freeConsumptionRecords += orderedCacheOverlayHits[targetIndex];   // Update free space
                            orderedCacheOverlayHits.RemoveAt(targetIndex);                    // Remove from cache-hit array
                            orderedCacheOverlayKeys.RemoveAt(targetIndex);                    // Remoce from keys array
                            cachedConsumption.Remove(elem.Key);
                        }
                    }
                    if (DateTime.Compare(DateTime.Now, futureCheckpoint) > 0) futureCheckpoint = DateTime.Now; 
                }
                Thread.Sleep(garbageCollectorInterval);
            }

        }

        private void UpdateOrder(DSpanGeoReq dSPanGeoReq)   //Keeps most hitted ones "on top"
        {

            int elemLoc = orderedCacheOverlayKeys.IndexOf(dSPanGeoReq);  // Element location (in overlay lists)
            int hitVal = orderedCacheOverlayHits[elemLoc];               // Its hit rate

            int targetLoc = elemLoc - 1;
            while (targetLoc >= 0 && orderedCacheOverlayHits[targetLoc] > hitVal) --targetLoc;   // Skip to find place

            if (targetLoc > 0 && targetLoc < orderedCacheOverlayKeys.Count - 2) //Keep index inside range
            {
                orderedCacheOverlayKeys.Insert(targetLoc, dSPanGeoReq);   // Replace
                orderedCacheOverlayKeys.RemoveAt(elemLoc + 1);            // Delete old
                orderedCacheOverlayHits.Insert(targetLoc, hitVal);        // Replace
                orderedCacheOverlayKeys.RemoveAt(elemLoc + 1);            // Delete old
            }

        }

        private Tuple<List<ConsumptionRecord>, DSpanGeoReq> SecondarySubContentScan(DSpanGeoReq dSpanGeoReq)
        {
            int cacheIdx = 0;
            foreach (var cacheHit in cachedConsumption)
            {
                if (cacheHit.Key.GName == dSpanGeoReq.GName && cacheHit.Value.CRecord.Count > 0 &&
                    cacheHit.Value.CRecord[0].CheckTimeRelationMine(dSpanGeoReq.From, "<=", true) &&
                    cacheHit.Value.CRecord[0].CheckTimeRelationMine(dSpanGeoReq.Till, "<=", true))
                {
                    int loc = 0;
                    List<ConsumptionRecord> targetRecord = cacheHit.Value.CRecord;
                    List<ConsumptionRecord> outRecords = new List<ConsumptionRecord>();
                    while (targetRecord[loc].CheckTimeRelationMine(dSpanGeoReq.Till, ">", true)) ++loc; //Find left boudary (newest day), list is Desc

                    while (loc < targetRecord.Count && targetRecord[loc].CheckTimeRelationMine(dSpanGeoReq.From, "<=", true))
                    {
                        outRecords.Add(targetRecord[loc]);
                        ++loc;
                    }
                    outRecords.Reverse();
                    return new Tuple<List<ConsumptionRecord>, DSpanGeoReq>(outRecords, cacheHit.Key);
                }
                cacheIdx++;
            }
            return new Tuple<List<ConsumptionRecord>, DSpanGeoReq>(null, null);
        }

        /// 
        /// <param name="dSpanGeoReq"></param>
        public Tuple<EConcumptionReadStatus, List<ConsumptionRecord>> DSpanGeoConsumptionRead(DSpanGeoReq dSpanGeoReq)
        {
            if (!cachedGeo.ContainsKey(dSpanGeoReq.GName)) throw new Exception();

            lock (cacheLock)
            {
                if (cachedConsumption.ContainsKey(dSpanGeoReq))
                {
                    orderedCacheOverlayHits[orderedCacheOverlayKeys.IndexOf(dSpanGeoReq)]++;   // For free space strategy
                    UpdateOrder(dSpanGeoReq);                                                  // Update order
                    return new Tuple<EConcumptionReadStatus, List<ConsumptionRecord>>
                        (EConcumptionReadStatus.CacheReadSuccess, cachedConsumption[dSpanGeoReq].CRecord);
                }
                else
                {
                    Tuple<List<ConsumptionRecord>, DSpanGeoReq> result;                        //Found as subcontent of existed cache hit
                    if ((result = SecondarySubContentScan(dSpanGeoReq)).Item1 != null)
                    {
                        orderedCacheOverlayHits[orderedCacheOverlayKeys.IndexOf(result.Item2)]++;
                        UpdateOrder(result.Item2);
                        return new Tuple<EConcumptionReadStatus, List<ConsumptionRecord>>
                             (EConcumptionReadStatus.CacheReadSuccess, result.Item1);
                    }

                    List<ConsumptionRecord> readResult;
                    try
                    {
                        //CacheHit

                        readResult = m_ConnectionControler.Object.ConsumptionReqPropagate
                            (new DSpanGeoReq(cachedGeo[dSpanGeoReq.GName], dSpanGeoReq.From, dSpanGeoReq.Till));  //User doesn't know GID only GNAME
                        dbOnline = true;
                    }
                    catch (DBOfflineException)
                    {
                        dbOnline = false;
                        return new Tuple<EConcumptionReadStatus, List<ConsumptionRecord>>
                            (EConcumptionReadStatus.DBReadFailed, new List<ConsumptionRecord>());
                    }

                    if (freeConsumptionRecords < readResult.Count)   // Not enough space
                    {
                        int tmp;
                        int reqSpace = readResult.Count - freeConsumptionRecords;
                        do
                        {
                            tmp = orderedCacheOverlayHits.Count - 1;                    // Delete last one (the least hitted one)                
                            reqSpace -= orderedCacheOverlayHits[tmp];                   // Now you need less memory
                            freeConsumptionRecords += orderedCacheOverlayHits[tmp];     // Record it as free
                            orderedCacheOverlayHits.RemoveAt(tmp);                      // Free it
                            cachedConsumption.Remove(orderedCacheOverlayKeys[tmp]);     // Remove overlays
                            orderedCacheOverlayKeys.RemoveAt(tmp);

                        } while (reqSpace > 0);
                    }

                    // New CacheHit => HitRate == 1 => Add at the end of the list (list stays ordered)
                    orderedCacheOverlayHits.Add(1);
                    orderedCacheOverlayKeys.Add(dSpanGeoReq);
                    cachedConsumption.Add(dSpanGeoReq, new CacheHit(readResult));
                    return new Tuple<EConcumptionReadStatus, List<ConsumptionRecord>>
                        (EConcumptionReadStatus.DBReadSuccess, readResult);
                }
            }
        }

        /// 
        /// <param name="update"></param>
        public bool ConsumptionUpdateHandler(string timeStampBase, ConsumptionUpdate update)
        {
            dbOnline = true;

            bool geoFull = false,
                 auditFull = false;

            if (freeGeoRecords < update.NewGeos.Count) geoFull = true;                  //Has no memory to accept report
            else
            {
                foreach (var geo in update.NewGeos)
                {
                    //if(!cachedGeo.ContainsKey(geo))   //DB side grants this            ????
                    //{
                    cachedGeo.Add(geo, geo); //Loaded from file => same gName & GID
                                             //}
                }
            }

            if (freeAuditRecords < update.DupsAndMisses.Count) auditFull = true;
            else
            {
                foreach (var record in update.DupsAndMisses)
                {
                    foreach (var dup in record.Value.Item1)
                    {
                        cachedAudit.Add(new AuditRecord(record.Key, timeStampBase + "-" + dup.Item1, dup.Item2));
                    }

                    foreach (var miss in record.Value.Item2)
                    {
                        cachedAudit.Add(new AuditRecord(record.Key, timeStampBase + "-" + miss, -1));
                    }

                }

            }

            if (auditFull && geoFull) throw new Exception();    //Later implent custom ones
            if (auditFull) throw new Exception();
            if (geoFull) throw new Exception();


            return true;
        }

        /// 
        /// <param name="geoRecord"></param>
        public EGeoRecordStatus ContainsGeo(GeoRecord geoRecord)
        {
            if (cachedGeo.ContainsKey(geoRecord.GName))
            {
                if (cachedGeo[geoRecord.GName] == geoRecord.GID)
                    return EGeoRecordStatus.GeoRecordUsed;
                else return EGeoRecordStatus.GNameUsed;
            }
            else if (cachedGeo.ContainsValue(geoRecord.GID))
                return EGeoRecordStatus.GIDUsed;

            return EGeoRecordStatus.GeoRecordFree;

        }

        /// 
        /// <param name="oldID"></param>
        /// <param name="newID"></param>
        public EUpdateGeoStatus UpdateGeoEntity(string oldName, string newName)
        {
            if (cachedGeo.ContainsKey(oldName))
            {
                try
                {
                    var retVal = m_ConnectionControler.Object.GeoEntityUpdate(oldName, newName);
                    cachedGeo.Add(newName, cachedGeo[oldName]);
                    cachedGeo.Remove(oldName);
                    return EUpdateGeoStatus.Success;
                }
                catch (Exception)
                { dbOnline = false; return EUpdateGeoStatus.DBWriteFailed; }
            }
            dbOnline = true;
            return EUpdateGeoStatus.OriginNotFound;
        }
    }
}
