using ConnectionControler_Project.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionControler_ProjectTest.ExceptionsTest
{
    [TestFixture]
    public class DBOfflineExceptionTest
    {
        [Test]
        public void DBOfflineException_TryConstructNullParam_ConstructMessageEmpty()
        {
            //Arrange
            string message = null;

            //Act
            DBOfflineException exc = new DBOfflineException(message);

            //Assert
            Assert.AreEqual(exc.Message, "");
        }

        [Test]
        public void DBOfflineException_TryConstructNotNullParam_ConstructsSame()
        {
            //Arrange
            string message = "message";

            //Act
            DBOfflineException exc = new DBOfflineException(message);

            //Assert
            Assert.AreEqual(exc.Message, message);
        }

        [Test]
        public void DBOfflineException_MessageProperty_RegularOutput()
        {
            //Arrange
            string message = "message";

            //Act
            DBOfflineException exc = new DBOfflineException(message);
            message = "message2";
            exc.Message = message;

            //Assert
            Assert.AreEqual(exc.Message, message);
        }

    }
}
