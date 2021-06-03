using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions.ExceptionAbstraction
{
    public class DBObjectNotFoundException:Exception
    {
        private string message;
        private string missID;
        private string details;
        public string MissID { get => missID; set => missID = value; }
        public string Details { get => details; set => details = value; }
        public string Message1 { get => message; set => message = value; }
    }
}
