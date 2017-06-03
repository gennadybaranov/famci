using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.UI
{
    public interface IGameModel
    {
        string GameName
        {
            get;
            set;
        }

        List<Point> SnakeCoordinates
        {
            get;
            set;
        }
        Point FoodCoordinates
        {
            get;
            set;
        }

        int Direction
        {
            get;
            set;
        }

        int YourScore
        {
            get;
            set;
        }


        void ResetSnake();
        void Step();

        event EventHandler GameOver;
        event EventHandler ScoreUpdated;
    }
}
