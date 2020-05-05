using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ContractStorage
{
    [ServiceContract]
    public interface IContract
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

        [OperationContract]
        bool StartGame(int gameId, int playerId);

        [OperationContract]
        bool IsGameStarted(int gameId);
    }
}
