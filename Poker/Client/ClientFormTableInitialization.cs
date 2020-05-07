using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class ClientForm
    {

        private void InitializeGameTable()
        {
            GameTable.RowCount = 6;
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Title
            GameTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 300)); // Image
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 5)); // Title
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Moves Chat
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 5)); // Basement

            GameTable.ColumnCount = 3;
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Left
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 600)); // Moves Chat
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Right

            GameTable.BackColor = System.Drawing.Color.Transparent;

            GameTable.Dock = DockStyle.Fill;

            // Sky
            GameTable.Controls.Add(LeaveGameButton, 0, 0);
            GameTable.Controls.Add(StartButton, 1, 0);
            GameTable.Controls.Add(GameIdLabel, 2, 0);

            // Title
            GameTable.Controls.Add(new Panel(), 0, 1);
            GameTable.Controls.Add(new Panel(), 1, 1);
            GameTable.Controls.Add(new Panel(), 2, 1);

            // Poker Table (image-graphics)
            GameTable.Controls.Add(new Panel(), 0, 2); // Left
            GameTable.Controls.Add(TablePanel, 1, 2); // Poker Table
            
            GameTable.Controls.Add(RuleBox, 2, 2); // Right

            // Title
            GameTable.Controls.Add(new Panel(), 0, 3);
            GameTable.Controls.Add(new Panel(), 1, 3);
            GameTable.Controls.Add(new Panel(), 2, 3);

            // Controls
            //GameTable.Controls.Add(new Panel(), 0, 4);
            //GameTable.Controls.Add(BetButton, 1, 4);
            //GameTable.Controls.Add(CallButton, 2, 4);
            //GameTable.Controls.Add(new Panel(), 3, 4);
            //GameTable.Controls.Add(ChatMessageInput, 4, 4);
            //GameTable.Controls.Add(new Panel(), 5, 4);
            //
            MovesChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            MovesChatTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            MovesChatTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            MovesChatTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            MovesChatTable.Dock = DockStyle.Fill;
            GameTable.Controls.Add(MovesChatTable, 1, 4);


            // --------------------------------- MOVES -------------------------------------
            MovesTable.RowCount = 2;
            MovesTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Call Check BetInput
            MovesTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Raise Fold Bet

            MovesTable.ColumnCount = 3;
            MovesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33)); // Call Raise
            MovesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33)); // Check Fold
            MovesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34)); // BetInput Bet

            MovesTable.BackColor = System.Drawing.Color.Transparent;

            MovesTable.Controls.Add(CallButton, 0, 0);
            MovesTable.Controls.Add(CheckButton, 1, 0);
            MovesTable.Controls.Add(BetInputBox, 2, 0);

            MovesTable.Controls.Add(RaiseButton, 0, 1);
            MovesTable.Controls.Add(FoldButton, 1, 1);
            MovesTable.Controls.Add(BetButton, 2, 1);

            MovesTable.Dock = DockStyle.Fill;

            MovesChatTable.Controls.Add(MovesTable, 0, 0);

            // ---------------------------------- /MOVES -------------------------------------


            // --------------------------------- ChatBox -------------------------------------
            ChatTable.RowCount = 3;
            ChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            ChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            ChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            ChatTable.ColumnCount = 1;
            ChatTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // New

            ChatTable.BackColor = System.Drawing.Color.Transparent;
            ChatTable.Controls.Add(GameChatBox, 0, 0);
            ChatTable.Controls.Add(ChatMessageInput, 0, 1);
            ChatTable.Controls.Add(SendButton, 0, 2);

            ChatTable.Dock = DockStyle.Fill;
            MovesChatTable.Controls.Add(ChatTable, 1, 0);


            // ---------------------------------- /ChatBox -------------------------------------

            // ---------------------------------- PlayerCards ------------------------------------
            PlayerCardsTable.RowCount = 1;
            PlayerCardsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            PlayerCardsTable.ColumnCount = 2;
            PlayerCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            PlayerCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            PlayerCardsTable.BackColor = System.Drawing.Color.Transparent;

            PlayerCardsTable.Dock = DockStyle.Fill;
            PlayerCardsTable.Controls.Add(PlayerCard1, 0, 0);
            PlayerCardsTable.Controls.Add(PlayerCard2, 1, 0);

            MovesChatTable.Controls.Add(PlayerCardsTable, 2, 0);
            // ---------------------------------- /PlayerCards

            // ------------------------------------ CardTable ----------------------------
            TableCardsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            TableCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            TableCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            TableCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            TableCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            TableCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            TableCardsTable.BackColor = System.Drawing.Color.Transparent;
            //TableCardsTable.BackColor = System.Drawing.Color.Green;
            TableCardsTable.AllowDrop = true;
            TableCardsTable.Controls.Add(TableCard1, 0, 0);
            TableCardsTable.Controls.Add(TableCard2, 1, 0);
            TableCardsTable.Controls.Add(TableCard3, 2, 0);
            TableCardsTable.Controls.Add(TableCard4, 3, 0);
            TableCardsTable.Controls.Add(TableCard5, 4, 0);

            


            //TableCardsTable.Size = new Size((int)(PokerTableSize.Width * 0.30), (int)(PokerTableSize.Height * 0.22));
            TableCardsTable.Size = new Size((int)(PokerTableSize.Width * 0.30), (int)(PokerTableSize.Height * 0.17));
            TableCardsTable.Location = new Point((int)(PokerTableSize.Width * 0.35), (int)(PokerTableSize.Height * 0.35));

            // ------------------------------------ Bank ---------------------------------
            BankLabel.Size = new Size((int)(PokerTableSize.Width * 0.12), (int)(PokerTableSize.Height * 0.07)); // 12
            BankLabel.Location = new Point((int)(PokerTableSize.Width * 0.44), (int)(PokerTableSize.Height * 0.70)); // 0.62
            // ------------------------------------ /Bank -------------------------------

            // -------------------------------------- Profiles =-------------------
            var ProfileSize = new Size((int)(PokerTableSize.Width * 0.12), (int)(PokerTableSize.Height * 0.12));
            foreach (var seatNum in PlayerProfileLabels.Keys)
            {
                PlayerProfileLabels[seatNum].Size = ProfileSize;
            }
            PlayerProfileLabel1.Location = new Point((int)(PokerTableSize.Width * 0.72), (int)(PokerTableSize.Height * 0.04));
            PlayerProfileLabel2.Location = new Point((int)(PokerTableSize.Width * 0.84), (int)(PokerTableSize.Height * 0.25));
            PlayerProfileLabel3.Location = new Point((int)(PokerTableSize.Width * 0.84), (int)(PokerTableSize.Height * 0.62));
            PlayerProfileLabel4.Location = new Point((int)(PokerTableSize.Width * 0.72), (int)(PokerTableSize.Height * 0.83));
            PlayerProfileLabel5.Location = new Point((int)(PokerTableSize.Width * 0.44), (int)(PokerTableSize.Height * 0.83));
            PlayerProfileLabel6.Location = new Point((int)(PokerTableSize.Width * 0.16), (int)(PokerTableSize.Height * 0.83));
            PlayerProfileLabel7.Location = new Point((int)(PokerTableSize.Width * 0.04), (int)(PokerTableSize.Height * 0.62));
            PlayerProfileLabel8.Location = new Point((int)(PokerTableSize.Width * 0.04), (int)(PokerTableSize.Height * 0.25));
            PlayerProfileLabel9.Location = new Point((int)(PokerTableSize.Width * 0.16), (int)(PokerTableSize.Height * 0.04));
            PlayerProfileLabel10.Location = new Point((int)(PokerTableSize.Width * 0.44), (int)(PokerTableSize.Height * 0.04));

            // ---------------------------------------/ Profiles -------------------

            // -------------------------------- /CardTable ---------------------------------


            GameTable.Controls.Add(new Panel(), 0, 5);
            GameTable.Controls.Add(new Panel(), 1, 5);
            GameTable.Controls.Add(new Panel(), 2, 5);


            // ---------------------------- ADDING CONTROLS to TABLE PANEL
            TablePanel.Controls.Add(BankLabel);  //< << <<< <<< ,<< ,<<<<< --------------------------------------------
            //BankPicBox.BackgroundImage = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\bigBank.png"));
            BankPicBox.Size = new Size((int)(PokerTableSize.Width * 0.30), (int)(PokerTableSize.Height * 0.17));
            BankPicBox.Location = new Point((int)(PokerTableSize.Width * 0.35), (int)(PokerTableSize.Height * 0.52));
            TablePanel.Controls.Add(BankPicBox);
            TablePanel.BackgroundImage = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\TableT1.png"));
            TablePanel.BackgroundImageLayout = ImageLayout.Stretch;
            TablePanel.Dock = DockStyle.Fill;
            foreach (var seatNum in PlayerProfileLabels.Keys)
            {
                TablePanel.Controls.Add(PlayerProfileLabels[seatNum]);
            }

            TablePanel.Controls.Add(TableCardsTable);

            //TablePanel.Controls.Add(PBox);
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

            CreateGameTable.BackColor = System.Drawing.Color.Transparent;

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
            CreateGameTable.Controls.Add(EnterGameIdLabel, 1, 3);
            CreateGameTable.Controls.Add(new Panel(), 2, 3);

            CreateGameTable.Controls.Add(new Panel(), 0, 4);
            CreateGameTable.Controls.Add(GameIdInput, 1, 4);
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

            OptionsTable.BackColor = System.Drawing.Color.Transparent;

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

            ChooseGameTable.BackColor = System.Drawing.Color.Transparent;

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

            MenuTable.BackColor = System.Drawing.Color.Transparent;

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
