using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Dekhtiarenko
{
    public class Obstacle
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
        }


        

    }
}
