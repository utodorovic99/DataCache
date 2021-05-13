using Common_Project.DistributedServices;
using DistributedDB_Project.DistributedCallHandler;
using DistributedDB_Project.DistributedDBCallHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project
{
    class Program
    {

        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof (DistributedDB_Project.DistributedDBCallHandler.DataCacheClientService)))
            {
                host.Open();
                Console.WriteLine("Server is online...");

                MainUIHandler adminSide = new MainUIHandler();
                adminSide.HandleMainMenu();

                Console.ReadLine();
                Console.ReadKey();
                host.Close();
            }

        }
    }
}
