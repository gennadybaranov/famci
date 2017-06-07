using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEGAMEGAME
{

    public class SnakePart
    {
        public int _x;
        public int _y;
        public int _direction; // 0 -left, 1 -top,2-right,3-bottom

        

        public SnakePart(int x,int y,int dir)
        {
            _x = x;
            _y = y;
            _direction = dir%4;
        }
        public void moveTo(int x,int y)
        {
            _x = x;
            _y = y;
        }
    }
}
