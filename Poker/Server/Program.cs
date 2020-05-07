using System;
using System.Collections.Generic;
using System.Threading;
using System.ServiceModel;
using ContractStorage;

namespace Server
{
    class Program : IDisposable
    {
        private static ServiceHost serviceHost;
        //public static Queue<int> GamesToStart = new Queue<int>();
        private static void StartServer(string address) {
            if (address[address.Length - 1] == '/')
                address = address.Substring(0, address.Length - 1);
            var uri = new Uri(string.Format("{0}/ServiceWCF", address));
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

        // Не работает
        static void Main1(string[] args)
        {
            Console.Title = "Server";
            StartServer("http://localhost:8000/");
            // Активное ожидание создания игр
            // Чтобы запустить игру в отдельноп птоке
            var Ticks = 50;
            while (true) {
                Ticks--;
                while (Game.GamesToStart.Count != 0) {
                    var gameIdToStart = Game.GamesToStart.Dequeue();

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

        static void MainA(string[] args) {

            StartServer("http://localhost:8000/");
            Console.WriteLine("< Server is Running >");

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
            serviceHost.Close();
            Console.WriteLine("Server successfully Finishd");
        }

        static void Main(string[] args) {
            Console.Title = "Server";
            StartServer("http://localhost:8000/");
            Console.WriteLine("< Server is Running >");
            var limit = 500;
            var gamesRunningActions = new List<Action>();
            var startedGames = new List<IAsyncResult>();
            while (true)
            {
                Console.WriteLine("Checking to start game");
                limit--;
                if (limit <= 0)
                    break;
                while (Game.GamesToStart.Count != 0)
                {
                    var gameIdToStart = Game.GamesToStart.Dequeue();

                    var game = TryGetGame(gameIdToStart);
                    if (game is null)
                        continue;
                    var gameToRun = new Action(game.Start);
                    gamesRunningActions.Add(gameToRun);
                    startedGames.Add(gameToRun.BeginInvoke(null, null));
                    for (int i = 0; i < startedGames.Count; i++) {
                        if (startedGames[i].IsCompleted) {
                            gamesRunningActions[i].EndInvoke(startedGames[i]);
                            gamesRunningActions.RemoveAt(i);
                            startedGames.RemoveAt(i);
                            Console.WriteLine("Game {0} -- Started", gameIdToStart);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
            Console.WriteLine("STOPPING");
        }

        // Не работает
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
