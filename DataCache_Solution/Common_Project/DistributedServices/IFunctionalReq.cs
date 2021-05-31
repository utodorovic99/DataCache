using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UI_Project.ClientServices
{
    [ServiceContract]
    public interface IFunctionalReq
    {
        [OperationContract]
        bool Echo();
    }
}
