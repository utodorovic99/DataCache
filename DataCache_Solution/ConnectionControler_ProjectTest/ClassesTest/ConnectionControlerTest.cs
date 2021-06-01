using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
using ConnectionControler_Project.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class ConnectionControlerTest
    {
        public static Mock<IDBReq> proxy = new Mock<IDBReq>();
        private static bool activityState=true;

        [ExcludeFromCodeCoverage]
        public static bool ActivityState() { return activityState; }

        [ExcludeFromCodeCoverage]
        public static ConsumptionUpdate FakeOstvConsumptionDBWrite(List<ConsumptionRecord> cRecords)
        {

            if (cRecords == null || cRecords.Count == 0) return new ConsumptionUpdate();

            if (proxy == null)
            {
                if (!TryReconnect()) throw new DBOfflineException("Remote Database is currently offline, check network connection and call support.");
            }

            try
            {
                activityState = true;
                return proxy.Object.OstvConsumptionDBWrite(cRecords);
            }
            catch (CommunicationObjectFaultedException)
            {
                activityState = false;
                throw new DBOfflineException("Remote Database is currently offline, check network connection and call support.");
            }
            catch (EndpointNotFoundException)
            {
                activityState = false;
                throw new DBOfflineException("Remote Database is currently offline, check network connection and call support.");
            }
        }

        public static bool TryReconnect()
        {
            activityState = false;
            try
            {
                proxy.Object.Echo();
                activityState = true;
            }
            catch (EndpointNotFoundException)
            {
                activityState = false;

            }
            catch (CommunicationObjectFaultedException)
            {
                activityState = false;
            }
            catch (System.InvalidOperationException) { activityState = false; }
            return activityState;
        }
    }
}
