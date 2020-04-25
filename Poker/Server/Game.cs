using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;

namespace Server
{
    public class BetNode
    {
        public Bet bet;
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

    public class Table
    {
        public int Count = 0;
        public List<PlayerInfo> Players;
        public int currentPlayer;

    }

    public static class Game
    {
        public static int[] IDS = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static List<BetNode> RoundHistory;
        public static Dictionary<int, PlayerInfo> PlayerBySeat;
        public static Dictionary<int, PlayerInfo> PlayerByID;
        public static bool[] Ready = new bool[10];
        public static bool GetName(int id, out string name)
        {
            name = string.Empty;
            if (PlayerByID.ContainsKey(id))
                name = PlayerByID[id].Name;
            return true;
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

        // Колода
        // PlayerInfos
        // Текущий игрок
        // Стол
        // Игроки за столом
        public static void Start()
        {

            // Назначние юлайндов
            // Цикл раундов пока есть хотя бы 2 игрока
            PreFlop();
            Flop();
            Turn();
            River();
            ShowDown();
        }
        private static void PreFlop()
        {
            // Тасовка колоды
            // Выдача 2 карт каждому
            // Круг торгов
            BettingRound();
        }

        private static void Flop()
        {
            // На столе 3 карты
            // Круг торгов
            BettingRound();
        }

        private static void Turn()
        {
            // На столе 1 карты
            // Круг торгов
            BettingRound();
        }

        private static void River()
        {
            // На столе 1 карты
            // Круг торгов
            // Определение итогового размера банка
            BettingRound();
        }

        private static void ShowDown()
        {
            // Вскрытие карт всеми оставшимися
            // Определить игрока с самой старшей комбинацией
            // Прибавить в банк победившего игрока банк игры

        }
        private static void BettingRound()
        {
            // Начиная с игрока слева от BB
            // Текущий игрок делает ставку
            // Если дошло до BB и он сделал Bet or Raise, то еще один круг
            // Если был второй круг 
        }

        private static void WaitForBet()
        {
            // Ожидать сообщения игроком ставки
            // Отаправить корректна ли ставка
        }
    }
}
