using Common_Project.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_ProjectTest.ClassesTest
{
    [TestFixture]
    public class ConsumptionUpdateTest
    {
        #region Constructor_Tests

        [Test]
        public void ConsumptionUpdate_TryConstruct_ReturnInstance()
        {
            //Arrange & Act
            ConsumptionUpdate update = new ConsumptionUpdate();

            //Assert
            Assert.IsNotNull(update);
            Assert.AreEqual(0, update.DupsAndMisses.Count);
            Assert.AreEqual(0, update.NewGeos.Count);
            Assert.AreEqual("", update.TimeStampBase);
        }

        #endregion

        #region Property_Tests

        [Test]
        public void DupsAndMisses_TryGet_Success() 
        {
            //Arrange
            ConsumptionUpdate update = new ConsumptionUpdate();

            List<Tuple<int, int>> dupsSRB = new List<Tuple<int, int>>();
            dupsSRB.Add(new Tuple<int, int>(1, 12444));
            dupsSRB.Add(new Tuple<int, int>(1, 12488));
            dupsSRB.Add(new Tuple<int, int>(2, 12314));

            List<int> missesSRB = new List<int>();
            missesSRB.Add(4); missesSRB.Add(5); missesSRB.Add(6); missesSRB.Add(7);

            List<Tuple<int, int>> dupsMNE = new List<Tuple<int, int>>();
            dupsMNE.Add(new Tuple<int, int>(1, 12444));
            dupsMNE.Add(new Tuple<int, int>(1, 12488));
            dupsMNE.Add(new Tuple<int, int>(2, 12314));

            List<int> missesMNE = new List<int>();
            missesMNE.Add(11); missesMNE.Add(9); missesMNE.Add(12); missesMNE.Add(8);
            Dictionary<string, Tuple<List<Tuple<int, int>>, List<int>>> expectedDupsAndMisses = 
                new Dictionary<string, Tuple<List<Tuple<int, int>>, List<int>>>();

            update.DupsAndMisses.Add("SRB", new Tuple<List<Tuple<int, int>>, List<int>>(dupsSRB, missesSRB));
            update.DupsAndMisses.Add("MNE", new Tuple<List<Tuple<int, int>>, List<int>>(dupsMNE, missesMNE));

            expectedDupsAndMisses.Add("SRB", new Tuple<List<Tuple<int, int>>, List<int>>(dupsSRB, missesSRB));
            expectedDupsAndMisses.Add("MNE", new Tuple<List<Tuple<int, int>>, List<int>>(dupsMNE, missesMNE));


            //Act
            var retVal = update.DupsAndMisses;

            //Assert
            Assert.AreEqual(expectedDupsAndMisses, update.DupsAndMisses);
        }

        [Test]
        public void DupsAndMisses_TrySet_Success()
        {
            //Arrange
            ConsumptionUpdate update = new ConsumptionUpdate();

            List<Tuple<int, int>> dupsSRB = new List<Tuple<int, int>>();
            dupsSRB.Add(new Tuple<int, int>(1, 12444));
            dupsSRB.Add(new Tuple<int, int>(1, 12488));
            dupsSRB.Add(new Tuple<int, int>(2, 12314));

            List<int> missesSRB = new List<int>();
            missesSRB.Add(4); missesSRB.Add(5); missesSRB.Add(6); missesSRB.Add(7);

            List<Tuple<int, int>> dupsMNE = new List<Tuple<int, int>>();
            dupsMNE.Add(new Tuple<int, int>(1, 12444));
            dupsMNE.Add(new Tuple<int, int>(1, 12488));
            dupsMNE.Add(new Tuple<int, int>(2, 12314));

            List<int> missesMNE = new List<int>();
            missesMNE.Add(11); missesMNE.Add(9); missesMNE.Add(12); missesMNE.Add(8);
            Dictionary<string, Tuple<List<Tuple<int, int>>, List<int>>> expectedDupsAndMisses =
                new Dictionary<string, Tuple<List<Tuple<int, int>>, List<int>>>();

            expectedDupsAndMisses.Add("SRB", new Tuple<List<Tuple<int, int>>, List<int>>(dupsSRB, missesSRB));
            expectedDupsAndMisses.Add("MNE", new Tuple<List<Tuple<int, int>>, List<int>>(dupsMNE, missesMNE));

            //Act
            update.DupsAndMisses = expectedDupsAndMisses;

            //Assert
            Assert.AreEqual(expectedDupsAndMisses, update.DupsAndMisses);
        }

        [Test]
        public void NewGeos_TryGet_Success()
        {
            //Arrange
            ConsumptionUpdate update = new ConsumptionUpdate();
            update.NewGeos.Add("SRB");
            update.NewGeos.Add("MNE");
            update.NewGeos.Add("RUS");

            //Act
            var geos = update.NewGeos;

            //Assert
            Assert.IsNotNull(geos);
            Assert.AreEqual(3, geos.Count);
            Assert.IsTrue(geos.Contains("SRB"));
            Assert.IsTrue(geos.Contains("MNE"));
            Assert.IsTrue(geos.Contains("RUS"));
        }

        [Test]
        public void NewGeos_TrySet_Success()
        {
            //Arrange
            ConsumptionUpdate update = new ConsumptionUpdate();
            List<string> geos = new List<string>();
            geos.Add("SRB");
            geos.Add("MNE");
            geos.Add("RUS");

            //Act
            update.NewGeos = geos;

            //Assert
            Assert.IsNotNull(geos);
            Assert.AreEqual(3, geos.Count);
            Assert.IsTrue(geos.Contains("SRB"));
            Assert.IsTrue(geos.Contains("MNE"));
            Assert.IsTrue(geos.Contains("RUS"));
        }

        #endregion

        #region ToStringTests

        [ExcludeFromCodeCoverage]
        private string EmultaeExpectedToStringCOnversion(List<string> newGeos, Dictionary<string, 
            Tuple<List<Tuple<int, int>>, List<int>>>  dupsAndMisses)
        {
            string retStr = "Consumption update with new geos:\r\n";
            foreach (var newGID in newGeos)
            {
                retStr += "\r\n" + newGID;
            }

            retStr += "\r\n" + "Duplicates and misses:\r\n";
            foreach (KeyValuePair<string, Tuple<List<Tuple<int, int>>, List<int>>> kValPair in dupsAndMisses)
            {
                retStr += "For GID " + kValPair.Key + " duplicates are:\r\n";
                foreach (var elem in kValPair.Value.Item1)
                {
                    retStr += "\r\nHour: " + elem.Item1 + " Value: " + elem.Item2;
                }

                retStr += "\r\nHours with no values are:\r\n";
                foreach (var miss in kValPair.Value.Item2)
                {
                    retStr += miss + " ";
                }
            }
            return retStr;
        }

        [Test]
        public void ToString_TryConvert_Success()
        {
            //Arrange
            ConsumptionUpdate update = new ConsumptionUpdate();

            List<string> geos = new List<string>();
            geos.Add("BiH");
            geos.Add("ESPN");

            List<Tuple<int, int>> dupsSRB = new List<Tuple<int, int>>();
            dupsSRB.Add(new Tuple<int, int>(1, 12444));
            dupsSRB.Add(new Tuple<int, int>(1, 12488));
            dupsSRB.Add(new Tuple<int, int>(2, 12314));

            List<int> missesSRB = new List<int>();
            missesSRB.Add(4); missesSRB.Add(5); missesSRB.Add(6); missesSRB.Add(7);

            List<Tuple<int, int>> dupsMNE = new List<Tuple<int, int>>();
            dupsMNE.Add(new Tuple<int, int>(1, 12444));
            dupsMNE.Add(new Tuple<int, int>(1, 12488));
            dupsMNE.Add(new Tuple<int, int>(2, 12314));

            List<int> missesMNE = new List<int>();
            missesMNE.Add(11); missesMNE.Add(9); missesMNE.Add(12); missesMNE.Add(8);
            Dictionary<string, Tuple<List<Tuple<int, int>>, List<int>>> expectedDupsAndMisses =
                new Dictionary<string, Tuple<List<Tuple<int, int>>, List<int>>>();

            expectedDupsAndMisses.Add("SRB", new Tuple<List<Tuple<int, int>>, List<int>>(dupsSRB, missesSRB));
            expectedDupsAndMisses.Add("MNE", new Tuple<List<Tuple<int, int>>, List<int>>(dupsMNE, missesMNE));

            //Act
            update.DupsAndMisses = expectedDupsAndMisses;
            update.NewGeos = geos;

            //Act
            var expected = EmultaeExpectedToStringCOnversion(geos, expectedDupsAndMisses);

            //Assert
            Assert.AreEqual(expected, update.ToString());
        }

        #endregion
    }
}
