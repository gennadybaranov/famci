using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEGAMEGAME
{
    public class Fruit:IDraw
    {
        public int _x;
        public int _y;
        public Fruit(int x,int y)
        {
            _x = x;
            _y = y;
        }

        public void Draw(Graphics gr, int a)
        {
            gr.FillRectangle(new SolidBrush(Color.Red),
                new Rectangle(_x * a, _y * a, a, a));
        }
    }
}
