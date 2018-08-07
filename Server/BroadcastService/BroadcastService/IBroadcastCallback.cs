using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastService
{
    public interface IBroadcastCallback
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToAllClient(EventData eventData);
    }
}
