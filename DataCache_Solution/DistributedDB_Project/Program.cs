using DistributedDB_Project.DistributedCallHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project
{
    class Program
    {
        private static readonly MainUIHandler mainUIHandler = new MainUIHandler();
        static void Main(string[] args)
        {
            // Later implement distributed DB call, currently working in local
            mainUIHandler.HandleMainMenu();
        }
    }
}
