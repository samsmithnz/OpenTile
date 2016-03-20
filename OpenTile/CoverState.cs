﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTile
{
    public class CoverState
    {
        public bool IsInCover { get; set; }
        public bool InNorthCover { get; set; }
        public bool InEastCover { get; set; }
        public bool InSouthCover { get; set; }
        public bool InWestCover { get; set; }
    }
}
