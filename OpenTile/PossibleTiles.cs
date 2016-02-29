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

        public static List<Point> FindTiles(Point startLocation, int range, int width, int height, bool[,] visitedTiles)
        {
            //bool[,] validTiles;
            //Array.Copy(visitedTles, validTiles, visitedTles.GetUpperBound(1));

            //validTiles = new bool[visitedTles.Length];
            //sourceArray.CopyTo(targetArray, 0);

            //bool[,] validTiles = new bool[height, width];
            //for (int x = 0; x < width; x++)
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        validTiles[y, x] = (x >= width) || (y >= height) ? Rectangle.Empty : tiles[y, x];
            //    }
            //}
            List<Point> result = new List<Point>();
            int yMin = startLocation.Y - range;
            if (yMin < 0)
            {
                yMin = 0;
            }
            int yMax = startLocation.Y + range;
            if (yMax > height - 1)
            {
                yMax = height - 1;
            }
            int xMin = startLocation.X - range;
            if (xMin < 0)
            {
                xMin = 0;
            }
            int xMax = startLocation.X + range;
            if (xMax > width - 1)
            {
                xMax = width - 1;
            }
            for (int y = yMin; y < yMax; y++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    System.Diagnostics.Debug.WriteLine("X: " + y + ",Y:" + y);
                    result.AddRange(FindAdjacentPoints(new Point(x, y), width, height, visitedTiles));
                }
            }
            //result = FindAdjacentPoints(startLocation, visitedTles);
            return result;
        }


        private static List<Point> FindAdjacentPoints(Point currentLocation, int width, int height, bool[,] visitedTiles)
        {
            List<Point> result = new List<Point>();
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

            if (visitedTiles[currentLocation.X, yMax] == true)
            {
                result.Add(new Point(currentLocation.X, yMax));
            }
            if (visitedTiles[xMax, yMax] == true)
            {
                result.Add(new Point(xMax, yMax));
            }
            if (visitedTiles[xMax, currentLocation.Y] == true)
            {
                result.Add(new Point(xMax, currentLocation.Y));
            }
            if (visitedTiles[xMax, yMin] == true)
            {
                result.Add(new Point(xMax, yMin));
            }
            if (visitedTiles[currentLocation.X, yMin] == true)
            {
                result.Add(new Point(currentLocation.X, yMin));
            }
            if (visitedTiles[xMin, yMin] == true)
            {
                result.Add(new Point(xMin, yMin));
            }
            if (visitedTiles[xMin, currentLocation.Y] == true)
            {
                result.Add(new Point(xMin, currentLocation.Y));
            }
            if (visitedTiles[xMin, yMax] == true)
            {
                result.Add(new Point(xMin, yMax));
            }
            return result;
        }
    }
}
