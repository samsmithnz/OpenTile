using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenTile
{
    public class PathFindingResult
    {

        public List<Tile> Tiles;
        public List<Vector3> Path;

        public Tile GetLastTile()
        {
            if (this.Tiles != null && this.Tiles.Count > 0)
            {
                return this.Tiles[this.Tiles.Count - 1];
            }
            else
            {
                return null;
            }
        }

        public PathFindingResult()
        {
            this.Tiles = new List<Tile>();
            this.Path = new List<Vector3>();
        }
    }
}
