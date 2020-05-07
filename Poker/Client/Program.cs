using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ContractStorage;
using System.Windows.Forms;

namespace Client
{
    class Program
    {
        public static IContract Proxy;
        [STAThread]
        static void Main()
        {
            Connect("http://localhost:8000/ServiceWCF");

            // Получили экземпляр proxy
            // Можно вызывать методы интерфейса Сервиса у service

            // Завершение
            Console.WriteLine("Press <Any Key> to finish Client");
            Console.ReadKey();

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClientForm(new GameProxy(Proxy)));
        }

        public static void Connect(string address) {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(address);
            var channelFactory = new ChannelFactory<IContract>(binding, endpoint);

            IContract service = channelFactory.CreateChannel();
            Proxy = service;
        }
    }
}
