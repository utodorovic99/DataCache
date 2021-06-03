using Common_Project.Classes;
using FileControler_Project.Enums;
using FileControler_Project.Handlers.XMLHandler.Classes;
using FileControler_ProjectTest.TestXMLs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileControler_ProjectTest.HandlersTest
{
	[TestFixture]
	public class XMLHandlerTest
	{

		[Test]
		public void XMLOstvConsumptionRead_TryReadNotExistingFile_ReturnsOpeningFailed()
		{
			//Arrange
			string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;

			FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\notExisting.xml");

			//Act
			XMLHandler xmlHandler = new XMLHandler();
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.OpeningFailed);
			Assert.AreEqual(result.Item2.Count, 0);
		}

		[Test]
		public void XMLOstvConsumptionRead_TryReadExistingFIleBadName_ReturnsFileNameConventionViolated()
		{
			//Arrange
			string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
			FileInfo fileInfo = new FileInfo(path + "\\TestXMLs\\bad_convention_example.xml");

			//Act
			XMLHandler xmlHandler = new XMLHandler();
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.FileNameConventionViolated);
			Assert.AreEqual(result.Item2.Count, 0);
		}

		[Test]
		public void XMLOstvConsumptionRead_TryReadCorrectFile_Success()
        {
			//Arrange
			string fullTmpFilePath = FileSystemEmulator.GetResourceTextFile("ostv_2018_05_07.xml");
			FileInfo fileInfo = new FileInfo(fullTmpFilePath);
			XMLHandler xmlHandler = new XMLHandler();

			//Act & Dispose
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);
			File.Delete(fullTmpFilePath);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.Success);
			Assert.AreNotEqual(result.Item2.Count, 0);
		}

		[Test]
		public void XMLOstvConsumptionRead_TryReadWithAudit_Success()
		{
			//Arrange
			string fullTmpFilePath = FileSystemEmulator.GetResourceTextFile("ostv_2018_05_08.xml");
			FileInfo fileInfo = new FileInfo(fullTmpFilePath);
			XMLHandler xmlHandler = new XMLHandler();

			//Act & Dispose
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);
			File.Delete(fullTmpFilePath);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.Success);
			Assert.AreNotEqual(result.Item2.Count, 0);
		}

		[Test]
		public void XMLOstvConsumptionRead_TryReadTotalCorrupt_ReturnsPartialReadSuccess()
		{
			//Arrange
			string fullTmpFilePath = FileSystemEmulator.GetResourceTextFile("ostv_2018_05_09.xml");
			FileInfo fileInfo = new FileInfo(fullTmpFilePath);
			XMLHandler xmlHandler = new XMLHandler();

			//Act & Dispose
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);
			File.Delete(fullTmpFilePath);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.PartialReadSuccess);
			Assert.AreNotEqual(result.Item2.Count, 0);
		}

		[Test]
		public void XMLOstvConsumptionRead_TryReadPartialCorrupt_PartialReadSuccess()
		{
			//Arrange
			string fullTmpFilePath = FileSystemEmulator.GetResourceTextFile("ostv_2018_05_10.xml");
			FileInfo fileInfo = new FileInfo(fullTmpFilePath);
			XMLHandler xmlHandler = new XMLHandler();

			//Act & Delete
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);
			File.Delete(fullTmpFilePath);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.PartialReadSuccess);
			Assert.AreNotEqual(result.Item2.Count, 0);
		}

		[Test]
		public void XMLOstvConsumptionRead_TryReadTotalCorrupt_InvalidFileStructure ()
		{
			//Arrange
			string fullTmpFilePath = FileSystemEmulator.GetResourceTextFile("ostv_2018_05_11.xml");
			FileInfo fileInfo = new FileInfo(fullTmpFilePath);
			XMLHandler xmlHandler = new XMLHandler();

			//Act & Dispose
			Tuple<EFileLoadStatus, List<ConsumptionRecord>> result = xmlHandler.XMLOstvConsumptionRead(fileInfo);
			File.Delete(fullTmpFilePath);

			//Assert
			Assert.AreEqual(result.Item1, EFileLoadStatus.InvalidFileStructure);
			Assert.AreEqual(result.Item2.Count, 0);
		}
	}
}
