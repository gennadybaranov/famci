using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Player
    {
        public string Name;
        public int Score;

        public Player()
        {
            Name = "none";
            Score = 0;
        }

        public Player(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
