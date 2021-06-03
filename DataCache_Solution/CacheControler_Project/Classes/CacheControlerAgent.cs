using CacheControler_Project.Enums;
using CacheControler_Project.Services;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Project.ClientServices;

namespace CacheControler_Project.Classes
{
    [ExcludeFromCodeCoverage]
    public class CacheControlerAgent : ICacheReq
    {
        private ConnectionControler connectionControler;
        public CacheControlerAgent()
        {
            connectionControler = ConnectionControler.Instance;
        }
        public bool TryReconnect() { return connectionControler.TryReconnect(); }

        public List<ConsumptionRecord> ConsumptionReqPropagate(DSpanGeoReq dSpanGeoReq)
        {
            return connectionControler.ConsumptionReqPropagate(dSpanGeoReq);
        }

        public bool Echo()
        {
            return connectionControler.Echo();
        }

        public EUpdateGeoStatus GeoEntityUpdate(string oldName, string newName)
        {
            return connectionControler.GeoEntityUpdate(oldName, newName);
        }

        public bool GeoEntityWrite(GeoRecord gRecord)
        {
            return connectionControler.GeoEntityWrite(gRecord);
        }

        public List<AuditRecord> ReadAuditContnet()
        {
            return connectionControler.ReadAuditContnet();
        }

        public Dictionary<string, string> ReadGeoContent()
        {
            return connectionControler.ReadGeoContent();
        }
    }
}