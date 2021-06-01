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
    public class AuditRecordTest
    {
        #region Constructor_Tests
        [Test]
        public void AuditRecord_TestBasicConstructor_ReturnsInstance()
        {
            //Act
            AuditRecord record = new AuditRecord();

            //Assert 
            Assert.IsNotNull(record);
            Assert.AreEqual("", record.GID);
            Assert.AreEqual("", record.TimeStamp);
            Assert.AreEqual(-1, record.DupVal);
        }

        [Test]
        public void AuditRecord_TestParamConstructor_ReturnsInstance()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;

            //Act
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);

            //Assert 
            Assert.IsNotNull(record);
            Assert.AreEqual(gID, record.GID);
            Assert.AreEqual(timeStamp, record.TimeStamp);
            Assert.AreEqual(dupVal, record.DupVal);
        }
        #endregion

        #region Property_Tests
        [Test]
        public void GID_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            string gIDResult;

            //Act
            gIDResult = record.GID;

            //Assert 
            Assert.IsNotNull(gIDResult);
            Assert.AreEqual(gID, record.GID);
        }

        [Test]
        public void GID_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            string newGID="SCG";

            //Act
            record.GID=newGID;

            //Assert 
            Assert.AreEqual(newGID, record.GID);
        }

        [Test]
        public void TimeStamp_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            string timeStampResult;

            //Act
            timeStampResult = record.TimeStamp;

            //Assert 
            Assert.IsNotNull(timeStampResult);
            Assert.AreEqual(timeStampResult, record.TimeStamp);
        }

        [Test]
        public void TimeStamp_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            string newTimeStamp = "2021-05-05-14";

            //Act
            record.TimeStamp = newTimeStamp;

            //Assert 
            Assert.AreEqual(newTimeStamp, record.TimeStamp);
        }

        [Test]
        public void DupVal_TryGet_Success()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            int dupValResult;

            //Act
            dupValResult = record.DupVal;

            //Assert 
            Assert.IsNotNull(dupVal);
            Assert.AreEqual(dupValResult, record.DupVal);
        }

        [Test]
        public void DupVal_TrySet_Success()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            int newDupVal = 44; ;

            //Act
            record.DupVal = newDupVal;

            //Assert 
            Assert.AreEqual(newDupVal, record.DupVal);
        }
        #endregion

        #region ToString_Tests
        [Test]
        public void ToString_TryGetHit_Succeed()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = 22;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            string toStr;

            //Act
            toStr = record.ToString();

            //Assert 
            Assert.IsNotNull(toStr);
            Assert.AreEqual(String.Format("Audit record GID: {0}\tTimeStamp: {1} \tType: ", record.GID, record.TimeStamp) +
                                          "Duplicate\tValue: " + record.DupVal, toStr); 
        }

        [Test]
        public void ToString_TryGetMiss_Succeed()
        {
            //Arrange
            string gID = "SRB";
            string timeStamp = "2021-05-05-13";
            int dupVal = -1;
            AuditRecord record = new AuditRecord(gID, timeStamp, dupVal);
            string toStr;

            //Act
            toStr = record.ToString();

            //Assert 
            Assert.IsNotNull(toStr);
            Assert.AreEqual(String.Format("Audit record GID: {0}\tTimeStamp: {1} \tType: ", record.GID, record.TimeStamp) + "Miss", toStr);
        }
        #endregion
    }
}
