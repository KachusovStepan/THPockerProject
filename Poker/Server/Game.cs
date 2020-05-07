using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameLogic;
using ContractStorage;

namespace Server
{
    public class BetNode
    {
        public Bet PlayerBet;
        public int Value;
        public int Seat;
        public int Id;

        public BetNode(int playerId, int playerSeat, Bet playerBet, int value)
        {
            Id = playerId;
            Seat = playerSeat;
            PlayerBet = playerBet;
            Value = value;
        }

        public BetNode(int playerId, int playerSeat, Bet playerBet) : this(playerId, playerSeat, playerBet, 0) { }
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

    public class Game
    {
        private static Dictionary<GameState, char> turnChar = new Dictionary<GameState, char>() {
            { GameState.PreFlop, 'p' },
            { GameState.Flop, 'f' },
            { GameState.Turn, 't' },
            { GameState.River, 'r' },
            { GameState.ShowTime, 's' },
            { GameState.NotStart, 'n' }
        };

        private static HashSet<int> gameIds = new HashSet<int>();

        public static int MinBet = 2;
        public readonly int GameId;
        public static int[] IDs { get; private set; }
        public List<BetNode> RoundHistory { get; private set; }
        public Dictionary<int, PlayerInfo> PlayerBySeat { get; private set; }
        public Dictionary<int, PlayerInfo> PlayerByID { get; private set; }
        public GameState CurrentState { get; private set; }
        public int Count { get; set; }
        public bool[] Ready { get; private set; }
        public int SmallBlindSeat { get; private set; }
        public int BigBlindSeat { get; private set; }
        public int DealerSeat { get; private set; }
        public bool[] DidPlayerMakeBet { get; private set; }
        public bool BetHasBeenMade { get; set; }
        public int CurrentPlayer { get; private set; }
        public int CurrentBank { get; private set; }
        public List<Card> TableCards { get; private set; }
        public static CardDeck Deck { get; private set; }
        public List<string> chat { get; private set; }
        public int roundMaxBet { get; private set; }
        public readonly Dictionary<int, BetNode> PlayerBets;
        public bool Started;

        private static Dictionary<int, Game> GameInstances = new Dictionary<int, Game>();
        public static Queue<int> GamesToStart = new Queue<int>();

        // Multiton
        private Game(int gameId)
        {
            gameIds.Add(gameId);
            SmallBlindSeat = -1;
            BigBlindSeat = -1;
            DealerSeat = -1;
            GameId = gameId;
            IDs = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            DidPlayerMakeBet = new bool[10];
            RoundHistory = new List<BetNode>();
            PlayerBySeat = new Dictionary<int, PlayerInfo>();
            PlayerByID = new Dictionary<int, PlayerInfo>();
            Ready = new bool[10];
            TableCards = new List<Card>();
            Deck = new CardDeck();
            PlayerBets = new Dictionary<int, BetNode>();
            chat = new List<string>();
            Started = false;
            CurrentBank = 0;
        }
        public static Game GetGameInstance(int gameId)
        {
            if (!GameInstances.ContainsKey(gameId))
            {
                var newGame = new Game(gameId);
                Game.gameIds.Add(gameId);
                Game.GameInstances.Add(gameId, newGame);
            }

            return GameInstances[gameId];
        }

        public static void DeleteGame(int gameId)
        {
            gameIds.Remove(gameId);
            GameInstances.Remove(gameId);
        }

        public static HashSet<int> ListGameIds()
        {
            return gameIds;
        }

        public bool GetName(int id, out string name)
        {
            name = string.Empty;
            if (PlayerByID.ContainsKey(id))
                name = PlayerByID[id].Name;
            return true;
        }

        public List<string> ListPlayerNames()
        {
            var names = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                if (PlayerBySeat.ContainsKey(i))
                {
                    names.Add(PlayerBySeat[i].Name);
                }
                else
                {
                    names.Add(null);
                }
            }

            return names;
        }

        public char GetRound()
        {
            return turnChar[CurrentState];
        }

        public static Dictionary<int, int> ShowGames()
        {
            var res = new Dictionary<int, int>();
            foreach (var gameId in GameInstances.Keys)
                res.Add(gameId, GameInstances[gameId].PlayerBySeat.Count);
            Console.WriteLine("About to show Games from ShowGames");
            return res;
        }

        public static bool CreateNewGame(int gameId)
        {
            if (!GameInstances.ContainsKey(gameId))
            {
                GetGameInstance(gameId);
                return true;
            }
            return false;
        }

        public int AddPlayer(string name, int position)
        {
            if (PlayerBySeat.ContainsKey(position))
                return -1;
            Count++;
            var player = new PlayerInfo(name, position);
            var id = IDs.Where(x => !PlayerByID.ContainsKey(x)).FirstOrDefault();
            PlayerByID.Add(id, player);
            PlayerBySeat.Add(position, player);
            return id;
        }

