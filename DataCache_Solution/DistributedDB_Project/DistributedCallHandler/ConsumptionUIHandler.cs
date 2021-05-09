using Common_Project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DistributedCallHandler
{
    public class ConsumptionUIHandler
    {
        private static readonly ConsumptionService consumptionService = new ConsumptionService();

        public void HandleConsumptionMenu()
        {
            String answer;
            do
            {
                Console.WriteLine("\n==============================================================\n");
                Console.WriteLine();
                Console.WriteLine("Choose consumption-related operation:");
                Console.WriteLine("1 - Show all");
                Console.WriteLine("2 - Show by geography");
                Console.WriteLine("3 - Show by geography & date");
                Console.WriteLine("4 - Show by geography & after date");
                Console.WriteLine("5 - Show by geography & before date");
                Console.WriteLine("6 - Show by geography & date span");
                Console.WriteLine();
                Console.WriteLine("7 - Insert new consumption - Single");
                Console.WriteLine("8 - Insert new consumption - Multiple");
                Console.WriteLine();
                Console.WriteLine("X - Exit consumption menu");

                answer = Console.ReadLine();
                Console.WriteLine("\n==============================================================\n");

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        ShowByGeography();
                        break;
                    case "3":
                        ShowByGeographyAndDate();
                        break;
                    case "4":
                        ShowByGeographyAndAfterDate();
                        break;
                    case "5":
                        ShowByGeographyAndBeforeDate();
                        break;
                    case "6":
                        ShowByGeographyAndDatespan();
                        break;
                    case "7":
                        InsertNewConsumptionSingle();
                        break;
                    case "8":
                        InsertNewConsumptionMultiple();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }


        private void ShowAll() 
        {
            Console.WriteLine("\t\t<< ALL CONSUMPTION RECORDS >> "+Environment.NewLine);
            int idx = 0;
            foreach(var consumptionRecord in consumptionService.HandleFindConsumptionAll())
            { 

                Console.WriteLine("\t"+ ++idx +".\t" +consumptionRecord.ToString());
            }
        }

        //For testing purposes consider user input (granted by user agent)
        private void ShowByGeography() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID=Console.ReadLine();

            Console.WriteLine(Environment.NewLine+"\t\t<< ALL CONSUMPTION RECORDS FOR {0} >>\r\n",targetGID);
            foreach (ConsumptionRecord currRecord in consumptionService.HandleGetByCountry(targetGID))
            {
                Console.WriteLine(currRecord.ToString());
            }
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndDate() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter date for target entities (Format: Year-Month-Day): ");
            string targetTimestamp = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} AND DATE {1} >>\r\n", targetGID, targetTimestamp);
            foreach (ConsumptionRecord currRecord in consumptionService.HandleGetByCountryAndDate(targetGID, targetTimestamp))
            {
                Console.WriteLine(currRecord.ToString());
            }
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndAfterDate() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter \"from\" date for target entities (Format: Year-Month-Day): ");
            string targetTimestamp = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} AFTER {1} >>\r\n", targetGID, targetTimestamp);
            foreach (ConsumptionRecord currRecord in consumptionService.HandleGetByGeographyAndAfterDate(targetGID, targetTimestamp))
            {
                Console.WriteLine(currRecord.ToString());
            }
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndBeforeDate() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter \"till\" date for target entities (Format: Year-Month-Day): ");
            string targetTimestamp = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} BEFORE {1} >>\r\n", targetGID, targetTimestamp);
            foreach (ConsumptionRecord currRecord in consumptionService.HandleGetByGeographyAndBeforeDate(targetGID, targetTimestamp))
            {
                Console.WriteLine(currRecord.ToString());
            }
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndDatespan() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter \"from\" date for target entities (Format: Year-Month-Day): ");
            string targetTimestampAfter = Console.ReadLine();
            Console.Write("Enter \"till\" date for target entities (Format: Year-Month-Day): ");
            string targetTimestampBefore = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} BETWEEN {1} AND {2} >>\r\n", targetGID, targetTimestampAfter, 
                                                                                                                         targetTimestampBefore);
            foreach (var currRecord in consumptionService.HandleGetByCountryAndDatespan(targetGID, targetTimestampAfter, targetTimestampBefore)) 
            {
                Console.WriteLine(currRecord.ToString());
            }
            Console.WriteLine(Environment.NewLine);
        }

        private void InsertNewConsumptionSingle()
        {
            ConsumptionRecord toAddConsumptionRecord = new ConsumptionRecord();
            Console.Write("Enter GID: ");
            toAddConsumptionRecord.GID=Console.ReadLine();

            Console.Write("Enter MWh: ");
            toAddConsumptionRecord.MWh= Int32.Parse(Console.ReadLine());

            Console.Write("Enter Timestamp (Format: Year-Month-Day): ");
            toAddConsumptionRecord.TimeStamp=Console.ReadLine();

            try
            {
                var retVal=consumptionService.HandleStoreConsumption(toAddConsumptionRecord);
                Console.WriteLine(Environment.NewLine + "\t\t<< INSERT SUCCEED >> ");
                Console.WriteLine(Environment.NewLine + retVal + Environment.NewLine);
            }
            catch(Exception e)
            {
                Console.WriteLine(Environment.NewLine + "\t\t<< INSERT FAILED >> ");
                Console.WriteLine(e.Message);
            }
   
        }
        private void InsertNewConsumptionMultiple() 
        {
            List<ConsumptionRecord> insertConsumptionList = new List<ConsumptionRecord>(1);
            string answer = "";

            do
            {
                ConsumptionRecord toAddConsumptionRecord = new ConsumptionRecord();
                Console.Write("Enter GID: ");
                toAddConsumptionRecord.GID = Console.ReadLine();

                Console.Write("Enter MWh: ");
                toAddConsumptionRecord.MWh = Int32.Parse(Console.ReadLine());

                Console.Write("Enter Timestamp (Format: Year-Month-Day): ");
                toAddConsumptionRecord.TimeStamp = Console.ReadLine();
                insertConsumptionList.Add(toAddConsumptionRecord);

                Console.WriteLine("Press any key to continue with pre-insert record input");
                Console.WriteLine("Press X to proceed to DB write");
                answer = Console.ReadLine();

            }
            while (!answer.ToUpper().Equals("X"));

            foreach(var consumptionRecord in insertConsumptionList)
            {
                try
                {
                    consumptionService.HandleStoreConsumption(insertConsumptionList);
                    Console.WriteLine("For" +consumptionRecord.ToString()+ "\t\t<< INSERT SUCCEED >> ");
                }
                catch (Exception e)
                {
                    Console.WriteLine("For" + consumptionRecord.ToString() + "\t\t<< INSERT SUCCEED >>\nError: ");
                    Console.WriteLine(e.Message);
                }
            }

        }

    }
}
