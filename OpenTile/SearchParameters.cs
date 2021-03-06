﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

//Initial implementation from: http://blog.two-cats.com/2014/06/a-star-example/

namespace OpenTile
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {
        public Point StartingLocation { get; set; }
        public Point EndLocation { get; set; }
        public string[,] Map { get; set; }

        public SearchParameters(Point startingLocation, Point endLocation, string[,] map)
        {
            this.StartingLocation = startingLocation;
            this.EndLocation = endLocation;
            this.Map = map;
        }
    }
}
