﻿using CacheControler_Project.Enums;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using DistributedDB_Project.Exceptions.ExceptionAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DistributedDBCallHandler
{
    public class DataCacheClientService : IDBReq
    {
        private readonly AuditService auditService = new AuditService();
        private readonly ConsumptionService consumptionService = new ConsumptionService();
        private readonly GeographyService geographyService = new GeographyService();

        public List<ConsumptionRecord> ConsumptionReqPropagate(DSpanGeoReq dSpanGeoReq)
        {
             return consumptionService.HandleGetByCountryAndDatespan(dSpanGeoReq.GName, dSpanGeoReq.From, dSpanGeoReq.Till);
        }

        public EUpdateGeoStatus GeoEntityUpdate(string oldID, string newID)
        {
            try {geographyService.HandleSingleGeoUpdate(oldID, newID); }
            catch (PrimaryKeyConstraintViolationException)
            {
                return EUpdateGeoStatus.OriginNotFound;
            }
            return EUpdateGeoStatus.Success;
        }

        public bool GeoEntityWrite(GeoRecord gRecord)
        {
            return geographyService.HandleSignleGeoWrite(gRecord);
        }

        public ConsumptionUpdate OstvConsumptionDBWrite(List<ConsumptionRecord> cRecords)
        {
            return consumptionService.HandleStoreConsumption(cRecords);
        }

        public List<AuditRecord> ReadAuditContnet()
        {
            return auditService.HandleShowAll();
        }

        public List<GeoRecord> ReadGeoContent()
        {
            return geographyService.HandleShowAll();
        }
    }
}