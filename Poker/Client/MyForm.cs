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


            // Рисовать стол
            // OnPaint
            // Invalidate ...


            Controls.Add(MenuTable);
            //Controls.Add(OptionsTable);
        }
    }
}
