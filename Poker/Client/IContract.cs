using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Client
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
    public class FastState
    {

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
}
