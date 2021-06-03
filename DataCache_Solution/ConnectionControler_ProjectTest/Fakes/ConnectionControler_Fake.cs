using CacheControler_Project.Enums;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using Common_Project.Exceptions;
using ConnectionControler_Project.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionControler_ProjectTest.Fakes
{


    public class ConnectionControler_Fake:IDBReq
    {
        private class FakeChannelFactory
        {
            public FakeChannelFactory()
            {

            }

            public Mock<IDBReq> CreateChannel() 
            {
                if (proxy != null) return proxy;
                else
                {
                    var channel = new  Mock<IDBReq>();

                    channel.Setup(x => x.Echo()).Returns(true);

                    return channel;
                }
                                   
            }
        }

        public static Mock<IDBReq> proxy = null;
        private static bool activityState = true;
        private static readonly object singletoneMtx = new object();
        private static ConnectionControler_Fake singletoneInstance = null;
        private static FakeChannelFactory consumptionChannel;

        public static bool ActivityState() { return activityState; }

        [ExcludeFromCodeCoverage]
        public static Mock<IDBReq> Proxy() { return proxy; }

        public static ConnectionControler_Fake Instance
        {
            get
            {
                lock (singletoneMtx)
                {
                    if (singletoneInstance == null)
                    {
                        singletoneInstance = new ConnectionControler_Fake();
                    }
                }
                return singletoneInstance;
            }
        }

        private ConnectionControler_Fake()
        {
            activityState = false;
            consumptionChannel = new FakeChannelFactory();
            proxy = consumptionChannel.CreateChannel();
            activityState = true;
        }

        public bool TryReconnect()
        {
            activityState = false;
            try
            {
                consumptionChannel = new FakeChannelFactory();
                proxy = consumptionChannel.CreateChannel();
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

        public bool Echo()
        {
            try
            {
                activityState = true;
                return proxy.Object.Echo();
            }
            catch (EndpointNotFoundException)
            {
                activityState = false;
                throw new DBOfflineException("Remote Database is currently offline, check network connection and call support.");
            }
            catch (CommunicationObjectFaultedException)
            {
                activityState = false;
                throw new DBOfflineException("Remote Database is currently offline, check network connection and call support.");
            }

        }

        public ConsumptionUpdate OstvConsumptionDBWrite(List<ConsumptionRecord> cRecords)
        {

            if (cRecords == null || cRecords.Count == 0) return new ConsumptionUpdate();

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

        public List<ConsumptionRecord> ConsumptionReqPropagate(DSpanGeoReq dSpanGeoReq)
        {
            if (dSpanGeoReq == null) throw new InvalidParamsException("Empty request sent");
            if (!dSpanGeoReq.IsComplete()) throw new InvalidParamsException("Incompleted request");

            try
            {
                activityState = true;
                return proxy.Object.ConsumptionReqPropagate(dSpanGeoReq);
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

        public List<AuditRecord> ReadAuditContnet()
        {
            try
            {
                activityState = true;
                return proxy.Object.ReadAuditContnet();
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

        public EUpdateGeoStatus GeoEntityUpdate(string oldName, string newName)
        {

            if (oldName == null || newName == null) throw new InvalidParamsException("Null param detected");
            if (oldName == "" || newName == "")     throw new InvalidParamsException("Empty param detected");
            if (oldName == newName)                 return EUpdateGeoStatus.ReqAborted;

            try
            {
                activityState = true;
                return proxy.Object.GeoEntityUpdate(oldName, newName);
            }
            catch (CommunicationObjectFaultedException)
            {
                activityState = false;
                return EUpdateGeoStatus.DBWriteFailed;
            }
            catch (EndpointNotFoundException)
            {
                activityState = false;
                return EUpdateGeoStatus.DBWriteFailed;
            }

        }
        public bool GeoEntityWrite(GeoRecord gRecord)
        {
            if (gRecord == null || !gRecord.IsComplete()) return false;

            try
            {
                activityState = true;
                return proxy.Object.GeoEntityWrite(gRecord);
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

        public Dictionary<string, string> ReadGeoContent()
        {

            try
            {
                activityState = true;
                return proxy.Object.ReadGeoContent();
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

    }
}
