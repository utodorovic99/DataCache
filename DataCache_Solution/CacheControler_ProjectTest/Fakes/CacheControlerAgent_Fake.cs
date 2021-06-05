using CacheControler_Project.Enums;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_ProjectTest.Fakes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheControler_ProjectTest.Fakes
{
    public class CacheControlerAgent_Fake
    {
        private ConnectionControler_Fake connectionControler = ConnectionControler_Fake.Instance;
        public ConnectionControler_Fake CacheControlerAgent()
        {
           return connectionControler;
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
