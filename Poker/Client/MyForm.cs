using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class ClientForm : Form
    {
        GameProxy Game;
        SyncTimer STimer;
        Size PokerTableSize = new Size(600, 300);
        Dictionary<int, Label> PlayerProfileLabels = new Dictionary<int, Label>()
        {
            { 0, PlayerProfileLabel1},
            { 1, PlayerProfileLabel2},
            { 2, PlayerProfileLabel3},
            { 3, PlayerProfileLabel4},
            { 4, PlayerProfileLabel5},
            { 5, PlayerProfileLabel6},
            { 6, PlayerProfileLabel7},
            { 7, PlayerProfileLabel8},
            { 8, PlayerProfileLabel9},
            { 9, PlayerProfileLabel10}
        };
        // Таблицы
        TableLayoutPanel MenuTable = new TableLayoutPanel();
        TableLayoutPanel ChooseGameTable = new TableLayoutPanel();
        TableLayoutPanel CreateGameTable = new TableLayoutPanel();
        TableLayoutPanel OptionsTable = new TableLayoutPanel();
        TableLayoutPanel GameTable = new TableLayoutPanel();

        TableLayoutPanel MovesChatTable = new TableLayoutPanel();
        TableLayoutPanel MovesTable = new TableLayoutPanel();
        TableLayoutPanel ChatTable = new TableLayoutPanel();
        TableLayoutPanel TableCardsTable = new TableLayoutPanel();

        Panel TablePanel = new Panel() {
            Dock = DockStyle.Fill
        };

        Thread TimerThread;

        // Background updating
        Action<int> GetState;
        Action<int> ChangeStartedGamesBox;
        Action<int> UpdatePlayerProfiles;
        Action<int> AskGameStarted;

        

        public ClientForm(GameProxy game)
        {
            Game = game;


            ClientSize = new Size(600, 600);
            // Menu Table Lining
            InitializeMenu();
            // Choose Game Table Lining
            InitializeChooseGameTable();
            // Game Options Table Lining
            InitializeOptionsTable();
            // Game Creation Table Lining
            InitializeCreateTable();
            // Game Table Lining
            InitializeGameTable();
            // Привзяка обработчиков и тд


            SetMenuButtonHandlers();

            SetOptionsHandlers();

            SetCreateHandlers();

            SetChooseHandlers();

            SetGameHandlers();

            // Делегат для обновления чата
            GetState = (int t) => {
                var currPlayerCount = Game.PlayerNames.Count;
                var success = Game.UpdateRegularData();
                if (!success) {
                    return;
                }
                for (int i = Math.Max(Game.MessagePointer, 0); i < Game.CurrentState.Chat.Count; i++)
                {
                    Game.MessagePointer++;
                    if (this.GameChatBox.Text != "")
                        this.GameChatBox.Text += "\r\n";
                    this.GameChatBox.Text += Game.CurrentState.Chat[i];
                }

                if (currPlayerCount != Game.CurrentState.PlayerCount) {
                    Game.SetPlayerNames(Game.CurrentGameId);
                }
            };

            UpdatePlayerProfiles = (int t) =>
            {
                for (int i = 0; i < 10; i++) {
                    if (Game.CurrentState.Banks.ContainsKey(i))
                    {
                        PlayerProfileLabels[i].Visible = true;
                        if (Game.CurrentState.CurrentPlayer == i)
                            PlayerProfileLabels[i].Text = String.Format("> {1}\r\n{0}$\r\nBet: {2}$", Game.CurrentState.Banks[i], Game.PlayerNames[i], Game.CurrentState.TableBanks[i]);
                        else
                            PlayerProfileLabels[i].Text = String.Format("{1}\r\n{0}$\r\nBet: {2}$", Game.CurrentState.Banks[i], Game.PlayerNames[i], Game.CurrentState.TableBanks[i]);
                    }
                    else {
                        PlayerProfileLabels[i].Visible = false;
                    }
                }

            };

            AskGameStarted = (int t) =>
            {
                Console.WriteLine("Asking Game -------------------------<");
                var started = Game.Proxy.IsGameStarted(Game.CurrentGameId);
                if (started) {
                    STimer.Tick -= AskGameStarted;
                    StartButton.Enabled = false;
                    StartButton.Visible = false;
                }
            };


            // Делегат для обновления запущенных игр
            ChangeStartedGamesBox = (int t) =>
            {
                var success = Game.GetExistingGamesWithPlayerCount();
                if (!success)
                    return;
                var builder = new StringBuilder();
                foreach (var key in Game.GamePlayerCountDict.Keys) {
                    builder.Append(String.Format("Game ID: {0}; Player Count: {1}\r\n", key, Game.GamePlayerCountDict[key]));
                }
                this.StartedGames.Text = builder.ToString();
            };


            // Таймер может лечь при переполнении, но можно пока оставить
            STimer = new SyncTimer();
            STimer.Interval = 500;

            var thR = new Thread(() =>
            {
                STimer.Start();
            });
            
            // Запуск потока с таймера
            thR.Start();

            // Чтобы корректно освободить поток с таймером перед завершением
            this.FormClosing += (sender, args) =>
            {
                thR.Abort();
            };



            // Рисовать стол
            // OnPaint
            // Invalidate ...


            Controls.Add(MenuTable);
            //Controls.Add(OptionsTable);
        }
    }
}
