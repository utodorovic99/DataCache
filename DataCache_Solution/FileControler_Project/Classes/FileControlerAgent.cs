using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
using ConnectionControler_Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileControler_Project.Classes
{
    public class FileControlerAgent : IFileReq
    {
        private ConnectionControler connectionControler;

        public FileControlerAgent()
        {
            connectionControler = ConnectionControler.Instance;
        }

        public ConsumptionUpdate OstvConsumptionDBWrite(List<ConsumptionRecord> cRecords)
        {
            return connectionControler.OstvConsumptionDBWrite(cRecords);
        }

        public bool TryReconnect() { return connectionControler.TryReconnect(); }

    }
}
