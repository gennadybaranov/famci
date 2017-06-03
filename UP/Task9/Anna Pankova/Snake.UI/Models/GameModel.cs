using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake.UI
{
    class GameModel : IGameModel
    {
        public const int WIDTH = 21;
        public const int HEIGHT = 20;

        public event EventHandler GameOver;
        public event EventHandler ScoreUpdated;
        public string GameName
        {
            get;
            set;
        }

        public List<Point> SnakeCoordinates
        {
            get;
            set;
        }

        public Point FoodCoordinates
        {
            get;
            set;
        }

        public int Direction
        {
            get;
            set;
        }

        public int YourScore
        {
            get;
            set;
        }

        public void Step()
        {
            bool foodIsEaten = false;
            if (SnakeCoordinates[0].X == FoodCoordinates.X && SnakeCoordinates[0].Y == FoodCoordinates.Y)
            {
                SnakeCoordinates.Insert(0, FoodCoordinates);

                Random random = new Random();
                int x = random.Next(1, WIDTH), y = random.Next(1, HEIGHT);
                FoodCoordinates = new Point(x, y);

                foodIsEaten = true;
                YourScore++;
                ScoreUpdated?.Invoke(this, null);
            }

            Point point = new Point();
            if (Direction == 1)
            {
                point = new Point(SnakeCoordinates[0].X + 1, SnakeCoordinates[0].Y);
            }
            else
            if (Direction == 2)
            {
                point = new Point(SnakeCoordinates[0].X, SnakeCoordinates[0].Y + 1);

            }
            else
            if (Direction == 3)
            {
                point = new Point(SnakeCoordinates[0].X - 1, SnakeCoordinates[0].Y);
            }
            else
            if (Direction == 4)
            {
                point = new Point(SnakeCoordinates[0].X, SnakeCoordinates[0].Y - 1);
            }

            point.X = (point.X + WIDTH) % WIDTH;
            point.Y = (point.Y + HEIGHT) % HEIGHT;

            foreach (var coordinate in SnakeCoordinates)
            {
                if (coordinate.X == point.X && coordinate.Y == point.Y)
                {
                    GameOver?.Invoke(null, null);
                }
            }

            SnakeCoordinates.Insert(0, point);

            if (!foodIsEaten)
            {
                SnakeCoordinates.RemoveAt(SnakeCoordinates.Count - 1);
            }
        }

        public void ResetSnake()
        {
            this.SnakeCoordinates = new List<Point>();
            Random random = new Random();
            int x = random.Next(1, WIDTH), y = random.Next(1, HEIGHT);
            FoodCoordinates = new Point(x, y);
            SnakeCoordinates.Add(new Point(20, 20));
            SnakeCoordinates.Add(new Point(20, 19));
            SnakeCoordinates.Add(new Point(20, 18));
            SnakeCoordinates.Add(new Point(20, 17));
            Direction = 2;
            YourScore = 0;
        }
    }
}
