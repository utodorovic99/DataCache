using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Project.ClientServices;

namespace DistributedDB_Project.Services
{
    public class FunctionalService : IFunctionalReq
    {
        public bool Echo()
        {
            return true;
        }
    }
}
