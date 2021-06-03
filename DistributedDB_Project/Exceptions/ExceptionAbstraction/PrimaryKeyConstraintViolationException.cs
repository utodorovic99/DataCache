using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions.ExceptionAbstraction
{
    public class PrimaryKeyConstraintViolationException:Exception
    {
        string failedKey;
        string table;
        string message;

        public PrimaryKeyConstraintViolationException(string failedKey, string table, string message)
        {
            this.failedKey = failedKey;
            this.table = table;
            this.message = message;
        }

        public string FailedKey { get => failedKey; set => failedKey = value; }
        public string Table { get => table; set => table = value; }
        public string Message { get => message; set => message = value; }
    }
}
