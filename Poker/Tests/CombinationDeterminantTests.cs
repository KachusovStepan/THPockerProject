using System;
using NUnit.Framework;
using System.Collections.Generic;
using GameLogic;

namespace Tests
{
    [TestFixture]
    class CombinationDeterminantTests
    {
        [TestCase("h2 h3 h4 h5 h6 hq hk", true, "h6 h5 h4 h3 h2")]
        [TestCase("h2 h3 h3 h5 h6 h4 hk", true, "h6 h5 h4 h3 h2")]
        [TestCase("h2 h3 h3 h5 hq h4 ha", true, "h5 h4 h3 h2 ha")]
        [TestCase("h2 h3 h3 h5 h2 h4 ha", true, "h5 h4 h3 h2 ha")]
        [TestCase("hj hk hq h0 h2 h3 ha", true, "ha hk hq hj h0")]
        public void StraightTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.Straight(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("h2 h3 h4 h5 h6 hq hk", true, "hk hq h6 h5 h4")]
        [TestCase("h2 h3 h3 h5 h6 h4 hk", true, "hk h6 h5 h4 h3")]
        [TestCase("d2 h3 d3 h5 dq d4 da", true, "da dq d4 d3 d2")]
        [TestCase("h2 c3 d3 s5 h2 c4 ha", false, null)]
        [TestCase("hj hk hq h0 c2 c3 ca", false, null)]
        public void FlashTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.Flash(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("h2 h3 h4 h5 h6 hq hk", true, "h6 h5 h4 h3 h2")]
        [TestCase("h7 h8 h9 h0 hj hq hk", true, "hk hq hj h0 h9")]
        [TestCase("d5 h3 d4 h5 d3 d2 da", true, "d5 d4 d3 d2 da")]
        [TestCase("h2 c3 d3 s5 h2 c4 ha", false, null)]
        [TestCase("hj hk hq h0 c2 c3 ca", false, null)]
        public void StraightFlashTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.StraightFlash(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("ha hk hq hj h0 hq hk", true, "ha hk hq hj h0")]
        [TestCase("hk ha h0 hq hj h4 h2", true, "ha hk hq hj h0")]
        [TestCase("da ha dk hq dq dj d0", true, "da dk dq dj d0")]
        [TestCase("h2 c3 d3 s5 h2 c4 ha", false, null)]
        [TestCase("hj hk hq h0 c2 c3 ca", false, null)]
        [TestCase("dj ha d0 hq cj h4 h2", false, null)]
        public void RoyalFlashTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.RoyalFlash(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("ha ha ha hj h0 hq hk", true, "ha ha ha hk hq")]
        [TestCase("h2 ha h2 hq hj h4 h2", true, "h2 h2 h2 ha hq")]
        [TestCase("d2 d3 d4 d4 d5 d4 d6", true, "d4 d4 d4 d6 d5")]
        [TestCase("h2 c3 d3 s5 h2 c4 ha", false, null)]
        [TestCase("hj hk hq h0 c2 c3 ca", false, null)]
        [TestCase("dj ha d0 hq cj h4 h2", false, null)]
        public void ThreeOfAKindTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.ThreeOfAKind(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("ha ha h2 hj h0 hq hk", true, "ha ha hk hq hj")]
        [TestCase("h2 ha h7 hq hj h4 h2", true, "h2 h2 ha hq hj")]
        [TestCase("d2 d3 d4 d4 d5 d9 d6", true, "d4 d4 d9 d6 d5")]
        [TestCase("h2 c3 d4 s5 h6 c7 ha", false, null)]
        [TestCase("hj hk hq h0 c2 c3 ca", false, null)]
        [TestCase("dj ha d0 hq c5 h4 h2", false, null)]
        public void PairTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.Pair(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("ha ha ha ha hk hq hj", true, "ha ha ha ha hk")]
        [TestCase("h2 ha h2 hq h2 h4 h2", true, "h2 h2 h2 h2 ha")]
        [TestCase("d4 d4 d4 d3 d3 d4 d6", true, "d4 d4 d4 d4 d6")]
        [TestCase("d4 d4 d4 d3 d3 d4 d2", true, "d4 d4 d4 d4 d3")]
        [TestCase("h2 c3 d4 s5 h6 c7 ha", false, null)]
        [TestCase("hj hk hq h0 c2 c3 ca", false, null)]
        [TestCase("dj ha d0 hq c5 h4 h2", false, null)]
        public void FourOfAKindTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.FourOfAKind(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("ha ha hk hk hq h3 h2", true, "ha ha hk hk hq")]
        [TestCase("h2 ha h2 ha h8 h7 h6", true, "ha ha h2 h2 h8")]
        [TestCase("d4 d4 d3 d3 d7 d7 dj", true, "d7 d7 d4 d4 dj")]
        [TestCase("h2 c2 d4 s5 h6 c7 ha", false, null)]
        [TestCase("h2 h2 hq h0 c2 c3 ca", false, null)]
        [TestCase("dj ha d0 hq c5 h4 h2", false, null)]
        public void TwoPairsTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.TwoPairs(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("ha ha ha hk hk h2 h2", true, "ha ha ha hk hk")]
        [TestCase("h2 ha h2 ha h8 ha h6", true, "ha ha ha h2 h2")]
        [TestCase("d4 d4 d7 d3 d7 d7 dj", true, "d7 d7 d7 d4 d4")]
        [TestCase("h2 c2 d4 s5 h6 c7 ha", false, null)]
        [TestCase("h2 h2 hq h0 c2 c3 ca", false, null)]
        [TestCase("dj ha d0 hq c5 h4 h2", false, null)]
        public void FullHouseTest(string stringCards, bool ex, string comb)
        {
            var cards = Helper.CreateCards(stringCards);
            var tpl = CombinationDeterminant.FullHouse(cards);
            Assert.AreEqual(ex, tpl.Item1);
            if (ex)
            {
                var expectedComb = Helper.CreateCards(comb);
                Assert.AreEqual(expectedComb.Count, tpl.Item2.Count);
                for (var i = 0; i < expectedComb.Count; i++)
                {
                    Assert.AreEqual(expectedComb[i], tpl.Item2[i]);
                }
            }
        }

        [TestCase("h6 h5 h4 h3 h2", "h6 h5 h4 h3 h2", 0)]
        [TestCase("h6 h6 h4 h3 h2", "h6 h6 h4 h3 h2", 0)]
        [TestCase("s2 s2 d5 h4 h3", "c2 c2 h5 h4 h3", 0)]
        [TestCase("s2 s2 d2 h9 h9", "c2 c2 h2 h9 h9", 0)]

        [TestCase("s5 s5 d3 h3 h4", "c5 c5 h2 h2 h4", 1)]
        [TestCase("s5 s5 d3 h3 h4", "c5 c5 h2 h2 hk", 1)]
        [TestCase("s5 s5 d5 h5 h4", "c4 c4 h4 h4 hq", 1)]

        [TestCase("s6 s5 s4 s3 s2", "sa s6 s5 s4 s3", -1)]
        [TestCase("s2 s2 d2 h3 h3", "c2 c2 h2 h4 h3", -1)]
        public void CompareCombinationsTest(string comb1, string comb2, int compRes)
        {
            var combination1 = Helper.CreateCards(comb1);
            var combination2 = Helper.CreateCards(comb2);
            var actCompRes = CombinationDeterminant.CompareCombinations(combination1, combination2);
            Assert.AreEqual(compRes, actCompRes);
        }
    }
}
