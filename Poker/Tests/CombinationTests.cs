using GameLogic;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    class CombinationTests
    {
        [TestCase("h2 d3 h4 h5 d6 hq dk", "d6 h5 h4 d3 h2")]
        [TestCase("d2 d3 d3 h5 h6 h4 hk", "h6 h5 h4 d3 d2")]
        [TestCase("d2 d3 d3 h5 hq h4 ha", "h5 h4 d3 d2 ha")]
        [TestCase("d2 d3 d3 h5 h2 h4 ha", "h5 h4 d3 d2 ha")]
        [TestCase("hj dk dq h0 h2 h3 da", "da dk dq hj h0")]
        public void StraightTest(string stringCards, string stringComb)
            => Test(stringCards, stringComb, CardCombination.Straight);


        [TestCase("h2 h3 h4 h5 h7 hq hk", "hk hq h7 h5 h4")]
        [TestCase("h2 h3 h3 h5 hq h4 hk", "hk hq h5 h4 h3")]
        [TestCase("d2 h3 d3 h5 dq d4 da", "da dq d4 d3 d2")]
        public void FlashTest(string stringCards, string stringComb)
            => Test(stringCards, stringComb, CardCombination.Flash);


        [TestCase("h2 h3 h4 h5 h6 hq hk", "h6 h5 h4 h3 h2")]
        [TestCase("h2 h3 h3 h5 h6 h4 hk", "h6 h5 h4 h3 h2")]
        [TestCase("d2 h3 d3 d5 dq d4 da", "d5 d4 d3 d2 da")]
        public void StraightFlashTest(string stringCards, string stringComb)
            => Test(stringCards, stringComb, CardCombination.StraightFlash);


        [TestCase("ha hk hq hj h0 hq hk", "ha hk hq hj h0")]
        [TestCase("hk ha h0 hq hj h4 h2", "ha hk hq hj h0")]
        [TestCase("da ha dk hq dq dj d0", "da dk dq dj d0")]
        public void RoyalFlashTest(string stringCards, string stringComb)
            => Test(stringCards, stringComb, CardCombination.RoyalFlash);


        [TestCase("ca sa ha cj h9 hq hk", "ca sa ha hk hq")]
        [TestCase("h2 ha s2 hq hj d4 d2", "h2 s2 d2 ha hq")]
        [TestCase("da s3 d4 s4 h5 c4 d6", "d4 s4 c4 da d6")]
        public void ThreeOfAKindTest(string stringCards, string stringComb)
           => Test(stringCards, stringComb, CardCombination.ThreeOfAKind);

        [TestCase("ha ha d2 d3 d4 c6 c9", "ha ha c9 c6 d4")]
        [TestCase("h2 da h7 dq cj h4 h2", "h2 h2 da dq cj")]
        [TestCase("d2 d3 d4 d4 c5 c9 h7", "d4 d4 c9 h7 c5")]
        public void PairTest(string stringCards, string stringComb)
           => Test(stringCards, stringComb, CardCombination.Pair);


        [TestCase("ha ca ck hk hq h3 s2", "ha ca ck hk hq")]
        [TestCase("h2 da c2 ha h8 s7 d6", "da ha h2 c2 h8")]
        [TestCase("d4 h4 d3 s3 s7 d7 dj", "s7 d7 d4 h4 dj")]
        public void TwoPairTest(string stringCards, string stringComb)
           => Test(stringCards, stringComb, CardCombination.TwoPair);


        [TestCase("ha ha ha ha hk hq hj", "ha ha ha ha hk")]
        [TestCase("h2 ha h2 hq h2 h4 h2", "h2 h2 h2 h2 ha")]
        [TestCase("d4 d4 d4 d3 d3 d4 d6", "d4 d4 d4 d4 d6")]
        [TestCase("d4 d4 d4 d3 d3 d4 d2", "d4 d4 d4 d4 d3")]
        public void FourOfAKindTest(string stringCards, string stringComb)
           => Test(stringCards, stringComb, CardCombination.FourOfAKind);

        [TestCase("ha ha ha hk hk h2 h2", "ha ha ha hk hk")]
        [TestCase("h2 ha h2 ha h8 ha h6", "ha ha ha h2 h2")]
        [TestCase("d4 d4 d7 d3 d7 d7 dj", "d7 d7 d7 d4 d4")]
        public void FullHouseTest(string stringCards, string stringComb)
           => Test(stringCards, stringComb, CardCombination.FullHouse);

        

        public void Test(string stringCards, string stringComb,
            CardCombination expectedCardCombination)
        {
            var expected = Helper.CreateCards(stringComb);
            var comb = new Combination(Helper.CreateCards(stringCards));
            Assert.AreEqual(expectedCardCombination, comb.CardCombination);
            Assert.AreEqual(expected, comb.FullCombination);
        }

        [TestCase("h2 c2 ck dq sa", "c2 h2 dk cq da", 0)]
        [TestCase("ha hk hq hj h0", "ca ck cq cj c0", 0)]
        [TestCase("ha hk cq hj h0", "ca ck cq cj d0", 0)]

        [TestCase("h3 c3 s3 h2 c2", "h2 c2 s2 h3 c3", 1)]
        [TestCase("h2 c2 d2 s2 ha", "h2 c2 d2 s2 hk", 1)]
        [TestCase("h2 h3 h4 h5 h6", "ha h2 h3 h4 h5", 1)]
        public void CompareCombinationsTest(string comb1, string comb2, int compRes)
        {
            var combination1 = new Combination(Helper.CreateCards(comb1));
            var combination2 = new Combination(Helper.CreateCards(comb2));
            Assert.AreEqual(compRes, combination1.CompareTo(combination2));
        }
    }
}
