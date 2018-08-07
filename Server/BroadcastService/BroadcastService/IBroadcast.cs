using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastService
{
    [ServiceContract(CallbackContract = typeof(IBroadcastCallback))]
    public interface IBroadcast
    {
        [OperationContract(IsOneWay = true)]
        void NotifyClient(EventData data);

        [OperationContract(IsOneWay = true)]
        void RegisterClient(string client);

    }
}
