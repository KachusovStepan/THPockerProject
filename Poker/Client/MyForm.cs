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

        Button ChooseTable;
        Button CreateTable;
        Button Exit;
        Button Options;

        Button JoinButton;
        Button BetButton;
        Button CallButton;
        Button RaiseButton;
        Button FoldButton;

        Button SendMessage;

        TextBox SendingBox;
        TextBox ChatBox;

        Button Send;

        Button LeaveButton;
        public ClientForm(GameProxy game) {
            Game = game;
            BindButtons();
        }

        public void BindButtons() {
            
        }
    }
}
