using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class ClientForm : Form
    {
        GameProxy Game;

        // Таблицы
        TableLayoutPanel MenuTable = new TableLayoutPanel();
        TableLayoutPanel ChooseGameTable = new TableLayoutPanel();
        TableLayoutPanel CreateGameTable = new TableLayoutPanel();
        TableLayoutPanel OptionsTable = new TableLayoutPanel();
        TableLayoutPanel GameTable = new TableLayoutPanel();

        // MenuTable Buttons
        Button ChooseGameButton = new Button
        {
            Text = "Choose Game",
            Dock = DockStyle.Fill
        };

        Button CreateGameButton = new Button
        {
            Text = "Create Game",
            Dock = DockStyle.Fill
        };

        Button OptionsButton = new Button
        {
            Text = "Options",
            Dock = DockStyle.Fill
        };

        Button ExitButton = new Button
        {
            Text = "Exit",
            Dock = DockStyle.Fill
        };

        // MenuTable Labels
        Label GameTitle = new Label()
        {
            Text = "Texas Hold'em Poker",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };


        // ChooseGameTable Buttons
        Button BackToMenuFromChooseGameButton = new Button
        {
            Text = "Back",
            Dock = DockStyle.Fill
        };

        Button JoinButton = new Button
        {
            Text = "Join",
            Dock = DockStyle.Fill
        };

        // ChooseGameTable Labels
        Label BackToMenuDescriptor = new Label()
        {
            Text = "Back to Menu",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };


        Label ChooseGameTitle = new Label()
        {
            Text = "Choose the Game",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        Label InputGameIdDescriptor = new Label()
        {
            Text = "Input game id to join",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };


        // ChooseGameTable Labels
        TextBox InputGameID = new TextBox()
        {
            Dock = DockStyle.Fill
        };

        TextBox StartedGames = new TextBox()
        {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            AcceptsReturn = true,
            AcceptsTab = true,
            WordWrap = true,
            Text = "",
            Dock = DockStyle.Fill,
            ReadOnly = true
        };


        // MenuTable Buttons
        Button SubmitName = new Button
        {
            Text = "Submit",
            Dock = DockStyle.Fill
        };

        // MenuTable Labels TextBoses
        Label ChangeNameLabel = new Label()
        {
            Text = "Change Name",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        TextBox InputPlayerName = new TextBox()
        {
            Text = "Unknown",
            Dock = DockStyle.Fill
        };


        // InitializeCreateTable Buttons  -------------------------------------------
        Button BackToMenuFromCreateGameButton = new Button
        {
            Text = "Back",
            Dock = DockStyle.Fill
        };

        Button CreateGame = new Button
        {
            Text = "Create",
            Dock = DockStyle.Fill
        };

        Label EnterMinFeeLabel = new Label()
        {
            Text = "Enter Min Fee",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        TextBox MinFeeInput = new TextBox()
        {
            Text = "2",
            Dock = DockStyle.Fill
        };

        Label CreateGameLabel = new Label()
        {
            Text = "Create Game",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };


        // Game Buttons
        Button LeaveGameButton = new Button
        {
            Text = "Leave The Game",
            Dock = DockStyle.Fill
        };

        Button BetButton = new Button
        {
            Text = "Bet",
            Dock = DockStyle.Fill
        };

        Button CallButton = new Button
        {
            Text = "Call",
            Dock = DockStyle.Fill
        };

        Button FoldButton = new Button
        {
            Text = "Fold",
            Dock = DockStyle.Fill
        };

        Button RaiseButton = new Button
        {
            Text = "Raise",
            Dock = DockStyle.Fill
        };

        TextBox GameChatBox = new TextBox()
        {
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            AcceptsReturn = true,
            AcceptsTab = true,
            WordWrap = true,
            Text = "",
            Dock = DockStyle.Fill,
            ReadOnly = true
        };

        TextBox ChatMessageInput = new TextBox()
        {
            Text = "",
            Dock = DockStyle.Fill
        };

        Button SendButton = new Button
        {
            Text = "Send",
            Dock = DockStyle.Fill
        };

        Label GameIdLabel = new Label()
        {
            Text = "GameId: - - - ",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        Button BackToMenuFromOptionsButton = new Button
        {
            Text = "Back",
            Dock = DockStyle.Fill
        };

        public ClientForm()
        {
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








            Controls.Add(MenuTable);
            //Controls.Add(OptionsTable);
        }

        private void SetGameHandlers()
        {
            LeaveGameButton.Click += (sender, args) =>
            {
                Controls.Remove(GameTable);
                Controls.Add(MenuTable);
            };
        }

        private void SetChooseHandlers()
        {
            BackToMenuFromChooseGameButton.Click += (sender, args) =>
            {
                Controls.Remove(ChooseGameTable);
                Controls.Add(MenuTable);
            };

            JoinButton.Click += (sender, args) =>
            {
                Controls.Remove(ChooseGameTable);
                Controls.Add(GameTable);
            };
        }

        private void SetCreateHandlers()
        {
            BackToMenuFromCreateGameButton.Click += (sender, args) =>
            {
                Controls.Remove(CreateGameTable);
                Controls.Add(MenuTable);
            };

            CreateGame.Click += (sender, args) =>
            {
                Controls.Remove(CreateGameTable);
                Controls.Add(GameTable);
            };
        }

        private void SetOptionsHandlers()
        {
            BackToMenuFromOptionsButton.Click += (sender, args) =>
            {
                Controls.Remove(OptionsTable);
                Controls.Add(MenuTable);
            };
        }


        private void SetMenuButtonHandlers()
        {
            CreateGameButton.Click += (sender, args) =>
            {
                Controls.Remove(MenuTable);
                Controls.Add(CreateGameTable);
            };

            ChooseGameButton.Click += (sender, args) =>
            {
                Controls.Remove(MenuTable);
                Controls.Add(ChooseGameTable);
            };

            OptionsButton.Click += (sender, args) =>
            {
                Controls.Remove(MenuTable);
                Controls.Add(OptionsTable);
            };
        }

        private void InitializeGameTable()
        {
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Title
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Image
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Title
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Moves
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Moves
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Basement


            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Left
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Bet Call
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Fold Raise
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40)); // Chat
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Smf
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Right

            GameTable.Dock = DockStyle.Fill;

            GameTable.Controls.Add(LeaveGameButton, 0, 0);
            GameTable.Controls.Add(new Panel(), 1, 0);
            GameTable.Controls.Add(new Panel(), 2, 0);
            GameTable.Controls.Add(GameIdLabel, 3, 0);
            GameTable.Controls.Add(new Panel(), 4, 0);
            GameTable.Controls.Add(new Panel(), 5, 0);

            GameTable.Controls.Add(new Panel(), 0, 1);
            GameTable.Controls.Add(new Panel(), 1, 1);
            GameTable.Controls.Add(new Panel(), 2, 1);
            GameTable.Controls.Add(new Panel(), 3, 1);
            GameTable.Controls.Add(new Panel(), 4, 1);
            GameTable.Controls.Add(new Panel(), 5, 1);

            GameTable.Controls.Add(new Panel(), 0, 2);
            GameTable.Controls.Add(new Panel(), 1, 2);
            GameTable.Controls.Add(new Panel(), 2, 2);
            GameTable.Controls.Add(new Panel(), 3, 2);
            GameTable.Controls.Add(new Panel(), 4, 2);
            GameTable.Controls.Add(new Panel(), 5, 2);

            GameTable.Controls.Add(new Panel(), 0, 3);
            GameTable.Controls.Add(new Panel(), 1, 3);
            GameTable.Controls.Add(new Panel(), 2, 3);
            GameTable.Controls.Add(new Panel(), 3, 3);
            GameTable.Controls.Add(new Panel(), 4, 3);
            GameTable.Controls.Add(new Panel(), 5, 3);

            GameTable.Controls.Add(new Panel(), 0, 4);
            GameTable.Controls.Add(BetButton, 1, 4);
            GameTable.Controls.Add(CallButton, 2, 4);
            GameTable.Controls.Add(new Panel(), 3, 4);
            GameTable.Controls.Add(ChatMessageInput, 4, 4);
            GameTable.Controls.Add(new Panel(), 5, 4);

            GameTable.Controls.Add(new Panel(), 0, 5);
            GameTable.Controls.Add(RaiseButton, 1, 5);
            GameTable.Controls.Add(FoldButton, 2, 5);
            GameTable.Controls.Add(GameChatBox, 3, 5);
            GameTable.Controls.Add(SendButton, 4, 5);
            GameTable.Controls.Add(new Panel(), 5, 5);

            GameTable.Controls.Add(new Panel(), 0, 6);
            GameTable.Controls.Add(new Panel(), 1, 6);
            GameTable.Controls.Add(new Panel(), 2, 6);
            GameTable.Controls.Add(new Panel(), 3, 6);
            GameTable.Controls.Add(new Panel(), 4, 6);
            GameTable.Controls.Add(new Panel(), 5, 6);
        }

        private void InitializeCreateTable()
        {
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Title
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Image
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Min Fee
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Input
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Start
            CreateGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Basement

            CreateGameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Left Button "Back"
            CreateGameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80)); // Main
            CreateGameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Right

            CreateGameTable.Dock = DockStyle.Fill;

            CreateGameTable.Controls.Add(BackToMenuFromCreateGameButton, 0, 0);
            CreateGameTable.Controls.Add(new Panel(), 1, 0);
            CreateGameTable.Controls.Add(new Panel(), 2, 0);

            CreateGameTable.Controls.Add(new Panel(), 0, 1);
            CreateGameTable.Controls.Add(CreateGameLabel, 1, 1);
            CreateGameTable.Controls.Add(new Panel(), 2, 1);

            CreateGameTable.Controls.Add(new Panel(), 0, 2);
            CreateGameTable.Controls.Add(new Panel(), 1, 2);
            CreateGameTable.Controls.Add(new Panel(), 2, 2);

            CreateGameTable.Controls.Add(new Panel(), 0, 3);
            CreateGameTable.Controls.Add(EnterMinFeeLabel, 1, 3);
            CreateGameTable.Controls.Add(new Panel(), 2, 3);

            CreateGameTable.Controls.Add(new Panel(), 0, 4);
            CreateGameTable.Controls.Add(MinFeeInput, 1, 4);
            CreateGameTable.Controls.Add(new Panel(), 2, 4);

            CreateGameTable.Controls.Add(new Panel(), 0, 5);
            CreateGameTable.Controls.Add(CreateGame, 1, 5);
            CreateGameTable.Controls.Add(new Panel(), 2, 5);

            CreateGameTable.Controls.Add(new Panel(), 0, 6);
            CreateGameTable.Controls.Add(new Panel(), 1, 6);
            CreateGameTable.Controls.Add(new Panel(), 2, 6);
        }

        private void InitializeOptionsTable()
        {
            OptionsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Sky
            OptionsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Title
            OptionsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Name
            OptionsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Submit
            OptionsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 30)); // Basement

            OptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Left Button "Back"
            OptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60)); // Main
            OptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Right

            OptionsTable.Dock = DockStyle.Fill;

            OptionsTable.Controls.Add(BackToMenuFromOptionsButton, 0, 0);
            OptionsTable.Controls.Add(new Panel(), 1, 0);
            OptionsTable.Controls.Add(new Panel(), 2, 0);

            OptionsTable.Controls.Add(new Panel(), 0, 1);
            OptionsTable.Controls.Add(ChangeNameLabel, 1, 1);
            OptionsTable.Controls.Add(new Panel(), 2, 1);

            OptionsTable.Controls.Add(new Panel(), 0, 2);
            OptionsTable.Controls.Add(InputPlayerName, 1, 2);
            OptionsTable.Controls.Add(new Panel(), 2, 2);

            OptionsTable.Controls.Add(new Panel(), 0, 3);
            OptionsTable.Controls.Add(SubmitName, 1, 3);
            OptionsTable.Controls.Add(new Panel(), 2, 3);

            OptionsTable.Controls.Add(new Panel(), 0, 4);
            OptionsTable.Controls.Add(new Panel(), 1, 4);
            OptionsTable.Controls.Add(new Panel(), 2, 4);
        }

        private void InitializeChooseGameTable()
        {
            ChooseGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky
            ChooseGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky2
            ChooseGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Title
            ChooseGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Game List
            ChooseGameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Basement

            ChooseGameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Left Button "Back"
            ChooseGameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60)); // Main
            ChooseGameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Right Input Game Id + Join

            ChooseGameTable.Dock = DockStyle.Fill;

            ChooseGameTable.Controls.Add(new Panel(), 0, 0);
            ChooseGameTable.Controls.Add(new Panel(), 1, 0);
            ChooseGameTable.Controls.Add(InputGameIdDescriptor, 2, 0);

            ChooseGameTable.Controls.Add(BackToMenuDescriptor, 0, 1);
            ChooseGameTable.Controls.Add(ChooseGameTitle, 1, 1);
            ChooseGameTable.Controls.Add(InputGameID, 2, 1);

            ChooseGameTable.Controls.Add(BackToMenuFromChooseGameButton, 0, 2);
            ChooseGameTable.Controls.Add(new Panel(), 1, 2);
            ChooseGameTable.Controls.Add(JoinButton, 2, 2);

            ChooseGameTable.Controls.Add(new Panel(), 0, 3);
            ChooseGameTable.Controls.Add(StartedGames, 1, 3);
            ChooseGameTable.Controls.Add(new Panel(), 2, 3);

            ChooseGameTable.Controls.Add(new Panel(), 0, 4);
            ChooseGameTable.Controls.Add(new Panel(), 1, 4);
            ChooseGameTable.Controls.Add(new Panel(), 2, 4);
        }

        private void InitializeMenu()
        {
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16)); // Title
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16)); // Choose Game
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16)); // Create Game
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16)); // Options
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 16)); // Exit
            MenuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Basement

            MenuTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Left
            MenuTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60)); // Main
            MenuTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20)); // Right

            MenuTable.Dock = DockStyle.Fill;

            // Adding Buttons to Main Panel
            MenuTable.Controls.Add(new Panel(), 0, 0);
            MenuTable.Controls.Add(new Panel(), 1, 0);
            MenuTable.Controls.Add(new Panel(), 2, 0);

            MenuTable.Controls.Add(new Panel(), 0, 1);
            MenuTable.Controls.Add(GameTitle, 1, 1);
            MenuTable.Controls.Add(new Panel(), 2, 1);

            MenuTable.Controls.Add(new Panel(), 0, 2);
            MenuTable.Controls.Add(ChooseGameButton, 1, 2);
            MenuTable.Controls.Add(new Panel(), 2, 2);

            MenuTable.Controls.Add(new Panel(), 0, 3);
            MenuTable.Controls.Add(CreateGameButton, 1, 3);
            MenuTable.Controls.Add(new Panel(), 2, 3);

            MenuTable.Controls.Add(new Panel(), 0, 4);
            MenuTable.Controls.Add(OptionsButton, 1, 4);
            MenuTable.Controls.Add(new Panel(), 2, 4);

            MenuTable.Controls.Add(new Panel(), 0, 5);
            MenuTable.Controls.Add(ExitButton, 1, 5);
            MenuTable.Controls.Add(new Panel(), 2, 5);

            MenuTable.Controls.Add(new Panel(), 0, 6);
            MenuTable.Controls.Add(new Panel(), 1, 6);
            MenuTable.Controls.Add(new Panel(), 2, 6);
        }
    }
}
