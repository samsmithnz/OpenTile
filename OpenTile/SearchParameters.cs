using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//Initial implementation from: http://blog.two-cats.com/2014/06/a-star-example/

namespace OpenTile
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {
        public Vector3 startingLocation { get; set; }
        public Vector3 EndLocation { get; set; }
        public string[,] Map { get; set; }

        public SearchParameters(Vector3 startingLocation, Vector3 endLocation, string[,] map)
        {
            this.startingLocation = startingLocation;
            this.EndLocation = endLocation;
            this.Map = map;
        }
    }
}
