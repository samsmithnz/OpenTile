using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//Initial implementation from: http://blog.two-cats.com/2014/06/a-star-example/

namespace OpenTile
{
    public class Tile
    {
        private Tile parentTile;

        /// <summary>
        /// The node's location in the grid
        /// </summary>
        public Vector3 Location { get; private set; }

        /// <summary>
        /// True when the node may be traversed, otherwise false
        /// </summary>
        /// //formerly IsWalkable
        public string TileType { get; set; }

        /// <summary>
        /// Cost from start to here
        /// </summary>
        public float G { get; private set; }

        /// <summary>
        /// Estimated cost from here to end
        /// </summary>
        public float H { get; private set; }

        /// <summary>
        /// Flags whether the node is open, closed or untested by the PathFinder
        /// </summary>
        public TileState State { get; set; }

        /// <summary>
        /// Estimated total cost (F = G + H)
        /// </summary>
        public float F
        {
            get { return this.G + this.H; }
        }

        public int TraversalCost
        {
            get { return Convert.ToInt32(F); }
        }

        /// <summary>
        /// Gets or sets the parent node. The start node's parent is always null.
        /// </summary>
        public Tile ParentTile
        {
            get { return this.parentTile; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                this.parentTile = value;
                this.G = this.parentTile.G + GetTraversalCost(this.Location, this.parentTile.Location);
            }
        }

        /// <summary>
        /// Creates a new instance of Node.
        /// </summary>
        /// <param name="x">The node's location along the X axis</param>
        /// <param name="z">The node's location along the Z axis</param>
        /// <param name="isWalkable">True if the node can be traversed, false if the node is a wall</param>
        /// <param name="endLocation">The location of the destination node</param>
        public Tile(int x, int z, string tileType, Vector3 endLocation)
        {
            this.Location = new Vector3(x, 0, z);
            this.State = TileState.Untested;
            this.TileType = tileType;
            this.H = GetTraversalCost(this.Location, endLocation);
            this.G = 0;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}: {2}", this.Location.x.ToString("0.00"), this.Location.z.ToString("0.00"), this.State.ToString());
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        internal static float GetTraversalCost(Vector3 location, Vector3 otherLocation)
        {
            float deltaX = otherLocation.x - location.x;
            float deltaY = otherLocation.z - location.z;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
