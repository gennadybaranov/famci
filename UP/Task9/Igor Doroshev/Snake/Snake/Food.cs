using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace SnakeNS
{
    public class Food
    {
        int healthImpact = 1;
        Point point;
        public Food(int x, int y, int healthImpact)
        {
            point = new Point(x, y, healthImpact >= 0 ? Color.Green : Color.Red);
            this.healthImpact = healthImpact;
        }
        public Point GetPoint()
        { return point; }
        public void SetPoint(int x, int y)
        {
            point.x = x;
            point.y = y;
        }
        public int GetHealthImpact()
        { return healthImpact; }
    }
}