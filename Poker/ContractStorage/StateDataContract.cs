using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ContractStorage
{
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

        [DataMember]
        public List<string> Chat;

        [DataMember]
        public Dictionary<char, int> Blinds;

        [DataMember]
        public char Round;

        [DataMember]
        public string Cards;

        [DataMember]
        public bool[] Ready;

        public State() {
            PlayerCount = 0;
            Banks = new Dictionary<int, int>();
            TableBank = 0;
            TableBanks = new Dictionary<int, int>();
            CurrentPlayer = -1;
            Chat = new List<string>();
            Blinds = new Dictionary<char, int>();
            Round = 'n';
        }

        public State(int playerCount, Dictionary<int, int> banks, Dictionary<int, int> tableBanks, int tableBank, int currPlayer, List<string> chat, Dictionary<char, int> blinds, char round, string cards, bool[] ready)
        {
            PlayerCount = playerCount;
            Banks = banks;
            TableBank = tableBank;
            TableBanks = tableBanks;
            CurrentPlayer = currPlayer;
            Chat = chat;
            Blinds = blinds;
            Round = round;
            Cards = cards;
            Ready = ready;
        }
    }
}
