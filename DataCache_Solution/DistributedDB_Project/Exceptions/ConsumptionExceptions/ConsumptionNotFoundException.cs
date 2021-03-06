using DistributedDB_Project.Exceptions.ExceptionAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions
{
    [Serializable]
    public class ConsumptionNotFoundException:DBObjectNotFoundException
    {
        private string message;
        private string missID;
        private string details;

        public ConsumptionNotFoundException(string message, string missID, string details)
        {
            this.message = message;
            this.MissID = missID;
            this.details = details;
        }

        public string MissID { get => missID; set => missID = value; }
        public string Details { get => details; set => details = value; }
    }
}
