using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OpenTile
{
    public class PossibleTiles
    {

        public static List<Point> FindTiles(Point startingLocation, int range, int width, int height, bool[,] map)
        {
            List<Point> possibleTiles = new List<Point>();

            //Ensure that we don't search off the side of the map
            int yMin = startingLocation.Y - range;
            if (yMin < 0)
            {
                yMin = 0;
            }
            int yMax = startingLocation.Y + range;
            if (yMax > height - 1)
            {
                yMax = height - 1;
            }
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

            //Start the search, looking in adjacent nodes
            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    //System.Diagnostics.Debug.WriteLine("X: " + x + ",Y:" + y);
                    if (map[x, y] == true && startingLocation != new Point(x, y))
                    {
                        int yIndex = startingLocation.Y - y;
                        if (yIndex < 0)
                        {
                            yIndex = yIndex * -1;
                        }
                        int xIndex = startingLocation.X - x;
                        if (xIndex < 0)
                        {
                            xIndex = xIndex * -1;
                        }
                        int costOfMovement = Convert.ToInt32(Math.Round(Math.Sqrt((xIndex) * (xIndex) + (yIndex) * (yIndex)), 0));
                        if (costOfMovement < range)
                        {
                            possibleTiles.Add(new Point(x, y));
                        }
                        else if (costOfMovement == range)
                        {
                            //Check that we can get a path to the point
                            SearchParameters searchParameters = new SearchParameters(startingLocation, new Point(x, y), map);
                            PathFinding pathFinder = new PathFinding(searchParameters);
                            List<Point> path = pathFinder.FindPath();
                            if (path.Count > 0 && path.Count <= range)
                            {
                                possibleTiles.Add(new Point(x, y));
                            }
                        }

                        //possibleTiles.AddRange(FindAdjacentPoints(startingLocation, new Point(x, y), width, height, xMin, xMax, yMin, yMax, map, possibleTiles));
                    }
                }
            }
            return possibleTiles;
        }

        //Get possible tiles, within constraints of map, including both square and diagonal tiles from current position
        private static List<Point> FindAdjacentPoints(Point originalStartLocation, Point currentLocation, int width, int height, int xMapMin, int xMapMax, int yMapMin, int yMapMax, bool[,] map, List<Point> currentPossibleTiles)
        {
            List<Point> adjacentTiles = new List<Point>();
            //Based on our current position, ensure we still stay on the map
            int yMin = currentLocation.Y - 1;
            if (yMin < yMapMin)
            {
                yMin = yMapMin;
            }
            int yMax = currentLocation.Y + 1;
            if (yMax > yMapMax)
            {
                yMax = yMapMax;
            }
            int xMin = currentLocation.X - 1;
            if (xMin < xMapMin)
            {
                xMin = xMapMin;
            }
            int xMax = currentLocation.X + 1;
            if (xMax > xMapMax)
            {
                xMax = xMapMax;
            }

            //Check each point around the current position
            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    if (map[x, y] == true && currentPossibleTiles.Contains(new Point(x, y)) == false && originalStartLocation != new Point(x, y))
                    {
                        adjacentTiles.Add(new Point(x, y));
                        //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
                        //{
                        //    Console.WriteLine("Here");
                        //}
                    }
                }
            }

            return adjacentTiles;
        }
    }
}