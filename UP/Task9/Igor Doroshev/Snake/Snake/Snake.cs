using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeNS
{
    public class Snake
    {
        private List<SnakePart> parts = new List<SnakePart>();
        private int dx = 0, dy = -1;
        private int flag = 0;
        public Snake(int x, int y)
        {
            parts.Add(new SnakePart(x, y, Color.Yellow));
            parts.Add(new SnakePart(x, y - dy, Color.LightBlue));
        }

        public SnakePart GetHead()
        { return parts[0]; }
        public List<SnakePart> GetParts()
        { return parts; }
        public void ChangeDirection(int dx, int dy)
        {
            if (this.dx != dx && this.dy != dy)
            {
                this.dx = dx;
                this.dy = dy;
            }
        }
        public bool Move()
        {
            parts[0].ChangeColor(Color.LightBlue);
            if (flag == 0) parts.RemoveAt(parts.Count - 1);
            Point head = parts[0].GetPoint();
            parts.Insert(0, new SnakePart(head.x + dx, head.y + dy, Color.Yellow));
            head = parts[0].GetPoint();
            flag = 0;
            for (int i = 1; i < parts.Count; i++)
            {
                Point point = parts[i].GetPoint();
                if (point.x == head.x && point.y == head.y) return false;
            }
            return true;
        }
        public void Increase()
        {
            flag = 1;
        }
        public bool Decrease()
        {
            if (parts.Count > 2)
            {
                parts.RemoveAt(parts.Count - 1);
                return true;
            }
            else return false;
        }
    }
}