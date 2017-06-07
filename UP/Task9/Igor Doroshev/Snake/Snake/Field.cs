using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNS
{
    public class Field
    {
        private List<Food> food = new List<Food>();
        private int fieldWidth, fieldHeight;
        private int gFood = 1, bFood = 1;
        Random random = new Random();
        //private int screenWidth, screenHeight;

        public Field(int fieldWidth, int fieldHeight)
        {
            this.fieldHeight = fieldHeight;
            this.fieldWidth = fieldWidth;
            food.Clear();
            
            for (int i = 0; i < gFood; i++)
            {
                food.Add(new Food(random.Next(0, fieldWidth - 1), random.Next(0, fieldHeight - 1), 1));
            }
            for (int i = 0; i < bFood; i++)
            {
                food.Add(new Food(random.Next(0, fieldWidth - 1), random.Next(0, fieldHeight - 1), -1));
            }
            /*screenWidth = width;
            screenHeight = height;
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;
            scale = Math.Min(screenWidth / fieldWidth, screenHeight / fieldHeight);*/
        }
        /*public void Resize(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;
            scale = Math.Min(screenWidth / fieldWidth, screenHeight / fieldHeight);
        }*/
        public void RegenerateFood(int pos)
        {
            food[pos].SetPoint(random.Next(0, fieldWidth - 1), random.Next(0, fieldHeight - 1));
        }
        public List<Food> GetFoodList()
        { return food; }
    }
}