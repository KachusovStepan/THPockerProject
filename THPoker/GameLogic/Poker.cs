using System;

namespace GameLogic
{
    public enum Bet
    {
        None,
        Call,
        Bet,
        Raise,
        Fold,
        Check,
        AllIn
    }

    public enum TurnRole
    {
        None,
        SmallBlind,
        BigBlind,
        Dealer
    }

    public enum GameState
    {
        NotStart,
        PreFlop,
        Flop,
        Tern,
        River,
        ShowTime
    }

    public enum PlayerState
    {
        None,
        Waiting,
        InGame
    }
    class Poker
    {
    }
}
