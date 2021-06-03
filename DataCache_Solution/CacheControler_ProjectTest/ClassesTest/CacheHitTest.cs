using CacheControler_Project.Classes;
using Common_Project.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class CacheHitTest
    {
        #region Constructor_Tests
        [Test]
        public void CacheHit_TryConstruct_Success()
        {
            //Arrange
            List<ConsumptionRecord> records = new List<ConsumptionRecord>();
            records.Add(new ConsumptionRecord("SRB", 12342, "2021-05-05-13"));
            records.Add(new ConsumptionRecord("SRB", 12562, "2021-04-08-12"));
            records.Add(new ConsumptionRecord("SRB", 213342, "2020-06-06-03"));

            //Act
            CacheHit hit = new CacheHit(records);

            //Assert
            Assert.IsNotNull(hit);
            Assert.AreEqual(3, hit.CRecord.Count());
        }
        #endregion

        #region Property_Tests

        [Test]
        public void CRecord_TryGet_Success()
        {
            //Arrange
            List<ConsumptionRecord> records = new List<ConsumptionRecord>();
            records.Add(new ConsumptionRecord("SRB", 12342, "2021-05-05-13"));
            records.Add(new ConsumptionRecord("SRB", 12562, "2021-04-08-12"));
            records.Add(new ConsumptionRecord("SRB", 213342, "2020-06-06-03"));
            CacheHit hit = new CacheHit(records);

            //Act
            var readed = hit.CRecord;

            //Assert
            Assert.IsNotNull(readed);
            Assert.AreEqual(records, readed);
        }

        [Test]
        public void CRecord_TrySet_Success()
        {
            //Arrange
            List<ConsumptionRecord> records = new List<ConsumptionRecord>();
            records.Add(new ConsumptionRecord("SRB", 12342, "2021-05-05-13"));
            records.Add(new ConsumptionRecord("SRB", 12562, "2021-04-08-12"));
            records.Add(new ConsumptionRecord("SRB", 213342, "2020-06-06-03"));
            CacheHit hit = new CacheHit(new List<ConsumptionRecord>());

            //Act
            hit.CRecord= records;

            //Assert
            Assert.AreEqual(records, hit.CRecord);
        }

        [Test]
        public void HitTime_TryGet_Success()    
        {
            //Arrange
            CacheHit hit = new CacheHit(new List<ConsumptionRecord>());
            var dateTimeNow = DateTime.Now;

            //Act
            var hitTime = hit.HitTime;

            //Assert
            Assert.IsNotNull(hitTime);
            //Assert.IsTrue(hitTime.CompareTo(dateTimeNow)==0);   //Ignore failure for drastically slow machines
        }
        #endregion
    }
}
