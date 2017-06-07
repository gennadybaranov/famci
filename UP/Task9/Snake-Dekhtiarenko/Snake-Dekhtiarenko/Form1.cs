using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Snake_Dekhtiarenko
{
    public partial class Form1 : Form
    {
        #region Constructor

        #region Variables

        int score;
        public string name;
        private int timer = 0;
        public int nodeHeight = 16;
        public int nodeWidth = 16;
        int direction = 4; // 2 - down, 4 - left, 8 - up, 6 - right;
        bool gameover = false;
        bool Pause = false;
        static Food food;
        static List<SnakePart> Snake = new List<SnakePart>();
        List<int> highScores = new List<int>();
        List<Tuple<string,int>> nameList = new List<Tuple<string,int>>();
        Timer gameTimer = new Timer();
        public List<Obstacle> obstacles = new List<Obstacle>();
        public struct ColorRGB
        {
            public byte R;
            public byte G;
            public byte B;
            public ColorRGB(Color value)
            {
                this.R = value.R;
                this.G = value.G;
                this.B = value.B;
            }
            public static implicit operator Color(ColorRGB rgb)
            {
                Color c = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                return c;
            }
            public static explicit operator ColorRGB(Color c)
            {
                return new ColorRGB(c);
            }
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
            gameTimer.Tick += Update;
            gameTimer.Interval = 100;
            gameTimer.Start();
            StartGame();
            
        }

        #endregion

        #region Form Events

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private EnterName _enterName;

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = false;
            this.Owner.Visible = true;
            this.Close();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = false;
            About dialogBox = new About();
            dialogBox.ShowDialog();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = false;
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = true;
        }




        #endregion

        #region Game Engine

        private void StartGame()
        {
            gameover = false;
            Snake.Clear();
            score = 0;
            gameTimer.Enabled = true;
            gameTimer.Interval = 100;
            SnakePart head = new SnakePart(25, 25);
            Snake.Add(head);
            food = new Food();
            GenerateObstacles(10);

        }

        private void Update(object sender, EventArgs e)
        {
            timer++;
            if (gameover)
            {
                if (Input.Press(Keys.Enter))
                {
                    gameTimer.Enabled = false;
                    this.Owner.Visible = true;
                    this.Close();
                }
            }
            else
            {
                if (Input.Press(Keys.Left))
                {
                    if (Snake.Count < 2 || Snake[0].X == Snake[1].X)
                        direction = 4;
                }
                else if (Input.Press(Keys.Right))
                {
                    if (Snake.Count < 2 || Snake[0].X == Snake[1].X)
                        direction = 6;
                }
                else if (Input.Press(Keys.Up))
                {
                    if (Snake.Count < 2 || Snake[0].Y == Snake[1].Y)
                        direction = 8;
                }
                else if (Input.Press(Keys.Down))
                {
                    if (Snake.Count < 2 || Snake[0].Y == Snake[1].Y)
                        direction = 2;
                }
                else if (Input.Press(Keys.Oemtilde))
                {
                    int x = Snake[Snake.Count() - 1].X;
                    int y = Snake[Snake.Count() - 1].Y;
                    SnakePart part = new SnakePart(x, y);
                    Snake.Add(part);
                    score++;
                }

                UpdateSnake();
            }
            pbCanvas.Invalidate();
        }

        private void UpdateSnake()
        {
            if (!gameover)
            {
                for (int i = Snake.Count() - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        switch (direction)
                        {
                            case 2:
                                Snake[i].Y++;
                                break;
                            case 4:
                                Snake[i].X--;
                                break;
                            case 6:
                                Snake[i].X++;
                                break;
                            case 8:
                                Snake[i].Y--;
                                break;
                        }
                        //Checking for collision with borders
                        if (Snake[0].X >= 58 || Snake[0].X < 0 || Snake[0].Y >= 41 || Snake[0].Y <= 1)
                        {
                            GameOver();

                            //// Append text to an existing file named "WriteLines.txt".
                            //using (StreamWriter outputFile =
                            //    new StreamWriter(System.IO.Directory.GetCurrentDirectory() + @"\Data.txt", true))
                            //{
                            //    outputFile.WriteLine(score.ToString());
                            //}
                        }
                        //Checking for collision with the body
                        for (int j = 1; j < Snake.Count(); j++)
                        {
                            if (Snake[0].X == Snake[j].X && Snake[0].Y == Snake[j].Y)
                            {
                                GameOver();

                            }
                        }
                        //Checking for collision with the obstacles
                        for (int j = 0; j < obstacles.Count(); j++)
                        {
                            if (Snake[0].X * nodeWidth == obstacles[j].X && Snake[0].Y * nodeHeight == obstacles[j].Y)
                            {
                                GameOver();
                            }
                        }
                        //Checking for collision with food
                        if (Snake[i].X * nodeWidth == food.X && Snake[i].Y * nodeHeight == food.Y)
                        {
                            int x = Snake[Snake.Count() - 1].X;
                            int y = Snake[Snake.Count() - 1].Y;
                            SnakePart part = new SnakePart(x, y);
                            Snake.Add(part);
                            score++;
                            if (gameTimer.Interval > 30)
                                gameTimer.Interval -= 2;
                            else if (gameTimer.Interval > 10)
                                gameTimer.Interval -= 1;
                            GenerateFood();
                        }
                    }
                    else
                    {
                        Snake[i].X = Snake[i - 1].X;
                        Snake[i].Y = Snake[i - 1].Y;
                    }
                }
            }
        }

        private void Draw(Graphics canvas)
        {
            Pause = !gameTimer.Enabled;
            Font font = this.Font;
            font = new Font(font.Name, 8.0f,
                font.Style, font.Unit,
                font.GdiCharSet, font.GdiVerticalFont);
            if (gameover)
            {
                font = new Font("Rockwell Condensed", 40.0f,
                    font.Style, font.Unit,
                    font.GdiCharSet, font.GdiVerticalFont);
                SizeF message = canvas.MeasureString("Fail! The Game Is Over", font);
                canvas.DrawString("Fail! The Game Is Over", font, Brushes.DeepPink,
                    new PointF(640 - message.Width, 320));
                message = canvas.MeasureString("Final Score : ", font);
                canvas.DrawString("Final Score : ", font, Brushes.LightSeaGreen, new PointF(560 - message.Width, 400));
                canvas.DrawString(score.ToString(), font, Brushes.Blue, new PointF(560, 400));
                font = new Font("Rockwell Condensed", 20.0f,
                    font.Style, font.Unit,
                    font.GdiCharSet, font.GdiVerticalFont);
                canvas.DrawString("Press Enter to Begin New Game", font, Brushes.Gray, new PointF(285, 600));
            }
            else
            {
                if (Pause)
                {
                    font = new Font(font.Name, 40.0f,
                        font.Style, font.Unit,
                        font.GdiCharSet, font.GdiVerticalFont);

                    canvas.DrawString("Game Paused", font, Brushes.DeepPink, 320, 350);
                }
                font = new Font("Rockwell Condensed", 15.0f,
                    font.Style, font.Unit,
                    font.GdiCharSet, font.GdiVerticalFont);
                canvas.DrawString("Score : " + score.ToString(), font, Brushes.White, 30, 620);
                Color foodColor = Color.Pink;
                Color snakeColor = new Color();
                double j = Math.PI * 2, frequency = 0.1;

                canvas.FillEllipse(new SolidBrush(foodColor), new Rectangle(food.X, food.Y, 16, 16));
                for (int i = 0; i < Snake.Count(); i++)
                {
                    ColorRGB snakeRGB = new ColorRGB();
                    snakeRGB.R = (byte)(Math.Sin(frequency * i + 4) * 127 + 128);
                    snakeRGB.G = (byte)(Math.Sin(frequency * i + 2) * 127 + 128);
                    snakeRGB.B = (byte)(Math.Sin(frequency * i + 7) * 127 + 128);
                    snakeColor = Color.FromArgb(snakeRGB.R, snakeRGB.G, snakeRGB.B);
                    canvas.FillRectangle(new SolidBrush(snakeColor),
                        new Rectangle(Snake[i].X * nodeWidth, Snake[i].Y * nodeHeight, nodeWidth, nodeHeight));
                    j += Math.PI / 2;
                }
                Color obstaclesColor = new Color();

                for (int i = 0; i < obstacles.Count(); i++)
                {
                    ColorRGB obstacleRGB = new ColorRGB();
                    obstacleRGB.R = (byte)(Math.Sin(frequency * timer + 4) * 127 + 128);
                    obstacleRGB.G = (byte)(Math.Sin(frequency * timer + 2) * 127 + 128);
                    obstacleRGB.B = (byte)(Math.Sin(frequency * timer + 0) * 127 + 128);
                    obstaclesColor = Color.FromArgb(obstacleRGB.R, obstacleRGB.G, obstacleRGB.B);
                    canvas.FillRectangle(new SolidBrush(obstaclesColor),
                      new Rectangle(obstacles[i].X, obstacles[i].Y, nodeWidth, nodeHeight));
                }
            }
        }

        private void GameOver()
        {
            gameover = true;
            // Append text to an existing file named "Data.txt".
            
            using (StreamWriter outputFile = new StreamWriter(System.IO.Directory.GetCurrentDirectory() + @"\Data.txt", true ))
            {
                string s = score.ToString() + " : " + name;
                outputFile.WriteLine(s);
            }
            gameTimer.Interval = 100;
        }

        private void GenerateFood()
        {
            food = new Food();
        }

        private void GenerateObstacles(int n)
        {
            obstacles.Clear();
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int a = random.Next(0, 58), b = random.Next(2, 41);
                Obstacle tmp = new Obstacle(a * 16, b * 16);
                if (food.X == tmp.X && food.Y == tmp.Y)
                {
                    tmp = new Obstacle(a * 16, b * 16);
                }
                else
                {
                    obstacles.Add(tmp);
                }
                
                
            }
        }

        #endregion


        private void GetHighScore()
        {
            using (StreamReader fs = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\Data.txt"))
            {
                string s = "";
                while (!fs.EndOfStream)
                {
                    s = fs.ReadLine();
                    string[] tmp = s.Split(':');
                    int a = Convert.ToInt32(tmp[0]);
                    string b = tmp[1];
                    highScores.Add(a);
                    Tuple<string, int> pair = new Tuple<string,int>(tmp[1], a);
                    nameList.Add(pair);
                }
            }
            nameList = nameList.OrderBy(o => o.Item2).ToList();
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = false;
            string s;
            High_Scores dialogBox = new High_Scores();
            GetHighScore();
            for (int i = nameList.Count() - 1; i >= 0; i--)
            {
                s = nameList[i].Item1 + " - " + nameList[i].Item2;
                dialogBox.ScoresList.Items.Add(s);
            }
            s = "TOP PLAYER : " + nameList[nameList.Count() - 1].Item1+ " - " +
                nameList[nameList.Count() - 1].Item2;
            dialogBox.TopPlayer.Items.Add(s);
            if (dialogBox.ShowDialog() != 0)
            {
                gameTimer.Enabled = true;
            }

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}