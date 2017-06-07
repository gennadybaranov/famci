using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SnakeNS
{
    public partial class fMenu : Form
    {
        List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>>();
        private System.Timers.Timer timer;
        Game game;
        Field field;
        Snake snake;
        int screenWidth, screenHeight;
        float scale;
        int xSize = 40, ySize = 40;
        bool gameOver = false;
        bool entered = false;

        public fMenu()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Score: 0";
            KeyPreview = true;
            Start.TabStop = false;
            BestScores.TabStop = false;
            Exit.TabStop = false;
            Entertb.Text = "Enter your name";
            Entertb.ForeColor = Color.Gray;
            
        }

        private void Start_Click(object sender, EventArgs e)
        {
            gameOver = false;
            GameOverlbl.Visible = false;
            Restartbtn.Visible = false;
            Backbtn.Visible = false;
            Showbtn.Visible = false;
            Entertb.Visible = false;
            menuPanel.Visible = false;
            menuPanel.Enabled = false;
            gamePanel.Visible = true;
            scoresPanel.Visible = false;
            game = new Game(xSize, ySize);
            //GameBox.Width = 1168;
            //GameBox.Height = 1168;
            screenWidth = GameBox.Size.Width;
            screenHeight = GameBox.Size.Height;
            scale = Math.Min((float)screenHeight / ySize, (float)screenWidth / xSize);
            snake = game.GetSnake();
            field = game.GetField();
            GameBox.Invalidate();
            timer = new System.Timers.Timer(50);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Start();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (!game.MoveSnake()) Gameover();
            toolStripStatusLabel1.Text = "Score: " + game.GetCurScore().ToString();
            GameBox.Invalidate();
        }

        private void Gameover()
        {
            timer.Stop();
            gameOver = true;
        }

        private void fMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamePanel.Visible && !Showbtn.Visible)
            {
                if (e.KeyCode == Keys.Left) snake.ChangeDirection(-1, 0); else
                if (e.KeyCode == Keys.Right) snake.ChangeDirection(1, 0); else
                if (e.KeyCode == Keys.Up) snake.ChangeDirection(0, -1); else
                if (e.KeyCode == Keys.Down) snake.ChangeDirection(0, 1); else
                if (e.KeyCode == Keys.Space)
                {
                    if (timer.Enabled) timer.Stop(); else timer.Start();
                }
                else
                {

                }
            }
        }

        private void SaveName()
        {
            string str;
            if (entered) str = Entertb.Text; else str = "NoName";
            entered = false;
            int i = 0;
            int curScore = game.GetCurScore();
            for (i = 0; i < scores.Count; i++)
            {
                if (scores[i].Value < curScore) break;
            }
            scores.Insert(i, new KeyValuePair<string, int>(str, curScore));
            scoresList.Items.Clear();
            for (i = 0; i < scores.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(scores[i].Key);
                lvi.SubItems.Add(scores[i].Value.ToString());
                scoresList.Items.Add(lvi);
            }
        }

        private void Restartbtn_Click(object sender, EventArgs e)
        {
            SaveName();
            Start_Click(sender, e);
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            SaveName();
            gamePanel.Visible = false;
            menuPanel.Visible = true;
            menuPanel.Enabled = true;
        }

        private void Showbtn_Click(object sender, EventArgs e)
        {
            SaveName();
            gamePanel.Visible = false;
            scoresPanel.Visible = true;
        }

        private void Entertb_MouseClick(object sender, MouseEventArgs e)
        {
            Entertb.Text = "";
            Entertb.ForeColor = Color.Black;
            entered = true;
        }

        private void Backbtnb_Click(object sender, EventArgs e)
        {
            scoresPanel.Visible = false;
            menuPanel.Visible = true;
            menuPanel.Enabled = true;
        }

        private void BestScores_Click(object sender, EventArgs e)
        {
            scoresPanel.Visible = true;
            menuPanel.Visible = false;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GameBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush sb = new SolidBrush(Color.Black);
            List<SnakePart> parts = snake.GetParts();
            List<Food> food = field.GetFoodList();
            foreach (SnakePart part in parts)
            {
                Point point = part.GetPoint();
                sb.Color = point.color;
                g.FillEllipse(sb, new RectangleF(point.x * scale, point.y * scale, scale, scale));

            }
            foreach (Food f in food)
            {
                Point point = f.GetPoint();
                sb.Color = point.color;
                g.FillEllipse(sb, new RectangleF(point.x * scale, point.y * scale, scale, scale));
            }

            if(gameOver)
            {
                GameOverlbl.Visible = true;
                Restartbtn.Visible = true;
                Backbtn.Visible = true;
                Showbtn.Visible = true;
                Entertb.Visible = true;
            }
        }
    }
}
