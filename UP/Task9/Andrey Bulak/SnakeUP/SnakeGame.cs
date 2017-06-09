using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SnakeUP
{
    public partial class SnakeGame : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        public SnakeGame()
        {
            InitializeComponent();
            new Settings();
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            StartGame();
        }
        private void StartGame()
        {
            Back.Visible = false;
            lblGameOver.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle();
            head.X = 20;
            head.Y = 0;
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
        }
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }
        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.GameOver == true)
            {
                
                
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;
                MovePlayer();
            }
            pbCanvas.Invalidate();
        }
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (!Settings.GameOver)
            {
                Brush snakeColor;
                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                        snakeColor = Brushes.Blue;
                    else
                        snakeColor = Brushes.Yellow;
                    canvas.FillEllipse(snakeColor, new Rectangle(Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height));
                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Settings.Width,
                        food.Y * Settings.Height, Settings.Width, Settings.Height));
                }
            }
            else 
            {
                string gameOver = null;
                Back.Visible = true;
                lblGameOver.Visible = true;

                if (Settings.Score >= Settings.Points)
                {
                    List<Player> players = new List<Player>();
                    using (StreamReader reader = File.OpenText("Score.txt"))
                    {
                        string score = null;
                        string date = null;
                        while (true)
                        {
                            Player temp = new Player();
                            score = reader.ReadLine();
                            date = reader.ReadLine();
                            if (score == null)
                            {
                                break;
                            }
                            temp.Score = Convert.ToInt32(score);
                            temp.Date = date;
                            players.Add(temp);
                        }
                    }
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (Settings.Score > players[i].Score)
                        {
                            players[players.Count - 1].Score = Settings.Score;
                            players[players.Count - 1].Date = DateTime.Now.ToLongDateString();
                            break;
                        }
                    }
                    using (StreamWriter writer = File.CreateText("Score.txt"))
                    {
                        for (int i = 0; i < players.Count; ++i)
                        {
                            writer.WriteLine(players[i].Score);
                            writer.WriteLine(players[i].Date);
                        }
                    }
                }
                gameOver = "Game Over \nYour final score is: " + Settings.Score + "\nPress enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;
                   
                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > maxXPos || Snake[i].Y > maxYPos)
                    {
                        Die();
                    }
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }

            }
        }

        private void Eat()
        {
            Circle food = new Circle();
            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();
            GenerateFood(); 
        }
        private void Die()
        {
            Settings.GameOver = true;
        }
       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            gameTimer.Enabled = false;
            StartMenu startMenu = new StartMenu();
            startMenu.Show();
        }
    }
}
