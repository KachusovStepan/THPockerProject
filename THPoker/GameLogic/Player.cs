using System;

namespace GameLogic
{
    public class PlayerInfo
    {
        public int ChipBank;
        public readonly string Name;
        public readonly int Position;
        public TurnRole Role { get; set; }
        public Tuple<Card, Card> Hand;
        public PlayerState State;
        public PlayerInfo(string name, int position, int chipBank)
        {
            Name = name;
            Position = position;
            ChipBank = chipBank;
            State = PlayerState.Waiting;
            Hand = null;
        }
    }

    class Player
    {
    }
}
