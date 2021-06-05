using Common_Project.Classes;
using Common_Project.DistributedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheControler_ProjectTest.Fakes.MoqInterface
{
    public interface ICacheToConnectOps: IDBReq
    {
        bool TryReconnect();
        bool AddNewGeoEntity(GeoRecord record);
    }
}
