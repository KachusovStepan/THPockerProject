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
                Console.WriteLine("- <Leave Buttom> -");

                var success = Game.LeaveTheGame();
                if (!success)
                    return;

                Console.WriteLine("Leaved");

                Controls.Remove(GameTable);
                Controls.Add(MenuTable);
            };
            
            SendButton.Click += (sender, args) =>
            {
                Console.WriteLine("- <Send Buttom> -");
                var message = this.ChatMessageInput.Text;
                if (message == "")
                    return;

                Console.WriteLine("Message {0}", message);
                Console.WriteLine("{0} of {1}", Game.PlayerId, Game.CurrentGameId);
                var success = Game.SendMessage(message);
                if (!success)
                    return;

                this.ChatMessageInput.Text = "";

                Console.WriteLine("Sent message \"{0}\"", message);
            };
        }

        private void SetChooseHandlers()
        {
            BackToMenuFromChooseGameButton.Click += (sender, args) =>
            {
                Console.WriteLine("- <Back Buttom> -");

                Controls.Remove(ChooseGameTable);
                Controls.Add(MenuTable);
            };

            // 1) Парс текста
            // 3) Установка имен участников
            // 4) Подключение к игре
            // 5) Переключение таблиц
            JoinButton.Click += (sender, args) =>
            {
                Console.WriteLine("- <Join Buttom> -");

                var gameIdSting = this.InputGameID.Text;
                // 1) Парс текста
                var success = int.TryParse(gameIdSting, out int targetId);

                if (!success)
                    return;

                Console.WriteLine("Success parsed {0}", targetId);

                // 3) Установка имен участников
                success = Game.SetState(targetId);
                if (!success)
                    return;

                Console.WriteLine("Set state of {0}", targetId);

                // 4) Подключение к игре
                success = Game.TryJoinTheGame(targetId);
                if (!success)
                    return;
                Game.CurrentGameId = targetId;
                Console.WriteLine("Joined the game {0}", targetId);

                // 5) Переключение таблиц
                Controls.Remove(ChooseGameTable);
                Controls.Add(GameTable);
            };
        }

        private void SetCreateHandlers()
        {
            // Переключение таблиц
            BackToMenuFromCreateGameButton.Click += (sender, args) =>
            {
                Console.WriteLine("- <Back Buttom> -");
                Controls.Remove(CreateGameTable);
                Controls.Add(MenuTable);
            };

            // 1) Парс текста
            // 2) Cоздание игры
            // 3) Установка имен участников
            // 4) Подключение к игре
            // 5) Переключение таблиц
            CreateGame.Click += (sender, args) =>
            {
                Console.WriteLine("- <CreateGame Buttom> -");
                var gameIdSting = this.MinFeeInput.Text;
                // 1) Парс текста
                var successed = int.TryParse(gameIdSting, out int targetId);

                if (!successed)
                    return;

                Console.WriteLine("Success parsed {0}", targetId);
                // 2) Cоздание игры
                successed = Game.CreateNewGame(targetId);
                if (!successed)
                    return;

                Console.WriteLine("Game was successfully created");

                Game.CurrentGameId = targetId;
                // 3) Установка имен участников
                Game.SetState(Game.CurrentGameId);
                // 4) Подключение к игре
                var success = Game.TryJoinTheGame(Game.CurrentGameId);
                if (!success)
                    return;
                Console.WriteLine("Joined");
                // 5) Переключение таблиц
                Controls.Remove(CreateGameTable);
                Controls.Add(GameTable);
            };
        }

        private void SetOptionsHandlers()
        {
            // Просто переход в новуб таблицу
            BackToMenuFromOptionsButton.Click += (sender, args) =>
            {
                Controls.Remove(OptionsTable);
                Controls.Add(MenuTable);
            };

            // Запись в обьект инры имени игрока
            SubmitName.Click += (sender, args) =>
            {
                var newPlayerName = this.InputPlayerName.Text;
                Game.PlayerName = newPlayerName;
                this.InputPlayerName.Text = "";
                this.ChangeNameLabel.Text = String.Format("Your Name: {0}", newPlayerName);
            };
        }

        private void SetMenuButtonHandlers()
        {
            // Просто переход в новуб таблицу
            CreateGameButton.Click += (sender, args) =>
            {
                Controls.Remove(MenuTable);
                Controls.Add(CreateGameTable);
            };

            // Просто переход в новуб таблицу
            ChooseGameButton.Click += (sender, args) =>
            {
                Controls.Remove(MenuTable);
                Controls.Add(ChooseGameTable);
            };

            // Просто переход в новуб таблицу
            OptionsButton.Click += (sender, args) =>
            {
                Controls.Remove(MenuTable);
                Controls.Add(OptionsTable);
            };

            // Завершение потока таймера и закрытие формы
            ExitButton.Click += (sender, args) =>
            {
                if (TimerThread != null)
                    TimerThread.Abort();
                this.Close();
            };
        }
    }
}
