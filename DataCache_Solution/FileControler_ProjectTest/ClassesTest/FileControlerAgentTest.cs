using FileControler_Project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common_Project;
using CacheControler_Project;
using ConnectionControler_Project;
using DistributedDB_Project;
using FileControler_Project;
using GUI_Integrator_Project;
using UI_Project;
using DistributedDB_Project.DistributedDBCallHandler;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
using ConnectionControler_ProjectTest.ClassesTest;
using Moq;
using Common_Project.Classes;
using ConnectionControler_Project.Exceptions;
using System.ServiceModel;
using NUnit.Framework;

namespace FileControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class FileControlerAgentTest
    {
        
        [Test]
        public void FileControlerAgent_TestContrictor_ReturnsInstance()
        {
            //Act
            FileControlerAgent controler = new FileControlerAgent();
            FileControlerAgent controler2 = new FileControlerAgent();

            //Assert
            Assert.IsNotNull(controler);
        }



        [Test]
        public void OstvConsumptionDBWrite_FakeTryWriteEmptyParam_ReturnsEmptyUpdate()
        {
            //Arrange
            List<ConsumptionRecord> inputParam = new List<ConsumptionRecord>();
            ConsumptionUpdate retVal;

            //Act
            retVal=ConnectionControlerTest.FakeOstvConsumptionDBWrite(inputParam);

            //Assert
            Assert.IsNotNull(retVal);
            Assert.AreEqual(0, retVal.NewGeos.Count);
            Assert.AreEqual(0, retVal.DupsAndMisses.Count);
            Assert.AreEqual("", retVal.TimeStampBase);
        }

        [Test]
        public void OstvConsumptionDBWrite_FakeTryWriteNullParam_ReturnsEmptyUpdate()
        {
            //Arrange
            List<ConsumptionRecord> inputParam = null;
            ConsumptionUpdate retVal;

            //Act
            retVal = ConnectionControlerTest.FakeOstvConsumptionDBWrite(inputParam);

            //Assert
            Assert.IsNotNull(retVal);
            Assert.AreEqual(0, retVal.NewGeos.Count);
            Assert.AreEqual(0, retVal.DupsAndMisses.Count);
            Assert.AreEqual("", retVal.TimeStampBase);
        }


        [Test]
        public void TryReconnect_Try_FailedDBOffline()
        {
            //Arrange
            List<ConsumptionRecord> inputParam = new List<ConsumptionRecord>();
            inputParam.Add(new ConsumptionRecord());
            ConsumptionUpdate retVal;
            ConnectionControlerTest.proxy.Setup(x => x.OstvConsumptionDBWrite(inputParam)).
                Throws(new CommunicationObjectFaultedException());

            //Act
            try
            {
                retVal = ConnectionControlerTest.FakeOstvConsumptionDBWrite(inputParam);
                Assert.Fail();
            }
            catch (DBOfflineException ex) 
            {
                Assert.IsFalse(ConnectionControlerTest.ActivityState());
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }
    }
}
