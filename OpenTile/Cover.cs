using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenTile
{
    public class Cover
    {
        /// <summary>
        /// Calculate if the player is in cover. 
        /// </summary>
        /// <returns>True if the player is in cover</returns>
        public static CoverState CalculateCover(Vector3 currentPosition, string[,] map, List<Vector3> enemyLocations)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            CoverState result = new CoverState();
            List<Vector3> coverTiles = FindAdjacentCover(currentPosition, width, height, map);
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
                foreach (Vector3 coverTileItem in coverTiles)
                {
                    if (currentPosition.x < coverTileItem.x)
                    {
                        result.InEastCover = true;
                        coverLineEast = Convert.ToInt32(coverTileItem.x) - 0;
                    }
                    if (currentPosition.x > coverTileItem.x)
                    {
                        result.InWestCover = true;
                        coverLineWest = Convert.ToInt32(coverTileItem.x) + 0;
                    }
                    if (currentPosition.z < coverTileItem.z)
                    {
                        result.InNorthCover = true;
                        coverLineNorth = Convert.ToInt32(coverTileItem.z) - 0;
                    }
                    if (currentPosition.z > coverTileItem.z)
                    {
                        result.InSouthCover = true;
                        coverLineSouth = Convert.ToInt32(coverTileItem.z) + 0;
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
                foreach (Vector3 enemyItem in enemyLocations)
                {
                    //NOTE: I don't think I need this now that I have cover lines
                    //Check to see if Enemy is right on top of the player, neutralizing each others cover and causing a flank
                    int xPosition = Convert.ToInt32(currentPosition.x - enemyItem.x);
                    if (xPosition < 0)
                    {
                        xPosition = xPosition * -1;
                    }
                    int zPosition = Convert.ToInt32(currentPosition.z - enemyItem.z);
                    if (zPosition < 0)
                    {
                        zPosition = zPosition * -1;
                    }

                    //Now check over all of the levels of cover

                    //Enemy is located NorthEast
                    if (enemyItem.z >= currentPosition.z && enemyItem.x >= currentPosition.x)
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
                        else if (result.InNorthCover == true && enemyItem.z <= coverLineNorth && result.InEastCover == false) //There is cover in the North, but the enemy is past it + no East cover
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InEastCover == true && enemyItem.x <= coverLineEast && result.InNorthCover == false) //There is cover in the East, but the enemy is past it + no North cover
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located NorthWest
                    if (enemyItem.z >= currentPosition.z && enemyItem.x <= currentPosition.x)
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
                        else if (result.InNorthCover == true && enemyItem.z <= coverLineNorth && result.InWestCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InWestCover == true && enemyItem.x >= coverLineWest && result.InNorthCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located SouthEast
                    if (enemyItem.z <= currentPosition.z && enemyItem.x >= currentPosition.x)
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
                        else if (result.InSouthCover == true && enemyItem.z >= coverLineSouth && result.InEastCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InEastCover == true && enemyItem.x <= coverLineEast && result.InSouthCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                    }

                    //Enemy is located SouthWest
                    if (enemyItem.z <= currentPosition.z && enemyItem.x <= currentPosition.x)
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
                        else if (result.InSouthCover == true && enemyItem.z >= coverLineSouth && result.InWestCover == false)
                        {
                            currentLocationIsFlanked = true;
                            break;
                        }
                        else if (result.InWestCover == true && enemyItem.x >= coverLineWest && result.InSouthCover == false)
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
        /// <returns>A List of Vector3 objects for each item of cover</returns>
        private static List<Vector3> FindAdjacentCover(Vector3 currentLocation, int width, int height, string[,] validTiles)
        {
            List<Vector3> result = new List<Vector3>();
            //Make adjustments to ensure that the search doesn't go off the edges of the map
            int xMin = Convert.ToInt32(currentLocation.x) - 1;
            if (xMin < 0)
            {
                xMin = 0;
            }
            int xMax = Convert.ToInt32(currentLocation.x) + 1;
            if (xMax > width - 1)
            {
                xMax = width - 1;
            }
            int zMin = Convert.ToInt32(currentLocation.z) - 1;
            if (zMin < 0)
            {
                zMin = 0;
            }
            int zMax = Convert.ToInt32(currentLocation.z) + 1;
            if (zMax > height - 1)
            {
                zMax = height - 1;
            }

            //Get possible tiles, within constraints of map, including only square titles from current position (not diagonally)
            if (validTiles[Convert.ToInt32(currentLocation.x), Convert.ToInt32(zMax)] != "")
            {
                result.Add(new Vector3(currentLocation.x, 0f, zMax));
            }
            if (validTiles[Convert.ToInt32(xMax), Convert.ToInt32(currentLocation.z)] != "")
            {
                result.Add(new Vector3(xMax, 0f, currentLocation.z));
            }
            if (validTiles[Convert.ToInt32(currentLocation.x), Convert.ToInt32(zMin)] != "")
            {
                result.Add(new Vector3(currentLocation.x, 0f, zMin));
            }
            if (validTiles[Convert.ToInt32(xMin), Convert.ToInt32(currentLocation.z)] != "")
            {
                result.Add(new Vector3(xMin, 0f, currentLocation.z));
            }
            return result;
        }
    }
}
