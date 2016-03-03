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
        /// <summary>
        /// Calculate if the player is in cover. 
        /// </summary>
        /// <returns>True if the player is in cover</returns>
        public static bool CalculateCover(Point currentPosition, int width, int height, bool[,] validTiles, List<Point> enemyLocations)
        {
            List<Point> coverTiles = FindAdjacentCover(currentPosition, width, height, validTiles);
            if (coverTiles.Count > 0)
            {
                if (enemyLocations == null || enemyLocations.Count == 0)
                {
                    return true;
                }
                else
                {
                    //Note that this result should be inversed
                    return !CalculateIfPlayerIsFlanked(currentPosition, width, height, validTiles, coverTiles, enemyLocations);
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calculate if the player is flanked - a sub function as part of the cover
        /// </summary>
        /// <returns>False indicates the player is safely in cover, true indicates the player is flanked.</returns>
        private static bool CalculateIfPlayerIsFlanked(Point currentPosition, int width, int height, bool[,] validTiles, List<Point> coverTiles, List<Point> enemyLocations)
        {
            bool currentLocationIsFlanked = false;
            if (coverTiles == null || coverTiles.Count == 0)
            {
                return currentLocationIsFlanked;
            }
            else if (enemyLocations == null || enemyLocations.Count == 0)
            {
                return currentLocationIsFlanked;
            }
            else
            {
                // Work out where the cover is relative to the player
                bool coverIsNorth = false;
                bool coverIsEast = false;
                bool coverIsSouth = false;
                bool coverIsWest = false;
                foreach (Point coverTileItem in coverTiles)
                {
                    if (currentPosition.Y < coverTileItem.Y)
                    {
                        coverIsNorth = true;
                    }
                    if (currentPosition.Y > coverTileItem.Y)
                    {
                        coverIsSouth = true;
                    }
                    if (currentPosition.X < coverTileItem.X)
                    {
                        coverIsEast = true;
                    }
                    if (currentPosition.X > coverTileItem.X)
                    {
                        coverIsWest = true;
                    }
                }

                //Work out where the enemy is relative to the cover
                foreach (Point enemyItem in enemyLocations)
                {
                    if (coverIsNorth == true && currentPosition.Y >= enemyItem.Y - 1)
                    {
                        currentLocationIsFlanked = true;
                        break;
                    }
                    else if (coverIsSouth == true && currentPosition.Y <= enemyItem.Y + 1)
                    {
                        currentLocationIsFlanked = true;
                        break;
                    }
                    else if (coverIsEast == true && currentPosition.X >= enemyItem.X - 1)
                    {
                        currentLocationIsFlanked = true;
                        break;
                    }
                    else if (coverIsWest == true && currentPosition.X <= enemyItem.X + 1)
                    {
                        currentLocationIsFlanked = true;
                        break;
                    }
                }

                return currentLocationIsFlanked;
            }            
        }

        /// <summary>
        /// Look at adjacent squares for cover
        /// </summary>
        /// <returns>A List of Point objects for each item of cover</returns>
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
