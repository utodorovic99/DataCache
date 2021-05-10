using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions.ExceptionAbstraction
{
    public class StillAttachedException:Exception
    {
        string targetTable;
        List<string> possibleConnections;
        string message;

        public StillAttachedException(string targetTable, List<string> possibleConnections, string message)
        {
            this.targetTable = targetTable;
            this.possibleConnections = possibleConnections;
            this.message = message;
        }

        public string TargetTable { get => targetTable; set => targetTable = value; }
        public List<string> PossibleConnections { get => possibleConnections; set => possibleConnections = value; }
        public string Message { get => message; set => message = value; }
    }
}
