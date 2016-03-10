using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace OpenTile
{
    public class PathFindingResult
    {

        public List<Tile> Tiles;
        public List<Point> Path;
        public Tile LastTile
        {
            get
            {
                if (Tiles != null && Tiles.Count > 0)
                {
                    return Tiles[Tiles.Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        public PathFindingResult()
        {
            Tiles = new List<Tile>();
            Path = new List<Point>();
        }
    }
}
