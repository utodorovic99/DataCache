using Common_Project.Classes;
using FileControler_Project.Classes;
using FileControler_Project.Enums;
using FileControler_Project.Handlers.XMLHandler.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class FileControlerTest
    {

        //Arrange

        //Act

        //Assert

        [Test]
        public void FileControler_TestConstructor_PassesAndDBOffline()
        {
            //Arrange & Act
            FileControler fileControler = new FileControler();

            //Assert
            Assert.IsNotNull(fileControler);
            Assert.IsFalse(fileControler.DbOnline);
        }

        [Test]
        public void DBTryReconnect_TryReconnect_Fails()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            bool retVal=

            //Act
            retVal =fileControler.DBTryReconnect();

            //Assert
            Assert.IsFalse(fileControler.DbOnline);
            Assert.IsFalse(retVal);
        }

        [Test]
        public void InitDBConsumptionWrite_TryExecuteOstvConsumptionRWOnDBOffline_ReturnsDBWriteFailed()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> retVal;
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\ostv_2018_05_07.xml");

            //Act
            retVal = fileControler.LoadFileStoreDB(fileInfo.FullName, ELoadDataType.Consumption);

            //Assert
            Assert.AreEqual(EFileLoadStatus.DBWriteFailed, retVal.Item2.Item1);
            Assert.AreEqual(0, retVal.Item2.Item2.DupsAndMisses.Count);
            Assert.AreEqual(0, retVal.Item2.Item2.NewGeos.Count);
            Assert.AreEqual("2018-05-07", retVal.Item1);
        }

        [Test]
        public void InitDBConsumptionWrite_TryExecuteOstvConsumptionRWOnBadFileExtension_ReturnsInvalidFileExtension()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> retVal;
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\ostv_2018_05_07.css");

            //Act
            retVal = fileControler.LoadFileStoreDB(fileInfo.FullName, ELoadDataType.Consumption);

            //Assert
            Assert.AreEqual(EFileLoadStatus.InvalidFileExtension, retVal.Item2.Item1);
            Assert.AreEqual(0, retVal.Item2.Item2.DupsAndMisses.Count);
            Assert.AreEqual(0, retVal.Item2.Item2.NewGeos.Count);
            Assert.AreEqual("2018-05-07", retVal.Item1);
        }

        [Test]
        public void InitDBConsumptionWrite_TryExecuteOstvConsumptionRWOnBadDataType_ReturnsInvalidFileExtension()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> retVal;
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\exp_2018_05_07.html");

            //Act
            retVal = fileControler.LoadFileStoreDB(fileInfo.FullName, ELoadDataType.Consumption);

            //Assert
            Assert.AreEqual(EFileLoadStatus.FileTypeNotSupported, retVal.Item2.Item1);
            Assert.AreEqual(0, retVal.Item2.Item2.DupsAndMisses.Count);
            Assert.AreEqual(0, retVal.Item2.Item2.NewGeos.Count);
            Assert.AreEqual("2018-05-07", retVal.Item1);
        }


        [Test]
        public void InitDBConsumptionWrite_TryExecuteOstvConsumptionRWOnMissmatchedDataType_ReturnsInvalidFileExtension()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> retVal;
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\ostv_2018_05_07.html");

            //Act
            retVal = fileControler.LoadFileStoreDB(fileInfo.FullName, ELoadDataType.Orphan);

            //Assert
            Assert.AreEqual(EFileLoadStatus.WrongFileTypeSeleceted, retVal.Item2.Item1);
            Assert.AreEqual(0, retVal.Item2.Item2.DupsAndMisses.Count);
            Assert.AreEqual(0, retVal.Item2.Item2.NewGeos.Count);
            Assert.AreEqual("2018-05-07", retVal.Item1);
        }

        [Test]
        public void InitDBConsumptionWrite_TryExecuteOstvConsumptionRWOnBadDateContext_ReturnsInvalidDateTimen()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> retVal;
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            var futureTime = DateTime.Now.AddDays(1);
            FileInfo fileInfo = new FileInfo(path + String.Format("\\TestXMLs\\ostv_{0}_{1}_{2}.xml",
                futureTime.Year, futureTime.Month, futureTime.Day));

            //Act
            retVal = fileControler.LoadFileStoreDB(String.Format(fileInfo.FullName, 
                futureTime.Year, futureTime.Month, futureTime.Day), ELoadDataType.Consumption);

            //Assert
            Assert.AreEqual(EFileLoadStatus.InvalidDateTime, retVal.Item2.Item1);
            Assert.AreEqual(0, retVal.Item2.Item2.DupsAndMisses.Count);
            Assert.AreEqual(0, retVal.Item2.Item2.NewGeos.Count);
            Assert.AreEqual("", retVal.Item1);
        }

        [Test]
        public void InitDBConsumptionWrite_TryExecuteOstvConsumptionRWOnBadDate_ReturnsInvalidDateTime()
        {
            //Arrange
            FileControler fileControler = new FileControler();
            Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> retVal;
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\exp_2018_01_32.html");

            //Act
            retVal = fileControler.LoadFileStoreDB(fileInfo.FullName, ELoadDataType.Consumption);

            //Assert
            Assert.AreEqual(EFileLoadStatus.InvalidDateTime, retVal.Item2.Item1);
            Assert.AreEqual(0, retVal.Item2.Item2.DupsAndMisses.Count);
            Assert.AreEqual(0, retVal.Item2.Item2.NewGeos.Count);
            Assert.AreEqual("", retVal.Item1);
        }

    }
}
