using Common_Project.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_ProjectTest.ClassesTest
{
    [TestFixture]
    public class DSpanGeoReqTest
    {
        #region Constructor_Tests
        [Test]
        public void DSpanGeoReq_TryConstruct_Success()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "2022-06-06";


            //Act
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);

            //Assert
            Assert.IsNotNull(req);
            Assert.AreEqual(name, req.GName);
            Assert.AreEqual(from, req.From);
            Assert.AreEqual(till, req.Till);
        }

        #endregion

        #region Property_Tests
        [Test]
        public void From_TryGet_Success()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);

            //Act
            var getVal = req.From;

            //Assert
            Assert.AreEqual(from, getVal);
        }

        [Test]
        public void From_TrySet_Success()
        {

        }

        [Test]
        public void GName_TryGet_Success()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);
            string setName = "DZSCG";

            //Act
            req.GName=setName;

            //Assert
            Assert.AreEqual(setName, req.GName);
        }

        [Test]
        public void GName_TrySet_Success()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);
            string setFrom = "1999-05-05";

            //Act
            req.From = setFrom;

            //Assert
            Assert.AreEqual(setFrom, req.From);
        }

        [Test]
        public void Till_TryGet_Success()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);

            //Act
            var getVal = req.Till;

            //Assert
            Assert.AreEqual(till, getVal);
        }

        [Test]
        public void Till_TrySet_Success()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);
            string setTill = "2035-05-05";

            //Act
            req.Till = setTill;

            //Assert
            Assert.AreEqual(setTill, req.Till);
        }
        #endregion

        #region IsComplete_Tests
        [Test]
        public void IsComplete_FailsByFrom_ReturnsFalse()
        {
            //Arrange
            string name = "SRB";
            string from = "";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);

            //Act & Assert
            Assert.IsFalse(req.IsComplete());
        }

        [Test]
        public void IsComplete_FailsByTill_ReturnsFalse()
        {
            //Arrange
            string name = "SRB";
            string from = "2021-05-05";
            string till = "";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);

            //Act & Assert
            Assert.IsFalse(req.IsComplete());
        }

        [Test]
        public void IsComplete_FailsByGName_ReturnsFalse()
        {
            //Arrange
            string name = "";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);

            //Act & Assert
            Assert.IsFalse(req.IsComplete());
        }

        #endregion

        #region ToString_Tests
        [Test]
        public void ToString_TryConvert_Success()
        {
            //Arrange
            string name = "";
            string from = "2021-05-05";
            string till = "2022-06-06";
            DSpanGeoReq req = new DSpanGeoReq(name, from, till);
            string expected = String.Format("Search request From:\t{0} \tTill:\t{1} \tfor area:\t{2}", from, till, name);

            //Act & Assert
            Assert.AreEqual(expected, req.ToString());
        }
        #endregion
    }
}
