using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEGAMEGAME
{
    public class Snake:IDraw
    {
        public  List<SnakePart> snake;
      
        public Snake()
        {
            snake = new List<SnakePart>();
            SnakePart head = new SnakePart(5, 5,2);
            snake.Add(head);
        }

        public void addPart()
        {
            int x = snake[snake.Count - 1]._x;
            int y = snake[snake.Count - 1]._y;
            int dir = snake[snake.Count - 1]._direction;
            switch (dir)
            {
                case 0:
                    x += 1;
                    break;
                case 1:
                    y -= 1;
                    break;
                case 2:
                    x -= 1;
                    break;
                case 3:
                    y += 1;
                    break;
            }
            
            SnakePart newPart = new SnakePart(x,y,dir);
            snake.Add(newPart);
        }

        public void move()
        {
            
            for (int i=snake.Count-1;i>0;--i)
            {
                snake[i]._x = snake[i-1]._x;
                snake[i]._y = snake[i-1]._y;
                snake[i]._direction = snake[i-1]._direction;
            }

            switch (snake[0]._direction)
            {
                case 0:
                    snake[0]._x -= 1;
                    break;
                case 1:
                    snake[0]._y -= 1;
                    break;
                case 2:
                    snake[0]._x += 1;
                    break;
                case 3:
                    snake[0]._y += 1;
                    break;
            }
        }

        public void moveLeft()
        {
            snake[0]._direction = 0;
        }

        public void moveUP()
        {
            snake[0]._direction = 1;
        }

        public void moveRight()
        {
            snake[0]._direction = 2;
        }

        public void moveDown()
        {
            snake[0]._direction = 3;
        }

        public void Draw(Graphics gr,int a)
        {
            foreach (var snakePart in snake)
            {
                gr.FillRectangle(new SolidBrush(Color.Green),
                    new Rectangle(snakePart._x* a, snakePart._y * a, a, a));
            }
        }
    }
}
