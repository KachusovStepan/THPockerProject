using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ServiceModel;

namespace Server
{
    class Program : IDisposable
    {
        private static ServiceHost serviceHost;
        public static Queue<int> GamesToStart = new Queue<int>();
        private static void StartServer(string address) {
            if (address[address.Length - 1] == '/')
                address = address.Substring(0, address.Length - 1);
            var uri = new Uri(string.Format("{0}/Service", address));
            var serviceType = typeof(MyService);
            var contractType = typeof(IContract);
            serviceHost = new ServiceHost(serviceType, uri);
            var binding = new BasicHttpBinding();
            serviceHost.AddServiceEndpoint(contractType, binding, "");
            serviceHost.Open();
            //Console.WriteLine("Press any key to finish");
            //Console.ReadKey();
            //serviceHost.Close();
        }

        static void Main(string[] args)
        {
            Console.Title = "Server";
            StartServer("http://localhost:8000/");
            // Активное ожидание создания игр
            // Чтобы запустить игру в отдельноп птоке
            var Ticks = 50;
            while (true) {
                Ticks--;
                while (GamesToStart.Count != 0) {
                    var gameIdToStart = GamesToStart.Dequeue();

                    var game = TryGetGame(gameIdToStart);
                    WaitCallback task = (state) => game.Start();
                    ThreadPool.QueueUserWorkItem(task);
                }
                Thread.Sleep(3000);
                if (Ticks <= 0)
                    break;
            }
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
            serviceHost.Close();
            serviceHost = null;
        }

        private static Game TryGetGame(int gameId)
        {
            if (!Game.ListGameIds().Contains(gameId))
            {
                return null;
            }

            return Game.GetGameInstance(gameId);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Console.WriteLine("FINALIZATION");
            if (serviceHost != null)
                serviceHost.Close();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
