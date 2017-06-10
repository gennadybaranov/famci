using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class MainForm : Form
    {
        Game game;
        S snake;
        public MainForm()
        {
            InitializeComponent();
            InitGameControls();
            pg.BackColor = game.backColor;
            game.Repaint += timer_Tick;      
        }
        public void InitGameControls()
        {
            game = new Game();
            snake = new S(pg.Width, pg.Height);
            panel_highscores.Visible = false;
            pg.Visible = false;
            panel_start.Visible = true;
            tb_name.Text = "";
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (snake.Move() != 0)
            {
                game.StopGame();
                game.curPlayer.score = snake.length;
                game.players.Add(game.curPlayer);
                game.curPlayer = null;

                pg.Invoke((MethodInvoker) delegate                {
                    pg.Visible = false;
                });
                
                panel_highscores.Visible = true;
                label_curScore.Text = snake.length.ToString();
            }
            pg.Invalidate();
        }

        private void pg_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;            

            int size = snake.map.cellSize;
            for (int i = 0; i < snake.map.numV; i++)
            {
                for (int j = 0; j < snake.map.numH; j++)
                {
                    int x = snake.map.padding + snake.map.cellSize * j;
                    int y = snake.map.padding + snake.map.cellSize * i;

                    switch (snake.map.field[i, j])
                    {
                        case 1:
                            g.FillRectangle(game.headBrush, x, y, size - game.penWidth, size - game.penWidth);
                            g.DrawRectangle(game.snakeBorderPen, x, y, size, size);
                            break;
                        case 2:
                            g.FillRectangle(game.bodyBrush, x, y, size, size);
                            g.DrawRectangle(game.snakeBorderPen, x, y, size, size);
                            break;
                        case 9:
                            g.FillRectangle(game.foodBrush, x, y, size, size);
                            g.DrawRectangle(game.foodBorderPen, x, y, size, size);
                            break;
                    }
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (snake.headDownDir != 0)
                        break;
                    snake.headDownDir = -1;
                    snake.headRightDir = 0;
                    break;
                case Keys.Down:
                    if (snake.headDownDir != 0)
                        break;
                    snake.headDownDir = 1;
                    snake.headRightDir = 0;
                    break;
                case Keys.Left:
                    if (snake.headRightDir != 0)
                        break;
                    snake.headRightDir = -1;
                    snake.headDownDir = 0;
                    break;
                case Keys.Right:
                    if (snake.headRightDir != 0)
                        break;
                    snake.headRightDir = 1;
                    snake.headDownDir = 0;
                    break;
            }        
        }

        private void tb_name_KeyUp(object sender, KeyEventArgs e)
        {
            string text = tb_name.Text;
            if (!String.IsNullOrEmpty(text))
            {
                btn_start.Enabled = true;
            }
            else
            {
                btn_start.Enabled = false;
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            game.curPlayer.Name = tb_name.Text;
            panel_start.Visible = false;
            pg.Visible = true;
            game.StartGame();
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            InitGameControls();
        }
        
    }
}