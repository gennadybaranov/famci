using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snake
{
    public class Game
    {
        public Player curPlayer;
        public List<Player> players;
        private Timer timer;
        public int penWidth;
        public Pen snakeBorderPen, foodBorderPen;
        public Brush headBrush, bodyBrush, foodBrush, backBrush;
        public Color backColor, snakeBorderColor, headColor, foodBorderColor;

        public event EventHandler Repaint;


        public Game()
        {
            curPlayer = new Player();
            players = new List<Player>();
            timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;

            backColor = Color.FromArgb(60, 60, 85);
            snakeBorderColor = Color.FromArgb(50, 55, 80);
            headColor = Color.FromArgb(40, 170, 220);
            foodBorderColor = Color.FromArgb(95,40,35);
            snakeBorderPen = new Pen(snakeBorderColor);
            foodBorderPen = new Pen(foodBorderColor);
            bodyBrush = new SolidBrush(headColor);
            headBrush = Brushes.MediumTurquoise;
            backBrush = new SolidBrush(backColor);
            foodBrush = Brushes.IndianRed;

            penWidth = 2;
            snakeBorderPen.Width = penWidth;
            foodBorderPen.Width = penWidth;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Repaint?.Invoke(this, EventArgs.Empty);
        }

        public void StartGame()
        {
            timer.Start();
        }
        public void StopGame()
        {
            timer.Stop();
        }
    }
}
