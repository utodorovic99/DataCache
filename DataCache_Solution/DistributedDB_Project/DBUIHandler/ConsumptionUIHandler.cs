using Common_Project.Classes;
using DistributedDB_Project.Exceptions;
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
                Console.WriteLine("1\t- Show all");
                Console.WriteLine("2\t- Show multiple by CID");
                Console.WriteLine("3\t- Show single by CID");
                Console.WriteLine("4\t- Show by geography");
                Console.WriteLine("5\t- Show by geography & date");
                Console.WriteLine("6\t- Show by geography & after date");
                Console.WriteLine("7\t- Show by geography & before date");
                Console.WriteLine("8\t- Show by geography & date span");
                Console.WriteLine("9\t- Count");
                Console.WriteLine("10\t- Exists by CID");
                Console.WriteLine("11\t- Exists by content");
                Console.WriteLine();
                Console.WriteLine("12\t- Insert new consumption - Single");
                Console.WriteLine("13\t- Insert new consumption - Multiple");
                Console.WriteLine();
                Console.WriteLine("14\t- Delete by CID");
                Console.WriteLine("15\t- Delete by content");
                Console.WriteLine();
                Console.WriteLine("X\t2" +
                    "- Exit consumption menu");

                answer = Console.ReadLine();
                Console.WriteLine("\n==============================================================\n");

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        ShowAllByCID();
                        break;
                    case "3":
                        ShowSingleByCID();
                        break;
                    case "4":
                        ShowByGeography();
                        break;
                    case "5":
                        ShowByGeographyAndDate();
                        break;
                    case "6":
                        ShowByGeographyAndAfterDate();
                        break;
                    case "7":
                        ShowByGeographyAndBeforeDate();
                        break;
                    case "8":
                        ShowByGeographyAndDatespan();
                        break;
                    case "9":
                        Count();
                        break;
                    case "10":
                        ExistsByCID();
                        break;
                    case "11":
                        ExistsByContent();
                        break;
                    case "12":
                        InsertNewConsumptionSingle();
                        break;
                    case "13":
                        InsertNewConsumptionMultiple();
                        break;
                    case "14":
                        DeleteByCID();
                        break;
                    case "15":
                        DeleteByContent();
                        break;
                    case "16":
                        DeleteAll();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void FormatMultipleConsumptionPrint(List<ConsumptionRecord> records)
        {
            int idx = 0;
            foreach (var consumptionRecord in records)
            {

                Console.WriteLine("\t" + ++idx + ".\t" + consumptionRecord.ToString());
            }
        }

        private void ShowAll() 
        {
            Console.WriteLine("\t\t<< ALL CONSUMPTION RECORDS >> "+Environment.NewLine);
            FormatMultipleConsumptionPrint(consumptionService.HandleFindConsumptionAll());

        }

        //For testing purposes consider user input (granted by user agent) (#)
        private void ShowAllByCID()
        {
            List<string> keys=new List<string>();
            string answer="";
            while(true)
            {
                Console.Write("Enter CID ('p' key to stop): ");
                answer = Console.ReadLine();
                if (answer.ToUpper().Equals("P")) break;
                keys.Add(answer);
            }
            Console.WriteLine("\t\t<< ALL CONSUMPTION RECORDS >> " + Environment.NewLine);
            Console.WriteLine(Environment.NewLine + "TargetCIDs:\n");
            foreach (var cID in keys) Console.WriteLine("\t" + cID);
            Console.WriteLine();

            FormatMultipleConsumptionPrint(consumptionService.HandleFindAllByCID(keys));
        }

        private void ShowSingleByCID()
        {
            Console.WriteLine("\t\t<< RECORD FOR TARGET CID >> " + Environment.NewLine);
            Console.Write("Enter target CID: ");
            Console.WriteLine(consumptionService.HandleFindSingleByCID(Console.ReadLine()));
        }

        //(#)
        private void ShowByGeography() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID=Console.ReadLine();

            Console.WriteLine(Environment.NewLine+"\t\t<< ALL CONSUMPTION RECORDS FOR {0} >>\r\n",targetGID);
            FormatMultipleConsumptionPrint(consumptionService.HandleGetByCountry(targetGID));
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndDate() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter date for target entities (Format: Year-Month-Day): ");
            string targetTimestamp = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} AND DATE {1} >>\r\n", targetGID, targetTimestamp);
            FormatMultipleConsumptionPrint(consumptionService.HandleGetByCountryAndDate(targetGID, targetTimestamp));
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndAfterDate() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter \"from\" date for target entities (Format: Year-Month-Day): ");
            string targetTimestamp = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} AFTER {1} >>\r\n", targetGID, targetTimestamp);
            FormatMultipleConsumptionPrint(consumptionService.HandleGetByGeographyAndAfterDate(targetGID, targetTimestamp));
            Console.WriteLine(Environment.NewLine);
        }

        private void ShowByGeographyAndBeforeDate() 
        {
            Console.Write("Enter GID for target entities: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter \"till\" date for target entities (Format: Year-Month-Day): ");
            string targetTimestamp = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "\t\t<< ALL CONSUMPTION RECORDS FOR {0} BEFORE {1} >>\r\n", targetGID, targetTimestamp);
            FormatMultipleConsumptionPrint(consumptionService.HandleGetByGeographyAndBeforeDate(targetGID, targetTimestamp));
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
            FormatMultipleConsumptionPrint(consumptionService.HandleGetByCountryAndDatespan(targetGID, targetTimestampAfter, targetTimestampBefore));
            Console.WriteLine(Environment.NewLine);
        }

        private void Count()
        {
            Console.WriteLine(Environment.NewLine + "\t\t<< CONSUMPTION RECORD TTABLE COUNTS: {0}>>\r\n", consumptionService.HandleCount());
        }

        private void ExistsByContent()
        {
            Console.Write("Enter CID for target entitiy: ");
            string targetCID = Console.ReadLine();
            Console.Write("Enter timestamp for target entities (Format: Year-Month-Day-Hour): ");
            string targetTimestamp = Console.ReadLine();
            Console.Write("Enter MWh for target entity: ");
            string targetMWh = Console.ReadLine();

            Console.Write(Environment.NewLine + "\t\t<< TARGET RECORD");  
            if (consumptionService.HandleExistsByContent(new ConsumptionRecord(targetCID, Int32.Parse(targetMWh), targetTimestamp)))
            {
                Console.WriteLine(" FOUND >>\r\n");
                return;
            }
            Console.WriteLine(" NOT FOUND >>\r\n");

        }
        private void ExistsByCID()
        {
            Console.Write("Enter CID for target entitiy: ");
            string targetCID = Console.ReadLine();


            Console.Write(Environment.NewLine + "\t\t<< TARGET RECORD");
            if (consumptionService.HandleExistsByCID(targetCID))
            {
                Console.WriteLine(" FOUND >>\r\n");
                return;
            }
            Console.WriteLine(" NOT FOUND >>\r\n");
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
                var report = consumptionService.HandleStoreConsumption(toAddConsumptionRecord);
                Console.WriteLine("For" + toAddConsumptionRecord.ToString() + "\t\t<< INSERT SUCCEED >> ");
                Console.WriteLine("\n\t===> REPORT <===\n");
                Console.WriteLine();
                Console.WriteLine(report);
                ShowAll();
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

            var report =consumptionService.HandleStoreConsumption(insertConsumptionList);
            Console.WriteLine("\t===> REPORT <===\n");
            Console.WriteLine();
            Console.WriteLine(report);
            Console.WriteLine();
            ShowAll();
            
        }

        private void DeleteByContent()
        {
            Console.Write("Enter GID: ");
            string gID = Console.ReadLine();

            Console.Write("Enter MWh: ");
            int mWh = Int32.Parse(Console.ReadLine());

            Console.Write("Enter Timestamp (Format: Year-Month-Day): ");
            string timeStamp = Console.ReadLine();


            if (consumptionService.HandleDeleteByContent(new ConsumptionRecord(gID, mWh, timeStamp)))
                Console.WriteLine("\t\t<< DELETE OPERATION SUCCEED >>");
            else
                Console.WriteLine("\t\t<< DELETE OPERATION FAILED >>\nError: Element not found");
            ShowAll();
        }
        private void DeleteByCID()
        {
            Console.Write("Enter CID: ");
            string cID = Console.ReadLine();

            if(consumptionService.HandleDeleteByCID(cID))
                Console.WriteLine("\t\t<< DELETE OPERATION SUCCEED >>");
            else
                Console.WriteLine("\t\t<< DELETE OPERATION FAILED >>\nError: Element not found");
            ShowAll();
        }

        private void DeleteAll()
        {
            Console.WriteLine("<< STATE BEFORE >>");
            ShowAll();
            consumptionService.HandleDeleteAll();
            Console.WriteLine("<< STATE AFTER >>");
            ShowAll();
        }
    }
}
