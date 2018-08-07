using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastService
{
    [DataContract]
    public class EventData
    {
        [DataMember]
        public string ClientName { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
