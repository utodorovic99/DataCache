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
    public class ConsumptionRecordTest
    {
        #region Constructor_Tests
        [Test]
        public void ConsumptionRecord_ConstructorNoParams_ReturnsInstance()
        {
            //Arrange & Act
            ConsumptionRecord record = new ConsumptionRecord();

            //Assert
            Assert.NotNull(record);
            Assert.AreEqual("", record.GID);
            Assert.AreEqual("", record.TimeStamp);
            Assert.AreEqual(-1, record.MWh);
        }

        [Test]
        public void ConsumptionRecord_ConstructorWithParams_ReturnsInstance()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            

            //Act
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Assert
            Assert.NotNull(record);
            Assert.AreEqual(gID, record.GID);
            Assert.AreEqual(timeStamp, record.TimeStamp);
            Assert.AreEqual(mWh, record.MWh);
        }
        #endregion

        #region Equals_Tests
        [Test]
        public void Equals_Compare_ReturnsTrue()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            ConsumptionRecord recordCmp = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act & Assert
            Assert.IsTrue(record.Equals(recordCmp));
        }

        [Test]
        public void Equals_Compare_FailByGID()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            ConsumptionRecord recordCmp = new ConsumptionRecord("SCG", mWh, timeStamp);

            //Act & Assert
            Assert.IsFalse(record.Equals(recordCmp));
        }

        [Test]
        public void Equals_Compare_FailByMWH()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            ConsumptionRecord recordCmp = new ConsumptionRecord(gID, 14000, timeStamp);

            //Act & Assert
            Assert.IsFalse(record.Equals(recordCmp));
        }

        [Test]
        public void Equals_Compare_FailByTimeStamp()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            ConsumptionRecord recordCmp = new ConsumptionRecord(gID, mWh, "2022-05-05-1");

            //Act & Assert
            Assert.IsFalse(record.Equals(recordCmp));
        }
        #endregion

        #region Property_Tests
        [Test]
        public void GID_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            string gIDGetVal;
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            gIDGetVal = record.GID;

            //Assert
            Assert.IsNotNull(gIDGetVal);
            Assert.AreEqual(gIDGetVal, gID);
        }

        [Test]
        public void GID_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            string gIDNewVal="SCG";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            record.GID=gIDNewVal;

            //Assert
            Assert.AreEqual(gIDNewVal, record.GID);
        }

        [Test]
        public void MWh_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            int mWHGetVal;
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            mWHGetVal = record.MWh;

            //Assert
            Assert.IsNotNull(mWHGetVal);
            Assert.AreEqual(mWHGetVal, mWh);
        }

        [Test]
        public void MWh_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            int mWHNewVal = 14000;
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            record.MWh = mWHNewVal;

            //Assert
            Assert.AreEqual(mWHNewVal, record.MWh);
        }

        [Test]
        public void TimeStamp_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            string timeStampGetVal;
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            timeStampGetVal = record.TimeStamp;

            //Assert
            Assert.IsNotNull(timeStampGetVal);
            Assert.AreEqual(timeStampGetVal, timeStamp);
        }

        [Test]
        public void TimeStamp_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            string timeStampNewVal = "SCG";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            record.TimeStamp = timeStampNewVal;

            //Assert
            Assert.AreEqual(timeStampNewVal, record.TimeStamp);
        }
        #endregion

        #region IsWholeEmpty_Tests

        [Test]
        public void IsWholeEmpty_Compare_Is()
        {
            //Arrange
            ConsumptionRecord record = new ConsumptionRecord();

            //Act & Assert
            Assert.IsTrue(record.IsWholeEmpty());
        }

        [Test]
        public void IsWholeEmpty_Compare_No()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act & Assert
            Assert.IsFalse(record.IsWholeEmpty());
        }
        #endregion

        #region HasUsableStatus_Tests
        [Test]
        public void HasUsableStatus_Check_Has()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-1";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act & Assert
            Assert.IsTrue(record.HasUsableStatus());
        }

        [Test]
        public void HasUsableStatus_Check_HasNot()
        {
            //Arrange
            ConsumptionRecord record = new ConsumptionRecord();

            //Act & Assert
            Assert.IsFalse(record.HasUsableStatus());
        }
        #endregion

        #region GetHour_Tests
        [Test]
        public void GetHour_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-4";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            int hourGetVal;

            //Act
            hourGetVal = record.GetHour();

            //Assert
            Assert.AreNotEqual(-1, hourGetVal);
            Assert.AreEqual(4, hourGetVal);
        }

        [Test]
        public void GetHour_TryGetBadFormat_Fail()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05:4";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            int hourGetVal;

            //Act
            hourGetVal = record.GetHour();

            //Assert
            Assert.AreEqual(-1, hourGetVal);
        }

        [Test]
        public void GetHour_TryGetNaN_Fail()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-hour";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            int hourGetVal;

            //Act
            hourGetVal = record.GetHour();

            //Assert
            Assert.AreEqual(-1, hourGetVal);
        }
        #endregion

        #region ToString_Tests
        [Test]
        public void ToString_Test()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-hour";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);
            string result;

            //Act
            result= record.ToString();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(String.Format("Consumption record GID: {0},\tHOUR: {1}, \tLOAD: {2}", gID, timeStamp, mWh), result);
        }
        #endregion

        #region CheckTimeRelationMine_Tests

            #region GT_OnValidParams_Tests
        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTDeltaDate_ReturnsTrue()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status=record.CheckTimeRelationMine("2021-05-04-05", ">", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTDeltaMonth_ReturnsTrue()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-04-05-05", ">", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTDeltaYear_ReturnsTrue()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2020-05-05-05", ">", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmGTDeltaYear_ReturnsTrue()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-04", ">", false);

            //Assert
            Assert.IsTrue(status);
        }

        #endregion

            #region GT_OnBadParams_Tests
        [Test]
        public void CheckTimeRelationMine_SameDateIgnoreHoursCheckRelationAmGT_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", ">", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTDeltaDate_ReturnsFalse()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-06-05", ">", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTDeltaMonth_ReturnsFalse()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-06-05-05", ">", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTDeltaYear_ReturnsFalse()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2022-05-05-05", ">", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmGTDeltaYear_ReturnsFalse()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", ">", false);

            //Assert
            Assert.IsFalse(status);
        }

        #endregion

            #region GTE_OnValidParams_Tests
        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTEDeltaDate_ReturnsTrue()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-04-05", ">=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTEDeltaMonth_ReturnsTrue()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-04-05-05", ">=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTEDeltaYear_ReturnsTrue()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2020-05-05-05", ">=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmGTEDeltaYear_ReturnsTrue()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-04", ">=", false);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_SameHourCheckRelationAmGTE_ReturnsTrue()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-05", ">=", false);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_SameDateIgnoreHoursCheckRelationAmGTE_ReturnsTrue()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", ">=", true);

            //Assert
            Assert.IsTrue(status);
        }

        #endregion

            #region GTE_OnBadParams_Tests

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTEDeltaDate_ReturnsFalse()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-06-05", ">=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTEDeltaMonth_ReturnsFalse()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-06-05-05", ">=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmGTEDeltaYear_ReturnsFalse()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2022-05-05-05", ">=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmGTEDeltaYear_ReturnsFalse()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", ">=", false);

            //Assert
            Assert.IsFalse(status);
        }

        #endregion

            #region LT_OnValidParams_Tests
        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTDeltaDate_ReturnsTrue()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-06-05", "<", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTDeltaMonth_ReturnsTrue()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-06-05-05", "<", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTDeltaYear_ReturnsTrue()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2022-05-05-05", "<", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmLTDeltaYear_ReturnsTrue()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", "<", false);

            //Assert
            Assert.IsTrue(status);
        }

        #endregion

            #region LT_OnBadParams_Tests
        [Test]
        public void CheckTimeRelationMine_SameDateIgnoreHoursCheckRelationAmLT_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", "<", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTDeltaDate_ReturnsFalse()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-04-05", "<", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTDeltaMonth_ReturnsFalse()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-04-05-05", "<", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTDeltaYear_ReturnsFalse()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2019-05-05-05", "<", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmLTDeltaYear_ReturnsFalse()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-04", "<", false);

            //Assert
            Assert.IsFalse(status);
        }

        #endregion

            #region LTE_OnValidParams_Tests
        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTEDeltaDate_ReturnsTrue()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-06-05", "<=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTEDeltaMonth_ReturnsTrue()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-06-05-05", "<=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTEDeltaYear_ReturnsTrue()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2022-05-05-05", "<=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmLTEDeltaYear_ReturnsTrue()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", "<=", false);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_SameHourCheckRelationAmLTE_ReturnsTrue()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-05", "<=", false);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_SameDateIgnoreHoursCheckRelationAmLT_ReturnsTrue()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", "<=", true);

            //Assert
            Assert.IsTrue(status);
        }

        #endregion

            #region LTE_OnBadParams_Tests

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTEDeltaDate_ReturnsFalse()   //Diff. in day    
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-04-05", "<=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTEDeltaMonth_ReturnsFalse()  //Diff. in month
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-04-05-05", "<=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationAmLTEDeltaYear_ReturnsFalse()   //Diff. in year
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2019-05-05-05", "<=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationAmLTEDeltaYear_ReturnsFalse()   //Diff. in hour
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-04", "<=", false);

            //Assert
            Assert.IsFalse(status);
        }

        #endregion

            #region EQ_OnValidParams_Tests
        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationEQ_ReturnsTrue()   
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", "=", true);

            //Assert
            Assert.IsTrue(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationEQ_ReturnsTrue()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-05", "=", false);

            //Assert
            Assert.IsTrue(status);
        }
        #endregion

            #region EQ_OnBadParams_Tests
        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationEQFailsDay_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-06-05", "=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_IgnoreHoursCheckRelationEQFailsMonth_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-06-05-05", "=", true);

            //Assert
            Assert.IsFalse(status);
        }

        public void CheckTimeRelationMine_IgnoreHoursCheckRelationEQFailsYear_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2020-05-05-05", "=", true);

            //Assert
            Assert.IsFalse(status);
        }

        [Test]
        public void CheckTimeRelationMine_CheckRelationEQFailsHour_ReturnsFalse()
        {
            //Arrange
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            var status = record.CheckTimeRelationMine("2021-05-05-06", "=", false);

            //Assert
            Assert.IsFalse(status);
        }
        #endregion

            #region Bad_AROP_Tests

        [Test]
        public void CheckTimeRelationMine_TryBadAROP_ThrowsArithmeticException()
        {
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            try
            {
                var status = record.CheckTimeRelationMine("2021-05-06-05", "^", true);
                Assert.Fail();
            }
            catch(ArithmeticException)
            {
               
            }
        }

        [Test]
        public void CheckTimeRelationMine_TryNullAROP_ThrowsArithmeticException()
        {
            string gID = "SRB";
            int mWh = 12000;
            string timeStamp = "2021-05-05-05";
            ConsumptionRecord record = new ConsumptionRecord(gID, mWh, timeStamp);

            //Act
            try
            {
                var status = record.CheckTimeRelationMine("2021-05-06-05", null, true);
                Assert.Fail();
            }
            catch (ArithmeticException)
            {

            }
        }

        #endregion

        #endregion

    }
}
