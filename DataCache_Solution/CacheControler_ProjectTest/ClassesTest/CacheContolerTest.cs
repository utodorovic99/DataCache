using CacheControler_ProjectTest.Fakes;
using NUnit.Framework;
using System.Collections.Generic;
using Common_Project;
using Common_Project.Classes;
using ConnectionControler_Project.Exceptions;
using CacheControler_Project.Enums;
using System.Threading;
using Moq;
using static System.Threading.Thread;
using System;

namespace CacheControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class CacheContolerTest
    {
        #region Constructor_Tests
        [Test]
        public void CacheControler_TryConstruct_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12444));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);

            //Act
            CacheControler_Fake controler = new CacheControler_Fake();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreSame(controler.CachedAudit, retAudits);
            Assert.AreSame(controler.CachedGeo, retGeos);
            Assert.IsTrue(controler.DBOnline);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }

        [Test]
        public void CacheControler_TryConstruct_SuccessButNotAcceptable()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-4", 12444));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-5", 12445));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-6", 12446));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1","SRB6");
            retGeos.Add("SERBIA2", "SRB5");
            retGeos.Add("SERBIA3", "SRB4");
            retGeos.Add("SERBIA4", "SRB3");
            retGeos.Add("SERBIA5", "SRB2");
            retGeos.Add("SERBIA6", "SRB1");

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);

            //Act
            CacheControler_Fake controler = new CacheControler_Fake();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsTrue(controler.DBOnline);
            Assert.IsFalse(controler.GeoAcceptable);
            Assert.IsFalse(controler.AuditAcceptable);
        }

        [Test]
        public void CacheControler_TryConstruct_SuccessDBOfflineByEchoFail()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Throws
                (new DBOfflineException("Remote Database is currently offline, check network connection and call support."));
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-4", 12444));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-5", 12445));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-6", 12446));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB6");
            retGeos.Add("SERBIA2", "SRB5");
            retGeos.Add("SERBIA3", "SRB4");
            retGeos.Add("SERBIA4", "SRB3");
            retGeos.Add("SERBIA5", "SRB2");
            retGeos.Add("SERBIA6", "SRB1");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);

            //Act
            CacheControler_Fake controler = new CacheControler_Fake();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsFalse(controler.DBOnline);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }

        [Test]
        public void CacheControler_TryConstruct_SuccessDBOfflineByReadAuditFail()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB6");
            retGeos.Add("SERBIA2", "SRB5");
            retGeos.Add("SERBIA3", "SRB4");
            retGeos.Add("SERBIA4", "SRB3");
            retGeos.Add("SERBIA5", "SRB2");
            retGeos.Add("SERBIA6", "SRB1");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).
                Throws(new DBOfflineException("Remote Database is currently offline, check network connection and call support."));
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);

            //Act
            CacheControler_Fake controler = new CacheControler_Fake();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsFalse(controler.DBOnline);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }

        [Test]
        public void CacheControler_TryConstruct_SuccessDBOfflineByReadGeoFail()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12443));


            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).
                Throws(new DBOfflineException("Remote Database is currently offline, check network connection and call support."));

            //Act
            CacheControler_Fake controler = new CacheControler_Fake();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreNotEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsFalse(controler.DBOnline);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }
        #endregion

        #region Property_Tests
        [Test]
        public void DBOnline_TryGet_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12444));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var retVal = controler.DBOnline;

            //Assert
            Assert.IsNotNull(retVal);
            Assert.IsTrue(retVal);
        }

        [Test]
        public void AuditAcceptable_TryGet_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12444));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var retVal = controler.AuditAcceptable;

            //Assert
            Assert.IsNotNull(retVal);
            Assert.IsTrue(retVal);
        }

        [Test]
        public void GeoAcceptable_TryGet_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12444));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var retVal = controler.GeoAcceptable;

            //Assert
            Assert.IsNotNull(retVal);
            Assert.IsTrue(retVal);
        }

        [Test]
        public void CachedAudit_TryGet_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12441));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var retVal = controler.CachedAudit;

            //Assert
            Assert.IsNotNull(retVal);
            Assert.AreSame(retAudits, retVal);
        }

        [Test]
        public void CachedGeo_TryGet_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12443));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SRB", "SERBIA");
            retGeos.Add("NMAC", "MACEDONIA");
            retGeos.Add("MNE", "MONTENEGRO");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var retVal = controler.CachedGeo;

            //Assert
            Assert.IsNotNull(retVal);
            Assert.AreSame(retGeos, retVal);
        }

        #endregion

        #region DBTryReconnect_Tests
        [Test]
        public void DBTryReconnect_Try_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(false);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12444));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            controler.DBTryReconnect();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreSame(controler.CachedAudit, retAudits);
            Assert.AreSame(controler.CachedGeo, retGeos);
            Assert.IsTrue(controler.DBOnline);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }

        [Test]
        public void DBTryReconnect_Try_BothNotAcceptable()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(false);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-4", 12444));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-5", 12445));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-6", 12446));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB1");
            retGeos.Add("SERBIA2", "SRB2");
            retGeos.Add("SERBIA3", "SRB3");
            retGeos.Add("SERBIA4", "SRB4");
            retGeos.Add("SERBIA5", "SRB5");
            retGeos.Add("SERBIA6", "SRB6");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var result = controler.DBTryReconnect();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsTrue(controler.DBOnline);
            Assert.IsTrue(result);
            Assert.IsFalse(controler.GeoAcceptable);
            Assert.IsFalse(controler.AuditAcceptable);
        }

        [Test]
        public void DBTryReconnect_Try_Fail_TryReconnectByDBOfflineException()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(false);
            List<AuditRecord> retAudits = new List<AuditRecord>();

            Dictionary<string, string> retGeos = new Dictionary<string, string>();

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(false);
            CacheControler_Fake controler = new CacheControler_Fake();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retGeos.Add("SERBIA1", "SRB1");

            //Act
            var result = controler.DBTryReconnect();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsFalse(controler.DBOnline);
            Assert.IsFalse(result);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }

        [Test]
        public void DBTryReconnect_Try_Fail_ReadAuditByDBOfflineException()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(false);
            List<AuditRecord> retAudits = new List<AuditRecord>();

            Dictionary<string, string> retGeos = new Dictionary<string, string>();

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).
                Throws(new DBOfflineException("Remote Database is currently offline, check network connection and call support."));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retGeos.Add("SERBIA1", "SRB1");


            //Act
            var result = controler.DBTryReconnect();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreEqual(0, controler.CachedAudit.Count);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsFalse(controler.DBOnline);
            Assert.IsFalse(result);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }

        [Test]
        public void DBTryReconnect_Try_Fail_ReadGeoByDBOfflineException()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(false);
            List<AuditRecord> retAudits = new List<AuditRecord>();

            Dictionary<string, string> retGeos = new Dictionary<string, string>();

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).
                Throws(new DBOfflineException("Remote Database is currently offline, check network connection and call support."));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));
            retGeos.Add("SERBIA1", "SRB1");

            //Act
            var result = controler.DBTryReconnect();

            //Assert
            Assert.IsNotNull(controler);
            Assert.AreSame(retAudits, controler.CachedAudit);
            Assert.AreEqual(0, controler.CachedGeo.Count);
            Assert.IsFalse(controler.DBOnline);
            Assert.IsFalse(result);
            Assert.IsTrue(controler.GeoAcceptable);
            Assert.IsTrue(controler.AuditAcceptable);
        }


        #endregion

        #region AddNewGeoEntity_Tests
        [Test]
        public void AddNewGeoEntity_TryOnFullMemory_ReturnsOutOfMemory()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB1");
            retGeos.Add("SERBIA2", "SRB2");
            retGeos.Add("SERBIA3", "SRB3");
            retGeos.Add("SERBIA4", "SRB4");
            retGeos.Add("SERBIA5", "SRB5");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            var before = controler.CachedGeo.Count;

            //Act
            var result = controler.AddNewGeoEntity(new GeoRecord("SRB22", "SERBIA122"));

            //Assert
            Assert.AreEqual(EPostGeoEntityStatus.OutOfMemory, result);
            Assert.AreEqual(before, controler.CachedGeo.Count);
        }

        [Test]
        public void AddNewGeoEntity_TryWriteSame_ReturnsOutOfMemory()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB1");


            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            var before = controler.CachedGeo.Count;

            //Act
            var result = controler.AddNewGeoEntity(new GeoRecord("SRB1", "SERBIA1"));

            //Assert
            Assert.AreEqual(EPostGeoEntityStatus.DBWriteAborted, result);
            Assert.AreEqual(before, controler.CachedGeo.Count);
        }

        [Test]
        public void AddNewGeoEntity_TryWrite_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB1");

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            var newEntity = new GeoRecord("SRB11", "SERBIA11");
            CacheControler_Fake.Proxy().Setup(x => x.GeoEntityWrite(newEntity)).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            var before = controler.CachedGeo.Count;

            //Act
            var result = controler.AddNewGeoEntity(newEntity);

            //Assert
            Assert.AreEqual(EPostGeoEntityStatus.Success, result);
            Assert.AreNotEqual(before, controler.CachedGeo.Count);
            Assert.IsTrue(controler.CachedGeo.ContainsKey(newEntity.GName));
            Assert.IsTrue(controler.DBOnline);
        }

        [Test]
        public void AddNewGeoEntity_TryWrite_ThrowsDBOfflineException()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB1");

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            var newEntity = new GeoRecord("SRB11", "SERBIA11");
            CacheControler_Fake.Proxy().Setup(x => x.GeoEntityWrite(newEntity)).
                Throws(new DBOfflineException("Remote Database is currently offline, check network connection and call support."));
            CacheControler_Fake controler = new CacheControler_Fake();
            var before = controler.CachedGeo.Count;

            //Act
            var result = controler.AddNewGeoEntity(newEntity);

            //Assert
            Assert.AreEqual(EPostGeoEntityStatus.DBWriteFailed, result);
            Assert.AreEqual(before, controler.CachedGeo.Count);
            Assert.IsFalse(controler.CachedGeo.ContainsKey(newEntity.GName));
            Assert.IsFalse(controler.DBOnline);
        }
        #endregion

        #region CacheGarbageCollevor_Tests
        [Test]
        public void CacheGarbageCollectorAndReadTypes_TryAll_Succes()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");

            string targetGID = "SRB";
            string targetGName = "SERBIA";
            string from = "2021-05-05";
            string till = "2021-11-05";
            List<ConsumptionRecord> output = new List<ConsumptionRecord>();
            output.Add(new ConsumptionRecord("SRB", 12552, "2021-05-01-01"));
            output.Add(new ConsumptionRecord("SRB", 124452, "2021-05-01-02"));
            output.Add(new ConsumptionRecord("SRB", 122134, "2021-05-02-01"));
            output.Add(new ConsumptionRecord("SRB", 121342, "2021-05-04-01"));

            var req = new DSpanGeoReq(targetGName, from, till);
            var subReq = new DSpanGeoReq(targetGName, from, till);
            var reqInverted = new DSpanGeoReq(targetGName, "2021-05-01-02", "2021-05-02-01");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();
            CacheControler_Fake.Proxy().Setup(x => x.ConsumptionReqPropagate(It.IsAny<DSpanGeoReq>())).Returns(output);

            //Act & Assert
            var targetConsumption = controler.DSpanGeoConsumptionRead(req);
            Assert.AreSame(output, targetConsumption.Item2);
            Assert.AreEqual(EConcumptionReadStatus.DBReadSuccess, targetConsumption.Item1);         //Read from DB

            targetConsumption = controler.DSpanGeoConsumptionRead(req);
            Assert.AreSame(output, targetConsumption.Item2);
            Assert.AreEqual(EConcumptionReadStatus.CacheReadSuccess, targetConsumption.Item1);      //Read from Cache

            targetConsumption = controler.DSpanGeoConsumptionRead(reqInverted);
            Assert.AreEqual(2, targetConsumption.Item2.Count);
            Assert.AreEqual(EConcumptionReadStatus.CacheReadSuccess, targetConsumption.Item1);      //Read from Cache

            Sleep(9000);

            targetConsumption = controler.DSpanGeoConsumptionRead(req);
            Assert.AreSame(output, targetConsumption.Item2);
            Assert.AreEqual(EConcumptionReadStatus.DBReadSuccess, targetConsumption.Item1);         //Read from DB again => GC deleted it
        }
        #endregion

        #region ConsumptionUpdateHandler_Tests
        [Test]
        public void ConsumptionUpdateHandler_TryUpdate_Success()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB1");

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            var beforeGeo = controler.CachedGeo.Count;
            var beforeAudit = controler.CachedAudit.Count;
            var update = new ConsumptionUpdate();
            update.NewGeos.Add("SERB2");
            update.NewGeos.Add("SERB3");
            update.DupsAndMisses = new Dictionary<string, System.Tuple<List<System.Tuple<int, int>>, List<int>>>();

            update.DupsAndMisses.Add("SRB1", new System.Tuple<List<System.Tuple<int, int>>, List<int>>(new List<System.Tuple<int, int>>(), new List<int>()));

            update.DupsAndMisses["SRB1"].Item1.Add(new System.Tuple<int, int>(5, 221));
            update.DupsAndMisses["SRB1"].Item2.Add(8);

            //Act
            var result = controler.ConsumptionUpdateHandler("2021-10-05-1", update);

            //Assert
            Assert.AreEqual(beforeGeo + 2, controler.CachedGeo.Count);
            Assert.AreEqual(beforeAudit + 2, controler.CachedAudit.Count);
        }

        [Test]
        public void ConsumptionUpdateHandler_TryUpdate_FailGeo()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12441));

            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA1", "SRB5");
            retGeos.Add("SERBIA2", "SRB4");
            retGeos.Add("SERBIA3", "SRB3");
            retGeos.Add("SERBIA4", "SRB2");
            retGeos.Add("SERBIA5", "SRB1");

            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake.Proxy().Setup(x => x.TryReconnect()).Returns(true);
            CacheControler_Fake controler = new CacheControler_Fake();
            var beforeGeo = controler.CachedGeo.Count;
            var beforeAudit = controler.CachedAudit.Count;
            var update = new ConsumptionUpdate();
            update.NewGeos.Add("SERB2");
            update.NewGeos.Add("SERB3");
            update.DupsAndMisses = new Dictionary<string, System.Tuple<List<System.Tuple<int, int>>, List<int>>>();

            update.DupsAndMisses.Add("SRB1", new System.Tuple<List<System.Tuple<int, int>>, List<int>>(new List<System.Tuple<int, int>>(), new List<int>()));

            update.DupsAndMisses["SRB1"].Item1.Add(new System.Tuple<int, int>(5, 221));
            update.DupsAndMisses["SRB1"].Item2.Add(8);

            //Act
            try
            {
                var result = controler.ConsumptionUpdateHandler("2021-10-05-1", update);
                Assert.Fail();
            }
            catch (Exception)
            { }

            //Assert
            Assert.AreEqual(beforeGeo, controler.CachedGeo.Count);
            Assert.AreEqual(beforeAudit + 2, controler.CachedAudit.Count);
        }
        #endregion

        #region ContainsGeo_Tests
        [Test]
        public void ContainsGeo_Try_ReturnsContexts()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12441));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var wholeUsed = controler.ContainsGeo(new GeoRecord("SRB", "SERBIA"));
            var nameUsed = controler.ContainsGeo(new GeoRecord("SRB1", "SERBIA"));
            var gIDUsed = controler.ContainsGeo(new GeoRecord("SRB", "SERBIA1"));
            var free = controler.ContainsGeo(new GeoRecord("SRB111", "SERBIA111"));


            //Assert
            Assert.AreEqual(EGeoRecordStatus.GeoRecordUsed, wholeUsed);
            Assert.AreEqual(EGeoRecordStatus.GNameUsed, nameUsed);
            Assert.AreEqual(EGeoRecordStatus.GIDUsed, gIDUsed);
            Assert.AreEqual(EGeoRecordStatus.GeoRecordFree, free);
        }

        #endregion

        #region UpdateGeoEntity_Tests
        [Test]
        public void UpdateGeoEntity_Try_ReturnsSuccess()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12441));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();

            //Act
            var outcome = controler.UpdateGeoEntity("SERBIA", "SRBIA11");

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.Success, outcome);
            Assert.IsTrue(controler.CachedGeo.ContainsKey("SRBIA11"));
        }

        [Test]
        public void UpdateGeoEntity_Try_ReturnsOriginNotFound()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12441));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            CacheControler_Fake controler = new CacheControler_Fake();
            var before = controler.CachedGeo;

            //Act
            var outcome = controler.UpdateGeoEntity("SERBIA999", "SRBIA11");

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.OriginNotFound, outcome);
            Assert.AreSame(before, controler.CachedGeo);
        }

        [Test]
        public void UpdateGeoEntity_DBOffline_ThrowsDBOfflineException()
        {
            //Arrange
            CacheControler_Fake.Proxy().Setup(x => x.Echo()).Returns(true);
            List<AuditRecord> retAudits = new List<AuditRecord>();
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-1", 12443));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-2", 12442));
            retAudits.Add(new AuditRecord("SRB", "2021-05-05-3", 12441));
            Dictionary<string, string> retGeos = new Dictionary<string, string>();
            retGeos.Add("SERBIA", "SRB");
            CacheControler_Fake.Proxy().Setup(x => x.ReadAuditContnet()).Returns(retAudits);
            CacheControler_Fake.Proxy().Setup(x => x.ReadGeoContent()).Returns(retGeos);
            string oldName = "SERBIA";
            string newName = "SRB";
            CacheControler_Fake.Proxy().Setup(x => x.GeoEntityUpdate(oldName, newName)).Throws(new Exception());
            CacheControler_Fake controler = new CacheControler_Fake();
            var before = controler.CachedGeo;

            //Act
            var outcome = controler.UpdateGeoEntity("SERBIA", "SRB");

            //Assert
            Assert.AreEqual(EUpdateGeoStatus.DBWriteFailed, outcome);
            Assert.IsFalse(controler.DBOnline);
            Assert.AreSame(before, controler.CachedGeo);
        }
        #endregion

    }
}
