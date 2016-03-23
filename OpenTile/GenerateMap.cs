﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTile
{
    public static class GenerateMap
    {
        public static bool[,] GenerateRandomMap(bool[,] map, int xMax, int zMax, int probOfMapBeingBlocked)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Utility.GenerateRandomNumber(1, 100))
                    {
                        map[x, z] = false;
                    }
                }
            }
            return map;
        }

        public static void DebugPrintOutMap(bool[,] map, int xMax, int zMax)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (map[x, z] == false)
                    {
                        Console.WriteLine(" this.map[" + x + ", " + z + "] = false;");
                    }
                }
            }
        }

    }
}
