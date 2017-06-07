using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEGAMEGAME
{
    class Game
    {
        public Snake _snake;
        public List<Fruit> fruits;
        public string GameName;
        public Game()
        {
            _snake = new Snake();
            fruits = new List<Fruit>();
            Fruit fruit = new Fruit(3,3);
            fruits.Add(fruit);
            GameName = "Default name";
        }

    }
}
