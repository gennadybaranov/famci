using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snake
{
    public class Game
    {
        public Timer _gameTimer;
        public bool _over;

        public Game(Timer gt, bool ov)
        {
            _gameTimer = gt;
            _over = ov;
        }

        public void Start()
        {
            _gameTimer.Start();
        }

        public void Stop()
        {            
            _gameTimer.Stop();
        }

        public void Fail()
        {
            _over = true;
            _gameTimer.Stop();
        }
    }
}
