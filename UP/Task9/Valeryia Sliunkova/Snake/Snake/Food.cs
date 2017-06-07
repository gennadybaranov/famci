using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food
    {
        public int X;
        public int Y;


        public Food()
        {
            Random random = new Random();
            int a = random.Next(0, 30), b = random.Next(2, 33);
            X = a;
            Y = b;
        }
    }
}
