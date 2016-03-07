using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
