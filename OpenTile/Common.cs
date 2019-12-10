using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenTile
{
    public static class Common
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

        public static Vector3 FindNearestTile(Vector3 currentLocation, List<Vector3> possibleTiles)
        {
            Vector3 result = Vector3.zero;
            double nearestDistance = 1000;
            foreach (Vector3 item in possibleTiles)
            {
                double distance = Math.Sqrt(Math.Pow((item.x - currentLocation.x), 2) + Math.Pow((item.y - currentLocation.y), 2));
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
