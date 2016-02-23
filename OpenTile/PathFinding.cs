﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTile
{
    public class PathFinding
    {
        private int width;
        private int height;
        private Tile[,] tiles;
        private Tile startTile;
        private Tile endTile;
        private SearchParameters searchParameters;

        /// <summary>
        /// Create a new instance of PathFinder
        /// </summary>
        /// <param name="searchParameters"></param>
        public PathFinding(SearchParameters searchParameters)
        {
            this.searchParameters = searchParameters;
            InitializeTiles(searchParameters.Map);
            this.startTile = this.tiles[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
            this.startTile.State = TileState.Open;
            this.endTile = this.tiles[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        public List<Point> FindPath()
        {
            // The start tile is the first entry in the 'open' list
            List<Point> path = new List<Point>();
            bool success = Search(startTile);
            if (success)
            {
                // If a path was found, follow the parents from the end tile to build a list of locations
                Tile tile = this.endTile;
                while (tile.ParentTile != null)
                {
                    path.Add(tile.Location);
                    tile = tile.ParentTile;
                }

                // Reverse the list so it's in the correct order when returned
                path.Reverse();
            }

            return path;
        }

        /// <summary>
        /// Builds the tile grid from a simple grid of booleans indicating areas which are and aren't walkable
        /// </summary>
        /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
        private void InitializeTiles(bool[,] map)
        {
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);
            this.tiles = new Tile[this.width, this.height];
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    this.tiles[x, y] = new Tile(x, y, map[x, y], this.searchParameters.EndLocation);
                }
            }
        }

        /// <summary>
        /// Attempts to find a path to the destination tile using <paramref name="currentTile"/> as the starting location
        /// </summary>
        /// <param name="currentTile">The tile from which to find a path</param>
        /// <returns>True if a path to the destination has been found, otherwise false</returns>
        private bool Search(Tile currentTile)
        {
            // Set the current tile to Closed since it cannot be traversed more than once
            currentTile.State = TileState.Closed;
            List<Tile> nextTiles = GetAdjacentWalkableTiles(currentTile);

            // Sort by F-value so that the shortest possible routes are considered first
            nextTiles.Sort((tile1, tile2) => tile1.F.CompareTo(tile2.F));
            foreach (var nextTile in nextTiles)
            {
                // Check whether the end tile has been reached
                if (nextTile.Location == this.endTile.Location)
                {
                    return true;
                }
                else
                {
                    // If not, check the next set of tiles
                    if (Search(nextTile)) // Note: Recurses back into Search(Tile)
                        return true;
                }
            }

            // The method returns false if this path leads to be a dead end
            return false;
        }

        /// <summary>
        /// Returns any tiles that are adjacent to <paramref name="fromTile"/> and may be considered to form the next step in the path
        /// </summary>
        /// <param name="fromTile">The tile from which to return the next possible tiles in the path</param>
        /// <returns>A list of next possible tiles in the path</returns>
        private List<Tile> GetAdjacentWalkableTiles(Tile fromTile)
        {
            List<Tile> walkableTiles = new List<Tile>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromTile.Location);

            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                    continue;

                Tile tile = this.tiles[x, y];
                // Ignore non-walkable tiles
                if (!tile.IsWalkable)
                    continue;

                // Ignore already-closed tiles
                if (tile.State == TileState.Closed)
                    continue;

                // Already-open tiles are only added to the list if their G-value is lower going via this route.
                if (tile.State == TileState.Open)
                {
                    float traversalCost = Tile.GetTraversalCost(tile.Location, tile.ParentTile.Location);
                    float gTemp = fromTile.G + traversalCost;
                    if (gTemp < tile.G)
                    {
                        tile.ParentTile = fromTile;
                        walkableTiles.Add(tile);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    tile.ParentTile = fromTile;
                    tile.State = TileState.Open;
                    walkableTiles.Add(tile);
                }
            }

            return walkableTiles;
        }

        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
        private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return new Point[]
            {
                new Point(fromLocation.X-1, fromLocation.Y-1),
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X-1, fromLocation.Y+1),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X+1, fromLocation.Y-1),
                new Point(fromLocation.X,   fromLocation.Y-1)
            };
        }
    }
}
