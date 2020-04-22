using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Combination : ICombination
    {
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
            return CombinationHelper.GetAllCombinationsChecks()
                .Where(combinationCheck => combinationCheck.Check(cards))
                .Select(combinationCheck => combinationCheck.Comb)
                .FirstOrDefault();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    static class CombinationHelper
    {
        public static List<(CardCombination Comb, Func<List<Card>, bool> Check)> GetAllCombinationsChecks()
        {
            return new List<(CardCombination, Func<List<Card>, bool>)>
            {
                ( CardCombination.RoyalFlash, IsRoyalFlash ),
                ( CardCombination.StraightFlush, IsStraightFlush ),
                ( CardCombination.FullHouse, IsFullHouse ),
                ( CardCombination.Straight, IsStraight ),
                ( CardCombination.Flush, IsFlush ),
                ( CardCombination.ThreeOfAKind, IsThreeOfAKind ),
                ( CardCombination.TwoPair, IsTwoPair ),
                ( CardCombination.Pair, IsPair )
            };
        }

        public static bool IsQuadsOrSetOrPair(List<Card> cards, int count)
        {
            return cards
                .GroupBy(card => card.Rank)
                .Any(group => group.Count() == count);
        }

        public static bool IsPair(List<Card> cards) => IsQuadsOrSetOrPair(cards, 2);

        public static bool IsTwoPair(List<Card> cards)
        {
            return cards
                .GroupBy(card => card.Rank)
                .Where(group => group.Count() == 2)
                .Count() >= 2;
        }

        public static bool IsThreeOfAKind(List<Card> cards) => IsQuadsOrSetOrPair(cards, 3);

        public static bool IsStraight(List<Card> cards)
        {
            var uniqueRanks = cards
                .Select(card => card.Rank)
                .Distinct()
                .OrderBy(rank => rank)
                .ToList();
            return uniqueRanks
                .Zip(uniqueRanks.Skip(1), (first, second) => (first, second))
                .Where(bigram => bigram.second - bigram.first == 1)
                .Count()
                .Equals(4);
        }

        public static bool IsFlush(List<Card> cards)
        {
            return cards
                .GroupBy(card => card.Suit)
                .Any(group => group.Count() >= 5);
        }

        public static bool IsFullHouse(List<Card> cards) => IsPair(cards) && IsThreeOfAKind(cards);

        public static bool IsFourOfAKind(List<Card> cards) => IsQuadsOrSetOrPair(cards, 4);

        public static bool IsStraightFlush(List<Card> cards)
        {
            foreach (var group in cards.GroupBy(card => card.Suit).Where(g => g.Count() >= 5))
            {
                if (IsStraight(group.Select(card => card).ToList()))
                    return true;
            }
            return false;
        }


        public static bool IsRoyalFlash(List<Card> cards)
        {
            return cards
                .GroupBy(card => card.Suit)
                .Where(group => group.Count() >= 5)
                .SelectMany(group => group)
                .Select(card => card.Rank)
                .OrderByDescending(rank => rank)
                .Take(5)
                .SequenceEqual(new[] { CardRank.Ace, CardRank.King, CardRank.Queen, CardRank.Jack, CardRank.Ten });
        }
    }
}
