using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {

        //variables:
        static List <Player> Players = new List<Player>();
        static List<Part> Snakee = new List<Part>();
        Timer gameTimer = new Timer();        
        static bool over = false;
        static Food food;
        static int nodeHeight = 10;
        static int nodeWidth = 10;
        int direction = 4;
        

        public Form1()
        {
            InitializeComponent();
            snakeCanvas.Hide();
            bestScoresPanel.Hide();
            this.KeyPreview = true;

        }
        private void label1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            bestScoresList.Items.Clear();
            Player first = new Player();
            first.Name = nameBox.Text;
            first.Score = 0;
            Players.Add(first);

            newGamePanel.Hide();
            snakeCanvas.Show();
            gameTimer.Tick += Update;
            gameTimer.Interval = 200;
            gameTimer.Start();
            StartGame();
        }

        private void StartGame()
        {
            over = false;            
            Snakee.Clear();
            Part head = new Part(25, 25);
            Snakee.Add(head);
            food = new Food();
        }

        private void snakeCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            Direction.Change(e.KeyCode, true);
        }

        private void snakeCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            Direction.Change(e.KeyCode, false);
        }

        private void snakeCanvas_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Update(object sender, EventArgs e)
        {
            if (over)
            {
                if (Direction.Press(Keys.Enter))
                    StartGame();
            }
            else
            {
                if (Direction.Press(Keys.Left))
                {
                    if (Snakee.Count < 2 || Snakee[0].X == Snakee[1].X)
                        direction = 4;
                }
                else if (Direction.Press(Keys.Right))
                {
                    if (Snakee.Count < 2 || Snakee[0].X == Snakee[1].X)
                        direction = 6;
                }
                else if (Direction.Press(Keys.Up))
                {
                    if (Snakee.Count < 2 || Snakee[0].Y == Snakee[1].Y)
                        direction = 8;

                }
                else if (Direction.Press(Keys.Down))
                {
                    if (Snakee.Count < 2 || Snakee[0].Y == Snakee[1].Y)
                        direction = 2;
                }
                UpdateSnake();
            }
            snakeCanvas.Invalidate();
        }

        private void UpdateSnake()
        {
            if (!over)
            {
                for (int i = Snakee.Count() - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        switch (direction)
                        {
                            case 2:
                                Snakee[i].Y++;
                                break;
                            case 4:
                                Snakee[i].X--;
                                break;
                            case 6:
                                Snakee[i].X++;
                                break;
                            case 8:
                                Snakee[i].Y--;
                                break;
                        }
                        //Checking for collision with borders
                        if (Snakee[0].X >= 30 || Snakee[0].X < 0 || Snakee[0].Y >= 33 || Snakee[0].Y < 2)
                        { over = true; gameTimer.Stop();}
                        //Checking for collision with the body
                        for (int j = 1; j < Snakee.Count(); j++)
                        {
                            if (Snakee[0].X == Snakee[j].X && Snakee[0].Y == Snakee[j].Y)
                            {
                                over = true;
                                gameTimer.Stop();
                            }
                        }
                        //Checking for collision with food
                        if (Snakee[i].X * nodeWidth == food.X * nodeWidth && Snakee[i].Y * nodeHeight == food.Y * nodeHeight)
                        {
                            int x = Snakee[Snakee.Count() - 1].X;
                            int y = Snakee[Snakee.Count() - 1].Y;
                            Part part = new Part(x, y);
                            Snakee.Add(part);
                            Players[Players.Count() - 1].Score++;
                            food = new Food();
                        }
                    }
                    else
                    {
                        Snakee[i].X = Snakee[i - 1].X;
                        Snakee[i].Y = Snakee[i - 1].Y;
                    }
                }
            }
        }

        private void Draw(Graphics snakeCanvas)
        {            
            Font font = Font;
            if (over)
            {
                SizeF message = snakeCanvas.MeasureString("The Game Is Over", font);
                snakeCanvas.DrawString("The Game Is Over", font, Brushes.White, new PointF(140 - message.Width / 2, 120));
                for (int l = 0; l < Players.Count(); l++)
                {
                    bestScoresList.Items.Add(Players[l].Name);
                    bestScoresList.Items.Add(Players[l].Score);
                }
            }
            else
            {
                Color foodColor = Color.Crimson;
                snakeCanvas.FillRectangle(new SolidBrush(foodColor), new Rectangle(food.X*nodeWidth, food.Y*nodeHeight, nodeWidth, nodeHeight));
                for (int i = 0; i < Snakee.Count(); i++)
                {
                    Color snakeColor = i == 0 ? Color.DodgerBlue : Color.Cornsilk;
                    snakeCanvas.FillRectangle(new SolidBrush(snakeColor), new Rectangle(Snakee[i].X * nodeWidth, Snakee[i].Y * nodeHeight, nodeWidth, nodeHeight));
                }
            toolStripStatusLabel1.Text = "Score: " +(Players[Players.Count() - 1].Score).ToString();

            }
        }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bestScoresPanel.Hide();
            newGamePanel.Show();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            bestScoresPanel.Hide();
            newGamePanel.Show();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            gameTimer = new Timer();
            snakeCanvas.Invalidate();
            snakeCanvas.Hide();
            newGamePanel.Show();
        }

        private void bestScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            newGamePanel.Hide();
            bestScoresPanel.Show();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
        }

        private void bestScoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            snakeCanvas.Hide();
            bestScoresPanel.Show();
        }
    }
}