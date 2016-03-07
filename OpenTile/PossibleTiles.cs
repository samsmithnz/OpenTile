using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            for (int y = yMin; y < yMax; y++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    System.Diagnostics.Debug.WriteLine("X: " + x + ",Y:" + y);
                    if (map[x, y] == true)
                    {
                        possibleTiles.AddRange(FindAdjacentPoints(startingLocation, new Point(x, y), width, height, xMin, xMax, yMin, yMax, map, possibleTiles));
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

            //Off the current position, check each point around it 
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

            //if (map[currentLocation.X, yMax] == true && currentPossibleTiles.Contains(new Point(currentLocation.X, yMax)) == false && originalStartLocation != new Point(currentLocation.X, yMax))
            //{
            //    adjacentTiles.Add(new Point(currentLocation.X, yMax));
            //    //if (adjacentTiles[adjacentTiles.Count-1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y ==2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[xMax, yMax] == true && currentPossibleTiles.Contains(new Point(xMax, yMax)) == false && adjacentTiles.Contains(new Point(xMax, yMax)) == false && originalStartLocation != new Point(xMax, yMax))
            //{
            //    adjacentTiles.Add(new Point(xMax, yMax));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[xMax, currentLocation.Y] == true && currentPossibleTiles.Contains(new Point(xMax, currentLocation.Y)) == false && adjacentTiles.Contains(new Point(xMax, currentLocation.Y)) == false && originalStartLocation != new Point(xMax, currentLocation.Y))
            //{
            //    adjacentTiles.Add(new Point(xMax, currentLocation.Y));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[xMax, yMin] == true && currentPossibleTiles.Contains(new Point(xMax, yMin)) == false && adjacentTiles.Contains(new Point(xMax, yMin)) == false && originalStartLocation != new Point(xMax, yMin))
            //{
            //    adjacentTiles.Add(new Point(xMax, yMin));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[currentLocation.X, yMin] == true && currentPossibleTiles.Contains(new Point(currentLocation.X, yMin)) == false && adjacentTiles.Contains(new Point(currentLocation.X, yMin)) == false && originalStartLocation != new Point(currentLocation.X, yMin))
            //{
            //    adjacentTiles.Add(new Point(currentLocation.X, yMin));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[xMin, yMin] == true && currentPossibleTiles.Contains(new Point(xMin, yMin)) == false && adjacentTiles.Contains(new Point(xMin, yMin)) == false && originalStartLocation != new Point(xMin, yMin))
            //{
            //    adjacentTiles.Add(new Point(xMin, yMin));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[xMin, currentLocation.Y] == true && currentPossibleTiles.Contains(new Point(xMin, currentLocation.Y)) == false && adjacentTiles.Contains(new Point(xMin, currentLocation.Y)) == false && originalStartLocation != new Point(xMin, currentLocation.Y))
            //{
            //    adjacentTiles.Add(new Point(xMin, currentLocation.Y));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            //if (map[xMin, yMax] == true && currentPossibleTiles.Contains(new Point(xMin, yMax)) == false && adjacentTiles.Contains(new Point(xMin, yMax)) == false && originalStartLocation != new Point(xMin, yMax))
            //{
            //    adjacentTiles.Add(new Point(xMin, yMax));
            //    //if (adjacentTiles[adjacentTiles.Count - 1].X == 1 && adjacentTiles[adjacentTiles.Count - 1].Y == 2)
            //    //{
            //    //    Console.WriteLine("Here");
            //    //}
            //}
            return adjacentTiles;
        }
    }
}