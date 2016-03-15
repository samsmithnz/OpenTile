﻿using System;
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
        public static bool CalculateCover(Point currentPosition, int width, int height, bool[,] validTiles, List<Point> enemyLocations)
        {
            List<Point> coverTiles = FindAdjacentCover(currentPosition, width, height, validTiles);
            //Note that this result should be inversed
            return !CalculateIfPlayerIsFlanked(currentPosition, width, height, validTiles, coverTiles, enemyLocations);
        }

        /// <summary>
        /// Calculate if the player is flanked - a sub function as part of the cover
        /// </summary>
        /// <returns>False indicates the player is safely in cover, true indicates the player is flanked.</returns>
        private static bool CalculateIfPlayerIsFlanked(Point currentPosition, int width, int height, bool[,] validTiles, List<Point> coverTiles, List<Point> enemyLocations)
        {
            bool currentLocationIsFlanked = false;
            if (coverTiles.Count == 0)
            {
                return true;
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
                int coverLineNorth = -1;
                int coverLineEast = -1;
                int coverLineSouth = -1;
                int coverLineWest = -1;
                foreach (Point coverTileItem in coverTiles)
                {
                    if (currentPosition.Y < coverTileItem.Y)
                    {
                        coverIsNorth = true;
                        coverLineNorth = coverTileItem.Y - 0;
                    }
                    if (currentPosition.Y > coverTileItem.Y)
                    {
                        coverIsSouth = true;
                        coverLineSouth = coverTileItem.Y + 0;
                    }
                    if (currentPosition.X < coverTileItem.X)
                    {
                        coverIsEast = true;
                        coverLineEast = coverTileItem.X - 0;
                    }
                    if (currentPosition.X > coverTileItem.X)
                    {
                        coverIsWest = true;
                        coverLineWest = coverTileItem.X + 0;
                    }
                }

                //Work out where the enemy is relative to the cover
                foreach (Point enemyItem in enemyLocations)
                {
                    //  In Cover
                    //  □ E □ □
                    //  □ ■ □ □ 
                    //  □ S ■ E 
                    //  □ □ □ □  

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
                        if (coverIsNorth == false && coverIsEast == false) //No cover in North or East = always flanked by Northeast Enenmy
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (coverIsNorth == true && enemyItem.Y < coverLineNorth && coverIsEast == true && enemyItem.X < coverLineEast)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (coverIsNorth == true && enemyItem.Y <= coverLineNorth && coverIsEast == false) //There is cover in the North, but the enemy is past it + no East cover
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (coverIsEast == true && enemyItem.X <= coverLineEast && coverIsNorth == false) //There is cover in the East, but the enemy is past it + no North cover
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located NorthWest
                    if (enemyItem.Y >= currentPosition.Y && enemyItem.X <= currentPosition.X)
                    {
                        if (coverIsNorth == false && coverIsWest == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (coverIsNorth != true && enemyItem.Y <= coverLineNorth && coverIsWest != true && enemyItem.X >= coverLineWest)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (coverIsNorth == true && enemyItem.Y <= coverLineNorth && coverIsWest == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (coverIsWest == true && enemyItem.X >= coverLineWest && coverIsNorth == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located SouthEast
                    if (enemyItem.Y <= currentPosition.Y && enemyItem.X >= currentPosition.X)
                    {
                        if (coverIsSouth == false && coverIsEast == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (coverIsSouth != true && enemyItem.Y >= coverLineSouth && coverIsEast != true && enemyItem.X <= coverLineEast)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (coverIsSouth == true && enemyItem.Y >= coverLineSouth && coverIsEast == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (coverIsEast == true && enemyItem.X <= coverLineEast && coverIsSouth == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located SouthWest
                    if (enemyItem.Y <= currentPosition.Y && enemyItem.X <= currentPosition.X)
                    {
                        if (coverIsSouth == false && coverIsWest == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        //else if (coverIsSouth != true && enemyItem.Y >= coverLineSouth && coverIsWest != true && enemyItem.X >= coverLineWest)
                        //{
                        //    currentLocationIsFlanked = true;
                        //    break;
                        //}
                        else if (coverIsSouth == true && enemyItem.Y >= coverLineSouth && coverIsWest == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (coverIsWest == true && enemyItem.X >= coverLineWest && coverIsSouth == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
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
