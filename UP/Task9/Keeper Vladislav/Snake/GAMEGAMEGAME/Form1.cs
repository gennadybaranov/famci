using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMEGAMEGAME
{
    public partial class Form1 : Form
    {
        private Game game;
        private int score;
        private int xClient;
        private int yClient;
        private Timer gameTimer;
        private Timer snakeTimer;
        private bool game_work;
        private Random random = new Random();
        int xCells;
        int yCells;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

        }

        public void UpdateTimer(object sender, EventArgs e)
        {

            game._snake.move();
            if (game._snake.snake[0]._x >= xCells || game._snake.snake[0]._x < 0
                    ||
                game._snake.snake[0]._y >= yCells-1 || game._snake.snake[0]._y < 1)
            {
                gameTimer.Stop();
                game_work = false;
                ScoresBox.Items.Add("Name: "+game.GameName +
                    ", score: "+ score.ToString());
                SnakePanel.Invalidate();
                return;
            }
            for (int i = 1; i < game._snake.snake.Count; ++i)
            {
                if ((game._snake.snake[0]._x == game._snake.snake[i]._x
                    && game._snake.snake[0]._y == game._snake.snake[i]._y)
                    )
                {
                    gameTimer.Stop();
                    game_work = false;
                    ScoresBox.Items.Add("Name: " + game.GameName +
                    ", score: " + score.ToString());
                    SnakePanel.Invalidate();
                    return;
                }
            }

            foreach (var fruit in game.fruits)
            {
                if (game._snake.snake[0]._x==fruit._x &&
                    game._snake.snake[0]._y==fruit._y)
                {
                    game._snake.addPart();
                    game.fruits.Remove(fruit);
                    game.fruits.Add
                        (
                            new Fruit
                            (
                            random.Next(1, xCells - 2),
                            random.Next(1, yCells - 2)
                            )
                        );
                    ++score;
                    toolStripStatusLabel1.Text = "Score: " + score.ToString();
                    break;
                }
            }
            SnakePanel.Invalidate();

        }

        public void UpdateSnakeTimer(object sender, EventArgs e)
        {
            if (Input.KeyPressed(Keys.Left))
            {
                if (game._snake.snake[0]._direction != 2 ||
                    game._snake.snake.Count < 2)
                {
                    game._snake.moveLeft();
                }
            }
            else if (Input.KeyPressed(Keys.Right))
            {
                if (game._snake.snake[0]._direction != 0 ||
                    game._snake.snake.Count < 2)
                {
                    game._snake.moveRight();
                }
            }
            else if (Input.KeyPressed(Keys.Up))
            {
                if (game._snake.snake[0]._direction != 3 ||
                    game._snake.snake.Count < 2)
                {
                    game._snake.moveUP();
                }
            }
            else if (Input.KeyPressed(Keys.Down))
            {
                if (game._snake.snake[0]._direction != 1 ||
                    game._snake.snake.Count < 2)
                {
                    game._snake.moveDown();
                }
            }
      
               
            SnakePanel.Invalidate();
        }

        private void MainMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NameBox.Text = "Default name";
            NameBox.ForeColor = Color.Gray;
            
            xClient = SnakePanel.Width;
            yClient = SnakePanel.Height;

        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            MainMenu.Visible = false;
            SnakePanel.Visible = true;
            BtnBackFromGame.Visible = false;
            game = new Game();
            gameTimer = new Timer();
            snakeTimer = new Timer();
            snakeTimer.Tick += UpdateSnakeTimer;
            snakeTimer.Interval = 1;
            snakeTimer.Start();
            gameTimer.Tick += UpdateTimer;
            gameTimer.Interval = 150;
            gameTimer.Start();
            game_work = true;
            toolStripStatusLabel1.Text = "Score: 0";
            score = 0;
            game.GameName = NameBox.Text;
        }

        private void BtnBest_Click(object sender, EventArgs e)
        {
            BestScoresPanel.Visible = true;
            MainMenu.Visible = false;
            SnakePanel.Visible = false;
            if (gameTimer!=null && snakeTimer!=null)
            {
                gameTimer.Stop();
                snakeTimer.Stop();
            }
            
        }

        private void ScoresBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            BestScoresPanel.Visible = false;
            MainMenu.Visible = true;
        }

        private void BestScoresPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SnakePanel_Paint(object sender, PaintEventArgs e)
        {


            Graphics gr = e.Graphics;
           
            
            gr.FillRectangle(new SolidBrush(Color.Black),
                new Rectangle(0,
                0, xClient, yClient));
            int a = 20; // длина стороны квадрата
            xCells = xClient / a;
            yCells = yClient / a;

            game._snake.Draw(gr, a);
            foreach (var fruit in game.fruits)
                fruit.Draw(gr, a);
            if (!game_work)
            {
                gameTimer.Stop();
                snakeTimer.Stop();
                SolidBrush br = new SolidBrush(Color.DarkRed);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                Rectangle centered = e.ClipRectangle;

                Font font = this.Font;
                font = new Font(font.Name, 18.0f,
                    font.Style, font.Unit,
                    font.GdiCharSet, font.GdiVerticalFont);

                gr.DrawString("You have lost :c\nTry again later, alligator",
                    font, br, centered, sf);
                BtnBackFromGame.Visible = true;
                
                

            }

            gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnakePanel.Visible = false;
            MainMenu.Visible = true;
            if (gameTimer != null && snakeTimer != null)
            {
                gameTimer.Stop();
                snakeTimer.Stop();
            }

        }

        private void bestScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnakePanel.Visible = false;
            BestScoresPanel.Visible = true;
            if (gameTimer != null && snakeTimer != null)
            {
                gameTimer.Stop();
                snakeTimer.Stop();
            }

        }

        private void SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void SnakePanel_Resize(object sender, EventArgs e)
        {
            xClient = SnakePanel.Width;
            yClient = SnakePanel.Height;
            SnakePanel.Refresh();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void NameBox_MouseDown(object sender, MouseEventArgs e)
        {
            NameBox.Text = "";
            NameBox.ForeColor = Color.Black;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void BtnBackFromGame_Click(object sender, EventArgs e)
        {
            SnakePanel.Visible = false;
            MainMenu.Visible = true;
        }
    }
}
