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

        static Dictionary<CardSuit, char> suitChar = new Dictionary<CardSuit, char>
        {
            { CardSuit.Hearts, 'h' },
            { CardSuit.Clubs, 'c' },
            { CardSuit.Spades, 's'},
            { CardSuit.Diamonds, 'd' }
        };

        static Dictionary<CardRank, char> rankChar = new Dictionary<CardRank, char>
        {
            { CardRank.Two, '2' },
            { CardRank.Three, '3' },
            { CardRank.Four, '4' },
            { CardRank.Five, '5' },
            { CardRank.Six, '6' },
            { CardRank.Seven, '7' },
            { CardRank.Eight, '8' },
            { CardRank.Nine, '9' },
            { CardRank.Ten, '0' },
            { CardRank.Jack, 'j' },
            { CardRank.Queen, 'q' },
            { CardRank.King, 'k' },
            { CardRank.Ace, 'a'},
        };

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

        public string GetSimpleRepresentation()
        {
            var repr = string.Format("{0}{1}", suitChar[Suit], rankChar[Rank]);
            return repr;
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

        public static List<Card> ParseCards(string stringCards) {
            var splitedCardString = stringCards.Split(' ');
            var cards = new List<Card>();
            foreach (var card in splitedCardString)
            {
                cards.Add(new Card(charSuits[card[0]], charRanks[card[1]]));
            }

            return cards;
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

    public class CombinationDeterminant
    {

        public static Tuple<bool, List<Card>> Straight(List<Card> cards)
        {
            var sorted = cards.OrderBy(x => -(int)x.Rank).ToList();
            bool aceFlag = sorted[0].Rank == CardRank.Ace;
            var seq = new List<Card>();
            seq.Add(sorted[0]);
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                if (seq.Count == 5)
                    break;
                if (sorted[i].Rank == sorted[i + 1].Rank + 1)
                {
                    seq.Add(sorted[i + 1]);
                }
                else if (sorted[i].Rank == sorted[i + 1].Rank)
                {
                    continue;
                }
                else
                {

                    seq.Clear();
                    seq.Add(sorted[i + 1]);
                }
            }
            if (seq[seq.Count - 1].Rank == CardRank.Two && aceFlag)
            {
                seq.Add(sorted[0]);
            }
            if (seq.Count == 5)
                return Tuple.Create(true, seq);
            else
                return new Tuple<bool, List<Card>>(false, null);
        }



        public static Tuple<bool, List<Card>> Flash(List<Card> cards)
        {
            var suits = new Dictionary<CardSuit, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Suit).SelectMany(x => { suits[x.Key] = Tuple.Create(x.Count(), x.OrderBy(c => -(int)c.Rank).ToList()); return x; }).ToList();
            foreach (var key in suits.Keys)
            {
                if (suits[key].Item1 >= 5)
                    return Tuple.Create(true, suits[key].Item2.Take(5).ToList());
            }
            return new Tuple<bool, List<Card>>(false, null);
        }

        public static Tuple<bool, List<Card>> StraightFlash(List<Card> cards)
        {
            var suits = new Dictionary<CardSuit, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Suit).SelectMany(x => { suits[x.Key] = Tuple.Create(x.Count(), x.OrderBy(c => -(int)c.Rank).ToList()); return x; }).ToList();
            foreach (var key in suits.Keys)
            {
                if (suits[key].Item1 >= 5)
                {
                    var res = Straight(suits[key].Item2);
                    if (!res.Item1)
                        break;
                    return Tuple.Create(true, res.Item2);
                }
            }
            return new Tuple<bool, List<Card>>(false, null);
        }

        public static Tuple<bool, List<Card>> RoyalFlash(List<Card> cards)
        {
            var res = StraightFlash(cards);
            if (res.Item1 && res.Item2[4].Rank == CardRank.Ten)
                return res;
            return new Tuple<bool, List<Card>>(false, null);
        }

        public static Tuple<bool, List<Card>> FourOfAKind(List<Card> cards)
        {
            var sorted = cards.OrderBy(x => -(int)x.Rank).ToList();

            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            foreach (var key in ranks.Keys)
            {
                if (ranks[key].Item1 == 4)
                {
                    var res = ranks[key].Item2;
                    foreach (var card in sorted)
                    {
                        if (card.Rank != res[0].Rank)
                        {
                            res.Add(card);
                            break;
                        }
                    }
                    return Tuple.Create(true, res);
                }
            }
            return new Tuple<bool, List<Card>>(false, null);
        }

        public static Tuple<bool, List<Card>> ThreeOfAKind(List<Card> cards)
        {
            var sorted = cards.OrderBy(x => -(int)x.Rank).ToList();

            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            foreach (var key in ranks.Keys)
            {
                if (ranks[key].Item1 == 3)
                {
                    var res = ranks[key].Item2;
                    foreach (var card in sorted)
                    {
                        if (card.Rank != res[0].Rank)
                        {
                            res.Add(card);
                            if (res.Count == 5)
                                break;
                        }
                    }
                    return Tuple.Create(true, res);
                }
            }
            return new Tuple<bool, List<Card>>(false, null);
        }

        public static Tuple<bool, List<Card>> Pair(List<Card> cards)
        {
            var sorted = cards.OrderBy(x => -(int)x.Rank).ToList();
            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            foreach (var key in ranks.Keys)
            {
                if (ranks[key].Item1 == 2)
                {
                    var res = ranks[key].Item2;
                    foreach (var card in sorted)
                    {
                        if (card.Rank != res[0].Rank)
                        {
                            res.Add(card);
                            if (res.Count == 5)
                                break;
                        }
                    }
                    return Tuple.Create(true, res);
                }
            }
            return new Tuple<bool, List<Card>>(false, null);
        }

        public static Tuple<bool, List<Card>> TwoPairs(List<Card> cards)
        {
            var sorted = cards.OrderBy(x => -(int)x.Rank).ToList();

            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            var res = new List<Card>();
            foreach (var group in ranks.Where(x => x.Value.Item1 == 2).OrderBy(x => -(int)x.Key).Take(2))
            {
                res.Add(group.Value.Item2[0]);
                res.Add(group.Value.Item2[1]);
            }
            if (res.Count != 4)
                return new Tuple<bool, List<Card>>(false, null);

            // Нужна только одна карта, поэтому ошибка отбоса пары не возникнет
            foreach (var card in sorted.Where(x => !res.Contains(x)))
            {
                res.Add(card);
                break;
            }

            return Tuple.Create(true, res);
        }

        public static Tuple<bool, List<Card>> FullHouse(List<Card> cards)
        {
            var sorted = cards.OrderBy(x => -(int)x.Rank).ToList();

            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            var res = new List<Card>();
            foreach (var group in ranks.Where(x => x.Value.Item1 == 3).OrderBy(x => -(int)x.Key))
            {
                res.Add(group.Value.Item2[0]);
                res.Add(group.Value.Item2[1]);
                res.Add(group.Value.Item2[1]);
                break;
            }
            if (res.Count != 3)
                return new Tuple<bool, List<Card>>(false, null);

            foreach (var group in ranks.Where(x => x.Value.Item1 == 2).OrderBy(x => -(int)x.Key))
            {
                res.Add(group.Value.Item2[0]);
                res.Add(group.Value.Item2[1]);
                break;
            }
            if (res.Count != 5)
                return new Tuple<bool, List<Card>>(false, null);

            return Tuple.Create(true, res);
        }

        public static Tuple<bool, List<Card>> HighCard(List<Card> cards)
        {
            var res = cards.OrderBy(x => -(int)x.Rank).Take(5).ToList();
            return Tuple.Create(true, res);
        }

        public static int CompareCombinations(List<Card> comb1, List<Card> comb2)
        {
            var res = 0;
            for (var i = 0; i < comb1.Count; i++)
            {
                if (comb1[i] > comb2[i])
                {
                    res = 1;
                    break;
                }
                else if (comb1[i] < comb2[i])
                {
                    res = -1;
                    break;
                }
            }

            return res;
        }
    }
}
