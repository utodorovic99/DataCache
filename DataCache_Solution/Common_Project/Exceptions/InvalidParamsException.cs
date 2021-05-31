using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Project.Exceptions
{
    public class InvalidParamsException:Exception
    {
        private string summary;

        public InvalidParamsException(string summary)
        {
            this.summary = summary;
        }

        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
    }
}
