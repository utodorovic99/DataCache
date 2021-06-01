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
    public class GeoRecordTest
    {
        #region Constructor_Tests
        [Test]
        public void GeoRecord_TryConstructNoParams_Success()
        {
            //Act
            GeoRecord record = new GeoRecord();

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual("", record.GID);
            Assert.AreEqual("", record.GName);
        }

        [Test]
        public void GeoRecord_TryConstructWithParams_Success()
        {
            //Arrange
            string gID = "SRB";
            string gName = "SERBIA";

            //Act
            GeoRecord record = new GeoRecord(gID, gName);

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(gID, record.GID);
            Assert.AreEqual(gName, record.GName);
        }
        #endregion

        #region Property_Tests
        [Test]
        public void GID_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            string gName = "SERBIA";

            //Act
            GeoRecord record = new GeoRecord(gID, gName);

            //Assert
            Assert.AreEqual(gID, record.GID);
        }

        [Test]
        public void GID_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";

            //Act
            GeoRecord record = new GeoRecord();
            record.GID = gID;

            //Assert
            Assert.AreEqual(gID, record.GID);
        }

        [Test]
        public void GName_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            string gName = "SERBIA";

            //Act
            GeoRecord record = new GeoRecord(gID, gName);

            //Assert
            Assert.AreEqual(gName, record.GName);
        }

        [Test]
        public void GName_TrySet_Success()
        {
            //Arrange
            string gName = "SERBIA";

            //Act
            GeoRecord record = new GeoRecord();
            record.GName= gName;

            //Assert
            Assert.AreEqual(gName, record.GName);
        }

        #endregion

        #region IsEmpty_Tests
        [Test]
        public void IsEmpty_Check_ReturnsTrue()
        {

            //Act
            GeoRecord record = new GeoRecord();

            //Assert
            Assert.IsTrue(record.IsEmpty());
        }

        [Test]
        public void IsEmpty_Check_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            string gName = "SERBIA";

            //Act
            GeoRecord record = new GeoRecord(gID, gName);

            //Assert
            Assert.IsFalse(record.IsEmpty());
        }
        #endregion

        #region IsComplete_Tests
        [Test]
        public void IsComplete_FailsByGName_ReturnsFalse()
        {
            //Act
            GeoRecord record = new GeoRecord("SRB", "");

            //Assert
            Assert.IsFalse(record.IsComplete());
        }

        [Test]
        public void IsComplete_FailsByGID_ReturnsFalse()
        {
            //Act
            GeoRecord record = new GeoRecord("", "SERBIA");

            //Assert
            Assert.IsFalse(record.IsComplete());
        }

        [Test]
        public void IsComplete_Success_ReturnsTrue()
        {
            //Act
            GeoRecord record = new GeoRecord("SRB", "SERBIA");

            //Assert
            Assert.IsTrue(record.IsComplete());
        }
        #endregion

        #region ToString_Tests
        [Test]
        public void ToString_TryConvert_Success()
        {
            //Arrange
            string gName = "SERBIA";
            string gID = "SRB";
            GeoRecord record = new GeoRecord(gID, gName);
            string expected = String.Format("Geographic entity: GID: {0}\tName: {1}", gID, gName);

            //Act & Assert
            Assert.AreEqual(expected, record.ToString());

        }
        #endregion
    }
}
