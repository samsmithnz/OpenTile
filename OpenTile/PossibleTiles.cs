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
                    Point newLocation = new Point(x, z);
                    if (map[x, z] == "" && startingLocation != newLocation)
                    {
                        //Check that we can get a path to the location
                        SearchParameters searchParameters = new SearchParameters(startingLocation, newLocation, map);
                        PathFinding pathFinder = new PathFinding(searchParameters);
                        PathFindingResult pathResult = pathFinder.FindPath();
                        if (pathResult != null && pathResult.Path.Count > 0 && pathResult.Path.Count <= range)
                        {
                            //Check that we haven't already added this location
                            if (possibleTiles.Contains(newLocation) == false)
                            {
                                possibleTiles.Add(newLocation);
                            }
                        }
                    }
                } //End of x for
            } //End of z for
            return possibleTiles;
        }

    }
}