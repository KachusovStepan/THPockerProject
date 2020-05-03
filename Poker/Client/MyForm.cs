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
        Button BackToMenuButton = new Button
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
            Text = "Back to Menu",
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
            Dock = DockStyle.Fill
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


        public ClientForm()
        {
            ClientSize = new Size(600, 600);
            // Menu Table Lining
            InitializeMenu();
            // Choose Game Table Lining
            InitializeChooseGameTable();
            // Game Options Table Lining
            InitializeOptionsTable();
            // Привзяка обработчиков и тд










            //Controls.Add(MenuTable);
            Controls.Add(OptionsTable);
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

            OptionsTable.Controls.Add(BackToMenuButton, 0, 0);
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
            ChooseGameTable.Controls.Add(new Panel(), 2, 0);

            ChooseGameTable.Controls.Add(BackToMenuDescriptor, 0, 1);
            ChooseGameTable.Controls.Add(ChooseGameTitle, 1, 1);
            ChooseGameTable.Controls.Add(InputGameIdDescriptor, 2, 1);

            ChooseGameTable.Controls.Add(BackToMenuButton, 0, 2);
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
