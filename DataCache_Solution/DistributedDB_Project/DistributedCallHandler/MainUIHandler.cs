using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DistributedCallHandler
{
    // Class for emulating distributed calls. Created for independent testing purposes 
    public class MainUIHandler
    {
        private readonly ConsumptionUIHandler consumptionUIHandler  = new ConsumptionUIHandler();
        private readonly AuditUIHandler auditUIHandler              = new AuditUIHandler();
        private readonly GeographyUIHandler geographyUIHandler      = new GeographyUIHandler();

        public void HandleMainMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Pick an option:");
                Console.WriteLine("1 - Consumption handling");
                Console.WriteLine("2 - Geography handling");
                Console.WriteLine("3 - Audit handling");
                Console.WriteLine("X - Exit");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        consumptionUIHandler.HandleConsumptionMenu();
                        break;
                    case "2":
                        geographyUIHandler.HandleGeographyMenu();
                        break;
                    case "3":
                        auditUIHandler.HandleAuditMenu();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }
    }
}
