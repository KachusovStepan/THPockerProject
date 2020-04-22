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
            return string.Format("({0} {1} {2})", Suit, Rank, IsOpen ? "Opened" : "Closed");
        }

        public static bool operator ==(Card card1, Card card2)
        {
            return card1.Suit == card2.Suit && card1.Rank == card2.Rank;
        }

        public static bool operator !=(Card card1, Card card2)
        {
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
        public CardDeck()
        {
            FillDeck();
            InGame = false;
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
    }


    public class Hand
    {
        private Tuple<Card, Card> cards = null;
        public void GetCards(Tuple<Card, Card> cards)
        {
            if (this.cards != null)
            {
                throw new InvalidOperationException("Hand already has cards");
            }

            this.cards = cards;
        }
    }

    public class Combination : ICombination {
        private readonly List<Card> cards;
        public CardCombination combination { get; private set; }
        public Combination(List<Card> cards)
        {
            this.cards = cards.OrderBy(x => x.Rank).ToList();
        }

        // need tests
        // use delegates in collection to determine combinations
        private CardCombination DetermineCardCombination(List<Card> cards)
        {
            var suits = new Dictionary<CardSuit, Tuple<int, List<Card>>>();
            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Suit).SelectMany(x => { suits[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            var possibleCombinations = new HashSet<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            var suitCount = suits.Count;
            var rankCount = ranks.Count;

            if (suitCount == 1)
            {
                // S, SF, RF
                if (IsStraight(cards))
                {
                    if (cards[0].Rank == CardRank.Ten)
                        return CardCombination.RoyalFlash;
                    return CardCombination.StraightFlush;
                }

            }

            if (rankCount == 2)
            {
                // FH, FOAK
                foreach (var r in ranks.Keys)
                {
                    if (ranks[r].Item2.Count == 3)
                        return CardCombination.FullHouse;
                }
                return CardCombination.FourOfAKind;
            }

            if (rankCount == 3)
            {
                // TP, TOAK
                foreach (var r in ranks.Keys)
                {
                    if (ranks[r].Item2.Count == 3)
                        return CardCombination.ThreeOfAKind;
                }
                return CardCombination.TwoPair;
            }

            if (rankCount == 4)
            {
                // P
                return CardCombination.Pair;
            }

            if (rankCount == 5)
            {
                // HK (others have been checked)
                return CardCombination.HighCard;
            }

            return CardCombination.None;
        }

        public static bool IsStraight(List<Card> cards)
        {
            var count = cards.Count;
            if (cards[0].Rank == CardRank.Two && cards[cards.Count - 1].Rank == CardRank.Ace)
            {
                count -= 1;
            }

            for (int i = 0; i < count - 1; i++)
            {
                if (cards[i].Rank != cards[i + 1].Rank - 1)
                {
                    return false;
                }
            }

            return true;
        }

        public int CompareTo(object obj) {
            throw new NotImplementedException();
        }
    }
}
