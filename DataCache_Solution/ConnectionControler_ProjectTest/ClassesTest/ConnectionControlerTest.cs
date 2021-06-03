using CacheControler_Project.Enums;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using Common_Project.Exceptions;
using ConnectionControler_Project.Classes;
using ConnectionControler_Project.Exceptions;
using ConnectionControler_ProjectTest.Fakes;
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

        #region Instance_Tests
        //[Test]
        //public void Instance_TryGetSingletoneInstanceTwice_ReturnsSameInstance()
        //{

        //    //Arrange & Act
        //    var controlerFirst = ConnectionControler_Fake.Instance;
        //    var controlerSecond = ConnectionControler_Fake.Instance;

        //    //Assert
        //    Assert.IsNotNull(controlerFirst);
        //    Assert.IsNotNull(controlerSecond);
        //    Assert.AreSame(controlerFirst, controlerSecond);
        //    Assert.IsTrue(ConnectionControler_Fake.ActivityState());
        //}
        #endregion

        #region TryReconnect_Tests

        [Test]
        public void TryReconnect_FailedByEndpointNotFoundException_ReturnsFalse()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x=>x.Echo()).Throws( new EndpointNotFoundException());

            //Act
            var status=controler.TryReconnect();

            //Assert
            Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            Assert.IsFalse(status);
        }

        [Test]
        public void TryReconnect_FailedByCommunicationObjectFaultedException_ReturnsFalse()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Throws(new CommunicationObjectFaultedException());

            //Act
            var status = controler.TryReconnect();

            //Assert
            Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            Assert.IsFalse(status);
        }

        [Test]
        public void TryReconnect_FailedByInvalidOperationException_ReturnsFalse()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Throws(new InvalidOperationException());

            //Act
            var status = controler.TryReconnect();

            //Assert
            Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            Assert.IsFalse(status);
        }

        [Test]
        public void TryReconnect_Success_ReturnsTrue()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);

            //Act
            var status = controler.TryReconnect();

            //Assert
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            Assert.IsTrue(status);
        }

        #endregion

        #region Echo_Tests
        [Test]
        public void Echo_FailedByEndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Throws(new EndpointNotFoundException());
            bool status=false;

            //Act & Assert
            try 
            {
                status = controler.Echo();
                Assert.Fail();
            }
            catch (DBOfflineException ex) 
            {
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
                Assert.IsFalse(status);
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
            
        }

        [Test]
        public void Echo_FailedByCommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Throws(new CommunicationObjectFaultedException());
            bool status = false;

            //Act & Assert
            try
            {
                status = controler.Echo();
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
                Assert.IsFalse(status);
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void Echo_Success_ReturnsTrue()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            
            //Act
            bool status = controler.Echo();
            //Assert
            Assert.IsTrue(status);
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
        }
        #endregion

        #region OstvConsumptionDBWrite_Tests
        [Test]
        public void OstvConsumptionDBWrite_TryEmptyInput_RetursnsEmptyOutput()
        {
            //Arrange
            List<ConsumptionRecord> input = new List<ConsumptionRecord>();
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act
            var result=controler.OstvConsumptionDBWrite(input);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.DupsAndMisses.Count);
            Assert.AreEqual(0, result.NewGeos.Count);
        }

        [Test]
        public void OstvConsumptionDBWrite_TryNullInput_RetursnsEmptyOutput()
        {
            //Arrange
            List<ConsumptionRecord> input = null;
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act
            var result = controler.OstvConsumptionDBWrite(input);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.DupsAndMisses.Count);
            Assert.AreEqual(0, result.NewGeos.Count);
        }

        [Test]
        public void OstvConsumptionDBWrite_CommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            List<ConsumptionRecord> input = new List<ConsumptionRecord>();
            input.Add(new ConsumptionRecord("SRB", 22441, "2021-05-05-01"));
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.OstvConsumptionDBWrite(input)).Throws(new CommunicationObjectFaultedException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act
            try
            {
                controler.OstvConsumptionDBWrite(input);
                Assert.Fail();
            } 
            catch(DBOfflineException ex)
            {
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void OstvConsumptionDBWrite_EndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            List<ConsumptionRecord> input = new List<ConsumptionRecord>();
            input.Add(new ConsumptionRecord("SRB", 22441, "2021-05-05-01"));
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.OstvConsumptionDBWrite(input)).Throws(new EndpointNotFoundException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act
            try
            {
                controler.OstvConsumptionDBWrite(input);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void OstvConsumptionDBWrite_Success_ReturnsNotEmptyUpdate()
        {
            //Arrange
            List<ConsumptionRecord> input = new List<ConsumptionRecord>();
            input.Add(new ConsumptionRecord("SRB", 22441, "2021-05-05-01"));
            input.Add(new ConsumptionRecord("SRB", 1111, "2021-05-05-01"));
            input.Add(new ConsumptionRecord("SRB", 2222, "2021-05-05-01"));
            input.Add(new ConsumptionRecord("SRB", -1, "2021-05-05-02"));
            input.Add(new ConsumptionRecord("SRB", -1, "2021-05-05-03"));
            var controler = ConnectionControler_Fake.Instance;

            ConsumptionUpdate retUpdate = new ConsumptionUpdate();
            Tuple<List<Tuple<int, int>>, List<int>> updateDupsAndMisses = new Tuple<List<Tuple<int, int>>, 
                List<int>>( new List<Tuple<int, int>>(), new List<int>()) ;
            updateDupsAndMisses.Item1.Add(new Tuple<int, int>(1, 1111));
            updateDupsAndMisses.Item1.Add(new Tuple<int, int>(1, 2222));
            updateDupsAndMisses.Item2.Add(2);
            updateDupsAndMisses.Item2.Add(3);

            retUpdate.TimeStampBase = "2021-05-05";
            retUpdate.DupsAndMisses.Add("SRB", updateDupsAndMisses);
            ConnectionControler_Fake.proxy.Setup(x => x.OstvConsumptionDBWrite(input)).Returns(retUpdate);
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();
            ConsumptionUpdate result;

            retUpdate.NewGeos.Add("SRB");

            //Act
            result = controler.OstvConsumptionDBWrite(input);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateDupsAndMisses.Item1, result.DupsAndMisses["SRB"].Item1);
            Assert.AreEqual(updateDupsAndMisses.Item2, result.DupsAndMisses["SRB"].Item2);
            Assert.AreEqual(retUpdate.NewGeos, result.NewGeos);
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
        }

        #endregion

        #region ConsumptionReqPropagate_Tests
        [Test]
        public void ConsumptionReqPropagate_TryOnIncompleteReqGName_ThrowsInvalidParamsException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();
            DSpanGeoReq inputParam = new DSpanGeoReq("", "2021-05-05", "2021-08-08");

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(inputParam);
                Assert.Fail();
            }
            catch(InvalidParamsException ex)
            {
                Assert.AreEqual("Incompleted request", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void ConsumptionReqPropagate_TryOnIncompleteReqFrom_ThrowsInvalidParamsException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();
            DSpanGeoReq inputParam = new DSpanGeoReq("SRB", "", "2021-08-08");

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(inputParam);
                Assert.Fail();
            }
            catch (InvalidParamsException ex)
            {
                Assert.AreEqual("Incompleted request", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void ConsumptionReqPropagate_TryOnIncompleteReqTill_ThrowsInvalidParamsException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();
            DSpanGeoReq inputParam = new DSpanGeoReq("SRB", "2021-05-05", "");

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(inputParam);
                Assert.Fail();
            }
            catch (InvalidParamsException ex)
            {
                Assert.AreEqual("Incompleted request", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void ConsumptionReqPropagate_TryOnNullReq_ThrowsInvalidParamsException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();
            DSpanGeoReq inputParam = null;

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(inputParam);
                Assert.Fail();
            }
            catch (InvalidParamsException ex)
            {
                Assert.AreEqual("Empty request sent", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void ConsumptionReqPropagate_CommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            DSpanGeoReq inputParam = new DSpanGeoReq("SRB", "2021-05-05", "2021-10-10");
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();
            ConnectionControler_Fake.proxy.Setup(x => x.ConsumptionReqPropagate(inputParam)).
                Throws(new CommunicationObjectFaultedException());

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(inputParam);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void ConsumptionReqPropagate_EndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            DSpanGeoReq inputParam = new DSpanGeoReq("SRB", "2021-05-05", "2021-10-10");
            ConnectionControler_Fake.proxy.Setup(x => x.ConsumptionReqPropagate(inputParam)).
                Throws(new EndpointNotFoundException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(inputParam);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void ConsumptionReqPropagate_TryPropagate_Success()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;

            DSpanGeoReq inputParam = new DSpanGeoReq("SRB", "2021-05-05", "2021-10-10");
            List<ConsumptionRecord> output = new List<ConsumptionRecord>();
            output.Add(new ConsumptionRecord("SRB", 20000, "2021-05-05-1"));
            output.Add(new ConsumptionRecord("SRB", 20023, "2021-07-05-2"));
            output.Add(new ConsumptionRecord("SRB", 20033, "2021-09-05-3"));

            ConnectionControler_Fake.proxy.Setup(x => x.ConsumptionReqPropagate(inputParam)).Returns(output);
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var retVal=controler.ConsumptionReqPropagate(inputParam);

            //Assert
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            Assert.IsNotNull(retVal);
        }
        #endregion

        #region ReadAuditContent_Tests

        [Test]
        public void ReadAuditContent_CommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x=>x.ReadAuditContnet()).Throws(new CommunicationObjectFaultedException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try 
            {
                controler.ReadAuditContnet();
                Assert.Fail();
            }
            catch(DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void ReadAuditContent_EndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.ReadAuditContnet()).Throws(new EndpointNotFoundException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.ReadAuditContnet();
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void ReadAuditContent_TryRead_Success()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            List<AuditRecord> output = new List<AuditRecord>();
            output.Add(new AuditRecord("SRB", "2021-05-05-13", 12345));
            output.Add(new AuditRecord("SRB", "2021-05-05-18", 88745));
            output.Add(new AuditRecord("SRB", "2021-05-05-13", -1));

            ConnectionControler_Fake.proxy.Setup(x => x.ReadAuditContnet()).Returns(output);
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var retVal=controler.ReadAuditContnet();

            //Assert
            Assert.IsNotNull(output);
        }
        #endregion

        #region GeoEntityUpdate_Tests
        [Test]
        public void GeoEntityUpdate_OldNameEmpty_ThrowsInvalidParamsException()
        {
            //Arrange
            string oldName = "";
            string newName = "SERBIA";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.GeoEntityUpdate(oldName, newName);
                Assert.Fail();
            }
            catch(InvalidParamsException ex)
            {
                Assert.AreEqual("Empty param detected", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void GeoEntityUpdate_NewNameEmpty_ThrowsInvalidParamsException()
        {
            //Arrange
            string oldName = "SERBIA";
            string newName = "";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.GeoEntityUpdate(oldName, newName);
                Assert.Fail();
            }
            catch (InvalidParamsException ex)
            {
                Assert.AreEqual("Empty param detected", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void GeoEntityUpdate_OldNameNull_ThrowsInvalidParamsException()
        {
            //Arrange
            string oldName = null;
            string newName = "SERBIA";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.GeoEntityUpdate(oldName, newName);
                Assert.Fail();
            }
            catch (InvalidParamsException ex)
            {
                Assert.AreEqual("Null param detected", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void GeoEntityUpdate_NewNameNull_ThrowsInvalidParamsException()
        {
            //Arrange
            string oldName = "SERBIA";
            string newName = null;
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.GeoEntityUpdate(oldName, newName);
                Assert.Fail();
            }
            catch (InvalidParamsException ex)
            {
                Assert.AreEqual("Null param detected", ex.Summary);
                Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void GeoEntityUpdate_CommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            string oldName = "DZSCG";
            string newName = "SERBIA";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityUpdate(oldName, newName)).
                Throws(new CommunicationObjectFaultedException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var retVal = controler.GeoEntityUpdate(oldName, newName);

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.DBWriteFailed, retVal);
            Assert.IsFalse(ConnectionControler_Fake.ActivityState());
        }

        [Test]
        public void GeoEntityUpdate_EndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            string oldName = "DZSCG";
            string newName = "SERBIA";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityUpdate(oldName, newName)).
                Throws(new EndpointNotFoundException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var retVal = controler.GeoEntityUpdate(oldName, newName);

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.DBWriteFailed, retVal);
            Assert.IsFalse(ConnectionControler_Fake.ActivityState());
        }

        [Test]
        public void GeoEntityUpdate_TryUpdate_Succesfull()
        {
            //Arrange
            string oldName = "DZSCG";
            string newName = "SERBIA";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityUpdate(oldName, newName)).
                Returns(EUpdateGeoStatus.Success);
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var retVal = controler.GeoEntityUpdate(oldName, newName);

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.Success, retVal);
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
        }

        [Test]
        public void GeoEntityUpdate_TryUpdateSameParams_Returns ()
        {
            //Arrange
            string oldName = "SERBIA";
            string newName = "SERBIA";
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityUpdate(oldName, newName)).
                Returns(EUpdateGeoStatus.Success);

            //Act 
            var retVal = controler.GeoEntityUpdate(oldName, newName);

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.ReqAborted, retVal);
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
        }
        #endregion

        #region GeoEntityWrite_Tests
        [Test]
        public void GeoEntityWrite_GeoRecordNull_ReturnsFalse()
        {
            //Arrange
            GeoRecord record = null;
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var result =controler.GeoEntityWrite(record);

            //Assert
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            Assert.IsFalse(result);
        }

        [Test]
        public void GeoEntityWrite_GeoRecordIncomplete_ReturnsFalse()
        {
            //Arrange
            GeoRecord record = new GeoRecord("SRB","");
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act 
            var result = controler.GeoEntityWrite(record);

            //Assert
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
            Assert.IsFalse(result);
        }

        [Test]
        public void GeoEntityWrite_CommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            GeoRecord record = new GeoRecord("SRB", "SERBIA");
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityWrite(record)).
                Throws(new CommunicationObjectFaultedException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.GeoEntityWrite(record);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            }

        }

        [Test]
        public void GeoEntityWrite_EndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            GeoRecord record = new GeoRecord("SRB", "SERBIA");
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityWrite(record)).
                Throws(new EndpointNotFoundException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.GeoEntityWrite(record);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
            }
        }

        [Test]
        public void GeoEntityWrite_TryWrite_Success()
        {
            //Arrange
            GeoRecord record = new GeoRecord("SRB", "SERBIA");
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.GeoEntityWrite(record)).
                Returns(true);
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            var result= controler.GeoEntityWrite(record);

            Assert.IsTrue(result);
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());  
        }
        #endregion

        #region ReadGeoContent_Tests
        [Test]
        public void ReadGeoContent_CommunicationObjectFaultedException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.ReadGeoContent()).
                Throws(new CommunicationObjectFaultedException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.ReadGeoContent();
                Assert.Fail();
            }
            catch(DBOfflineException ex)
            {
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void ReadGeoContent_EndpointNotFoundException_ThrowsDBOfflineException()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.ReadGeoContent()).
                Throws(new EndpointNotFoundException());
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            //Act & Assert
            try
            {
                controler.ReadGeoContent();
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.IsFalse(ConnectionControler_Fake.ActivityState());
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }

        [Test]
        public void ReadGeoContent_TryGet_Success()
        {
            //Arrange
            var controler = ConnectionControler_Fake.Instance;
            ConnectionControler_Fake.proxy.Setup(x => x.Echo()).Returns(true);
            controler.Echo();

            Dictionary<string, string> outVal = new Dictionary<string, string>();
            outVal.Add("SRB", "SERBIA");
            outVal.Add("MNE", "MONTENEGRO");
            Dictionary<string, string> retVal;
            ConnectionControler_Fake.proxy.Setup(x => x.ReadGeoContent()).Returns(outVal);

            //Act & Assert
            retVal= controler.ReadGeoContent();
            Assert.IsTrue(ConnectionControler_Fake.ActivityState());
        }
        #endregion

        #region Other_Tests
        [Test]
        public void OstvConsumptionDBWrite_TryRecconectFailed_ThrowsDBOfflineException()
        {
            //Arrange
            List<ConsumptionRecord> input = new List<ConsumptionRecord>();
            ConnectionControler controler = ConnectionControler.Instance;

            //Act & Assert
            try
            {
                controler.OstvConsumptionDBWrite(input);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler.ActivityState);
            }
        }

        [Test]
        public void ConsumptionReqPropagate_TryRecconectFailed_ThrowsDBOfflineException()
        {
            //Arrange
            DSpanGeoReq input = new DSpanGeoReq("SRB", "2021-05-05", "2023-10-10");
            ConnectionControler controler = ConnectionControler.Instance;

            //Act & Assert
            try
            {
                controler.ConsumptionReqPropagate(input);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler.ActivityState);
            }

        }

        [Test]
        public void ReadAuditContnet_TryRecconectFailed_ThrowsDBOfflineException()
        {
            //Arrange
            ConnectionControler controler = ConnectionControler.Instance;

            //Act & Assert
            try
            {
                controler.ReadAuditContnet();
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler.ActivityState);
            }

        }

        [Test]
        public void ReadGeoContnet_TryRecconectFailed_ThrowsDBOfflineException()
        {
            //Arrange
            ConnectionControler controler = ConnectionControler.Instance;

            //Act & Assert
            try
            {
                controler.ReadGeoContent();
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler.ActivityState);
            }

        }

        [Test]
        public void GeoEntityUpdate_TryRecconectFailed_ThrowsDBOfflineException()
        {
            //Arrange
            ConnectionControler controler = ConnectionControler.Instance;
            string oldName = "DZSCG";
            string newName = "SERBIA";

            //Act & Assert
            try
            {
                controler.GeoEntityUpdate(oldName, newName);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler.ActivityState);
            }

        }

        [Test]
        public void GeoEntityWrite_TryRecconectFailed_ThrowsDBOfflineException()
        {
            //Arrange
            ConnectionControler controler = ConnectionControler.Instance;
            GeoRecord input = new GeoRecord("SRB", "SERBIA");

            //Act & Assert
            try
            {
                controler.GeoEntityWrite(input);
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
                Assert.IsFalse(ConnectionControler.ActivityState);
            }

        }
        #endregion
    }
}
