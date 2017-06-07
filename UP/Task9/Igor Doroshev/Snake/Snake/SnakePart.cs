using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeNS
{
    public class SnakePart 
    {
        private Point point;
        public SnakePart(int x, int y, Color color)
        {
            point = new Point(x, y, color);
        }
        public Point GetPoint()
        { return point; }
        public void ChangeColor(Color color)
        {
            point.color = color;
        }
    }
}