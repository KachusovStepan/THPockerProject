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
        // MenuTable Buttons
        Button ChooseGameButton = new MyButton
        {
            Text = "Choose Game",
            Dock = DockStyle.Fill
        };

        Button CreateGameButton = new MyButton
        {
            Text = "Create Game",
            Dock = DockStyle.Fill
        };

        Button OptionsButton = new MyButton
        {
            Text = "Options",
            Dock = DockStyle.Fill
        };

        Button ExitButton = new MyButton
        {
            Text = "Exit",
            Dock = DockStyle.Fill
        };

        // MenuTable Labels
        Label GameTitle = new Label()
        {
            BackColor = Color.Cyan,
            Text = "Texas Hold'em Poker",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };


        // ChooseGameTable Buttons
        Button BackToMenuFromChooseGameButton = new MyButton
        {
            Text = "Back",
            Dock = DockStyle.Fill
        };

        Button JoinButton = new MyButton
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
            BackColor = Color.Cyan,
            Text = "Choose the Game",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        Label InputGameIdDescriptor = new Label()
        {
            BackColor = Color.Cyan,
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
        Button SubmitName = new MyButton
        {
            Text = "Submit",
            Dock = DockStyle.Fill
        };

        // MenuTable Labels TextBoses
        Label ChangeNameLabel = new Label()
        {
            BackColor = Color.Cyan,
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
        Button BackToMenuFromCreateGameButton = new MyButton
        {
            Text = "Back",
            Dock = DockStyle.Fill
        };

        Button CreateGame = new MyButton
        {
            Text = "Create",
            Dock = DockStyle.Fill
        };

        Label EnterGameIdLabel = new Label()
        {
            Text = "Enter Game Id",
            Dock = DockStyle.Fill,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };

        TextBox GameIdInput = new TextBox()
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
        Button LeaveGameButton = new MyButton
        {
            Text = "Leave The Game",
            Dock = DockStyle.Fill
        };

        Button BetButton = new MyButton
        {
            Text = "Bet",
            Dock = DockStyle.Fill
        };

        TextBox BetInputBox = new TextBox
        {
            Text = "",
            Dock = DockStyle.Fill
        };

        Button CallButton = new MyButton
        {
            Text = "Call",
            Dock = DockStyle.Fill
        };

        Button FoldButton = new MyButton
        {
            Text = "Fold",
            Dock = DockStyle.Fill
        };

        Button RaiseButton = new MyButton
        {
            Text = "Raise",
            Dock = DockStyle.Fill
        };

        Button CheckButton = new MyButton
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

        Button SendButton = new MyButton
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

        Button BackToMenuFromOptionsButton = new MyButton
        {
            Text = "Back",
            Dock = DockStyle.Fill
        };


        // --------------------------------------------------------- TableCards ---------------------------------------
        static PictureBox TableCard1 = new PictureBox()
        {
            BackColor = System.Drawing.Color.Transparent,
            AllowDrop = true,
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
            //SizeMode = PictureBoxSizeMode.StretchImage,
            //Image = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\2c.png")),
            Dock = DockStyle.Fill
            //BackColor = Color.FromArgb(100, 88, 44, 55)
        };

        static PictureBox TableCard2 = new PictureBox()
        {
            BackColor = System.Drawing.Color.Transparent,
            AllowDrop = true,
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
            //SizeMode = PictureBoxSizeMode.StretchImage,
            //Image = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\2d.png")),
            Dock = DockStyle.Fill
            //BackColor = Color.FromArgb(100, 88, 44, 55)
        };

        static PictureBox TableCard3 = new PictureBox()
        {
            BackColor = System.Drawing.Color.Transparent,
            AllowDrop = true,
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
            //SizeMode = PictureBoxSizeMode.StretchImage,
            //Image = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\2h.png")),
            Dock = DockStyle.Fill
            //BackColor = Color.FromArgb(100, 88, 44, 55)
        };

        static PictureBox TableCard4 = new PictureBox()
        {
            BackColor = System.Drawing.Color.Transparent,
            AllowDrop = true,
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
            //SizeMode = PictureBoxSizeMode.StretchImage,
            //Image = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\2s.png")),
            Dock = DockStyle.Fill
            //BackColor = Color.FromArgb(100, 88, 44, 55)
        };

        static PictureBox TableCard5 = new PictureBox()
        {
            BackColor = System.Drawing.Color.Transparent,
            AllowDrop = true,
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
            //SizeMode = PictureBoxSizeMode.StretchImage,
            //Image = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\3c.png")),
            Dock = DockStyle.Fill
            //BackColor = Color.FromArgb(100, 88, 44, 55)
        };

        // ---------------------------------------------- Player Profile Labels
        static Label PlayerProfileLabel1 = new Label
        {
            Text = "Name -- \r\nBank -- <1>"
        };

        static Label PlayerProfileLabel2 = new Label
        {
            Text = "Name -- \r\nBank -- <2>"
        };

        static Label PlayerProfileLabel3 = new Label
        {
            Text = "Name -- \r\nBank -- <3>"
        };

        static Label PlayerProfileLabel4 = new Label
        {
            Text = "Name -- \r\nBank -- <4>"
        };

        static Label PlayerProfileLabel5 = new Label
        {
            Text = "Name -- \r\nBank -- <5>"
        };

        static Label PlayerProfileLabel6 = new Label
        {
            Text = "Name -- \r\nBank -- <6>"
        };

        static Label PlayerProfileLabel7 = new Label
        {
            Text = "Name -- \r\nBank -- <7>"
        };

        static Label PlayerProfileLabel8 = new Label
        {
            Text = "Name -- \r\nBank -- <8>"
        };

        static Label PlayerProfileLabel9 = new Label
        {
            Text = "Name -- \r\nBank -- <9>"
        };

        static Label PlayerProfileLabel10 = new Label
        {
            Text = "Name -- \r\nBank -- <10>"
        };

        static Button StartButton = new Button()
        {
            BackColor = Color.Cyan,
            Text = "Start",
            Dock = DockStyle.Fill
        };

        //static TextBox BankBox = new TextBox
        //{
        //    Text = "0$",
        //    ReadOnly = true
        //};

        static Label BankLabel = new Label
        {
            BackColor = Color.Transparent,
            ForeColor = Color.Gold,
            Text = "0$"
        };

        static Label BankLabel1 = new Label
        {
            BackColor = Color.Transparent,
            BackgroundImage = Image.FromFile(String.Format(Directory.GetCurrentDirectory() + "\\..\\..\\Images\\2h.png")),
            Text = "0$"
        };

        static PictureBox BankPicBox = new PictureBox
        {
            BackColor = System.Drawing.Color.Transparent,
            AllowDrop = true,
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom,
            Location = new Point(200, 100)
        };



        static PictureBox PlayerCard1 = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.StretchImage,
            Dock = DockStyle.Fill
        };

        static PictureBox PlayerCard2 = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.StretchImage,
            Dock = DockStyle.Fill
        };

        static TextBox RuleBox = new TextBox
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


    }
}
