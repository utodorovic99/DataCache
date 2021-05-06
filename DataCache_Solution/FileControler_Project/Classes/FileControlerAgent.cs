using Common_Project.Classes;
using Common_Project.DistributedServices;
using ConnectionControler_Project.Classes;
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
            connectionControler = new ConnectionControler();
        }

        public ConsumptionUpdate OstvConsumptionDBWrite(List<ConsumptionRecord> cRecords)
        {
            try
            {
                return connectionControler.OstvConsumptionDBWrite(cRecords);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
