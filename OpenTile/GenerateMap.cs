using System;
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

        public static string[,] GenerateTestMap(string[,] map, int xMax, int zMax)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (z == 5 && x == 5)
                    {
                        map[x, z] = "HW";
                    }
                    else if (z == 5 && x == 3)
                    {
                        map[x, z] = "LW";
                    }
                    else
                    {
                        map[x, z] = "";
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
                        Console.WriteLine(" this.map[" + x + ", " + z + "] = " + map[x, z] + ";");
                    }
                }
            }
        }

    }
}
