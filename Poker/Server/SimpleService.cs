using System;
using System.Collections.Generic;
using GameLogic;
using ContractStorage;
using System.ServiceModel;

namespace Server
{
    // Service
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    class MyService : IContract
    {
        public bool IsGameStarted(int gameId) {
            var game = TryGetGame(gameId);
            if (game is null)
                return false;
            return game.Started;
        }
        public bool StartGame(int gameId, int playerId) {
            var game = TryGetGame(gameId);
            if (game is null)
                return false;
            if (!game.PlayerByID.ContainsKey(playerId))
                return false;
            if (game.Started)
                return false;
            Game.GamesToStart.Enqueue(gameId);
            Console.WriteLine("Added Game to Queue to Start");
            return true;
        }
        public Dictionary<int, int> ShowExistingGames()
        {
            var games = Game.ShowGames();
            return games;
        }

        public bool CreateNewGame(int gameId)
        {
            var successed = Game.CreateNewGame(gameId);
            if (successed)
            {
                Console.WriteLine("Added Game to Queue to Start");
                //Program.GamesToStart.Enqueue(gameId);
            }
            return successed;
        }

        public int Join(int gameId, string name, int position)
        {
            Console.WriteLine("Join");
            var games = Game.ShowGames();
            if (!games.ContainsKey(gameId))
            {
                return -1;
            }

            var playerId = Game.GetGameInstance(gameId).AddPlayer(name, position);

            return playerId;
        }

        public bool LeaveTheGame(int gameId, int playerId)
        {
            Console.WriteLine("Leave");
            var games = Game.ShowGames();
            if (!games.ContainsKey(gameId))
            {
                return false;
            }
            var successed = Game.GetGameInstance(gameId).RemovePlayer(playerId);
            return successed;
        }

        public bool SendMessage(int gameId, int playerId, string msg)
        {
            Console.WriteLine("Try Show Games from SendMeessage");
            var games = Game.ShowGames();
            if (games is null)
                Console.WriteLine("Games is NULL");
            foreach (var key in games.Keys ) {
                Console.WriteLine("{0} - {0}", key, games[key]);
            }
            Console.WriteLine("Got Games len of {0}", games.Count);
            if (!games.ContainsKey(gameId))
            {
                Console.WriteLine("Fail send {2} to game {0}|  player {1}", gameId, playerId, msg);
                return false;
            }
            Console.WriteLine("Game is about to send {2} to game {0}|  player {1}", gameId, playerId, msg);
            var successed = Game.GetGameInstance(gameId).AddMessage(playerId, msg);
            if (successed)
                Console.WriteLine("Game sended {2} to game {0}|  player {1} OK", gameId, playerId, msg);
            else
                Console.WriteLine("Failed");
            return successed;
        }

        public string CheckCards(int gameId, int playerId)
        {
            var games = Game.ShowGames();
            if (!games.ContainsKey(gameId) || Game.GetGameInstance(gameId).PlayerByID.ContainsKey(playerId))
            {
                return string.Empty;
            }

            var playerHand = Game.GetGameInstance(gameId).PlayerByID[playerId].Hand;
            var stringHand = string.Format("{0} {1}", playerHand.Item1.GetSimpleRepresentation(), playerHand.Item2.GetSimpleRepresentation());
            return stringHand;
        }

        public State GetState(int gameId)
        {
            var games = Game.ShowGames();
            if (!games.ContainsKey(gameId))
            {
                return null;
            }

            var gameState = Game.GetGameInstance(gameId).GetGameState();
            return gameState;
        }

        public bool DoBet(int gameId, int playerId, int bet)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Bet, bet);
            return successed;
        }

        public bool DoRaise(int gameId, int playerId, int bet)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Raise, bet);
            return successed;
        }

        public bool DoCall(int gameId, int playerId)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Call, 0);
            return successed;
        }

        public bool DoFold(int gameId, int playerId)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Fold, 0);
            return successed;
        }

        public bool DoCheck(int gameId, int playerId)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Check, 0);
            return successed;
        }

        private bool DoPlayerBet(int gameId, int playerId, Bet bet, int value)
        {
            var game = TryGetGame(gameId);
            if (game is null)
                return false;

            var playerInfo = game.PlayerByID[playerId];
            if (game.CurrentPlayer != playerInfo.Position)
            {
                return false;
            }

            if (game.BetHasBeenMade)
            {
                return false;
            }

            var playerBet = new BetNode(playerId, playerInfo.Position, bet, value);
            game.RoundHistory.Add(playerBet);
            game.PlayerBets[playerInfo.Position] = playerBet;
            game.BetHasBeenMade = true;
            return true;
        }

        public string CheckTableCards(int gameId)
        {
            var game = TryGetGame(gameId);
            if (game is null)
                return null;

            var tableCards = Game.GetGameInstance(gameId).TableCards;
            var res = String.Empty;
            foreach (var card in tableCards) {
                res += String.Format("{0} ", card.GetSimpleRepresentation());
            }
            res = res.Substring(0, res.Length - 1);
            return res;
        }

        public char CheckRound(int gameId)
        {
            var game = TryGetGame(gameId);
            if (game is null)
                return char.MinValue;
            return game.GetRound();
        }

        public List<string> CheckPlayerNames(int gameId)
        {
            var game = TryGetGame(gameId);
            if (game is null)
                return null;

            return game.ListPlayerNames();
        }

        public Dictionary<char, int> CheckRoles(int gameId)
        {
            var game = TryGetGame(gameId);
            if (game is null)
                return null;

            var blindSeats = new Dictionary<char, int> { { 'd', game.DealerSeat }, { 's', game.SmallBlindSeat }, { 'b', game.BigBlindSeat } };
            return blindSeats;
        }

        public bool[] CheckAlive(int gameId)
        {
            var game = TryGetGame(gameId);
            if (game is null)
                return null;

            return game.Ready;
        }

        private Game TryGetGame(int gameId)
        {
            if (!Game.ListGameIds().Contains(gameId))
            {
                return null;
            }

            return Game.GetGameInstance(gameId);
        }
    }
}
