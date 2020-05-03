using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Server;
using GameLogic;

namespace Tests
{
    [TestFixture]
    class GameTests
    {
        [TestCase("11", "01")]
        [TestCase("01", "11")]
        [TestCase("101", "022")]
        [TestCase("010", "111")]
        [TestCase("100100011", "033377778")]
        public void NextTest(string ready, string next)
        {
            var g = ChangeReadyArray(ready);
            for (int i = 0; i < ready.Length; i++)
            {
                var expected = int.Parse(next[i].ToString());
                Assert.AreEqual(expected, g.Next(i));
            }
        }

        [TestCase("111", "012")]
        [TestCase("1011", "023")]
        [TestCase("0111", "123")]
        [TestCase("01011011", "134")]
        public void SetInitialRolesTest(string ready, string roles)
        {
            var g = ChangeReadyArray(ready);
            var expected = roles
                .Select(chr => int.Parse(chr.ToString()))
                .ToList();
            var expectedD = expected[0];
            var expectedSB = expected[1];
            var expectedBB = expected[2];

            g.SetInitialRoles();
            Assert.AreEqual(expectedD, g.DealerSeat);
            Assert.AreEqual(expectedSB, g.SmallBlindSeat);
            Assert.AreEqual(expectedBB, g.BigBlindSeat);
        }

        [TestCase("111", "012 120 201")]
        [TestCase("0111", "123 231 312")]
        [TestCase("01011011", "134 346 467 671 713")]
        public void UpdateTest(string ready, string roles)
        {
            var g = ChangeReadyArray(ready);
            var expected = roles
                .Split()
                .Select(str => str
                    .Select(chr => int.Parse(chr.ToString()))
                    .ToList());
            g.SetInitialRoles();
            foreach (var e in expected)
            {
                var expectedD = e[0];
                var expectedSB = e[1];
                var expectedBB = e[2];
                Assert.AreEqual(expectedD, g.DealerSeat);
                Assert.AreEqual(expectedSB, g.SmallBlindSeat);
                Assert.AreEqual(expectedBB, g.BigBlindSeat);
                g.Update();
            }
        }

        [Test]
        public void GetName_WhenNameExists()
        {
            var g = Game.GetGameInstance(0);
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                var names = new List<string>();
                Clear(g);
                for (int j = 0; j < 10; j++)
                {
                    names.Add((j + rand.NextDouble()).ToString());
                    g.AddPlayer(names[j], j);
                }
                for (int j = 0; j < 10; j++)
                {
                    string name;
                    Assert.IsTrue(g.GetName(j + 1, out name));
                    Assert.AreEqual(names[j], name);
                }
            }
        }
        
        [Test]
        public void GetName_WhenNameDoesNotExist()
        {
            var g = Game.GetGameInstance(0);
            for (int i = 0; i < 10; i++)
            {
                string name;
                g.GetName(i, out name);
                Assert.IsEmpty(name);
            }
        }

        [Test]
        public void GetName_AfterRemoving()
        {
            var g = Game.GetGameInstance(0);
            for (int i = 0; i < 100; i++)
            {
                Clear(g);
                for (int j = 0; j < 10; j++)
                {
                    g.AddPlayer(j.ToString(), j);
                }
                for (int j = 0; j < 10; j++)
                {
                    g.RemovePlayer(j + 1);
                    string name;
                    g.GetName(j + 1, out name);
                    Assert.IsEmpty(name);
                }
                Assert.IsEmpty(g.PlayerByID);
                Assert.IsEmpty(g.PlayerBySeat);
            }
        }

        [Test]
        public void CollectMoneyTest()
        {
            var g = Game.GetGameInstance(0);
            var rand = new Random();
            int sum;
            int bet;
            for (int i = 0; i < 100; i++)
            {
                sum = 0;
                g.GetType().GetProperty("CurrentBank").SetValue(g, 0);
                Clear(g);
                for (int j = 0; j < 10; j++)
                {
                    bet = rand.Next(1000);
                    sum += bet;
                    g.AddPlayer("", j);
                    g.PlayerBySeat[j].TableBet = bet;
                }
                g.CollectMoney();
                Assert.AreEqual(sum, g.CurrentBank);
            }
        }

        [Test]
        public void RewardWinnerTest()
        {
            var g = Game.GetGameInstance(0);
            var rand = new Random();
            int bank;
            for (int i = 0; i < 100; i++)
            {
                bank = rand.Next();
                g.GetType().GetProperty("CurrentBank").SetValue(g, bank);
                g.AddPlayer("", i % 10);
                g.PlayerBySeat[i % 10].ChipBank = 0;
                g.RewardWinner(i % 10);
                Assert.AreEqual(bank, g.PlayerBySeat[i % 10].ChipBank);
                Assert.AreEqual(0, g.CurrentBank);
            }
        }

        //[TestCase("b b", "1 1", "99 99", "1 1", 2)]
        //[TestCase("b r c", "1 2 1", "98 98", "2 2", 2)]
        //public void BettingRoundTest(
        //    string betsStr,
        //    string valuesStr,
        //    string expectedPlayersBanksStr,
        //    string expectedTableBetsStr,
        //    int playersCount
        //    )
        //    {
        //    var g = Game.GetGameInstance(0);
        //    Clear(g);
        //    var converted = ConvertFromString(betsStr, valuesStr,
        //        expectedPlayersBanksStr, expectedTableBetsStr);
        //    var bets = converted.bets;
        //    var values = converted.values;
        //    var expectedPlayersBanks = converted.playersBanks;
        //    var expectedTableBets = converted.tableBets;

        //    for (int i = 0; i < playersCount; i++)
        //    {
        //        g.AddPlayer(i.ToString(), i);
        //        g.Ready[i] = true;
        //    }
            

        //    g.BettingRound();

        //    for (int i = 0; i < playersCount; i++)
        //    {
        //        Assert.AreEqual(expectedTableBets[i], g.PlayerBySeat[i].TableBet);
        //        Assert.AreEqual(expectedPlayersBanks[i], g.PlayerBySeat[i].ChipBank);
        //    }
        //}


        public (List<Bet> bets, List<int> values, List<int> playersBanks, List<int> tableBets) 
            ConvertFromString(
            string betsStr,
            string valuesStr,
            string expectedPlayersBanksStr,
            string expectedTableBetsStr)
        {
            Func<string, List<int>> convert = str => str
                .Split()
                .Select(x => int.Parse(x))
                .ToList();

            var bets = betsStr
                .Split()
                .Select(bet =>
                {
                    switch (bet)
                    {
                        case "c":
                            return Bet.Call;
                        case "b":
                            return Bet.Bet;
                        case "r":
                            return Bet.Raise;
                        case "f":
                            return Bet.Fold;
                        case "ch":
                            return Bet.Check;
                        case "a":
                            return Bet.AllIn;
                        default:
                            return Bet.None;
                    }
                })
                .ToList();

            return (bets, convert(valuesStr),
                convert(expectedPlayersBanksStr),
                convert(expectedTableBetsStr));
        }

        public void Clear(Game game)
        {
            game.PlayerBySeat.Clear();
            game.PlayerByID.Clear();
        }

        public Game ChangeReadyArray(string ready)
        {
            var g = Game.GetGameInstance(0);
            g.GetType().GetProperty("Ready").SetValue(g, new bool[10]);
            for (int i = 0; i < ready.Length; i++)
            {
                g.Ready[i] = ready[i] == '1' ? true : false;
            }
            return g;
        }
    }
}
