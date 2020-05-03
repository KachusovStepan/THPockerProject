using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class ClientForm : Form
    {
        GameProxy Game;

        Button ChooseTable = new Button
        {
            Text = "Choose a Table"
        };
        Button CreateTable = new Button
        {
            Text = "Create a Table"
        };
        Button Exit = new Button
        {
            Text = "Exit"
        };
        Button Options;

        Button JoinButton = new Button
        {
            Text = "Join a Table"
        };
        Button BetButton = new Button
        {
            Text = "Bet"
        };
        Button CallButton = new Button
        {
            Text = "Call"
        };
        Button RaiseButton = new Button
        {
            Text = "Raise"
        };
        Button FoldButton = new Button
        {
            Text = "Fold"
        };

        Button SendMessage = new Button
        {
            Text = "Send"
        };

        TextBox SendingBox;
        TextBox ChatBox;

        Button LeaveButton = new Button
        {
            Text = "Leave the Table"
        };

        public ClientForm(GameProxy game) {
            Game = game;
            BindButtons();
        }

        public void BindButtons() {
            
        }
    }
}
