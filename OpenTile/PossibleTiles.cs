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

        public static List<Point> FindTiles(Point startLocation, int range, bool[,] visitedTles)
        {
            List<Point> result = new List<Point>();
            result = FindAdjacentPoints(startLocation, visitedTles);
            return result;
        }

        private static List<Point> FindAdjacentPoints(Point currentLocation, bool[,] visitedTles)
        {
            List<Point> result = new List<Point>();
            result.Add(new Point(currentLocation.X, currentLocation.Y + 1));
            result.Add(new Point(currentLocation.X, currentLocation.Y - 1));
            result.Add(new Point(currentLocation.X - 1, currentLocation.Y));
            result.Add(new Point(currentLocation.X + 1, currentLocation.Y));
            return result;
        }
    }
}
