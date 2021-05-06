using CacheControler_Project.Enums;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheControler_Project.Classes
{
    public class CacheControlerAgent : IConsumptionReq, IAuditReq, IGeographyReq
    {
        private ConnectionControler connectionControler;

        public CacheControlerAgent()
        {
            connectionControler = new ConnectionControler();
        }

        public List<ConsumptionRecord> ConsumptionReqPropagate(DSpanGeoReq dSpanGeoReq)
        {
            return connectionControler.ConsumptionReqPropagate(dSpanGeoReq);
        }

        public EUpdateGeoStatus GeoEntityUpdate(string oldID, string newID)
        {
            return connectionControler.GeoEntityUpdate(oldID, newID);
        }

        public bool GeoEntityWrite(GeoRecord gRecord)
        {
            return connectionControler.GeoEntityWrite(gRecord);
        }

        public List<AuditRecord> ReadAuditContnet()
        {
            return connectionControler.ReadAuditContnet();
        }

        public List<GeoRecord> ReadGeoContent()
        {
            return ReadGeoContent();
        }
    }
}