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

        TextBox BetInputBox = new TextBox
        {
            Text = "",
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

        Button CheckButton = new Button
        {
            Text = "Check",
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
    }
}
