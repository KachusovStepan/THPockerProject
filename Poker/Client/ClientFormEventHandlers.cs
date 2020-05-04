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
    }
}
