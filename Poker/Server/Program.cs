using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        private static void StartServer(string address) {
            if (address[address.Length - 1] == '/')
                address = address.Substring(0, address.Length - 1);
            var uri = new Uri(string.Format("{0}/Service", address));
            var serviceType = typeof(IContract);
            var serviceHost = new ServiceHost(serviceType, uri);
            var binding = new BasicHttpBinding();
            serviceHost.AddServiceEndpoint(serviceType, binding, address);
            serviceHost.Open();
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
            serviceHost.Close();
        }

        static void Main(string[] args)
        {
            Console.Title = "Server";
            StartServer("http://localhost:8000/");
        }
    }
}
