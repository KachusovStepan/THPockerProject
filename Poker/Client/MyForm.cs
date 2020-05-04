using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        GameProxy Game;

        // Таблицы
        TableLayoutPanel MenuTable = new TableLayoutPanel();
        TableLayoutPanel ChooseGameTable = new TableLayoutPanel();
        TableLayoutPanel CreateGameTable = new TableLayoutPanel();
        TableLayoutPanel OptionsTable = new TableLayoutPanel();
        TableLayoutPanel GameTable = new TableLayoutPanel();

        TableLayoutPanel MovesChatTable = new TableLayoutPanel();
        TableLayoutPanel MovesTable = new TableLayoutPanel();
        TableLayoutPanel ChatTable = new TableLayoutPanel();

        Thread TimerThread;
        

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

            // Запуск таймера

            Action<int> changeChat = (int t) => {
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
            };

            var timer = new SyncTimer();
            timer.Interval = 500;
            timer.Tick += changeChat;

            var thR = new Thread(() =>
            {
                timer.Start();
            });

            thR.Start();

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
