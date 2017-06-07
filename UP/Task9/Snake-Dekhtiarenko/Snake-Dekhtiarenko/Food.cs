using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Dekhtiarenko
{
    public class Food
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Food(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Food()
        {
            Random random = new Random();
            int a = random.Next(0, 58), b = random.Next(2, 41);
            X = a * 16;
            Y = b * 16;
        }
    }
}
