using CacheControler_Project.Classes;
using Common_Project.Classes;
using ConnectionControler_Project.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class CacheControlerAgentTest
    {
        #region Constructor_Tests
        [Test]
        public void CacheControlerAgent_TryConstruct_Success()
        {
            //Act
            var agent = new CacheControlerAgentTest();

            //Assert
            Assert.IsNotNull(agent);
        }
        #endregion

        #region TryReconnect_Tests
        [Test]
        public void TryReconnect_TryReconnectOnDBOffline_ReturnsFalse()
        {
            //Arrange
            CacheControlerAgent agent = new CacheControlerAgent();

            //Act
            var status = agent.TryReconnect();

            //Assert
            Assert.IsFalse(status);
        }
        #endregion

        #region ConsumptionReqPropagate_Tests
        [Test]
        public void ConsumptionReqPropagate_TryReconnectOnDBOffline_ThrowsDBOfflineException()
        {
            //Arrange
            CacheControlerAgent agent = new CacheControlerAgent();
            var input = new DSpanGeoReq("SRB","2021-05-05","2021-10-10");

            //Act & Assert
            try
            {
                agent.ConsumptionReqPropagate(input);
                Assert.Fail();
            }
            catch(DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }
        #endregion

        #region Echo_Tests
        [Test]
        public void Echo_TryReconnectOnDBOffline_ReturnsFalse()
        {
            //Arrange
            CacheControlerAgent agent = new CacheControlerAgent();

            //Act & Assert
            try
            {
                agent.Echo();
                Assert.Fail();
            }
            catch (DBOfflineException ex)
            {
                Assert.AreEqual("Remote Database is currently offline, check network connection and call support.", ex.Message);
            }
        }
        #endregion

        #region GeoEntityUpdate_Tests
        #endregion

        #region GeoEntityWrite_Tests
        #endregion

        #region ReadAuditContnete_Tests
        #endregion

        #region ReadGeoContent_Tests
        #endregion
    }
}
