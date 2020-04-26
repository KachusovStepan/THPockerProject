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
        bool DoBet(int gameId, int playerId, int bet);

        [OperationContract]
        bool DoCall(int gameId, int playerId);

        [OperationContract]
        bool DoRaise(int gameId, int playerId);

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
    }


    [DataContract(Namespace = "OtherNamespace")]
    public class State
    {
        [DataMember]
        public int PlayerCount;

        [DataMember]
        public Dictionary<int, int> Banks;

        [DataMember]
        public int TableBank;

        [DataMember]
        public Dictionary<int, string> Seats;

        [DataMember]
        public Dictionary<char, int> Roles;

        [DataMember]
        public char Round;

        [DataMember]
        public int CurrentPlayer;

        public State(int playerCount, Dictionary<int, int> banks, int tableBank, Dictionary<int, string> seatNames, Dictionary<char, int> blindSeats, char currRound, int currPlayer)
        {
            PlayerCount = playerCount;
            Banks = banks;
            TableBank = tableBank;
            Seats = seatNames;
            Roles = blindSeats;
            Round = currRound;
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

        public bool DoBet(int gameId, int playerId, int bet)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Bet, bet);
            return successed;
        }

        public bool DoCall(int gameId, int playerId)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Call, 0);
            return successed;
        }

        public bool DoRaise(int gameId, int playerId)
        {
            var successed = DoPlayerBet(gameId, playerId, Bet.Raise, 0);
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
            var games = Game.ShowGames();
            if (!games.ContainsKey(gameId) || Game.GetGameInstance(gameId).PlayerByID.ContainsKey(playerId))
            {
                return false;
            }

            var game = Game.GetGameInstance(gameId);

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
            game.BetHasBeenMade = true;
            return true;
        }
    }
}
