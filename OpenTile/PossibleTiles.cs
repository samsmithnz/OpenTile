using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace OpenTile
{
    public class PossibleTiles
    {

        public static List<Point> FindTiles(Point startingLocation, int range, string[,] map)
        {
            List<Point> possibleTiles = new List<Point>();
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            //Ensure that we don't search off the side of the map
            int xMin = startingLocation.X - range;
            if (xMin < 0)
            {
                xMin = 0;
            }
            int xMax = startingLocation.X + range;
            if (xMax > width - 1)
            {
                xMax = width - 1;
            }
            int zMin = startingLocation.Y - range;
            if (zMin < 0)
            {
                zMin = 0;
            }
            int zMax = startingLocation.Y + range;
            if (zMax > height - 1)
            {
                zMax = height - 1;
            }

            //Start the search, looking in adjacent nodes
            for (int z = zMin; z <= zMax; z++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    //System.Diagnostics.Debug.WriteLine("x,z:" + x + "," + z + ", Map:" + map[x, z]);
                    Point newVector = new Point(x, z);
                    //System.Diagnostics.Debug.WriteLine("newVector:" + newVector.ToString());
                    //if (map[x, z] != "")
                    //{
                    //    System.Diagnostics.Debug.WriteLine("No Map: '" + map[x, z] + "'");
                    //}
                    //else if (startingLocation == newVector)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("matching startingLocation: " + startingLocation + ", newVector: " + newVector);
                    //}
                    //else 
                    if (map[x, z] == "" && startingLocation != new Point(x, z))
                    {
                        //int xIndex = Convert.ToInt32(startingLocation.X) - x;
                        //if (xIndex < 0)
                        //{
                        //    xIndex = xIndex * -1;
                        //}
                        //int zIndex = Convert.ToInt32(startingLocation.Y) - z;
                        //if (zIndex < 0)
                        //{
                        //    zIndex = zIndex * -1;
                        //}
                        //int costOfMovement = Convert.ToInt32(Math.Round(Math.Sqrt((xIndex) * (xIndex) + (zIndex) * (zIndex)), 0));
                        //if (costOfMovement < range)
                        //{
                        //    //TODO: Need to u
                        //    possibleTiles.Add(new Point(x, y));
                        //}
                        //else 

                        //if (costOfMovement <= range)
                        //{
                        //Check that we can get a path to the point
                        SearchParameters searchParameters = new SearchParameters(startingLocation, new Point(x, z), map);
                        PathFinding pathFinder = new PathFinding(searchParameters);
                        PathFindingResult pathResult = pathFinder.FindPath();
                        if (pathResult != null && pathResult.Path.Count > 0 && pathResult.Path.Count <= range)
                        {
                            //foreach (Tile item in pathResult.Tiles)
                            //{
                            //    Debug.WriteLine("F:" + item.F + "," + item.ToString() + ",TraversalCost:" + item.TraversalCost);
                            //}
                            if (possibleTiles.Contains(newVector) == false)
                            {
                                possibleTiles.Add(new Point(x, z));
                            }
                        }
                        //}

                        //possibleTiles.AddRange(FindAdjacentPoints(startingLocation, new Point(x, y), width, height, xMin, xMax, yMin, yMax, map, possibleTiles));
                    }
                }
            }
            return possibleTiles;
        }

        ////Get possible tiles, within constraints of map, including both square and diagonal tiles from current position
        //private static List<Point> FindAdjacentPoints(Point originalStartLocation, Point currentLocation, int width, int height, int xMapMin, int xMapMax, int yMapMin, int yMapMax, string[,] map, List<Point> currentPossibleTiles)
        //{
        //    List<Point> adjacentTiles = new List<Point>();
        //    //Based on our current position, ensure we still stay on the map
        //    int yMin = currentLocation.Y - 1;
        //    if (yMin < yMapMin)
        //    {
        //        yMin = yMapMin;
        //    }
        //    int yMax = currentLocation.Y + 1;
        //    if (yMax > yMapMax)
        //    {
        //        yMax = yMapMax;
        //    }
        //    int xMin = currentLocation.X - 1;
        //    if (xMin < xMapMin)
        //    {
        //        xMin = xMapMin;
        //    }
        //    int xMax = currentLocation.X + 1;
        //    if (xMax > xMapMax)
        //    {
        //        xMax = xMapMax;
        //    }

        //    //Check each point around the current position
        //    for (int y = yMin; y <= yMax; y++)
        //    {
        //        for (int x = xMin; x <= xMax; x++)
        //        {
        //            if (map[x, y] == true && currentPossibleTiles.Contains(new Point(x, y)) == false && originalStartLocation != new Point(x, y))
        //            {
        //                adjacentTiles.Add(new Point(x, y));
        //                //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
        //                //{
        //                //    Console.WriteLine("Here");
        //                //}
        //            }
        //        }
        //    }

        //    return adjacentTiles;
        //}
    }
}