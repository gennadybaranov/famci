using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class S
    {
        public int length;
        public int headRightDir, headDownDir, tailRightDir, tailDownDir;
        public int headCellX, headCellY, tailCellX, tailCellY;
        public Map map;
        public bool increased;

        public S(int w, int h)
        {
            length = 1;
            increased = false;
            headRightDir = 1;
            headDownDir = 0;
            tailRightDir = headRightDir;
            tailDownDir = headDownDir;

            map = new Map(w, h);
            Random r = new Random();
            headCellX = map.numH / 2;
            headCellY = map.numV /2;
            tailCellX = headCellX;
            tailCellY = headCellY;
            map.field[headCellY, headCellX] = 1;
            map.PlaceFood();
        }

        public void ProcessTailDirecton()
        {
            if (length > 1)
            {
                int tailNextCellX = tailCellX + tailRightDir;
                int tailNextCellY = tailCellY + tailDownDir;
                if (tailNextCellY == -1 ||
                    tailNextCellX == -1 ||
                    tailNextCellX == map.numH ||
                    tailNextCellY == map.numV ||
                    map.field[tailNextCellY, tailNextCellX] == 0)
                {
                    // change tail deirection
                    if (tailDownDir != 0)
                    {
                        tailDownDir = 0;
                        tailRightDir = 1;
                        tailNextCellX = tailCellX + 1;
                        if (tailNextCellX == map.numH ||
                            map.field[tailCellY, tailNextCellX] == 0)
                            tailRightDir = -1;
                    }
                    else
                    {
                        tailRightDir = 0;
                        tailDownDir = 1;
                        tailNextCellY = tailCellY + 1;
                        if (tailNextCellY == map.numV ||
                            map.field[tailNextCellY, tailCellX] == 0)
                            tailDownDir = -1;
                    }
                }
            }
            else
            {
                tailRightDir = headRightDir;
                tailDownDir = headDownDir;
            }
        }

        public int Move()
        {
            int newHeadCellX = headCellX + headRightDir;
            int newHeadCellY = headCellY + headDownDir;

            if (newHeadCellX == map.numH ||
                newHeadCellY == map.numV ||
                newHeadCellX == -1 ||
                newHeadCellY == - 1)
            {
                return -1;
            }

            ProcessTailDirecton();
            
            switch (map.field[newHeadCellY, newHeadCellX])
            {
                case 0:
                    map.field[tailCellY, tailCellX] = 0;
                    tailCellX += tailRightDir;
                    tailCellY += tailDownDir;
                    break;
                case 9:
                    map.PlaceFood();
                    map.field[tailCellY, tailCellX] = 2;
                    length++;
                    break;
                case 2:
                    return -1;
            }
            if (length >= 2)
                map.field[headCellY, headCellX] = 2;

            headCellX = newHeadCellX;
            headCellY = newHeadCellY;
            map.field[headCellY, headCellX] = 1;
            int v1 = headCellX;
            int v2 = headCellY;
            int v3 = length;

            return 0;
        }
    }
}
