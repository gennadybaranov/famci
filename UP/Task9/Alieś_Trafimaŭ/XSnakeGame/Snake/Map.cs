using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Map
    {
        public int numH, numV;
        public int[,] field;
        /*  1 - snake head
         *  2 - snake body
         *  9 - food
         *  0 - empty cell
         */
        public int padding = 3;
        public int cellSize = 20; // width and height of a map cell        
        Random r;
        public Map(int w, int h)
        {
            numH = (w - 2*padding) / cellSize;
            numV = (h - 2*padding) / cellSize;
            field = new int[numV, numH];
            for (int i = 0; i < numV; i++)
            {
                for (int j = 0; j < numH; j++)
                    field[i, j] = 0;
            }
            r = new Random();
        }     
        public void PlaceFood()
        {
            int x, y;
            while (true)
            {
                x = r.Next(0, numH);
                y = r.Next(0, numV);
                if (field[y, x] == 0)
                    break;
            }
            field[y, x] = 9;
        }
    }
}
