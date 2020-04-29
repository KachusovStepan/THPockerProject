using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Server
{
    [ServiceContract]
    interface IContract
    {
        [OperationContract]
        Dictionary<int, int> ShowExistingGames();

        [OperationContract]
        bool CreateNewGame(int gameId);

        [OperationContract]
        int Join(int gameId, string name, int position);

        [OperationContract]
        bool LeaveTheGame(int gameId, int playerId);

        [OperationContract]
        bool DoRaise(int gameId, int playerId, int bet);

        [OperationContract]
        bool DoCall(int gameId, int playerId);

        [OperationContract]
        bool DoFold(int gameId, int playerId);

        [OperationContract]
        bool DoCheck(int gameId, int playerId);

        [OperationContract]
        bool SendMessage(int gameId, int playerId, string msg);

        [OperationContract]
        string CheckCards(int gameId, int playerId);

        [OperationContract]
        State GetState(int gameId);

        [OperationContract]
        string CheckTableCards(int gameId);

        [OperationContract]
        char CheckRound(int gameId);

        [OperationContract]
        List<string> CheckPlayerNames(int gameId);

        [OperationContract]
        Dictionary<char, int> CheckRoles(int gameId);

        [OperationContract]
        bool[] CheckAlive(int gameId);
    }

    [DataContract(Namespace = "OtherNamespace")]
    public class FastState {
    
    }

    [DataContract(Namespace = "OtherNamespace")]
    public class State
    {
        [DataMember]
        public int PlayerCount;

        [DataMember]
        public Dictionary<int, int> Banks;

        [DataMember]
        public Dictionary<int, int> TableBanks;

        [DataMember]
        public int TableBank;

        [DataMember]
        public int CurrentPlayer;

        public State(int playerCount, Dictionary<int, int> banks, Dictionary<int, int> tableBanks, int tableBank, int currPlayer)
        {
            PlayerCount = playerCount;
            Banks = banks;
            TableBank = tableBank;
            TableBanks = tableBanks;
            CurrentPlayer = currPlayer;
        }
    }


    [DataContract(Namespace = "OtherNamespace")]
    public class Point
    {
        [DataMember]
        public double X;

        [DataMember]
        public double Y;

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    // Service
    class MyService : IContract
    {
        public Dictionary<int, int> ShowExistingGames()
        {
            var games = Game.ShowGames();
            return games;
        }

        public bool CreateNewGame(int gameId)
        {
            var successed = Game.CreateNewGame(gameId);
            if (successed)
                Program.GamesToStart.Enqueue(gameId);
            return successed;
        }

        public int Join(int gameId, string name, int position)
        {
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
            var games = Game.ShowGames();
            if (!games.ContainsKey(gameId))
            {
                return false;
            }

            var successed = Game.GetGameInstance(gameId).AddMessage(playerId, msg);
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

        public bool DoRaise(int gameId, int playerId, int bet)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Bet, bet);
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
