using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFClient.ServiceReference1;

namespace WPFClient
{
    public class BroadcastCallback : ServiceReference1.IBroadcastCallback
    {
        private SynchronizationContext syncContext = AsyncOperationManager.SynchronizationContext;
        private EventHandler callBackHandler;

        public void BroadcastToAllClient(EventData eventData)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast), eventData);
        }

        public void SetHandler(EventHandler handler)
        {
            this.callBackHandler = handler;
        }

        private void OnBroadcast(object eventData)
        {
            this.callBackHandler.Invoke(eventData, null);
        }
    }
}
