using DistributedDB_Project.Exceptions.ExceptionAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions.AuditExceptions
{
    public class AuditNotFoundException:DBObjectNotFoundException
    {
        string gID;
        string timeStamp;
        string message;

        public AuditNotFoundException(string gID, string timeStamp, string message)
        {
            this.gID = gID;
            this.timeStamp = timeStamp;
            this.message = message;
        }

        public string GID { get => gID; set => gID = value; }
        public string TimeStamp { get => timeStamp; set => timeStamp = value; }
        public string Message2 { get => message; set => message = value; }
    }
}
