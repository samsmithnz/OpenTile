using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTile.Win
{
    public class ASCIITile
    {
        public ASCIITile()
        {
            NorthSideIsOpen = true;
            EastSideIsOpen = true;
            SouthSideIsOpen = true;
            WestSideIsOpen = true;
        }
        public bool NorthSideIsOpen { get; set; }
        public bool EastSideIsOpen { get; set; }
        public bool SouthSideIsOpen { get; set; }
        public bool WestSideIsOpen { get; set; }

        public string Row1
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                //Top row
                //╔═══╗
                if (WestSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetNorthWestCorner());
                }
                if (NorthSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                    sb.Append(ASCIIPieces.GetSpace());
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetWestEastSide());
                    sb.Append(ASCIIPieces.GetWestEastSide());
                    sb.Append(ASCIIPieces.GetWestEastSide());
                }
                if (EastSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetNorthEastCorner());
                }

                return sb.ToString();
            }
        }

        public string Row2
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                //Middle Row
                //║   ║
                if (WestSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetNorthSouthSide());
                }
                sb.Append(ASCIIPieces.GetSpace());
                sb.Append(ASCIIPieces.GetSpace());
                sb.Append(ASCIIPieces.GetSpace());
                if (EastSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetNorthSouthSide());
                }

                return sb.ToString();
            }
        }

        public string Row3
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                //Bottom Row 
                //╚═══╝
                if (WestSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetSouthWestCorner());
                }

                if (SouthSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                    sb.Append(ASCIIPieces.GetSpace());
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetWestEastSide());
                    sb.Append(ASCIIPieces.GetWestEastSide());
                    sb.Append(ASCIIPieces.GetWestEastSide());
                }
                if (EastSideIsOpen == true)
                {
                    sb.Append(ASCIIPieces.GetSpace());
                }
                else
                {
                    sb.Append(ASCIIPieces.GetSouthEastCorner());
                }

                return sb.ToString();
            }
        }

        //public string GetASCIIText()
        //{
        //    //╔═══╦═ ═╗
        //    //  0 ║ 0 ║ 
        //    //╠═══╬═══╣
        //    //║ 0 ║ 1
        //    //╚═ ═╩═══╝
        //    StringBuilder sb = new StringBuilder();

        //    //Top row
        //    //╔═══╗
        //    sb.Append(ASCIIPieces.GetNorthWestCorner());
        //    if (NorthSideIsOpen == true)
        //    {
        //        sb.Append(ASCIIPieces.GetWestEastSide());
        //        sb.Append(ASCIIPieces.GetWestEastSide());
        //        sb.Append(ASCIIPieces.GetWestEastSide());
        //    }
        //    else
        //    {
        //        sb.Append(ASCIIPieces.GetSpace());
        //        sb.Append(ASCIIPieces.GetSpace());
        //        sb.Append(ASCIIPieces.GetSpace());
        //    }
        //    sb.Append(ASCIIPieces.GetNorthEastCorner());

        //    //Middle Row
        //    //║   ║
        //    if (WestSideIsOpen == true)
        //    {
        //        sb.Append(ASCIIPieces.GetSpace());
        //    }
        //    else
        //    {
        //        sb.Append(ASCIIPieces.GetNorthSouthSide());
        //    }
        //    sb.Append(ASCIIPieces.GetSpace());
        //    sb.Append(ASCIIPieces.GetSpace());
        //    sb.Append(ASCIIPieces.GetSpace());
        //    if (EastSideIsOpen == true)
        //    {
        //        sb.Append(ASCIIPieces.GetSpace());
        //    }
        //    else
        //    {
        //        sb.Append(ASCIIPieces.GetNorthSouthSide());
        //    }

        //    //Bottom Row 
        //    //╚═══╝
        //    sb.Append(ASCIIPieces.GetSouthWestCorner());
        //    if (SouthSideIsOpen == true)
        //    {
        //        sb.Append(ASCIIPieces.GetWestEastSide());
        //        sb.Append(ASCIIPieces.GetWestEastSide());
        //        sb.Append(ASCIIPieces.GetWestEastSide());
        //    }
        //    else
        //    {
        //        sb.Append(ASCIIPieces.GetSpace());
        //        sb.Append(ASCIIPieces.GetSpace());
        //        sb.Append(ASCIIPieces.GetSpace());
        //    }
        //    sb.Append(ASCIIPieces.GetSouthEastCorner());

        //    return sb.ToString();
        //}
    }
}
