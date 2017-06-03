using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake.UI.Interfaces
{
    public interface ISnakeGameView
    {
        void DrawSnake(Graphics g, List<Point> snakeCoordinates);
        void DrawFood(Graphics g, Point coordinates);
        int YourScore
        {
            set;
        }

        int BestScore
        {
            set;
        }

        event EventHandler<PaintEventArgs> PaintRequested;
        event EventHandler<KeyEventArgs> KeyDownEvent;
        void InvalidateArea();
    }
}