        public bool RemovePlayer(int playerId)
        {
            if (!PlayerByID.ContainsKey(playerId))
            {
                return false;
            }
            var playerInfo = PlayerByID[playerId];
            var seat = playerInfo.Position;
            PlayerBySeat.Remove(seat);
            PlayerByID.Remove(playerId);
            if (Ready[seat])
            {
                Ready[seat] = false;
                Count--;
            }

            if (PlayerByID.Count == 0)
                Game.DeleteGame(GameId);
            return true;
        }

        public bool AddMessage(int playerId, string playerMessage)
        {
            Console.WriteLine("Adding message {1} of Player {0}", playerId, playerMessage);
            if (!PlayerByID.ContainsKey(playerId))
            {
                Console.WriteLine("Check is failed no such Id {0}", playerId);
                foreach (var id in PlayerByID.Keys)
                    Console.WriteLine("id - {0}", id);
                return false;
            }
            var playerInfo = PlayerByID[playerId];
            var message = String.Format("[{0}]: {1}", playerInfo.Name, playerMessage);
            chat.Add(message);
            return true;
        }

        public State GetGameState()
        {
            var tableBanks = new Dictionary<int, int>();
            var banks = new Dictionary<int, int>();
            var seatNames = new Dictionary<int, string>();
            foreach (var seat in PlayerBySeat.Keys)
            {
                tableBanks.Add(seat, PlayerBySeat[seat].TableBet);
                banks.Add(seat, PlayerBySeat[seat].ChipBank);
                seatNames.Add(seat, PlayerBySeat[seat].Name);
            }

            Dictionary<char, int> blindSeats = null;
            if (DealerSeat != -1)
                blindSeats = new Dictionary<char, int> { { 'd', DealerSeat }, { 's', SmallBlindSeat }, { 'b', BigBlindSeat } };
            var state = new State(PlayerByID.Count, banks, tableBanks, CurrentBank, CurrentPlayer, chat, blindSeats, turnChar[CurrentState], Card.FromCardListToString(TableCards), Ready);
            return state;
        }
        public void Start()
        {
            Started = true;
            Func<Action, bool> conductBettingRoungAndReportIfRoundIsOver = currentRound =>
            {
                currentRound();
                roundMaxBet = 0;
                if (Ready.Count(x => x) < 2)
                {
                    CheckWinner();
                    return true;
                }
                return false;
            };
            Action setReadyPlayers = () =>
            {
                for (int i = 0; i < 10; i++)
                    Ready[i] = PlayerBySeat.ContainsKey(i);
            };

            setReadyPlayers();
            SetInitialRoles();
            while (true)
            {
                if (Count == 0)
                    break;
                Deck.Shuffle();
                TableCards.Clear();
                setReadyPlayers();
                if (!TakeBlinds()) break;
                if (conductBettingRoungAndReportIfRoundIsOver(PreFlop)) continue;
                if (conductBettingRoungAndReportIfRoundIsOver(Flop)) continue;
                if (conductBettingRoungAndReportIfRoundIsOver(Turn)) continue;
                if (conductBettingRoungAndReportIfRoundIsOver(River)) continue;
                ShowDown();
                Update();
            }
        }

        private bool TakeBlinds()
        {
            Func<int, bool> moveBlinds = blindSeat =>
            {
                var minBet = MinBet;
                if (blindSeat == SmallBlindSeat)
                    minBet /= 2;
                while (PlayerBySeat[blindSeat].ChipBank < minBet)
                {
                    Ready[blindSeat] = false;
                    if (blindSeat == SmallBlindSeat)
                        SmallBlindSeat = Next(SmallBlindSeat + 1);
                    BigBlindSeat = Next(BigBlindSeat + 1);
                    if (SmallBlindSeat == BigBlindSeat)
                        return false;
                }
                return true;
            };

            if (!moveBlinds(SmallBlindSeat)) return false;
            if (!moveBlinds(BigBlindSeat)) return false;
            PlayerBySeat[SmallBlindSeat].ChipBank -= MinBet / 2;
            PlayerBySeat[BigBlindSeat].ChipBank -= MinBet;
            roundMaxBet = MinBet;

            PlayerBySeat[SmallBlindSeat].TableBet += MinBet / 2;
            PlayerBySeat[BigBlindSeat].TableBet += MinBet;
            return true;
        }

