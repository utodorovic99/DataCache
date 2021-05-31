using Common_Project.Classes;
using DistributedDB_Project.Exceptions.ExceptionAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DistributedCallHandler
{
    public class GeographyUIHandler
    {
        private static readonly GeographyService geographyService = new GeographyService();
        public void HandleGeographyMenu()
        {
            String answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose geography-related operation:");
                Console.WriteLine("1\t- Show all");
                Console.WriteLine("2\t- Show by GID");
                Console.WriteLine("3\t- Show multiple by GID");
                Console.WriteLine("4\t -Count");
                Console.WriteLine();
                Console.WriteLine("5\t- Add new geography - Single");
                Console.WriteLine("6\t- Add new geography - Multiple");
                Console.WriteLine();
                Console.WriteLine("7\t- Delete single by GID");
                Console.WriteLine("8\t- Delete multiple by GID");
                Console.WriteLine("9\t- Delete all");
                Console.WriteLine();
                Console.WriteLine("10\t- Modify by GID");
                Console.WriteLine();
                Console.WriteLine("X - Exit geography menu");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        ShowByGID();
                        break;
                    case "3":
                        ShowMultipleByGID();
                        break;
                    case "4":
                        Count();
                        break;
                    case "5":
                        AddNewGeographySingle();
                        break;
                    case "6":
                        AddNewGeographyMultiple();
                        break;
                    case "7":
                        DeleteSingleGeoByGID();
                        break;
                    case "8":
                        DeleteMultipleGeoByGID();
                        break;
                    case "9":
                        DeleteAll();
                        break;
                    case "10":
                        ModifyByGID();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void FormatedPrintOut(Dictionary<string, string> records)
        { 
            foreach (var record in records) 
                Console.WriteLine("GID:\t"+record.Value + "\t"+"GNAME: "+record.Key); 
        }

        private void FormatedPrintOut(List<GeoRecord> records )
        {
            foreach (var record in records)
                Console.WriteLine(record);
        }

        private void ShowAll()
        {
            Console.WriteLine("\t\t<< GEOGRAPHY CONTENT >>" + Environment.NewLine);
            FormatedPrintOut(geographyService.HandleShowAll());
        }

        private void ShowByGID()
        {
            Console.Write("Enter target GID: ");
            
            var retVal = geographyService.HandleShowByGID(Console.ReadLine());
            if (retVal.IsEmpty())
                Console.WriteLine("\t\t<< TARGET ELEMENT NOT FOUND >>" + Environment.NewLine);
            else
            {
                Console.WriteLine("\t\t<< TARGET ELEMENT FOUND >>" + Environment.NewLine);
                Console.WriteLine(retVal);
            } 
        }

        private void ShowMultipleByGID()
        {
            List<string> keys = new List<string>();
            string tmpKey;

            while(true)
            {
                Console.Write("Enter GID (press 'p' for stop): ");
                tmpKey=Console.ReadLine();

                if (tmpKey.ToUpper().Equals("P")) break;

                keys.Add(tmpKey);
            }
            FormatedPrintOut(geographyService.HandleShowMultipleByGID(keys));
        }

        private void Count()
        {
            Console.WriteLine("\t\t<< GEOGRAPHY TABLE COUNTS "+ geographyService.HandleCount()+" >>" + Environment.NewLine);
        }

        private void AddNewGeographySingle()
        {
            GeoRecord toAdd = new GeoRecord();
            Console.Write("Enter GID: ");
            toAdd.GID = Console.ReadLine();
            Console.Write("Enter GNAME: ");
            toAdd.GName = Console.ReadLine();
            
            try
            {
                geographyService.HandleSignleGeoWrite(toAdd);
                Console.WriteLine("\t\t<< RECORD SUCCESFULLY ADDED >>" + Environment.NewLine);
                ShowAll();
            }
            catch(PrimaryKeyConstraintViolationException exc)
            { Console.WriteLine("\t\t<< RECORD INSER FAILED >>\nError: {0}", exc.Message); }
            
        }

        private void AddNewGeographyMultiple()
        {
            List<GeoRecord> toAddList = new List<GeoRecord>();
            string tmpGID, tmpGName;
            while (true)
            {
                Console.Write("Enter GID (press 'p' for stop): ");
                tmpGID = Console.ReadLine();

                if (tmpGID.ToUpper().Equals("P")) break;

                Console.Write("Enter GNAME: ");
                tmpGName = Console.ReadLine();
                toAddList.Add(new GeoRecord(tmpGID, tmpGName));
            }
            geographyService.HandleMultipleGeoWrite(toAddList);
            Console.WriteLine("\nUsed GID records dumped, current state:\n\n");
            ShowAll();
        }

        private void DeleteSingleGeoByGID()
        {
            try 
            {
                Console.Write("Enter GID to ");
                if (geographyService.HandleDeleteSingleGeoByGID(Console.ReadLine()))
                {
                    Console.WriteLine("\t\t<< SUCCESSFULLY DELTED >>\n");
                    ShowAll();
                }
                else
                {
                    Console.WriteLine("\t\t<< RECORD DELETE FAILED >>\nError: Element not found");
                }
            }
            catch(StillAttachedException stex)
            {
                Console.WriteLine("\t\t<< RECORD DELETE FAILED >>\nError: {0}", stex.Message);
            }

        }

        private void DeleteMultipleGeoByGID()
        {
            string tmpGID;
            List<string> targs = new List<string>();
            while (true)
            {
                Console.Write("Enter GID (press 'p' for stop): ");
                tmpGID = Console.ReadLine();

                if (tmpGID.ToUpper().Equals("P")) break;
                targs.Add(tmpGID);

            }
            geographyService.HandleDeleteMultipleGeoContent(targs);
            ShowAll();
        }

        private void DeleteAll()
        {
            geographyService.HandleDeleteAll();
            ShowAll();
        }
        private void ModifyByGID()
        {
            Console.Write("Enter target GID: ");
            string targetGID = Console.ReadLine();
            Console.Write("Enter new GNAME: ");
            string newGName = Console.ReadLine();
            try
            {
                geographyService.HandleSingleGeoUpdate(targetGID, newGName);
                Console.WriteLine("\t\t<< RECORD UPDATE SUCCESFULL >>\n");
            }
            catch(PrimaryKeyConstraintViolationException ex)
            {
                Console.WriteLine("\t\t<< RECORD UPDATE FAILED >>\nError: {0}", ex.Message);
            }
            ShowAll();
        }


    }
}
