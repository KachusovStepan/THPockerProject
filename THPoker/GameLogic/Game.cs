using System;
using System.Collections.Generic;

namespace GameLogic
{
    class Game
    {
        public GameState State { get; private set; }
        private List<PlayerInfo> PlayerInfos = new List<PlayerInfo>();
        private bool[] seats = new bool[10];
        public void AddPlayer(string name, int seatNumber, int chipBank)
        {
            if (seats[seatNumber])
                throw new InvalidOperationException("This position is already in use");
            if (name == null || name == "")
                throw new InvalidOperationException("Player name is not specified");
            seats[seatNumber] = true;
            var newPlayerInfo = new PlayerInfo(name, seatNumber, chipBank);
            PlayerInfos.Add(newPlayerInfo);
        }

        public void RemovePlayer(PlayerInfo playerInfo)
        {
            // Что если игрок уйдет во время раунда?
            var seatNumber = playerInfo.Position;
            seats[seatNumber] = false;
            PlayerInfos.Remove(playerInfo);
        }
    }
}
