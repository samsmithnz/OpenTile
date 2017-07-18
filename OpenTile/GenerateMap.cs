﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTile
{
    public static class GenerateMap
    {
        public static string[,] GenerateRandomMap(string[,] map, int xMax, int zMax, int probOfMapBeingBlocked)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
                    {
                        map[x, z] = "W";
                    }
                }
            }
            return map;
        }

        public static void DebugPrintOutMap(string[,] map, int xMax, int zMax)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (map[x, z] != "")
                    {
                        Console.WriteLine(" this.map[" + x + ", " + z + "] = "+ map[x, z] + ";");
                    }
                }
            }
        }

    }
}
