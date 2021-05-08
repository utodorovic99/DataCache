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
                Console.WriteLine("1 - Show all");
                Console.WriteLine("2 - Show by GID");
                Console.WriteLine();
                Console.WriteLine("3 - Add new geography - Single");
                Console.WriteLine("4 - Add new geography - Multiple");
                Console.WriteLine();
                Console.WriteLine("X - Exit consumption menu");

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
                        AddNewGeographySingle();
                        break;
                    case "4":
                        AddNewGeographyMultiple();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }

        private void ShowAll()
        {
            
        }

        private void ShowByGID()
        {

        }

        private void AddNewGeographySingle()
        {

        }

        private void AddNewGeographyMultiple()
        {

        }


    }
}
