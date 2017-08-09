using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OpenTile
{
    public class Cover
    {
        /// <summary>
        /// Calculate if the player is in cover. 
        /// </summary>
        /// <returns>True if the player is in cover</returns>
        public static CoverState CalculateCover(Point currentPosition, string[,] map, List<Point> enemyLocations)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            CoverState result = new CoverState();
            List<Point> coverTiles = FindAdjacentCover(currentPosition, width, height, map);
            int coverLineNorth = -1;
            int coverLineEast = -1;
            int coverLineSouth = -1;
            int coverLineWest = -1;
            bool currentLocationIsFlanked = false;

            if (coverTiles.Count == 0)
            {
                result.IsInCover = false;
                return result;
            }
            else
            {
                // Work out where the cover is relative to the player
                foreach (Point coverTileItem in coverTiles)
                {
                    if (currentPosition.Y < coverTileItem.Y)
                    {
                        result.InNorthCover = true;
                        coverLineNorth = coverTileItem.Y - 0;
                    }
                    if (currentPosition.Y > coverTileItem.Y)
                    {
                        result.InSouthCover = true;
                        coverLineSouth = coverTileItem.Y + 0;
                    }
                    if (currentPosition.X < coverTileItem.X)
                    {
                        result.InEastCover = true;
                        coverLineEast = coverTileItem.X - 0;
                    }
                    if (currentPosition.X > coverTileItem.X)
                    {
                        result.InWestCover = true;
                        coverLineWest = coverTileItem.X + 0;
                    }
                }
            }

            if (enemyLocations == null || enemyLocations.Count == 0)
            {
                result.IsInCover = true;
                return result;
            }
            else
            {
                //Work out where the enemy is relative to the cover
                foreach (Point enemyItem in enemyLocations)
                {
                    //NOTE: I don't think I need this now that I have cover lines
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

                    //Now check over all of the levels of cover

                    //Enemy is located NorthEast
                    if (enemyItem.Y >= currentPosition.Y && enemyItem.X >= currentPosition.X)
                    {
                        if (result.InNorthCover == false && result.InEastCover == false) //No cover in North or East = always flanked by Northeast Enenmy
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (result.InNorthCover == true && enemyItem.Y < coverLineNorth && result.InEastCover == true && enemyItem.X < coverLineEast)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (result.InNorthCover == true && enemyItem.Y <= coverLineNorth && result.InEastCover == false) //There is cover in the North, but the enemy is past it + no East cover
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InEastCover == true && enemyItem.X <= coverLineEast && result.InNorthCover == false) //There is cover in the East, but the enemy is past it + no North cover
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located NorthWest
                    if (enemyItem.Y >= currentPosition.Y && enemyItem.X <= currentPosition.X)
                    {
                        if (result.InNorthCover == false && result.InWestCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (result.InNorthCover != true && enemyItem.Y <= coverLineNorth && result.InWestCover != true && enemyItem.X >= coverLineWest)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (result.InNorthCover == true && enemyItem.Y <= coverLineNorth && result.InWestCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InWestCover == true && enemyItem.X >= coverLineWest && result.InNorthCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located SouthEast
                    if (enemyItem.Y <= currentPosition.Y && enemyItem.X >= currentPosition.X)
                    {
                        if (result.InSouthCover == false && result.InEastCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (result.InSouthCover != true && enemyItem.Y >= coverLineSouth && result.InEastCover != true && enemyItem.X <= coverLineEast)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (result.InSouthCover == true && enemyItem.Y >= coverLineSouth && result.InEastCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InEastCover == true && enemyItem.X <= coverLineEast && result.InSouthCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located SouthWest
                    if (enemyItem.Y <= currentPosition.Y && enemyItem.X <= currentPosition.X)
                    {
                        if (result.InSouthCover == false && result.InWestCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (result.InSouthCover != true && enemyItem.Y >= coverLineSouth && result.InWestCover != true && enemyItem.X >= coverLineWest)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (result.InSouthCover == true && enemyItem.Y >= coverLineSouth && result.InWestCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InWestCover == true && enemyItem.X >= coverLineWest && result.InSouthCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                }

                result.IsInCover = !currentLocationIsFlanked;
                return result;
            }
        }

        /// <summary>
        /// Look at adjacent squares for cover
        /// </summary>
        /// <returns>A List of Point objects for each item of cover</returns>
        private static List<Point> FindAdjacentCover(Point currentLocation, int width, int height, string[,] validTiles)
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
            if (validTiles[currentLocation.X, yMax] == "W")
            {
                result.Add(new Point(currentLocation.X, yMax));
            }
            if (validTiles[xMax, currentLocation.Y] == "W")
            {
                result.Add(new Point(xMax, currentLocation.Y));
            }
            if (validTiles[currentLocation.X, yMin] == "W")
            {
                result.Add(new Point(currentLocation.X, yMin));
            }
            if (validTiles[xMin, currentLocation.Y] == "W")
            {
                result.Add(new Point(xMin, currentLocation.Y));
            }
            return result;
        }
    }
}
