using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameLogic;

namespace Server
{
    public class BetNode
    {
        public Bet PlayerBet;
        public int Value;
        public int Seat;
        public int Id;
    }

    public class PlayerInfo
    {
        public int ChipBank;
        public readonly string Name;
        public readonly int Position;
        public int ID;
        public TurnRole Role;
        public Tuple<Card, Card> Hand;
        public int TableBet;
        public PlayerInfo(string name, int position, int chipBank)
        {
            Name = name;
            Position = position;
            ChipBank = chipBank;
            Hand = null;
        }

        public PlayerInfo(string name, int position)
        {
            Name = name;
            Position = position;
            ChipBank = 100;
            Hand = null;
        }
    }

    public static class Game
    {
        public static int[] IDS = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static List<BetNode> RoundHistory = new List<BetNode>();
        public static Dictionary<int, PlayerInfo> PlayerBySeat = new Dictionary<int, PlayerInfo>();
        public static Dictionary<int, PlayerInfo> PlayerByID = new Dictionary<int, PlayerInfo>();
        public static GameState CurrentState;
        public static int Count = 0;
        public static bool[] Ready = new bool[10];
        public static int SB = 0;
        public static int BB = 0;
        public static int D = 0;
        public static bool BetHasBeenMade = false;
        public static int CurrentPlayer;
        public static int CurrentBank;
        public static List<Card> TableCards = new List<Card>();
        public static CardDeck Deck = new CardDeck();

        public static bool GetName(int id, out string name)
        {
            name = string.Empty;
            if (PlayerByID.ContainsKey(id))
                name = PlayerByID[id].Name;
            return true;
        }

        public static void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Ready[i] = PlayerBySeat.ContainsKey(i);
            }

            // Назначние блайндов
            SetInitialRoles();

            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    Ready[i] = PlayerBySeat.ContainsKey(i);
                }
                // Цикл раундов пока есть хотя бы 2 игрока
                PreFlop();

                Update();
                if (Count == 0)
                {
                    continue;
                }
                Flop();

                Update();
                if (Count == 0)
                {
                    continue;
                }
                Turn();

                Update();
                if (Count == 0)
                {
                    continue;
                }
                River();

                Update();
                if (Count == 0)
                {
                    continue;
                }
                ShowDown();
            }
        }
        private static void PreFlop()
        {
            // Тасовка колоды
            Deck.Shuffle();

            // Выдача 2 карт каждому
            for (int i = 0; i < 10; i++) {
                if (Ready[i]) {
                    PlayerBySeat[i].Hand = Tuple.Create(Deck.GetCard(), Deck.GetCard());
                }
            }

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private static void Flop()
        {
            // На столе 3 карты
            TableCards.Add(Deck.GetCard());
            TableCards.Add(Deck.GetCard());
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private static void Turn()
        {
            // На стол добавляется 1 карты
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private static void River()
        {
            // На стол добавляется 1 карты
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private static void ShowDown()
        {
            // Вскрытие карт всеми оставшимися
            // Определить игрока с самой старшей комбинацией
            // Прибавить в банк победившего игрока банк игры
            // RewardWinner(int seat)
        }
        private static void BettingRound()
        {
            // Начиная с игрока слева от BB
            // Текущий игрок делает ставку
            // Если дошло до BB и он сделал Bet or Raise, то еще один круг
            // Если был второй круг 
            var curr = Next((BB + 1) % 10);
            while (Count > 1)
            {
                BetHasBeenMade = false;
                CurrentPlayer = curr;
                WaitForBet();
                Execute(RoundHistory[RoundHistory.Count - 1]);

                if (curr == Next((curr + 1) % 10) && PlayerBySeat[BB].TableBet == PlayerBySeat[curr].TableBet)
                    break;

                curr = Next((curr + 1) % 10);
            }
            if (Count == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Ready[i])
                    {
                        Count = 0;
                        Ready[i] = false;
                        RewardWinner(i);
                        return;
                    }
                }
            }
        }

        public static void SetInitialRoles()
        {
            D = Next(0);
            SB = Next((D + 1) % 10);
            BB = Next((SB + 1) % 10);
        }

        public static int Next(int start)
        {
            var i = start;
            while (!Ready[i])
            {
                i = (i + 1) % 10;
            }
            return i;
        }

        public static void Update()
        {
            Count = Ready.Where(x => x).Count();
            if (Count == 1)
                return;
            D = SB;
            SB = BB;
            BB = Next((SB + 1) % 10);
        }

        public static int AddPlayer(string name, int position)
        {
            if (PlayerBySeat.ContainsKey(position))
                return -1;
            var player = new PlayerInfo(name, position);
            var id = IDS.Where(x => !PlayerByID.ContainsKey(x)).FirstOrDefault();
            PlayerByID.Add(id, player);
            PlayerBySeat.Add(position, player);
            return id;
        }

        // Плохое ожидание
        private static void WaitForBet()
        {
            // Ожидать сообщения игроком ставки
            while (!BetHasBeenMade)
            {
                Thread.Sleep(100);
            }
        }

        public static void Execute(BetNode bet)
        {
            var player = PlayerBySeat[bet.Seat];
            if (bet.PlayerBet == Bet.Bet)
            {
                player.TableBet += bet.Value;
                player.ChipBank -= bet.Value;
            }
            else if (bet.PlayerBet == Bet.Call)
            {
                player.TableBet += RoundHistory[RoundHistory.Count - 2].Value;
                player.ChipBank -= RoundHistory[RoundHistory.Count - 2].Value;
            }
            else if (bet.PlayerBet == Bet.Fold)
            {
                Count--;
                Ready[bet.Seat] = false;
            }
            else if (bet.PlayerBet == Bet.Raise)
            {
                player.TableBet += RoundHistory[RoundHistory.Count - 2].Value * 2;
                player.ChipBank -= RoundHistory[RoundHistory.Count - 2].Value * 2;
            }
        }

        public static void CollectMoney()
        {
            foreach (var seat in PlayerBySeat.Keys)
            {
                CurrentBank += PlayerBySeat[seat].TableBet;
                PlayerBySeat[seat].TableBet = 0;
            }
        }

        public static void RewardWinner(int seat)
        {
            PlayerBySeat[seat].ChipBank += CurrentBank;
            CurrentBank = 0;
        }
    }
}
