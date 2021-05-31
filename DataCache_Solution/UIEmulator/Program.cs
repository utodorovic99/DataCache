///////////////////////////////////////////////////////////
//  Project that emulates GUI method calls created for
//  independent testing purposes
//  Generated manually
//  Created on:      28-Apr-2021 11:55:24 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using CacheControler_Project.Enums;
using Common_Project.Classes;
using ConnectionControler_Project.Exceptions;
using FileControler_Project.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Project.Classes;

namespace UIEmulator
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("\r\n\t===============================| Welcome to UI DataCache Test Emulator |===============================\n\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            UI ui = new UI();
            if (!ui.DBOnline) Console.WriteLine("DB Offline try reconnect or call support");

            WriteGeoAll(ui.GetGeographicEntities());                                    // Read geo all
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());                                       // Read audit all
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);

            string fileName = "ostv_2018_05_07.xml";                                    // Load regular file
            string fileLoadPath = Path.GetFullPath(fileName);


            //Fail, if DB Console App is not started
            try
            {
                ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));
            }
            catch (DBOfflineException dbe)
            {
                Console.WriteLine("Error:\t" + dbe.Message);
            }
            Console.ReadKey(true);

            //Start DB console app and then try
            if (ui.DBReconnect()) Console.WriteLine("Reconnecting succesfull");
            else                  Console.WriteLine("Reconnect failed");

            WriteGeoAll(ui.GetGeographicEntities());                                    // Read geo all (initial state)
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());                                       // Read audit all (initial state)
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            try
            {
                ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));
            }
            catch (DBOfflineException dbe)
            {
                Console.WriteLine("Error:\t" + dbe.Message);
            }
            Console.ReadKey(true);


            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            fileName = "ostv_2018_05_08.xml";                                           // Load file with audit
            fileLoadPath = Path.GetFullPath(fileName);          
            ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            try
            {
                ui.PostGeoEntitiy(new GeoRecord("223", "SOMALIA"));                         // Insert geo
            }
            catch (DBOfflineException dbe)
            {
                Console.WriteLine("Error:\t" + dbe.Message);
            }
   
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            try
            {
                ui.UpdateGeoEntity("SOMALIA", "SOM");           // Editing geo
            }
            catch (DBOfflineException dbe)
            {
                Console.WriteLine("Error:\t" + dbe.Message);
            }

                                                    
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            DSpanGeoReq req = new DSpanGeoReq("SRB", "2018-05-08", "2021-12-31");       // Read interval
            var search_1_result = ui.InitConsumptionRead(req);
            if (search_1_result.Item1 == EConcumptionReadStatus.DBReadFailed)
                Console.WriteLine("DB offline...");
            else
            {
                if (search_1_result.Item1 == EConcumptionReadStatus.CacheReadSuccess) 
                     Console.Write("\t<<< CACHE HIT >>>");
                else Console.Write("\t<<< DB READ >>>");

                PrintSearchResult(req, search_1_result.Item2);
            }
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            DSpanGeoReq req2 = new DSpanGeoReq("SRB", "2021-01-01", "2021-12-31");      // Read sub-interval
            var search_2_result = ui.InitConsumptionRead(req2);
            if (search_2_result.Item1 == EConcumptionReadStatus.DBReadFailed)
                Console.WriteLine("DB offline...");
            else
            {
                if (search_2_result.Item1 == EConcumptionReadStatus.CacheReadSuccess)
                    Console.Write("\t<<< CACHE HIT >>>");
                else Console.Write("\t<<< DB READ >>>");

                PrintSearchResult(req2, search_2_result.Item2);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            DSpanGeoReq req3 = new DSpanGeoReq("SRB", "2018-05-08", "2018-12-31");       // Read sub-interval
            var search_3_result = ui.InitConsumptionRead(req3);
            if (search_3_result.Item1 == EConcumptionReadStatus.DBReadFailed)
                Console.WriteLine("DB offline...");
            else
            {
                if (search_3_result.Item1 == EConcumptionReadStatus.CacheReadSuccess)
                    Console.Write("\t<<< CACHE HIT >>>");
                else Console.Write("\t<<< DB READ >>>");

                PrintSearchResult(req3, search_3_result.Item2);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        private static void PrintSearchResult(DSpanGeoReq req, List<ConsumptionRecord> records)
        {

            Console.WriteLine(Environment.NewLine + req);
            Console.WriteLine();
            int loc = 0;
            foreach(var record in records)
            {
                ++loc;
                Console.WriteLine(loc+".\t"+record);
            }
            Console.WriteLine();
        }

        private static void WriteGeoAll(List<string> geos)
        {
            foreach (var geo in geos) Console.WriteLine("\t"+geo);
        }

        private static void WriteAuditAll(List<AuditRecord> audits)
        {
            foreach (var audit in audits) Console.WriteLine(audit);
        }

        private static void ParseDBWriteResult( EFileLoadStatus status)
        {
            switch (status)
            {
                case EFileLoadStatus.DBWriteFailed:
                    {
                        break;
                    }
                case EFileLoadStatus.Failed:
                    {
                        break;
                    }
                case EFileLoadStatus.FileTypeNotSupported:
                    {
                        Console.WriteLine("FAIL: Only 'ostv' data type supported");
                        break;
                    }
                case EFileLoadStatus.InvalidFileExtension:
                    {
                        Console.WriteLine("FAIL: Only '.xml' document type supported ");
                        break;
                    }
                case EFileLoadStatus.InvalidFileStructure:
                    {
                        Console.WriteLine("FAIL: All records have wrong structure");
                        break;
                    }
                case EFileLoadStatus.OpeningFailed:
                    {
                        Console.WriteLine("FAIL: Selected file is used by another process");
                        break;
                    }
                case EFileLoadStatus.PartialReadSuccess:
                    {
                        Console.WriteLine("REPORT: Corrupted element structure records found, others written successfully");
                        break;
                    }
                case EFileLoadStatus.Success:
                    {
                        Console.WriteLine("REPORT: All elements written successfully");
                        break;
                    }
            }
        }

    }

}
