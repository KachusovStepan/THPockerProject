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
            { GameState.ShowTime, 's' }
        };

        private int MinBet;
        public readonly int GameId;
        public static int[] IDS;
        public List<BetNode> RoundHistory;
        public Dictionary<int, PlayerInfo> PlayerBySeat;
        public Dictionary<int, PlayerInfo> PlayerByID;
        public GameState CurrentState;
        public int Count = 0;
        public bool[] Ready;
        public int SB = 0;
        public int BB = 0;
        public int D = 0;
        public bool BetHasBeenMade;
        public int CurrentPlayer;
        public int CurrentBank;
        public List<Card> TableCards;
        public static CardDeck Deck;
        private List<string> chat;
        private int roundMaxBet;
        public readonly Dictionary<int, BetNode> PlayerBets;

        private static Dictionary<int, Game> GameInstances = new Dictionary<int, Game>();

        // Multiton
        private Game(int gameId)
        {
            GameId = gameId;
            IDS = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            RoundHistory = new List<BetNode>();
            PlayerBySeat = new Dictionary<int, PlayerInfo>();
            PlayerByID = new Dictionary<int, PlayerInfo>();
            Ready = new bool[10];
            TableCards = new List<Card>();
            Deck = new CardDeck();
            PlayerBets = new Dictionary<int, BetNode>();
        }
        public static Game GetGameInstance(int gameId)
        {
            if (!GameInstances.ContainsKey(gameId))
            {
                var newGame = new Game(gameId);
                Game.GameInstances.Add(gameId, newGame);
            }

            return GameInstances[gameId];
        }
        

        public bool GetName(int id, out string name)
        {
            name = string.Empty;
            if (PlayerByID.ContainsKey(id))
                name = PlayerByID[id].Name;
            return true;
        }

        public static Dictionary<int, int> ShowGames()
        {
            var res = new Dictionary<int, int>();
            foreach (var gameId in GameInstances.Keys)
                res.Add(gameId, GameInstances[gameId].PlayerBySeat.Count);

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
            var player = new PlayerInfo(name, position);
            var id = IDS.Where(x => !PlayerByID.ContainsKey(x)).FirstOrDefault();
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
            return true;
        }

        public bool AddMessage(int playerId, string playerMessage)
        {
            if (PlayerByID.ContainsKey(playerId))
                return false;
            var playerInfo = PlayerByID[playerId];
            var message = String.Format("[{0}]: {1}", playerInfo.Name, playerMessage);
            chat.Add(message);
            return true;
        }

        public State GetGameState()
        {
            var banks = new Dictionary<int, int>();
            var seatNames = new Dictionary<int, string>();
            foreach (var seat in PlayerBySeat.Keys)
            {
                banks.Add(seat, PlayerBySeat[seat].ChipBank);
                seatNames.Add(seat, PlayerBySeat[seat].Name);
            }
            var blindSeats = new Dictionary<char, int> { { 'd', D }, { 's', SB }, { 'b', BB } };
            var state = new State(PlayerByID.Count, banks, CurrentBank, seatNames, blindSeats, turnChar[CurrentState], CurrentPlayer);
            return state;
        }

        public void Start()
        {
            Func<Action, bool> conductBettingRoungAndReportIfGameIsOver = currentRound =>
            {
                currentRound();
                return Count == 0;
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
                Deck.Shuffle();
                TableCards.Clear();
                setReadyPlayers();
                // Цикл раундов пока есть хотя бы 2 игрока
                if (conductBettingRoungAndReportIfGameIsOver(PreFlop)) break;
                if (conductBettingRoungAndReportIfGameIsOver(Flop)) break;
                if (conductBettingRoungAndReportIfGameIsOver(Turn)) break;
                if (conductBettingRoungAndReportIfGameIsOver(River)) break;
                Update();
            }
        }
        private void PreFlop()
        {
            // TODO  Сделать слепые ставки
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

        private void Flop()
        {
            // На столе 3 карты
            TableCards.Add(Deck.GetCard());
            TableCards.Add(Deck.GetCard());
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void Turn()
        {
            // На стол добавляется 1 карты
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void River()
        {
            // На стол добавляется 1 карты
            TableCards.Add(Deck.GetCard());

            // Круг торгов
            BettingRound();
            CollectMoney();
        }

        private void ShowDown()
        {
            // Вскрытие карт всеми оставшимися
            // Определить игрока с самой старшей комбинацией
            // Прибавить в банк победившего игрока банк игры
            // RewardWinner(int seat)
        }

        // изменил на public для тестов
        public void BettingRound()
        {
            // Начиная с игрока слева от BB
            // Текущий игрок делает ставку
            // Если дошло до BB и он сделал Bet or Raise, то еще один круг
            // Если был второй круг
            var current = Next((D + 1) % 10);
            if (CurrentState == GameState.PreFlop)
                current = Next((BB + 1) % 10);
            var sumOfBetsBySeat = PlayerBets.ToDictionary(pair => pair.Key, pair => 0);
            BetNode bet;

            while (true)
            {
                BetHasBeenMade = false;
                //WaitForBet();
                bet = PlayerBets[current];
                if (bet.PlayerBet == Bet.Call)
                    bet.Value = roundMaxBet;
                if (bet.PlayerBet == Bet.Fold)
                    Ready[bet.Seat] = false;
                Execute(bet);

                sumOfBetsBySeat[current] = bet.Value;
                if (sumOfBetsBySeat
                    .Where(pair => Ready[pair.Value])
                    .GroupBy(pair => pair.Value)
                    .Count() == 1)
                    break;
                current = Next((current + 1) % 10);
            }
            //CheckWinner();
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
                        Count = 0;
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
            D = Next(0);
            SB = Next((D + 1) % 10);
            BB = Next((SB + 1) % 10);
        }

        public int Next(int start)
        {
            var i = start;
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
            D = SB;
            SB = BB;
            BB = Next((SB + 1) % 10);
        }

        // Плохое ожидание
        private void WaitForBet()
        {
            // Ожидать сообщения игроком ставки
            while (!BetHasBeenMade)
            {
                Thread.Sleep(100);
            }
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
