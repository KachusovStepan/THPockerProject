using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractStorage;

namespace Client
{
    public class GameProxy
    {
        public IContract Proxy { get; private set; }
        public int CurrentGameId { get; private set; }
        public Dictionary<int, int> GamePlayerCountDict;
        public string PlayerName = "Unknown";
        public int PlayerId;
        public List<string> PlayerNames;
        public GameProxy(IContract proxy) {
            Proxy = proxy;
        }

        public bool GetExistingGamesWithPlayerCount() {
            var gamePlayerCountDict = Proxy.ShowExistingGames();
            if (gamePlayerCountDict is null)
                return false;
            GamePlayerCountDict = gamePlayerCountDict;
            return true;
        }

        public bool CreateNewGame(int gameId) {
            var successed = Proxy.CreateNewGame(gameId);
            return successed;
        }

        public void SetName(string name) {
            PlayerName = name;
        }

        public bool SetState(int gameId) {
            var playerNames = Proxy.CheckPlayerNames(gameId);
            if (playerNames is null) {
                return false;
            }
            PlayerNames = playerNames;
            return true;
        }

        public bool TryJoinTheGame(int gameId) {
            var position = -1;
            for (int i = 0; i < 10; i++) {
                if (PlayerNames[i] != null) {
                    position = i;
                    break;
                }       
            }
            if (position == -1)
                return false;

            var playerId = Proxy.Join(gameId, PlayerName, position);
            if (playerId == -1)
                return false;
            PlayerId = playerId;
            return true;
        }
    }
}
