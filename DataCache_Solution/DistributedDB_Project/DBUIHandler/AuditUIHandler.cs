using Common_Project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.DistributedCallHandler
{
    public class AuditUIHandler
    {
        private static readonly AuditService auditService = new AuditService();
        public void HandleAuditMenu()
        {
            String answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose audit-related operation:");
                Console.WriteLine("1 -\tShow all");
                Console.WriteLine("2 -\tShow multiple by AID");
                Console.WriteLine("3 -\tShow duplicates");
                Console.WriteLine("4 -\tShow duplicates by geo");
                Console.WriteLine("5 -\tShow misses");
                Console.WriteLine("6 -\tShow misses by geo");
                Console.WriteLine("7 -\tShow audit by geo");
                Console.WriteLine("8 -\tCount");
                Console.WriteLine("9 -\tExists by AID");
                Console.WriteLine();
                Console.WriteLine("\tNote:Adding new Audit content is done through\n" +
                    "              \tconumption menu (Records with wrong params)");
                Console.WriteLine();
                Console.WriteLine("X - Exit consumption menu");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        ShowMultipleByAID();
                        break;
                    case "3":
                        ShowDuplicatesAll();
                        break;
                    case "4":
                        ShowDuplicatesAllByGeo();
                        break;
                    case "5":
                        ShowMissesAll();
                        break;
                    case "6":
                        ShowMissesAllByGeo();
                        break;
                    case "7":
                        ShowDupsAndMissesByGeo();
                        break;
                    case "8":
                        Count();
                        break;
                    case "9":
                        ExistsByAID();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void FormatedPrintOut(List<AuditRecord> records)
        {
            foreach(var record in records)
            {
                Console.WriteLine(record.ToString());
            }
        }

        private void ExistsByAID()
        {
            Console.WriteLine("Enter target AID: ");
            if (auditService.HandleExistsByAID(Console.ReadLine()))
            { Console.WriteLine("\t\t<< RECORD FOUND >>\n"); return; }

                Console.WriteLine("\t\t<< RECORD NOT FOUND >>\n");
        }
        private void ShowAll()
        {
            Console.WriteLine("\t\t<< ALL AUDIT RECORDS >>\n");
            FormatedPrintOut(auditService.HandleShowAll());
        }

        private void ShowMultipleByAID()
        {
            List<string> keys = new List<string>();
            string tmpKey;

            while (true)
            {
                Console.Write("Enter AID (press 'p' for stop): ");
                tmpKey = Console.ReadLine();

                if (tmpKey.ToUpper().Equals("P")) break;

                keys.Add(tmpKey);
            }
            Console.WriteLine();
            FormatedPrintOut(auditService.HandleShowMultipleByAid(keys));
        }

        private void ShowDuplicatesAll()
        {
            Console.WriteLine("\t\t<< ALL DUPLICATES >>\n");
            FormatedPrintOut(auditService.HandleShowDuplicatesAll());
        }
        private void ShowDuplicatesAllByGeo()
        {
            Console.Write("Enter target GID: ");
            var gID = Console.ReadLine();

            Console.WriteLine("\t\t<< ALL DUPLICATES FOR GID: {0} >>\n", gID);
            FormatedPrintOut(auditService.HandleShowDuplicatesAllByGeo(gID));
        }
        private void ShowMissesAll()
        {
            Console.WriteLine("\t\t<< ALL MISSES>>\n");
            FormatedPrintOut(auditService.HandleShowMissesAll());
        }
        private void ShowMissesAllByGeo()
        {
            Console.Write("Enter target GID: ");
            var gID = Console.ReadLine();

            Console.WriteLine("\t\t<< ALL DUPLICATES FOR GID: {0} >>\n", gID);
            FormatedPrintOut(auditService.HandleShowMissesAllByGeo(gID));
        }

        private void ShowDupsAndMissesByGeo()
        {
            Console.Write("Enter target GID: ");
            var gID = Console.ReadLine();

            Console.WriteLine("\t\t<< AUDIT RECORDS FOR GID: {0} >>\n", gID);
            FormatedPrintOut(auditService.HandleShowDupsAndMissesByGeo(gID));
        }

        private void Count()
        {
            Console.WriteLine("\t\t<< AUDIT RECORD TABLE COUNTS: {0} >>\n", auditService.HandleCount());
        }
    }
}
