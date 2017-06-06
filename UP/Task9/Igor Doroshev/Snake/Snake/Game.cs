using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;

namespace SnakeNS
{
    public class Game
    {
        
        private Field field;
        private Snake snake;
        private int curScore;
        private int fieldWidth, fieldHeight;

        public Game(int fieldWidth, int fieldHeight)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            field = new Field(fieldWidth, fieldHeight);
            snake = new Snake(fieldWidth / 2, fieldHeight / 2);
            curScore = 0;
            
        }

        public Snake GetSnake()
        { return snake; }
        public int GetCurScore()
        { return curScore; }
        public Field GetField()
        { return field; }
        public bool MoveSnake()
        {
            bool crashed = !snake.Move();
            Point head = snake.GetHead().GetPoint();
            if (head.x < 0 || head.x >= fieldWidth ||
                head.y < 0 || head.y > fieldHeight || crashed) return false;
            List<Food> foodList = field.GetFoodList();
            int i = 0;
            foreach (Food food in foodList)
            {
                Point point = food.GetPoint();
                if (point.x == head.x && point.y == head.y)
                {
                    if (point.color == Color.Red)
                    {
                        if (snake.Decrease()) curScore--;

                    }
                    else
                    {
                        snake.Increase();
                        curScore++;
                    }
                    field.RegenerateFood(i);
                    break;
                }
                
                i++;
            }
            return true;

        }
    }
}
