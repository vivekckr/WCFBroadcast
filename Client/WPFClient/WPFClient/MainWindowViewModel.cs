using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string message;
        private string broadcastMessage;
        private ServiceReference1.BroadcastClient client;
        private delegate void HandleBroadcastCallback(object sender, EventArgs e);

        public event PropertyChangedEventHandler PropertyChanged;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged();
            }
        }

        public string BroadcastMessage
        {
            get
            {
                return broadcastMessage;
            }
            set
            {
                broadcastMessage = value;
                NotifyPropertyChanged();
            }
        }

        private string clientID;

        public string ClientID
        {
            get
            {
                return clientID;
            }
            set
            {
                clientID = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OnBroadcastmessage
        {
            get
            {
                return new Command(this.onButtonClick, true);
            }
        }

        

        public MainWindowViewModel()
        {
            // find any other way to create unique client display id
            this.ClientID = "Client " + Guid.NewGuid().ToString();
            this.RegisterClient();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                var eventData = (ServiceReference1.EventData)sender;
                if (this.BroadcastMessage != "")
                    this.BroadcastMessage += "\r\n";

                this.BroadcastMessage += string.Format("{0} (from {1})", eventData.Message, eventData.ClientName);
            }
            catch (Exception ex)
            {
            }
        }

        private void RegisterClient()
        {
            if ((this.client != null))
            {
                this.client.Abort();
                this.client = null;
            }

            BroadcastCallback broadcastCallback = new BroadcastCallback();
            broadcastCallback.SetHandler(this.HandleBroadcast);

            InstanceContext context = new InstanceContext(broadcastCallback);
            this.client = new ServiceReference1.BroadcastClient(context);
            this.client.RegisterClient(this.clientID);
        }

        
        private void onButtonClick()
        {
            if (this.client == null)
            {
                this.BroadcastMessage = "Client is not registered";
                return;
            }

            if (string.IsNullOrEmpty(this.Message))
            {
                this.BroadcastMessage = "Cannot broadcast an empty message";
                return;
            }

            this.client.NotifyClient(new ServiceReference1.EventData()
            {
                ClientName = this.clientID,
                Message = this.message
            });
        }
    }
}
