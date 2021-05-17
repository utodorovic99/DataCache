///////////////////////////////////////////////////////////
//  Project that emulates GUI method calls created for
//  independent testing purposes
//  Generated manually
//  Created on:      28-Apr-2021 11:55:24 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using Common_Project.Classes;
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

            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);

            string fileName = "ostv_2018_05_07.xml";
            string fileLoadPath = Path.GetFullPath(fileName);   // Emulating client path delivery
            ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            fileName = "ostv_2018_05_08.xml";
            fileLoadPath = Path.GetFullPath(fileName);          // Emulating client path delivery
            ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            WriteAuditAll(ui.GetAuditEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            ui.PostGeoEntitiy(new GeoRecord("223", "SOMALIA"));
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            ui.UpdateGeoEntity("SOMALIA", "SOM");
            WriteGeoAll(ui.GetGeographicEntities());
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            DSpanGeoReq req = new DSpanGeoReq("SRB", "2018-05-08", "2021-12-31");
            PrintSearchResult(req, ui.InitConsumptionRead(req).Item2);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            DSpanGeoReq req2 = new DSpanGeoReq("SRB", "2021-01-01", "2021-12-31");
            PrintSearchResult(req, ui.InitConsumptionRead(req2).Item2);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

            DSpanGeoReq req3 = new DSpanGeoReq("SRB", "2018-05-08", "2018-12-31");
            PrintSearchResult(req, ui.InitConsumptionRead(req3).Item2);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();

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
