using FileControler_Project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common_Project;
using CacheControler_Project;
using ConnectionControler_Project;
using DistributedDB_Project;
using FileControler_Project;
using GUI_Integrator_Project;
using UI_Project;
using DistributedDB_Project.DistributedDBCallHandler;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
using ConnectionControler_ProjectTest.ClassesTest;
using Moq;
using Common_Project.Classes;
using ConnectionControler_Project.Exceptions;
using System.ServiceModel;
using NUnit.Framework;

namespace FileControler_ProjectTest.ClassesTest
{
    [TestFixture]
    public class FileControlerAgentTest
    {
        

        

        [Test]
        public void TryReconnect_Try_FailedDBOffline()
        {
            //Arrange
            FileControlerAgent controler = new FileControlerAgent();
            bool result;

            //Act
            result = controler.TryReconnect();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
