using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OpenTile
{
    public class Cover
    {
        public static bool CalculateCover(Point currentPosition, int width, int height, bool[,] validTiles)
        {
            List<Point> coverTiles = FindAdjacentCover(currentPosition, width, height, validTiles);
            if (coverTiles.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<Point> FindAdjacentCover(Point currentLocation, int width, int height, bool[,] validTiles)
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

            //Get possible tiles, within constraints of map, including only square titles from current position (not diagonally)
            if (validTiles[currentLocation.X, yMax] == false)
            {
                result.Add(new Point(currentLocation.X, yMax));
            }
            if (validTiles[xMax, currentLocation.Y] == false)
            {
                result.Add(new Point(xMax, currentLocation.Y));
            }
            if (validTiles[currentLocation.X, yMin] == false)
            {
                result.Add(new Point(currentLocation.X, yMin));
            }
            if (validTiles[xMin, currentLocation.Y] == false)
            {
                result.Add(new Point(xMin, currentLocation.Y));
            }
            return result;
        }
    }
}
