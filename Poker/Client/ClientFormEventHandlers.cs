using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

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
                this.STimer.Tick -= GetState;
                this.STimer.Tick -= UpdatePlayerProfiles;
                this.STimer.Tick -= AskGameStarted;
                Console.WriteLine("Leaved");

                Controls.Remove(GameTable);
                Controls.Add(MenuTable);
            };
            
            // 1) Получение сообщения из поля ввода
            // 2) Отправка сообщения
            // 3) Стирание сообщения из поля ввода
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


            // ----------------------------- MOVES -------------------------------

            // 1) Парс текста
            // 2) Попытка сделать ход
            BetButton.Click += (sender, args) =>
            {
                Console.WriteLine("* <Bet Buttom> *");

                var playerBetString = this.BetInputBox.Text;
                // 1) Парс текста
                var success = int.TryParse(playerBetString, out int bet);
                if (!success) {
                    return;
                }

                if (bet < 2) {
                    return;
                }
                try
                {
                    success = Game.MakeBet(bet);
                }
                catch (CommunicationException ce) {
                    this.STimer.Tick -= GetState;
                    this.STimer.Tick -= UpdatePlayerProfiles;
                    this.STimer.Tick -= AskGameStarted;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(GameTable);
                        this.Controls.Add(MenuTable);
                    }
                    return;
                }
                if (!success) {
                    Console.WriteLine("Fail to Make Bet");
                    return;
                }

                this.BetInputBox.Text = "";
            };

            // 1) Попытка сделать ход
            CallButton.Click += (sender, args) =>
            {
                Console.WriteLine("* <Call Buttom> *");
                var success = false;
                try {
                    success = Game.MakeCall();
                }
                catch (CommunicationException ce)
                {
                    this.STimer.Tick -= GetState;
                    this.STimer.Tick -= UpdatePlayerProfiles;
                    this.STimer.Tick -= AskGameStarted;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(GameTable);
                        this.Controls.Add(MenuTable);
                    }
                }
                if (!success)
                    {
                        Console.WriteLine("Fail to Make Call");
                        return;
                    }
                };

            // 1) Попытка сделать ход
            RaiseButton.Click += (sender, args) =>
            {
                Console.WriteLine("* <Raise Buttom> *");

                var playerBetString = this.BetInputBox.Text;
                // 1) Парс текста
                var success = int.TryParse(playerBetString, out int bet);
                if (!success)
                {
                    return;
                }

                try
                {
                    success = Game.MakeRaise(bet);
                }
                catch (CommunicationException ce)
                {
                    this.STimer.Tick -= GetState;
                    this.STimer.Tick -= UpdatePlayerProfiles;
                    this.STimer.Tick -= AskGameStarted;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(GameTable);
                        this.Controls.Add(MenuTable);
                    }
                }
                if (!success)
                {
                    Console.WriteLine("Fail to Make Raise");
                    return;
                }

                this.BetInputBox.Text = "";
            };

            // 1) Попытка сделать ход
            CheckButton.Click += (sender, args) =>
            {
                Console.WriteLine("* <Check Buttom> *");
                var success = false;
                try
                {
                    success = Game.MakeCheck();
                }
                catch (CommunicationException ce)
                {
                    this.STimer.Tick -= GetState;
                    this.STimer.Tick -= UpdatePlayerProfiles;
                    this.STimer.Tick -= AskGameStarted;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(GameTable);
                        this.Controls.Add(MenuTable);
                    }
                }
                if (!success)
                {
                    Console.WriteLine("Fail to Make Check");
                }
            };

            // 1) Попытка сделать ход
            FoldButton.Click += (sender, args) =>
            {
                Console.WriteLine("* <Fold Buttom> *");
                var success = false;
                try
                {
                    success = Game.MakeFold();
                }
                catch (CommunicationException ce)
                {
                    this.STimer.Tick -= GetState;
                    this.STimer.Tick -= UpdatePlayerProfiles;
                    this.STimer.Tick -= AskGameStarted;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(GameTable);
                        this.Controls.Add(MenuTable);
                    }
                }
                if (!success)
                {
                    Console.WriteLine("Fail to Make Fold");
                }
            };

            // ------------------------ /MOVES --------------------------------

            StartButton.Click += (sender, args) =>
            {
                Console.WriteLine("- <Start Buttom> -");
                if (Game.CurrentState.PlayerCount <= 1)
                    return;
                var success = false;
                try
                {
                    success = Game.Proxy.StartGame(Game.CurrentGameId, Game.PlayerId);
                }
                catch (CommunicationException ce)
                {
                    this.STimer.Tick -= GetState;
                    this.STimer.Tick -= UpdatePlayerProfiles;
                    this.STimer.Tick -= AskGameStarted;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(GameTable);
                        this.Controls.Add(MenuTable);
                    }
                }

                if (!success)
                    return;
                //STimer.Tick -= AskGameStarted;
                Console.WriteLine("Starting Game");
                StartButton.Enabled = false;
                StartButton.Visible = false;
                
            };
        }

        private void SetChooseHandlers()
        {
            BackToMenuFromChooseGameButton.Click += (sender, args) =>
            {
                Console.WriteLine("- <Back Buttom> -");

                this.STimer.Tick -= ChangeStartedGamesBox;
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
                success = Game.SetPlayerNames(targetId);
                if (!success)
                    return;

                Console.WriteLine("Set state of {0}", targetId);

                // 4) Подключение к игре
                try
                {
                    success = Game.TryJoinTheGame(targetId);
                }
                catch (CommunicationException ce) {
                    STimer.Tick -= ChangeStartedGamesBox;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(ChooseGameTable);
                        this.Controls.Add(MenuTable);
                    }
                    return;
                }

                if (!success)
                    return;
                Game.CurrentGameId = targetId;
                Console.WriteLine("Joined the game {0}", targetId);
                this.GameIdLabel.Text = String.Format("Game ID: {0}", targetId.ToString());

                this.STimer.Tick -= ChangeStartedGamesBox;
                this.STimer.Tick += GetState;
                this.STimer.Tick += UpdatePlayerProfiles;
                this.STimer.Tick += AskGameStarted;
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
                var gameIdSting = this.GameIdInput.Text;
                // 1) Парс текста
                var successed = int.TryParse(gameIdSting, out int targetId);

                if (!successed)
                    return;

                Console.WriteLine("Success parsed {0}", targetId);
                // 2) Cоздание игры
                try
                {
                    successed = Game.CreateNewGame(targetId);
                }
                catch (CommunicationException se) {
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption, msgButtons, msgQuestion);
                    if (result == DialogResult.OK)
                    {
                        this.Controls.Remove(CreateGameTable);
                        this.Controls.Add(MenuTable);
                    }
                    return;
                }
                if (!successed)
                    return;

                Console.WriteLine("Game was successfully created");

                Game.CurrentGameId = targetId;
                // 3) Установка имен участников

                Game.SetPlayerNames(Game.CurrentGameId);
                // 4) Подключение к игре
                var success = Game.TryJoinTheGame(Game.CurrentGameId);
                if (!success)
                    return;
                Console.WriteLine("Joined");
                this.GameIdLabel.Text = String.Format("Game ID: {0}", targetId.ToString());
                this.STimer.Tick += GetState;
                this.STimer.Tick += UpdatePlayerProfiles;
                this.STimer.Tick += AskGameStarted;
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
                this.STimer.Tick += ChangeStartedGamesBox;
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
