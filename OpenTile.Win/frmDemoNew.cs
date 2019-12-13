using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTile;

namespace OpenTile.Win
{
    public partial class frmDemoNew : Form
    {
        public frmDemoNew()
        {
            InitializeComponent();
        }

        private string[,] map;
        private SearchParameters searchParameters;

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            txtMap.Text = "";
            int width = int.Parse(txtWidth.Text);
            int height = int.Parse(txtHeight.Text);
            int range = int.Parse(txtRange.Text);
            int startingWidth = 6;
            int startingHeight = 6;

            // Create a larger maze with custom start and end points
            InitializeMap(width, height, new Point(0, 0), new Point(width - 1, height - 1), false);
            //AddRandomItems(width, height, 20);
            AddTestMap();
            //AddStartingLocation(startingWidth, startingHeight);
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();

            //PathFinding pathFinder = new PathFinding(searchParameters);
            //PathFindingResult pathResult = pathFinder.FindPath();
            //pathResult.Path.AddRange(PossibleTiles.FindTiles(new Point(0, 0), range, this.map));
            txtMap.Text += ShowRoute("New Map!");
            txtMap.Text += Environment.NewLine;
        }

        private void AddStartingLocation(int startingWidth, int startingHeight)
        {
            for (int y = this.map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (x < startingWidth && y < startingHeight)
                    {
                        // Show the start position
                        this.map[x, y] = "";
                    }
                }
            }
        }

        /// <summary>
        /// Returns the map and path as a simple grid as a string
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private string ShowRoute(string title)
        {
            ASCIITile[,] newMap = new ASCIITile[this.map.GetLength(0), this.map.GetLength(1)];
            for (int z = this.map.GetLength(1) - 1; z >= 0; z--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    ASCIITile newTile = new ASCIITile();
                    if (this.map[x, z] != "")
                    {
                        System.Diagnostics.Debug.WriteLine(this.map[x, z]);
                    }
                    if (this.map[x, z] == "N")
                    {
                        newTile.NorthSideIsOpen = true;
                        newTile.EastSideIsOpen = false;
                        newTile.SouthSideIsOpen = false;
                        newTile.WestSideIsOpen = false;
                    }
                    else if (this.map[x, z] == "E")
                    {
                        newTile.NorthSideIsOpen = false;
                        newTile.EastSideIsOpen = true;
                        newTile.SouthSideIsOpen = false;
                        newTile.WestSideIsOpen = false;
                    }
                    else if (this.map[x, z] == "S")
                    {
                        newTile.NorthSideIsOpen = false;
                        newTile.EastSideIsOpen = false;
                        newTile.SouthSideIsOpen = true;
                        newTile.WestSideIsOpen = false;
                    }
                    else if (this.map[x, z] == "W")
                    {
                        newTile.NorthSideIsOpen = false;
                        newTile.EastSideIsOpen = false;
                        newTile.SouthSideIsOpen = false;
                        newTile.WestSideIsOpen = true;
                    }
                    else if (this.map[x, z] == "H")
                    {
                        newTile.NorthSideIsOpen = false;
                        newTile.EastSideIsOpen = false;
                        newTile.SouthSideIsOpen = false;
                        newTile.WestSideIsOpen = false;
                    }
                    else 
                    {
                        newTile.NorthSideIsOpen = true;
                        newTile.EastSideIsOpen = true;
                        newTile.SouthSideIsOpen = true;
                        newTile.WestSideIsOpen = true;
                    }
                    newMap[x, z] = newTile;
                }
            }

            StringBuilder route = new StringBuilder();
            route.AppendFormat("{0}\r\n", title);
            for (int z = this.map.GetLength(1) - 1; z >= 0; z--) // Invert the Z-axis so that coordinate 0,0 is shown in the bottom-left
            {
                string row1 = "";
                string row2 = "";
                string row3 = "";
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    row1 += newMap[x, z].Row1;
                    row2 += newMap[x, z].Row2;
                    row3 += newMap[x, z].Row3;
                    //        if (this.searchParameters.startingLocation.Equals(new Point(x, z)))
                    //        {
                    //            // Show the end position
                    //            route.Append('S');
                    //        }
                    //        else if (this.searchParameters.EndLocation.Equals(new Point(x, z)))
                    //        {
                    //            // Show the end position
                    //            route.Append('F');
                    //        }
                    //        else if (this.map[x, z] == "W")
                    //        {
                    //            // Show any barriers
                    //            route.Append('░');
                    //        }
                    //        //else if (path != null && path.Where(p => p.X == x && p.Y == y).Any())
                    //        //{
                    //        //    // Show the path in between
                    //        //    route.Append('*');
                    //        //}
                    //        else
                    //        {
                    //            // Show nodes that aren't part of the path
                    //            route.Append('.');
                    //        }
                    //        //╔═══╦═ ═╗
                    //        //  0 ║ 0 ║ 
                    //        //╠═══╬═══╣
                    //        //║ 0 ║ 1
                    //        //╚═ ═╩═══╝

                }
                route.Append(row1);
                route.Append(Environment.NewLine);
                route.Append(row2);
                route.Append(Environment.NewLine);
                route.Append(row3);
                route.Append(Environment.NewLine);
            }

            route.Append(Environment.NewLine);
            route.Append(Environment.NewLine);
            route.AppendFormat("{0}\r\n", title);
            for (int y = this.map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (this.searchParameters.StartingLocation.Equals(new Point(x, y)))
                    {
                        // Show the end position
                        route.Append('S');
                    }
                    else if (this.searchParameters.EndLocation.Equals(new Point(x, y)))
                    {
                        // Show the end position
                        route.Append('F');
                    }
                    else if (this.map[x, y] != "")
                    {
                        // Show any barriers
                        route.Append(this.map[x, y]);
                    }
                    //else if (path != null && path.Where(p => p.X == x && p.Y == y).Any())
                    //{
                    //    // Show the path in between
                    //    route.Append('*');
                    //}
                    else
                    {
                        // Show nodes that aren't part of the path
                        route.Append('·');
                    }
                    //╔═══╦═ ═╗
                    //  0 ║ 0 ║ 
                    //╠═══╬═══╣
                    //║ 0 ║ 1
                    //╚═ ═╩═══╝

                }
                route.Append(Environment.NewLine);
            }
            return route.ToString();
        }

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        private void InitializeMap(int xMax, int zMax, Point startingLocation, Point endLocation, bool locationsNotSet)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new string[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[x, z] = "";
                }
            }

            if (locationsNotSet == true)
            {
                startingLocation = new Point(1, 2);
                endLocation = new Point(5, 2);
            }
            this.searchParameters = new SearchParameters(startingLocation, endLocation, map);
        }

        private void AddTestMap()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S ■ ■ ■ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ □ □ □ □

            this.map[3, 3] = "N";
            this.map[3, 2] = "H";
            this.map[3, 1] = "S";
            this.map[4, 2] = "E";
            this.map[2, 2] = "W";
        }

        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[4, 1] = "W";
        }

        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[3, 0] = "W";
        }

        private void AddWallWithMaze()
        {
            //  S ■ ■ □ ■ ■ F
            //  □ ■ □ ■ □ ■ □
            //  □ ■ □ ■ □ ■ □
            //  □ ■ □ ■ □ ■ □
            //  ■ □ ■ ■ ■ □ ■

            // long path
            this.map[0, 0] = "W";
            this.map[1, 4] = "W";
            this.map[1, 3] = "W";
            this.map[1, 2] = "W";
            this.map[1, 1] = "W";
            this.map[2, 4] = "W";
            this.map[2, 0] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[3, 0] = "W";
            this.map[4, 4] = "W";
            this.map[4, 0] = "W";
            this.map[5, 4] = "W";
            this.map[5, 3] = "W";
            this.map[5, 2] = "W";
            this.map[5, 1] = "W";
            this.map[6, 0] = "W";
        }

        private void AddWallWithSpinningMaze()
        {
            //  4 S ■ □ □ □ □ □
            //  3 □ ■ □ ■ ■ ■ □
            //  2 □ ■ □ □ F ■ □
            //  1 □ ■ ■ ■ ■ ■ □
            //  0 □ □ □ □ □ □ □
            //    0 1 2 3 4 5 6

            // long path
            this.map[1, 1] = "W";
            this.map[1, 2] = "W";
            this.map[1, 3] = "W";
            this.map[1, 4] = "W";
            this.map[2, 1] = "W";
            this.map[3, 1] = "W";
            this.map[3, 3] = "W";
            this.map[4, 1] = "W";
            this.map[4, 3] = "W";
            this.map[5, 1] = "W";
            this.map[5, 2] = "W";
            this.map[5, 3] = "W";
        }

        private void AddRandomItems(int xMax, int zMax, int probOfMapBeingBlocked)
        {
            //Initial randomization
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = "H"; //high ground
                    }
                    else if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = "N"; //Opening to the North
                    }
                    else if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = "E"; //Opening to the East
                    }
                    else if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = "S"; //Opening to the South
                    }
                    else if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = "W"; //Opening to the West
                    }
                }
            }

            //Make corrections
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    //Check that if the opening is to the North, that there isn't high ground above it
                    if (this.map[x, z] == "N")
                    {
                        if (x > 0 && this.map[x - 1, z] != "")
                        {
                            this.map[x, z] = "H";
                        }
                    }
                    //Check that if the opening is to the South, that there isn't high ground below it
                    if (this.map[x, z] == "S")
                    {
                        if (x < xMax - 1 && this.map[x + 1, z] != "")
                        {
                            this.map[x, z] = "H";
                        }
                    }
                    //Check that if the opening is to the East, that there isn't high ground to the East of it
                    if (this.map[x, z] == "N")
                    {
                        if (z > 0 && this.map[x, z - 1] != "")
                        {
                            this.map[x, z] = "H";
                        }
                    }
                    //Check that if the opening is to the West, that there isn't high ground to the West of it
                    if (this.map[x, z] == "S")
                    {
                        if (z < zMax - 1 && this.map[x, z + 1] != "")
                        {
                            this.map[x, z] = "H";
                        }
                    }
                }
            }

        }

        private void DebugPrintOutMap(int xMax, int zMax)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (this.map[x, z] != "")
                    {
                        Console.WriteLine(" this.map[" + x + ", " + z + "] = " + this.map[x, z] + ";");
                    }
                }
            }
        }

        private void btnDebugPrint_Click(object sender, EventArgs e)
        {
            DebugPrintOutMap(70, 40);
        }
    }



}


