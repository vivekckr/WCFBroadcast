using BroadcastService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Servicehost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Broadcastservice));

            try
            {
                host.Open();
                GetHostInfo(host);
                Console.ReadLine();
                host.Close();
            }
            catch(Exception e)
            {
                ////
                Console.WriteLine(e);
                host.Abort();
            }
        }

        static void GetHostInfo(ServiceHost host)
        {
            Console.WriteLine("{0}",host.Description.ServiceType);
            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(endpoint.Address);
            }
        }
    }
}