        private void PreFlop()
        {
            CurrentState = GameState.PreFlop;
            TableCards.Clear();
            //new
            for (int i = 0; i < 10; i++)
            {
                if (Ready[i])
                {
                    PlayerBySeat[i].Hand = Tuple.Create(Deck.GetCard(), Deck.GetCard());
                }
            }

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void Flop()
        {
            CurrentState = GameState.Flop;
            TableCards.Add(Deck.GetCard());
            TableCards.Add(Deck.GetCard());
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void Turn()
        {
            CurrentState = GameState.Turn;
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void River()
        {
            CurrentState = GameState.River;
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void ShowDown()
        {
            CurrentState = GameState.ShowTime;
            var winners = new List<PlayerInfo>();
            var bestComb = Ready
                .Select((flag, seat) => (flag, seat))
                .Where(pair => pair.flag)
                .Select(pair => PlayerBySeat[pair.seat])
                .Select(player => new Combination(player.Hand, TableCards))
                .OrderByDescending(comb => comb)
                .FirstOrDefault();
            for (int i = 0; i < Ready.Length; i++)
            {
                if (!Ready[i])
                    continue;
                var comb = new Combination(PlayerBySeat[i].Hand, TableCards);
                if (comb.CompareTo(bestComb) == 0)
                    winners.Add(PlayerBySeat[i]);
            }
            foreach (var winner in winners)
            {
                winner.ChipBank += CurrentBank / winners.Count;
            }
            CurrentBank = 0;
        }

        private void BettingRound()
        {
            var current = Next((CurrentState == GameState.PreFlop ? BigBlindSeat : DealerSeat) + 1);
            var sumOfBetsBySeat = Ready
                .Select((flag, seat) => (flag, seat))
                .Where(pair => pair.flag)
                .ToDictionary(pair => pair.seat, pair => 0);
            var playersCount = sumOfBetsBySeat.Count;
            var turnsCount = 0;
            
            if (CurrentState == GameState.PreFlop)
            {
                sumOfBetsBySeat[SmallBlindSeat] = PlayerBySeat[SmallBlindSeat].TableBet;
                sumOfBetsBySeat[BigBlindSeat] = PlayerBySeat[BigBlindSeat].TableBet;
            }
            BetNode bet;

            while (true)
            {
                CurrentPlayer = current;
                WaitForBet();
                bet = PlayerBets[current];
                if (bet.PlayerBet == Bet.Call)
                    bet.Value = roundMaxBet - PlayerBySeat[current].TableBet;
                if (bet.PlayerBet == Bet.Fold)
                {
                    Ready[bet.Seat] = false;
                    CurrentBank += PlayerBySeat[current].TableBet;
                    PlayerBySeat[current].TableBet = 0;
                }
                Execute(bet);
                turnsCount++;

                sumOfBetsBySeat[current] += bet.Value;
                var areAllBetsEqual = sumOfBetsBySeat
                    .Where(pair => Ready[pair.Key])
                    .GroupBy(pair => pair.Value)
                    .Count() == 1;
                if (areAllBetsEqual && turnsCount >= playersCount)
                    break;
                current = Next(current + 1);
            }
        }

        public void CheckWinner()
        {
            if (Ready.Count(x => x) == 1)
            {
                // Определение кто это
                for (int i = 0; i < 10; i++)
                {
                    if (Ready[i])
                    {
                        Ready[i] = false;
                        CollectMoney();
                        RewardWinner(i);
                        CurrentBank = 0;
                        return;
                    }
                }
            }
        }

        public void SetInitialRoles()
        {
            DealerSeat = Next(0);
            SmallBlindSeat = Next(DealerSeat + 1);
            BigBlindSeat = Next(SmallBlindSeat + 1);
        }

        public int Next(int start)
        {
            var i = start % 10;
            while (!Ready[i])
            {
                i = (i + 1) % 10;
            }
            return i;
        }

        public void Update()
        {
            Count = Ready.Where(x => x).Count();
            if (Count == 1)
                return;
            DealerSeat = SmallBlindSeat;
            SmallBlindSeat = BigBlindSeat;
            BigBlindSeat = Next(SmallBlindSeat + 1);
        }

        // Плохое ожидание
        private void WaitForBet()
        {
            while (!DidPlayerMakeBet[CurrentPlayer])
            {
                Thread.Sleep(100);
            }
            DidPlayerMakeBet[CurrentPlayer] = false;
        }

        public void Execute(BetNode betNode)
        {
            var player = PlayerBySeat[betNode.Seat];
            player.TableBet += betNode.Value;
            player.ChipBank -= betNode.Value;
            roundMaxBet = Math.Max(player.TableBet, roundMaxBet);
        }

        public void CollectMoney()
        {
            foreach (var seat in PlayerBySeat.Keys)
            {
                CurrentBank += PlayerBySeat[seat].TableBet;
                PlayerBySeat[seat].TableBet = 0;
            }
        }

        public void RewardWinner(int seat)
        {
            PlayerBySeat[seat].ChipBank += CurrentBank;
            CurrentBank = 0;
        }
    }
}
// 4