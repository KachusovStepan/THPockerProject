using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm
    {
        private void InitializeGameTable()
        {
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Sky
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10)); // Title
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Image
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 5)); // Title
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // Moves Chat
            GameTable.RowStyles.Add(new RowStyle(SizeType.Percent, 5)); // Basement


            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Left
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80)); // Moves Chat
            GameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10)); // Right

            GameTable.Dock = DockStyle.Fill;

            // Sky
            GameTable.Controls.Add(LeaveGameButton, 0, 0);
            GameTable.Controls.Add(new Panel(), 1, 0);
            GameTable.Controls.Add(GameIdLabel, 2, 0);

            // Title
            GameTable.Controls.Add(new Panel(), 0, 1);
            GameTable.Controls.Add(new Panel(), 1, 1);
            GameTable.Controls.Add(new Panel(), 2, 1);

            // Poker Table (image-graphics)
            GameTable.Controls.Add(new Panel(), 0, 2); // Left
            GameTable.Controls.Add(new Panel(), 1, 2); // Poker Table
            GameTable.Controls.Add(new Panel(), 2, 2); // Right

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
            MovesChatTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            MovesChatTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            MovesChatTable.Dock = DockStyle.Fill;
            GameTable.Controls.Add(MovesChatTable, 1, 4);


            // --------------------------------- MOVES -------------------------------------
            MovesTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Call Check BetInput
            MovesTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); // Raise Fold Bet

            MovesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33)); // Call Raise
            MovesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33)); // Check Fold
            MovesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34)); // BetInput Bet

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

            ChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            ChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            ChatTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            ChatTable.Controls.Add(GameChatBox, 0, 0);
            ChatTable.Controls.Add(ChatMessageInput, 0, 1);
            ChatTable.Controls.Add(SendButton, 0, 2);

            ChatTable.Dock = DockStyle.Fill;
            MovesChatTable.Controls.Add(ChatTable, 1, 0);


            // ---------------------------------- /ChatBox -------------------------------------



            GameTable.Controls.Add(new Panel(), 0, 5);
            GameTable.Controls.Add(new Panel(), 1, 5);
            GameTable.Controls.Add(new Panel(), 2, 5);
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
