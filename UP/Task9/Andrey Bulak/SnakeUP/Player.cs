using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeUP
{
    public class Player
    {
        public string Date { get; set; }
        public int Score { get; set; }

        public Player()
        {
            Date = null;
            Score = 0;
        }
        public Player(string date, int score)
        {
            Date = date;
            Score = score;
        }
    }
}
