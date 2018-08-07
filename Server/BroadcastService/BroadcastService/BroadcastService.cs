using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Broadcastservice : IBroadcast
    {
        private static Dictionary<string, IBroadcastCallback> clients = new Dictionary<string, IBroadcastCallback>();
        private static object locker = new object();

        public void NotifyClient(EventData data)
        {
            lock (locker)
            {
                var inactiveClients = new List<string>();
                foreach (var client in clients)
                {
                    if (client.Key != data.ClientName)
                    {
                        try
                        {
                            client.Value.BroadcastToAllClient(data);
                        }
                        catch (Exception ex)
                        {
                            inactiveClients.Add(client.Key);
                        }
                    }
                }

                if (inactiveClients.Count > 0)
                {
                    foreach (var client in inactiveClients)
                    {
                        clients.Remove(client);
                    }
                }
            }
        }

        public void RegisterClient(string client)
        {
            if (client != null && client != "")
            {
                try
                {
                    IBroadcastCallback callback = OperationContext.Current.GetCallbackChannel<IBroadcastCallback>();
                    lock (locker)
                    {
                        if (clients.Keys.Contains(client))
                            clients.Remove(client);

                        clients.Add(client, callback);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
