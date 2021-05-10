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
                Console.WriteLine("1 - Show all");
                Console.WriteLine("2 - Show duplicates");
                Console.WriteLine("3 - Show duplicates by geo");
                Console.WriteLine("4 - Show misses");
                Console.WriteLine("5 - Show misses by geo");
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
                        ShowDuplicatesAll();
                        break;
                    case "3":
                        ShowDuplicatesAllByGeo();
                        break;
                    case "4":
                        ShowMissesAll();
                        break;
                    case "5":
                        ShowMissesAllByGeo();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void ShowAll()
        {

        }
        private void ShowDuplicatesAll()
        {
        
        }
        private void ShowDuplicatesAllByGeo()
        {
        
        }
        private void ShowMissesAll()
        { 
        
        }
        private void ShowMissesAllByGeo()
        { 
        
        }
    }
}
