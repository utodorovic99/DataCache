using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions.ExceptionAbstraction
{
    public class DBLoginFailed:Exception
    {
        private string message;
        private string loginConfigFile;
        private string paramFailed;

        public DBLoginFailed(string message, string loginConfigFile, string paramFailed)
        {
            this.Message = message;
            this.LoginConfigFile = loginConfigFile;
            this.paramFailed = paramFailed;
        }

        public string Message { get => message; set => message = value; }
        public string LoginConfigFile { get => loginConfigFile; set => loginConfigFile = value; }
        public string ParamFailed { get => paramFailed; set => paramFailed = value; }
    }
}
