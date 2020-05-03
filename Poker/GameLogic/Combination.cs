using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Combination : IComparable
    {
        public CardCombination CardCombination { get; private set; }
        public List<Card> HandRank { get; private set; }
        public List<Card> Kickers { get; private set; }
        public List<Card> FullCombination { get => HandRank.Concat(Kickers).ToList(); }

        public Combination(Tuple<Card, Card> hand, List<Card> tableCards)
            : this(new[] { hand.Item1, hand.Item2 }.Concat(tableCards).ToList()) { }

        public Combination(List<Card> cards)
        {
            var checks = new List<Func<List<Card>, bool>>
            {
                IsRoyalFlash,
                IsStraightFlash,
                IsFourOfAKind,
                IsFullHouse,
                IsFlash,
                IsStraight,
                IsThreeOfAKind,
                IsTwoPair,
                IsPair,
                IsHighCard
            };

            foreach (var check in checks)
            {
                if (check(cards))
                    break;
            }
        }

        private bool IsHighCard(List<Card> cards)
        {
            var res = cards.OrderByDescending(x => x.Rank).Take(5).ToList();
            return UpdateCombination(CardCombination.HighCard, res, 5);
        }

        private bool IsPair(List<Card> cards) => IsSimpleNumeralCombination(cards, 2);

        private bool IsTwoPair(List<Card> cards)
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
                return false;

            res.Add(sorted.Where(x => !res.Contains(x)).FirstOrDefault());

            return UpdateCombination(CardCombination.TwoPair, res, 4);
        }

        private bool IsThreeOfAKind(List<Card> cards) => IsSimpleNumeralCombination(cards, 3);

        private bool IsStraight(List<Card> cards)
        {
            var sorted = cards.OrderByDescending(x => x.Rank).ToList();
            bool aceFlag = sorted[0].Rank == CardRank.Ace;
            var res = new List<Card>();
            res.Add(sorted[0]);
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                if (res.Count == 5)
                    break;
                if (sorted[i].Rank == sorted[i + 1].Rank + 1)
                {
                    res.Add(sorted[i + 1]);
                }
                else if (sorted[i].Rank == sorted[i + 1].Rank)
                {
                    continue;
                }
                else
                {
                    res.Clear();
                    res.Add(sorted[i + 1]);
                }
            }
            if (res[res.Count - 1].Rank == CardRank.Two && aceFlag)
            {
                res.Add(sorted[0]);
            }
            if (res.Count == 5)
                return UpdateCombination(CardCombination.Straight, res, 5);
            return false;
        }

        private bool IsFlash(List<Card> cards)
        {
            var res = cards
                .GroupBy(card => card.Suit)
                .Where(group => group.Count() >= 5)
                .SelectMany(group => group)
                .OrderByDescending(card => card.Rank)
                .Take(5)
                .ToList();

            if (res.Count == 5)
                return UpdateCombination(CardCombination.Flash, res, 5);
            return false;
        }

        private bool IsFullHouse(List<Card> cards)
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
                return false;

            foreach (var group in ranks.Where(x => x.Value.Item1 == 2).OrderBy(x => -(int)x.Key))
            {
                res.Add(group.Value.Item2[0]);
                res.Add(group.Value.Item2[1]);
                break;
            }
            if (res.Count != 5)
                return false;

            return UpdateCombination(CardCombination.FullHouse, res, 5);
        }

        private bool IsFourOfAKind(List<Card> cards) => IsSimpleNumeralCombination(cards, 4);

        private bool IsStraightFlash(List<Card> cards)
        {
            var possibleFlash = cards
                .GroupBy(card => card.Suit)
                .Where(g => g.Count() >= 5)
                .SelectMany(group => group)
                .ToList();
            if (possibleFlash.Count > 0 && IsStraight(possibleFlash))
            {
                CardCombination = CardCombination.StraightFlash;
                return true;
            }
            return false;
        }

        private bool IsRoyalFlash(List<Card> cards)
        {
            if (IsStraightFlash(cards) && HandRank[0].Rank == CardRank.Ace)
            {
                CardCombination = CardCombination.RoyalFlash;
                return true;
            }
            return false;
        }        

        private bool UpdateCombination(CardCombination cardCombination,
            List<Card> cards, int handRankCardsCount)
        {
            CardCombination = cardCombination;
            HandRank = cards.Take(handRankCardsCount).ToList();
            Kickers = cards.Skip(handRankCardsCount).ToList();
            return true;
        }

        private bool IsSimpleNumeralCombination(List<Card> cards, int number)
        {
            if (number < 2 || number > 4)
                throw new ArgumentException();

            var sorted = cards.OrderByDescending(x => x.Rank).ToList();
            var ranks = new Dictionary<CardRank, Tuple<int, List<Card>>>();
            cards.GroupBy(x => x.Rank).SelectMany(x => { ranks[x.Key] = Tuple.Create(x.Count(), x.ToList()); return x; }).ToList();
            foreach (var key in ranks.Keys)
            {
                if (ranks[key].Item1 == number)
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

                    CardCombination = number == 2
                        ? CardCombination.Pair
                        : number == 3
                            ? CardCombination.ThreeOfAKind
                            : CardCombination.FourOfAKind;
                    HandRank = res.Take(number).ToList();
                    Kickers = res.Skip(number).ToList();
                    return true;
                }
            }
            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentException();

            var otherCombination = obj as Combination;
            if (otherCombination == null)
                throw new ArgumentException();

            Func<List<Card>, List<Card>, int> listComparer = (list1, list2) =>
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    var cmp = list1[i].CompareTo(list2[i]);
                    if (cmp != 0)
                        return cmp;
                }
                return 0;
            };
            var compareResult = CardCombination.CompareTo(otherCombination.CardCombination);
            if (compareResult == 0)
                compareResult = listComparer(HandRank, otherCombination.HandRank);
            if (compareResult == 0)
                compareResult = listComparer(Kickers, otherCombination.Kickers);
            return compareResult;
        }
    }
}
