using Common_Project.DistributedServices;
using DistributedDB_Project.DistributedCallHandler;
using DistributedDB_Project.DistributedDBCallHandler;
using DistributedDB_Project.Exceptions.ExceptionAbstraction;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            {
                bool passed = false;
                do
                {
                    try
                    {
                        Connection.DBConnectionParams.LoadLoginParams();
                        passed = true;
                    }
                    catch(ConfigurationErrorsException)
                    {
                        Console.WriteLine("Connection params source file not found...");
                    }
                    catch (DBLoginFailed exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.Write("Try relog (Y/N): ");
                        if ( Console.ReadLine().ToUpper().Equals("N")) return;
                    }
                } while (!passed);
                

            }

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
