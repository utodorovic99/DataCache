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
            Trace.WriteLine("Writing: {0}", fileName);
            var loadReult = ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption);

            //With Dups and Misses
            fileName = "ostv_2018_05_08.xml";
            fileLoadPath = Path.GetFullPath(fileName);   
            Trace.WriteLine("Writing: {0}", fileName);
            loadReult = ui.InitFileLoad(fileLoadPath, ELoadDataType.Consumption);


            Console.ReadKey(true);
        }
    }

}
