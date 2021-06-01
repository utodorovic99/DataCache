using Common_Project.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_ProjectTest.Exceptions
{
    [TestFixture]
    public class InvalidParamsExceptionTest
    {
        #region Constructor_Tests
        [Test]
        public void InvalidParamsException_TryConstruct_Success()
        {
            //Arrange & Act
            string summary = "summary text";
            InvalidParamsException exc = new InvalidParamsException(summary);

            //Assert
            Assert.IsNotNull(exc);
            Assert.AreEqual(summary, exc.Summary);
        }
        #endregion

        #region Property_Tests
        [Test]
        public void Summary_TryGet_Success()
        {
            //Arrange 
            string summary = "summary text";
            InvalidParamsException exc = new InvalidParamsException(summary);

            //Act
            var summaryGet = exc.Summary;

            //Assert
            Assert.IsNotNull(summaryGet);
            Assert.AreEqual(summary, exc.Summary);
        }

        [Test]
        public void Summary_TrySet_Success()
        {
            //Arrange 
            string summary = "summary text";
            InvalidParamsException exc = new InvalidParamsException(summary);
            string summaryNew = "summary new";

            //Act
            exc.Summary=summaryNew;

            //Assert
            Assert.AreEqual(summaryNew, exc.Summary);
        }
        #endregion
    }
}
