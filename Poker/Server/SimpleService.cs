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
        int Join();

        [OperationContract]
        bool LeaveTheGame(int id);

        [OperationContract]
        bool Bet(int id, int bet);

        [OperationContract]
        bool Call(int id);

        [OperationContract]
        bool Raise(int id);

        [OperationContract]
        bool Fold(int id);

        [OperationContract]
        bool Check(int id);

        [OperationContract]
        bool SendMessage(int id, string msg);

        [OperationContract]
        void CheckCards(int id);

        [OperationContract]
        State GetState(int id);
    }


    [DataContract(Namespace = "OtherNamespace")]
    public class State
    {
        [DataMember]
        public int PlayerCount;

        [DataMember]
        public List<int> Banks;

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
    }

    class SimpleService : IContract
    {
        public bool SendMessage(string msg, int address) {
            Console.WriteLine("Player {0} doing {1}", address, msg);
            if (msg == "")
                return false;
            return true;
        }
    }
}
