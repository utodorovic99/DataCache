using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionControler_Project.Exceptions
{
    public class DBOfflineException:Exception
    {
        private string custMessage;

        public DBOfflineException(string message)
        {
            if (message == null) custMessage = "";
            else                 this.custMessage = message;

        }

        public string Message { get => custMessage; set => custMessage = value; }
    }
}
