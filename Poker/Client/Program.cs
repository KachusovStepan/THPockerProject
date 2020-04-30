using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ContractStorage;

namespace Client
{
    class Program
    {
        static void Main()
        {
            Console.Title = "CLIENT";
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8000/ServiceWCF");
            var channelFactory = new ChannelFactory<IContract>(binding, endpoint);

            IContract service = channelFactory.CreateChannel();

            // Получили экземпляр proxy
            // Можно вызывать методы интерфейса Сервиса у service

            // Завершение
            Console.WriteLine("Press <Any Key> to finish Client");
            Console.ReadKey();
        }
    }
}
