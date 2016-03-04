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
                    if (coverTiles.Count == 1)
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
                    else //OK, here comes a temporary, massive hack
                    {
                        //  In Cover
                        //  □ E □ □
                        //  □ ■ □ □ 
                        //  □ S ■ E 
                        //  □ □ □ □  

                        //Check to see if Enemy is right on top of the player, neutralizing each others cover and causing a flank
                        int xPosition = currentPosition.X - enemyItem.X;
                        if (xPosition < 0)
                        {
                            xPosition = xPosition * -1;
                        }
                        int yPosition = currentPosition.Y - enemyItem.Y;
                        if (yPosition < 0)
                        {
                            yPosition = yPosition * -1;
                        }
                        if (xPosition == 1 && yPosition == 1)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }

                        //  In Cover
                        //  □ E □ □
                        //  □ ■ □ □ 
                        //  □ S ■ E 
                        //  □ □ □ □ 
                        foreach (Point coverItem in coverTiles)
                        {
                            if (coverIsEast == true && coverItem.X > currentPosition.X && currentPosition.X == enemyItem.X && coverItem.X == currentPosition.X)
                            {
                                //We can ignore this, as the cover is between the player and enemy - the player is definitely in cover.
                            }
                            else if (coverIsWest == true && coverItem.X < currentPosition.X && currentPosition.X == enemyItem.X && coverItem.X == currentPosition.X)
                            {
                                //We can ignore this, as the cover is between the player and enemy - the player is definitely in cover.
                            }
                            else if (coverIsNorth == true && coverItem.Y > currentPosition.Y && currentPosition.Y == enemyItem.Y && coverItem.Y == currentPosition.Y)
                            {
                                //We can ignore this, as the cover is between the player and enemy - the player is definitely in cover.
                            }
                            else if (coverIsSouth == true && coverItem.Y < currentPosition.Y && currentPosition.Y == enemyItem.Y && coverItem.Y == currentPosition.Y)
                            {
                                //We can ignore this, as the cover is between the player and enemy - the player is definitely in cover.
                            }
                            else
                            {
                                //currentLocationIsFlanked = true;

                            }

                            //if (coverItem.X > currentPosition.X && coverItem.X > enemyItem.X)
                            //{

                            //}
                        }

                        //if (coverIsNorth == true && currentPosition.Y >= enemyItem.Y - 1)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        //else if (coverIsSouth == true && currentPosition.Y <= enemyItem.Y + 1)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        //else if (coverIsEast == true && currentPosition.X >= enemyItem.X - 1)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        //else if (coverIsWest == true && currentPosition.X <= enemyItem.X + 1)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}

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
            //Make adjustments to ensure that the search doesn't go off the edges of the map
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
