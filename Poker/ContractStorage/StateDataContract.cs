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

        public State(int playerCount, Dictionary<int, int> banks, Dictionary<int, int> tableBanks, int tableBank, int currPlayer, List<string> chat)
        {
            PlayerCount = playerCount;
            Banks = banks;
            TableBank = tableBank;
            TableBanks = tableBanks;
            CurrentPlayer = currPlayer;
            Chat = chat;
        }
    }
}
