using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDB_Project.Exceptions.ExceptionAbstraction
{
    class ForbiddenOrderException:Exception
    {
        string message;

        public ForbiddenOrderException(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

    }
}
