using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTile
{
   public static class ASCIIPieces
    {
        //Pieces
        public static string GetNorthWestCorner()
        {
            //╔:Top left corner
            return "╔";
        }

        public static string GetWestEastSide()
        {
            //═:Top
            return "═";
        }

        public static string GetNorthEastCorner()
        {
            //╗:Top right corner
            return "╗";
        }

        public static string GetNorthSideWithSouthJoin()
        {
            //╦:Top with south join
            return "╦";
        }

        public static string GetNorthSouthSide()
        {
            //║:Side
            return "║";
        }

        public static string GetNorthSouthSideWithEastJoin()
        {
            //╠:Side with East join
            return "╠";
        }

        public static string GetNorthSouthSideWithWestEastJoins()
        {
            //╬:Side with East and West joins
            return "╬";
        }

        public static string GetNorthSouthSideWithWestJoin()
        {
            //╣:Side with West join
            return "╣";
        }

        public static string GetSouthWestCorner()
        {
            //╚:Bottom left corner
            return "╚";
        }

        public static string GetWestEastSideWithNorthJoin()
        {
            //╩:Bottom with North join
            return "╩";
        }

        public static string GetSouthEastCorner()
        {
            //╝:Bottom right corner
            return "╝";
        }

        public static string GetSpace()
        {
            // :Space
            return " ";
        }


    }
}
