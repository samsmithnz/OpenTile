using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OpenTile
{
    public static class Utility
    {

        /// <summary>
        /// Generate a random number (int) within lower and upper bounds
        /// </summary>
        private static System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        public static int GenerateRandomNumber(int lowerBound, int upperBound)
        {
            int result = rnd.Next(lowerBound, upperBound + 1); //+1 because the upperbound is never used
            //Debug.LogWarning("GenerateRandomNumber (range " + lowerBound + "-" + upperBound + "): " + result);
            return result;
        }

        public static Point FindNearestTile(Point currentLocation, List<Point> possibleTiles)
        {
            Point result = Point.Empty;
            double nearestDistance = 1000;
            foreach (Point item in possibleTiles)
            {
                double distance = Math.Sqrt(Math.Pow((item.X - currentLocation.X), 2) + Math.Pow((item.Y - currentLocation.Y), 2));
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    result = item;
                }
            }
            return result;
        }
    }
}
