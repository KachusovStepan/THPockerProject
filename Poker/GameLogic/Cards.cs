using System;
using System.Collections.Generic;
using System.Linq;


namespace GameLogic
{
    public enum CardSuit
    {
        NonSet,
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    public enum CardRank
    {
        NonSet = 0,
        Ace = 13,
        Two = 1,
        Three = 2,
        Four = 3,
        Five = 4,
        Six = 5,
        Seven = 6,
        Eight = 7,
        Nine = 8,
        Ten = 9,
        Jack = 10,
        Queen = 11,
        King = 12
    }

    public class Card : ICard
    {
        static Dictionary<char, CardSuit> charSuits = new Dictionary<char, CardSuit>
        {
            { 'h', CardSuit.Hearts },
            { 'c', CardSuit.Clubs },
            { 's', CardSuit.Spades },
            { 'd', CardSuit.Diamonds }
        };

        static Dictionary<char, CardRank> charRanks = new Dictionary<char, CardRank>
        {
            { '2', CardRank.Two },
            { '3', CardRank.Three },
            { '4', CardRank.Four },
            { '5', CardRank.Five },
            { '6', CardRank.Six },
            { '7', CardRank.Seven },
            { '8', CardRank.Eight },
            { '9', CardRank.Nine },
            { '0', CardRank.Ten },
            { 'j', CardRank.Jack },
            { 'q', CardRank.Queen },
            { 'k', CardRank.King },
            { 'a', CardRank.Ace },
        };

        static Dictionary<CardSuit, char> suitChar = 
            charSuits.ToDictionary(pair => pair.Value, pair => pair.Key);

        static Dictionary<CardRank, char> rankChar =
            charRanks.ToDictionary(pair => pair.Value, pair => pair.Key);

        public readonly CardSuit Suit;
        public readonly CardRank Rank;
        public bool IsOpen { get; private set; }
        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public void Open()
        {
            if (IsOpen)
                throw new InvalidOperationException("Card is already opened");
            IsOpen = true;
        }

        public void Close()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Card is already closed");
            IsOpen = false;
        }

        public override int GetHashCode()
        {
            var hash = (int)((int)Rank * 10 + Suit);
            return hash;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Card;
            if (obj is null)
                return false;
            if (this.GetHashCode() != other.GetHashCode())
                return false;
            return true;
        }

        public override string ToString()
        {
            var converSuits = new Dictionary<CardSuit, string>
            {
                { CardSuit.Diamonds, "\u2662"},
                { CardSuit.Hearts, "\u2661"},
                { CardSuit.Clubs, "\u2667" },
                { CardSuit.Spades, "\u2664" },
                { CardSuit.NonSet, "NonSet" }
            };
            return string.Format("({0} {1}", rankChar[Rank].ToString().ToUpper(), converSuits[Suit]);
        }

        public string GetSimpleRepresentation()
        {
            var repr = string.Format("{0}{1}", suitChar[Suit], rankChar[Rank]);
            return repr;
        }

        public static bool operator ==(Card card1, Card card2)
        {
            if (card1 is null)
                return card2 is null;
            if (card2 is null)
                return card1 is null;
                
            return card1.Suit == card2.Suit && card1.Rank == card2.Rank;
        }

        public static bool operator !=(Card card1, Card card2)
        {
            if (card1 is null)
                return !(card2 is null);
            if (card2 is null)
                return !(card1 is null);

            return !(card1 == card2);
        }

        public static bool operator <(Card card1, Card card2)
        {
            return card1.Rank < card2.Rank;
        }

        public static bool operator >(Card card1, Card card2)
        {
            return card1.Rank > card2.Rank;
        }

        public static bool operator <=(Card card1, Card card2)
        {
            return !(card1 > card2);
        }

        public static bool operator >=(Card card1, Card card2)
        {
            return !(card1 < card2);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            var otherCard = obj as Card;
            if (otherCard == null)
                throw new ArgumentException("Object in not a Card");
            var compRes = 0;
            if (this.Rank < otherCard.Rank)
                compRes = -1;
            else if (this.Rank > otherCard.Rank)
                compRes = 1;
            return compRes;
        }
    }

    public class CardDeck : ICardDeck
    {
        private readonly Card[] cards = new Card[52];
        public bool InGame { get; private set; }
        private int pointer;
        public CardDeck()
        {
            FillDeck();
            InGame = false;
            pointer = 0;
        }

        public void Shuffle()
        {
            var random = new Random();
            for (var i = cards.Length - 1; i > 0; i--)
            {
                int n = random.Next(i + 1);
                var tempCard = cards[i];
                cards[i] = cards[n];
                cards[n] = tempCard;
            }
        }

        public void SetReady()
        {
            if (InGame)
            {
                throw new InvalidOperationException("Deck is already in use");
            }

            InGame = true;
        }

        public void FillDeck()
        {
            if (InGame)
            {
                throw new InvalidOperationException("Deck is already in use");
            }

            for (int s = 1; s < 5; s++)
            {
                cards[(s - 1) * 13] = new Card((CardSuit)s, CardRank.Ace);
                for (int r = 1; r < 13; r++)
                    cards[(s - 1) * 13 + r] = new Card((CardSuit)s, (CardRank)r);
            }
        }

        public void Print()
        {
            foreach (var card in cards)
            {
                Console.WriteLine(card);
            }
        }

        public Card GetCard() {
            var card = cards[pointer];
            pointer++;
            return card;
        }
    }


    public class Hand
    {
        public (Card first, Card second) Cards { get; }

        public Hand(CardDeck deck)
        {
            Cards = (deck.GetCard(), deck.GetCard());
        }
    }
}
