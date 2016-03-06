using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OpenTile
{
    public class PossibleTiles
    {

        public static List<Point> FindTiles(Point startingLocation, int range, int width, int height, bool[,] map)
        {

            List<Point> possibleTiles = new List<Point>();
            int yMin = startingLocation.Y - range;
            if (yMin < 0)
            {
                yMin = 0;
            }
            int yMax = startingLocation.Y + range;
            if (yMax > height - 1)
            {
                yMax = height - 1;
            }
            int xMin = startingLocation.X - range;
            if (xMin < 0)
            {
                xMin = 0;
            }
            int xMax = startingLocation.X + range;
            if (xMax > width - 1)
            {
                xMax = width - 1;
            }
            for (int y = yMin; y < yMax; y++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    System.Diagnostics.Debug.WriteLine("X: " + x + ",Y:" + y);
                    if (map[x, y] == true && possibleTiles.Contains(new Point(x, y)) == false)
                    {
                        possibleTiles.AddRange(FindAdjacentPoints(new Point(x, y), width, height, map, possibleTiles));
                    }
                }
            }
            return possibleTiles;
        }


        private static List<Point> FindAdjacentPoints(Point currentLocation, int width, int height, bool[,] visitedTiles, List<Point> currentPossibleTiles)
        {
            List<Point> adjacentTiles = new List<Point>();
            int yMin = currentLocation.Y - 1;
            if (yMin < 0)
            {
                yMin = 0;
            }
            int yMax = currentLocation.Y + 1;
            if (yMax > height - 1)
            {
                yMax = height - 1;
            }
            int xMin = currentLocation.X - 1;
            if (xMin < 0)
            {
                xMin = 0;
            }
            int xMax = currentLocation.X + 1;
            if (xMax > width - 1)
            {
                xMax = width - 1;
            }

            //Get possible tiles, within constraints of map, including both square and diagonal tiles from current position
            if (visitedTiles[currentLocation.X, yMax] == true && currentPossibleTiles.Contains(new Point(currentLocation.X, yMax)) == false)
            {
                adjacentTiles.Add(new Point(currentLocation.X, yMax));
            }
            if (visitedTiles[xMax, yMax] == true && currentPossibleTiles.Contains(new Point(xMax, yMax)) == false)
            {
                adjacentTiles.Add(new Point(xMax, yMax));
            }
            if (visitedTiles[xMax, currentLocation.Y] == true && currentPossibleTiles.Contains(new Point(xMax, currentLocation.Y)) == false)
            {
                adjacentTiles.Add(new Point(xMax, currentLocation.Y));
            }
            if (visitedTiles[xMax, yMin] == true && currentPossibleTiles.Contains(new Point(xMax, yMin)) == false)
            {
                adjacentTiles.Add(new Point(xMax, yMin));
            }
            if (visitedTiles[currentLocation.X, yMin] == true && currentPossibleTiles.Contains(new Point(currentLocation.X, yMin)) == false)
            {
                adjacentTiles.Add(new Point(currentLocation.X, yMin));
            }
            if (visitedTiles[xMin, yMin] == true && currentPossibleTiles.Contains(new Point(xMin, yMin)) == false)
            {
                adjacentTiles.Add(new Point(xMin, yMin));
            }
            if (visitedTiles[xMin, currentLocation.Y] == true && currentPossibleTiles.Contains(new Point(xMin, currentLocation.Y)) == false)
            {
                adjacentTiles.Add(new Point(xMin, currentLocation.Y));
            }
            if (visitedTiles[xMin, yMax] == true && currentPossibleTiles.Contains(new Point(xMin, yMax)) == false)
            {
                adjacentTiles.Add(new Point(xMin, yMax));
            }
            return adjacentTiles;
        }
    }
}
