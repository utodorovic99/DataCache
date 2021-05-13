///////////////////////////////////////////////////////////
//  Project that emulates GUI method calls created for
//  independent testing purposes
//  Generated manually
//  Created on:      28-Apr-2021 11:55:24 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

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

            // Regular
            UI ui = new UI();
            ui.InitDataLoad();
            string fileName = "ostv_2018_05_07.xml";
            string fileLoadPath = Path.GetFullPath(fileName);   // Emulating client path delivery
            ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));

            fileName = "ostv_2018_05_08.xml";
            fileLoadPath = Path.GetFullPath(fileName);          // Emulating client path delivery
            ParseDBWriteResult(ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption));

            Console.ReadKey(true);
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
