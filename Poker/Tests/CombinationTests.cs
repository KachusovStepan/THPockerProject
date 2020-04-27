using System;
using GameLogic;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CombinationDeterminationTests
    {
        [TestCase("h2 h3 h4 c4 h6 h7 da", true)] // inside
        [TestCase("h2 h3 s4 c4 h6 h7 d2", true)] // on the edges
        [TestCase("hq c3 d4 c7 s6 hj dj", true)] // near at the begginig
        [TestCase("ha ca d4 c7 h6 h8 dj", true)] // high rank
        [TestCase("h2 ck dq c3 h4 h5 dk", true)] // kings inside
        [TestCase("s2 sk sq s3 s4 s5 sk", true)] // same suits (wierd two kings of spades)
        [TestCase("h3 h9 h6 h0 h4 h2 c0", true)] // tens
        [TestCase("h2 ck d0 c3 d6 h5 s5", true)] // at the end
        public void IsPair_CanFindThanOnlyOnePair_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsPair(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h2 h3 h4 h5 h6 h7 h8", false)]
        [TestCase("h2 c3 d4 s5 d6 h7 c8", false)]
        [TestCase("hj cq d4 s6 da h2 c0", false)]
        [TestCase("hj ca d2 s6 d7 h0 c9", false)]
        public void IsPair_ThanNoPairs_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsPair(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void IsPair_ThanEmptyList_Falset()
        {
            Assert.AreEqual(CombinationHelper.IsPair(new List<Card>()), false);
        }

        [TestCase("h2 h2 h4 h4 h6 h7 h8", true)]
        [TestCase("h2 c3 d3 s5 d6 ha ca", true)]
        public void IsPair_ThanTwoPairs_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsPair(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h2 h2 h4 h4 h6 h7 h8", true)]
        [TestCase("c2 d2 s4 h4 h6 h7 h8", true)]
        [TestCase("c2 d2 s5 h4 h6 s8 h8", true)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", true)]
        [TestCase("cq d3 s2 ha ha s3 h8", true)]
        [TestCase("cq d3 sj hk hk sj h8", true)]
        public void IsTwoPairs_ThanTwoPairs_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsTwoPair(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h2 h3 h4 c4 h6 h7 da", false)]
        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("h2 ck dq c3 h4 h5 dk", false)]
        [TestCase("s2 sk sq s3 s4 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]
        public void IsTwoPairs_ThanOnePair_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsTwoPair(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void IsTwoPair_ThanEmptyList_False()
        {
            Assert.AreEqual(CombinationHelper.IsTwoPair(new List<Card>()), false);
        }

        [TestCase("c2 h2 d2 c3 d6 h4 s5", true)]
        [TestCase("ca hq dq cq d6 h4 s5", true)]
        [TestCase("ck hj dk c5 dk h4 s5", true)]
        [TestCase("ck hj d2 c0 d0 h0 s7", true)]
        [TestCase("ck hj d2 c0 d4 h4 s4", true)]

        [TestCase("ck hk d2 ck d3 hj s4", true)]
        [TestCase("c7 hk d2 c7 d3 hj s7", true)]
        [TestCase("c9 h6 d2 c6 d3 h6 s7", true)]
        public void IsThreeOfAKind_ThanThreeCardsAreTheSameRank_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsThreeOfAKind(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h2 h3 h4 c4 h6 h7 da", false)]
        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("h2 ck dq c3 h4 h5 dk", false)]
        [TestCase("s2 sk sq s3 s4 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]

        [TestCase("h2 h2 h4 h4 h6 h7 h8", false)]
        [TestCase("c2 d2 s4 h4 h6 h7 h8", false)]
        [TestCase("c2 d2 s5 h4 h6 s8 h8", false)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", false)]
        [TestCase("cq d3 s2 ha ha s3 h8", false)]
        [TestCase("cq d3 sj hk hk sj h8", false)]

        public void IsThreeOfAKind_ThanNoThreeOfAKind_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsThreeOfAKind(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void IsThreeOfAKind_ThanEmptyList_False()
        {
            Assert.AreEqual(CombinationHelper.IsTwoPair(new List<Card>()), false);
        }


        [TestCase("h0 hj d4 c5 d6 h7 s8", true)]
        [TestCase("h2 h3 d4 c5 d6 hq sa", true)]
        [TestCase("hj h4 d5 c6 d7 h8 sk", true)]
        [TestCase("hj h4 d9 c0 dj hq sk", true)]
        public void IsStraight_ThanIsSimpleStraight_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraight(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h2 h3 d4 c5 da h7 s8", true)]
        [TestCase("ha h2 d3 c4 d5 hq sa", true)]
        [TestCase("hk ha d2 c3 d4 h8 sk", true)]
        [TestCase("hj hq dk ca d2 h4 s5", true)]
        public void IsStraight_ThanIsCyrcledStraight_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraight(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h0 h4 d4 c5 d6 h7 s8", true)]
        [TestCase("h2 h3 d4 c5 d5 h6 sa", true)]
        [TestCase("hj h4 d5 c6 d7 h8 s8", true)]
        public void IsStraight_ThanIsStraightWithAPair_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraight(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h4 h4 d5 c5 d6 h7 s8", true)]
        [TestCase("h3 h3 d4 c5 d5 h6 s7", true)]
        [TestCase("h4 h5 d6 c6 d7 h7 s8", true)]
        public void IsStraight_ThanIsStraightWithTwoPairs_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraight(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h4 h5 d3 c6 d7 ha sj", true)]
        [TestCase("h3 h7 d5 c6 d8 h9 sq", true)]
        [TestCase("h4 hk dq cj d0 h9 s8", true)]
        public void IsStraight_ThanIsStraightNotSorted_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraight(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }



        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("hq ck dq c3 h7 h5 dk", false)]
        [TestCase("s2 sk sq s2 s8 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]

        [TestCase("h2 h2 h4 h4 hq hk h8", false)]
        [TestCase("c2 d2 s4 h4 h0 h7 h8", false)]
        [TestCase("cq d2 s7 h4 h6 s9 h9", false)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", false)]
        [TestCase("cq d3 s2 ha ha s3 h8", false)]
        [TestCase("cq d3 sj hk hk sj h8", false)]
        public void IsStraight_ThanNoStraight_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraight(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq h3 hj hk hk sj d8", true)]
        [TestCase("cq h3 hj hk hk hj d8", true)]
        [TestCase("hq h3 dj dk dk dj d8", true)]
        [TestCase("cq h3 cj ck ck dj c8", true)]
        [TestCase("sq s3 cj sk ck sj s8", true)]
        public void IsFlash_ThanIsFlash_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFlash(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq c3 hj dk ck sj d8", false)]
        [TestCase("cq h3 sj hk sk hj d8", false)]
        [TestCase("hq h3 dj dk ck dj d8", false)]
        [TestCase("cq h3 cj ck dk dj c8", false)]
        [TestCase("hq s3 cj hk ck sj s8", false)]
        public void IsFlash_ThanNoFlash_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFlash(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("s3 c3 d3 sk ck s5 s8", true)]
        [TestCase("s2 c3 d3 s3 ck sk s8", true)]
        [TestCase("s2 cq d3 s3 ck sk sk", true)]
        [TestCase("s5 cq d2 s2 c2 d3 sq", true)]
        [TestCase("s7 cj d2 sj c2 dj sk", true)]
        public void IsFullHouse_ThanIsFullHouse_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFullHouse(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("hq ck dq c3 h7 h5 dk", false)]
        [TestCase("s2 sk sq s2 s8 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]

        [TestCase("h2 h2 h4 h4 hq hk h8", false)]
        [TestCase("c2 d2 s4 h4 h0 h7 h8", false)]
        [TestCase("cq d2 s7 h4 h6 s9 h9", false)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", false)]
        [TestCase("cq d3 s2 ha ha s3 h8", false)]
        [TestCase("cq d3 sj hk hk sj h8", false)]
        public void IsFullHouse_ThanNoFullHouse_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFullHouse(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("s3 c3 d3 sk ck s5 s5", true)]
        [TestCase("sq c3 d3 s3 ck sk sq", true)]
        [TestCase("s2 c2 d3 s3 ck sk sk", true)]
        [TestCase("s5 cq d2 s2 c2 d3 sq", true)]
        [TestCase("sk cj d2 sj c2 dj sk", true)]
        public void IsFullHouse_ThanIsFullHouseWithPairs_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFullHouse(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("s4 c4 d4 s4 c2 dj sk", true)]
        [TestCase("s0 c4 d4 s4 c4 dj sk", true)]
        [TestCase("s5 c4 d5 s4 c5 dj s5", true)]
        [TestCase("sq c5 dq sq c5 dq s5", true)]
        public void IsFourOfAKind_ThanIsFourOfAKind_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFourOfAKind(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("hq ck dq c3 h7 h5 dk", false)]
        [TestCase("s2 sk sq s2 s8 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]

        [TestCase("h2 h2 h4 h4 hq hk h8", false)]
        [TestCase("c2 d2 s4 h4 h0 h7 h8", false)]
        [TestCase("cq d2 s7 h4 h6 s9 h9", false)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", false)]
        [TestCase("cq d3 s2 ha ha s3 h8", false)]
        [TestCase("cq d3 sj hk hk sj h8", false)]

        [TestCase("s3 c3 d3 sk ck s5 s8", false)]
        [TestCase("s2 c3 d3 s3 ck sk s8", false)]
        [TestCase("s2 cq d3 s3 ck sk sk", false)]
        [TestCase("s5 cq d2 s2 c2 d3 sq", false)]
        [TestCase("s7 cj d2 sj c2 dj sk", false)]
        public void IsFourOfAKind_ThanNoFourOfAKind_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsFourOfAKind(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("h0 hj d4 h5 h6 h7 h8", true)]
        [TestCase("h2 h3 h4 h5 h6 hq ha", true)]
        [TestCase("hj h4 h5 h6 h7 h8 hk", true)]
        [TestCase("hj h4 h9 h0 hj hq hk", true)]

        [TestCase("s4 s5 s3 s6 s7 sa sj", true)]
        [TestCase("s3 s7 s5 s6 s8 s9 sq", true)]
        [TestCase("s4 sk sq sj s0 s9 s8", true)]

        [TestCase("d4 d4 d5 d5 d6 d7 d8", true)]
        [TestCase("d3 d3 d4 d5 d5 d6 d7", true)]
        [TestCase("d4 d5 d6 d6 d7 d7 d8", true)]
        public void IsStraightFlash_ThanIsStraightFlash_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraightFlash(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("hq ck dq c3 h7 h5 dk", false)]
        [TestCase("s2 sk sq s2 s8 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]

        [TestCase("h2 h2 h4 h4 hq hk h8", false)]
        [TestCase("c2 d2 s4 h4 h0 h7 h8", false)]
        [TestCase("cq d2 s7 h4 h6 s9 h9", false)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", false)]
        [TestCase("cq d3 s2 ha ha s3 h8", false)]
        [TestCase("cq d3 sj hk hk sj h8", false)]

        [TestCase("h2 h3 d4 c5 da h7 s8", false)]
        [TestCase("ha h2 d3 c4 d5 hq sa", false)]
        [TestCase("hk ha d2 c3 d4 h8 sk", false)]
        [TestCase("hj hq dk ca d2 h4 s5", false)]

        [TestCase("hq h3 hj hk hk sj d8", false)]
        [TestCase("cq h3 hj hk hk hj d8", false)]
        [TestCase("hq h3 dj dk dk dj d8", false)]
        [TestCase("cq h3 cj ck ck dj c8", false)]
        [TestCase("sq s3 cj sk ck sj s8", false)]
        public void IsStraightFlash_ThanNoStraightFlash_False(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsStraightFlash(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq h3 h0 hj hq hk ha", true)]
        [TestCase("hq h0 hj hq hk ha h8", true)]
        [TestCase("h0 hj hq hk ha h2 h3", true)]
        [TestCase("h0 h3 hj h7 hq hk ha", true)]
        [TestCase("h0 hj hq hk ha h2 h8", true)]

        [TestCase("sk s0 sq sa sj s2 s8", true)]
        [TestCase("sk s0 sq s2 sj sj sa", true)]

        [TestCase("d0 d0 dj dj dq dk da", true)]
        [TestCase("d0 d0 d0 dj dq dk da", true)]
        [TestCase("d0 d0 d0 dj dq dk da", true)]
        public void IsRoyalFlash_ThanIsRoyalFlash_True(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsRoyalFlash(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestCase("hq c3 d4 c7 s6 hj dj", false)]
        [TestCase("ha ca d4 c7 h6 h8 dj", false)]
        [TestCase("hq ck dq c3 h7 h5 dk", false)]
        [TestCase("s2 sk sq s2 s8 s5 sk", false)]
        [TestCase("h3 h9 h6 h0 h4 h2 c0", false)]
        [TestCase("h2 ck d0 c3 d6 h5 s5", false)]

        [TestCase("h2 h2 h4 h4 hq hk h8", false)]
        [TestCase("c2 d2 s4 h4 h0 h7 h8", false)]
        [TestCase("cq d2 s7 h4 h6 s9 h9", false)]
        [TestCase("c2 d3 s2 h4 h8 sj h8", false)]
        [TestCase("cq d3 s2 ha ha s3 h8", false)]
        [TestCase("cq d3 sj hk hk sj h8", false)]

        [TestCase("h2 h3 d4 c5 da h7 s8", false)]
        [TestCase("ha h2 d3 c4 d5 hq sa", false)]
        [TestCase("hk ha d2 c3 d4 h8 sk", false)]
        [TestCase("hj hq dk ca d2 h4 s5", false)]

        [TestCase("hq h3 hj hk hk sj d8", false)]
        [TestCase("cq h3 hj hk hk hj d8", false)]
        [TestCase("hq h3 dj dk dk dj d8", false)]
        [TestCase("cq h3 cj ck ck dj c8", false)]
        [TestCase("sq s3 cj sk ck sj s8", false)]

        [TestCase("sk s0 sq s2 sj s2 s8", false)]
        [TestCase("sq s0 sq s2 sj sj sq", false)]

        [TestCase("d9 d0 dj dj dq dk d2", false)]
        [TestCase("d9 d0 dj dj dq dk d5", false)]
        [TestCase("d6 d7 d8 d9 d0 dj dq", false)]
        public void IsRoyalFlash_ThanNoRoyalFlash_Flash(string stringCards, bool expectedResult)
        {
            var cards = Helper.CreateCards(stringCards);
            var actualResult = CombinationHelper.IsRoyalFlash(cards);
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
