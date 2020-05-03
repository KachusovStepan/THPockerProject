using System;
using System.Collections.Generic;

namespace GameLogic
{
    public interface ICard : IComparable
    {
        bool IsOpen { get; }
        void Open();
        void Close();
    }

    public interface ICardDeck
    {
        bool InGame { get; }
        void Shuffle();
        void FillDeck();
        void SetReady();
    }
}
