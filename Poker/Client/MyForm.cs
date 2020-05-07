using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class ClientForm : Form
    {
        GameProxy Game;
        SyncTimer STimer;
        Size PokerTableSize = new Size(600, 300);
        
        Dictionary<int, Label> PlayerProfileLabels = new Dictionary<int, Label>()
        {
            { 0, PlayerProfileLabel1},
            { 1, PlayerProfileLabel2},
            { 2, PlayerProfileLabel3},
            { 3, PlayerProfileLabel4},
            { 4, PlayerProfileLabel5},
            { 5, PlayerProfileLabel6},
            { 6, PlayerProfileLabel7},
            { 7, PlayerProfileLabel8},
            { 8, PlayerProfileLabel9},
            { 9, PlayerProfileLabel10}
        };

        Dictionary<int, PictureBox> TableCardBoxes = new Dictionary<int, PictureBox>()
        {
            { 0, TableCard1},
            { 1, TableCard2},
            { 2, TableCard3},
            { 3, TableCard4},
            { 4, TableCard5}
        };

        // Таблицы
        TableLayoutPanel MenuTable = new TableLayoutPanel();
        TableLayoutPanel ChooseGameTable = new TableLayoutPanel();
        TableLayoutPanel CreateGameTable = new TableLayoutPanel();
        TableLayoutPanel OptionsTable = new TableLayoutPanel();
        TableLayoutPanel GameTable = new TableLayoutPanel();

        TableLayoutPanel MovesChatTable = new TableLayoutPanel();
        TableLayoutPanel MovesTable = new TableLayoutPanel();
        TableLayoutPanel ChatTable = new TableLayoutPanel();
        TableLayoutPanel TableCardsTable = new TableLayoutPanel();

        TableLayoutPanel PlayerCardsTable = new TableLayoutPanel();

        Panel TablePanel = new Panel() {
            Dock = DockStyle.Fill
        };

        Thread TimerThread;

        // Background updating
        Action<int> GetState;
        Action<int> ChangeStartedGamesBox;
        Action<int> UpdatePlayerProfiles;
        Action<int> AskGameStarted;

        

        public ClientForm(GameProxy game)
        {
            Game = game;

            Dictionary<string, Image> Images = new Dictionary<string, Image>
            {
                { "cs1", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "small1.png"))},
                { "cs2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "small2.png"))},
                { "cs3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "small3.png"))},
                { "cs4", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "small4.png"))},
                { "cs5", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "small5.png"))},
                { "cs6", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "small6.png"))},
                { "cm1", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "medium1.png"))},
                { "cm2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "medium2.png"))},
                { "cm3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "medium3.png"))},
                { "cb1", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "big1.png"))},
                { "cb2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "big2.png"))},
                { "cb3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Chips\\{1}", Directory.GetCurrentDirectory(), "big3.png"))},

                { "h2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h2.png"))},
                { "c2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c2.png"))},
                { "d2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d2.png"))},
                { "s2", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s2.png"))},

                { "h3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h3.png"))},
                { "c3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c3.png"))},
                { "d3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d3.png"))},
                { "s3", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s3.png"))},

                { "h4", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h4.png"))},
                { "c4", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c4.png"))},
                { "d4", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d4.png"))},
                { "s4", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s4.png"))},

                { "h5", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h5.png"))},
                { "c5", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c5.png"))},
                { "d5", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d5.png"))},
                { "s5", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s5.png"))},

                { "h6", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h6.png"))},
                { "c6", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c6.png"))},
                { "d6", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d6.png"))},
                { "s6", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s6.png"))},

                { "h7", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h7.png"))},
                { "c7", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c7.png"))},
                { "d7", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d7.png"))},
                { "s7", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s7.png"))},

                { "h8", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h8.png"))},
                { "c8", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c8.png"))},
                { "d8", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d8.png"))},
                { "s8", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s8.png"))},

                { "h9", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h9.png"))},
                { "c9", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c9.png"))},
                { "d9", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d9.png"))},
                { "s9", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s9.png"))},

                { "h0", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "h0.png"))},
                { "c0", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "c0.png"))},
                { "d0", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "d0.png"))},
                { "s0", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "s0.png"))},

                { "hj", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "hj.png"))},
                { "cj", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "cj.png"))},
                { "dj", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "dj.png"))},
                { "sj", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "sj.png"))},

                { "hq", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "hq.png"))},
                { "cq", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "cq.png"))},
                { "dq", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "dq.png"))},
                { "sq", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "sq.png"))},

                { "qk", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "hk.png"))},
                { "ck", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "ck.png"))},
                { "dk", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "dk.png"))},
                { "sk", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "sk.png"))},

                { "qa", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "ha.png"))},
                { "ca", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "ca.png"))},
                { "da", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "da.png"))},
                { "sa", Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}", Directory.GetCurrentDirectory(), "sa.png"))}
            };

            ClientSize = new Size(600, 600);
            // Menu Table Lining
            InitializeMenu();
            // Choose Game Table Lining
            InitializeChooseGameTable();
            // Game Options Table Lining
            InitializeOptionsTable();
            // Game Creation Table Lining
            InitializeCreateTable();
            // Game Table Lining
            InitializeGameTable();
            // Привзяка обработчиков и тд


            SetMenuButtonHandlers();

            SetOptionsHandlers();

            SetCreateHandlers();

            SetChooseHandlers();

            SetGameHandlers();

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Gray;
            this.BackgroundImage = Image.FromFile(String.Format("{0}\\..\\..\\Images\\{1}", Directory.GetCurrentDirectory(), "cool_back.jpg"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            
            // Делегат для обновления чата
            GetState = (int t) => {
                try
                {
                    lock (Game.CurrentState)
                    {
                        var currPlayerCount = Game.PlayerNames.Count;
                        var success = Game.UpdateRegularData();
                        if (!success)
                        {
                            return;
                        }
                        for (int i = Math.Max(Game.MessagePointer, 0); i < Game.CurrentState.Chat.Count; i++)
                        {
                            Game.MessagePointer++;
                            if (this.GameChatBox.Text != "")
                                this.GameChatBox.Text += "\r\n";
                            this.GameChatBox.Text += Game.CurrentState.Chat[i];
                        }

                        if (currPlayerCount != Game.CurrentState.PlayerCount)
                        {
                            Game.SetPlayerNames(Game.CurrentGameId);
                        }

                        if (Game.CurrentState.Cards != null && Game.CurrentState.Cards.Length != 0)
                        {
                            Console.WriteLine(Game.CurrentState.Cards);
                            var splited = Game.CurrentState.Cards.Split(' ');
                            for (int i = 0; i < splited.Length; i++)
                            {
                                //this.TableCardBoxes[i].Visible = true;
                                this.TableCardBoxes[i].BackgroundImage = Images[splited[i]];
                            }

                            for (int i = splited.Length; i < 5; i++)
                            {
                                this.TableCardBoxes[i].BackgroundImage = null;
                                //this.TableCardBoxes[i].Visible = false;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                this.TableCardBoxes[i].BackgroundImage = null;
                                //this.TableCardBoxes[i].Visible = false;
                            }
                        }
                    }
                    if (Game != null && Game.Proxy != null)
                    {
                        var pcards = Game.Proxy.CheckCards(Game.CurrentGameId, Game.PlayerId);
                        Console.WriteLine(pcards);
                        if (pcards != null && pcards.Length != 0)
                        {
                            var splited = pcards.Split(' ');
                            PlayerCard1.Image = Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}.png", Directory.GetCurrentDirectory(), splited[0]));
                            PlayerCard2.Image = Image.FromFile(String.Format("{0}\\..\\..\\Images\\Cards\\{1}.png", Directory.GetCurrentDirectory(), splited[1]));
                        }
                    }
                }
                catch (CommunicationException ce) {
                    STimer.Tick -= GetState;
                    STimer.Tick -= AskGameStarted;
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
            };

            UpdatePlayerProfiles = (int t) =>
            {
                lock (Game.CurrentState) {
                    BankLabel.Text = Game.CurrentState.TableBank + "$";
                    for (int i = 0; i < 10; i++)
                    {
                        if (Game.CurrentState.Banks.ContainsKey(i))
                        {
                            PlayerProfileLabels[i].Visible = true;
                            if (Game.CurrentState.CurrentPlayer == i)
                            {
                                PlayerProfileLabels[i].BackColor = Color.GreenYellow;
                                PlayerProfileLabels[i].Text = String.Format("> {1}\r\n{0}$\r\nBet: {2}$", Game.CurrentState.Banks[i], Game.PlayerNames[i], Game.CurrentState.TableBanks[i]);
                            }
                            else
                            {
                                if (Game.CurrentState.Ready[i])
                                    PlayerProfileLabels[i].BackColor = Color.White;
                                else
                                    PlayerProfileLabels[i].BackColor = Color.Gray;
                                PlayerProfileLabels[i].Text = String.Format("{1}\r\n{0}$\r\nBet: {2}$", Game.CurrentState.Banks[i], Game.PlayerNames[i], Game.CurrentState.TableBanks[i]);
                            }
                        }
                        else
                        {
                            PlayerProfileLabels[i].Visible = false;
                        }
                    }
                    if (Game.CurrentState != null && Game.CurrentState.Blinds != null)
                    {
                        foreach (var blind in Game.CurrentState.Blinds.Keys)
                        {
                            PlayerProfileLabels[Game.CurrentState.Blinds[blind]].Text += " " + blind;
                        }
                    }

                    if (Game.CurrentState.TableBank == 0)
                    {
                        BankPicBox.BackgroundImage = null;
                    }
                    else {
                        if (Game.CurrentState.TableBank > 80)
                            BankPicBox.BackgroundImage = Images["cb3"];
                        else if (Game.CurrentState.TableBank > 60)
                            BankPicBox.BackgroundImage = Images["cb2"];
                        else if (Game.CurrentState.TableBank > 40)
                            BankPicBox.BackgroundImage = Images["cb1"];
                        else if (Game.CurrentState.TableBank > 25)
                            BankPicBox.BackgroundImage = Images["cm3"];
                        else if (Game.CurrentState.TableBank > 15)
                            BankPicBox.BackgroundImage = Images["cm2"];
                        else if (Game.CurrentState.TableBank > 6)
                            BankPicBox.BackgroundImage = Images["cm1"];
                        else if (Game.CurrentState.TableBank == 6)
                            BankPicBox.BackgroundImage = Images["cs6"];
                        else if (Game.CurrentState.TableBank == 5)
                            BankPicBox.BackgroundImage = Images["cs5"];
                        else if (Game.CurrentState.TableBank == 4)
                            BankPicBox.BackgroundImage = Images["cs4"];
                        else if (Game.CurrentState.TableBank == 3)
                            BankPicBox.BackgroundImage = Images["cs3"];
                        else if (Game.CurrentState.TableBank == 2)
                            BankPicBox.BackgroundImage = Images["cs2"];
                        else
                            BankPicBox.BackgroundImage = Images["cs1"];
                    }
                    //BankLabel.Text = Game.CurrentState.TableBank + "$"; // new
                }
            };

            AskGameStarted = (int t) =>
            {
                try
                {
                    var started = Game.Proxy.IsGameStarted(Game.CurrentGameId);
                    if (started)
                    {
                        //STimer.Tick -= AskGameStarted;
                        StartButton.Enabled = false;
                        StartButton.Visible = false;
                    }
                    else
                    {
                        StartButton.Enabled = true;
                        StartButton.Visible = true;
                    }
                }
                catch (CommunicationException ce)
                {
                    STimer.Tick -= GetState;
                    STimer.Tick -= AskGameStarted;
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
            };


            // Делегат для обновления запущенных игр
            ChangeStartedGamesBox = (int t) =>
            {
                try
                {
                    var success = Game.GetExistingGamesWithPlayerCount();
                    if (!success)
                        return;
                    var builder = new StringBuilder();
                    foreach (var key in Game.GamePlayerCountDict.Keys)
                    {
                        builder.Append(String.Format("Game ID: {0}; Player Count: {1}\r\n", key, Game.GamePlayerCountDict[key]));
                    }
                    this.StartedGames.Text = builder.ToString();
                }

                catch (CommunicationException exception)
                {
                    STimer.Tick -= ChangeStartedGamesBox;
                    this.StartedGames.Text = "No Connection";
                    var msg = "Server is not Responding";
                    var msgButtons = MessageBoxButtons.OK;
                    var caption = "... Back to Menu";
                    var msgQuestion = MessageBoxIcon.Error;
                    var result = MessageBox.Show(msg, caption,  msgButtons, msgQuestion);
                    if (result == DialogResult.OK) {
                        this.Controls.Remove(ChooseGameTable);
                        this.Controls.Add(MenuTable);
                    }
                }
            };


            // Таймер может лечь при переполнении, но можно пока оставить
            STimer = new SyncTimer();
            STimer.Interval = 500;

            var thR = new Thread(() =>
            {
                STimer.Start();
            });
            
            // Запуск потока с таймера
            thR.Start();

            // Чтобы корректно освободить поток с таймером перед завершением
            this.FormClosing += (sender, args) =>
            {
                thR.Abort();
            };

            var lines = File.ReadAllLines(String.Format("{0}\\..\\..\\Images\\{1}", Directory.GetCurrentDirectory(), "rulets.txt"));
            foreach (var line in lines) {
                RuleBox.Text += line + "\r\n";
            }


            // Рисовать стол
            // OnPaint
            // Invalidate ...


            Controls.Add(MenuTable);
            //Controls.Add(OptionsTable);
        }
    }
}
