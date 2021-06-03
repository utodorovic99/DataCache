using Common_Project.DistributedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Project.ClientServices;

namespace CacheControler_Project.Services
{
    interface ICacheReq: IConsumptionReq, IAuditReq, IGeographyReq, IFunctionalReq
    {
    }
}
