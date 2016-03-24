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
    public partial class frmPossibleTiles : Form
    {
        public frmPossibleTiles()
        {
            InitializeComponent();
        }

        private string[,] map;
        //private SearchParameters searchParameters;

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            txtMap.Text = "";
            int range = 1; //USE txtRange.Text !!
            if (txtRange.Text != "")
            {
                range = int.Parse(txtRange.Text);
            }

            //CRITERIA
            // Point startingLocation = new Point(20, 20);
            // int height = 40;
            // int width = 70;
            //// range = 15;
            // InitializeMap(width, height, startingLocation);
            // //  □ □ □ □ □ □ □ 
            // //  □ □ □ □ □ □ □ 
            // //  □ □ □ □ ■ ■ ■ 
            // //  □ □ □ S ■ □ ■ 
            // //  □ □ □ □ ■ ■ ■
            // //  □ □ □ □ □ □ □
            // //  □ □ □ □ □ □ □ 
            // this.map[15, 15] = "W";
            // this.map[15, 14] = "W";
            // this.map[15, 13] = "W";
            // this.map[14, 15] = "W";
            // this.map[14, 13] = "W";
            // this.map[13, 15] = "W";
            // this.map[13, 14] = "W";
            // this.map[13, 13] = "W";
            // this.map[25, 15] = "W";
            // this.map[25, 14] = "W";
            // this.map[25, 13] = "W";
            // this.map[24, 15] = "W";
            // this.map[24, 13] = "W";
            // this.map[23, 15] = "W";
            // this.map[23, 14] = "W";
            // this.map[23, 13] = "W";
            // this.map[15, 25] = "W";
            // this.map[15, 24] = "W";
            // this.map[15, 23] = "W";
            // this.map[14, 25] = "W";
            // this.map[14, 23] = "W";
            // this.map[13, 25] = "W";
            // this.map[13, 24] = "W";
            // this.map[13, 23] = "W";



            //Point startingLocation = new Point(5, 5);
            //int width = 11;
            //int height = 11;
            ////int range = 2;
            //InitializeMap(width, height, startingLocation);
            ////AddRandomItems(width, height, 40);
            //this.map[0, 0] = "W";
            //this.map[5, 0] = "W";
            //this.map[6, 0] = "W";
            //this.map[8, 0] = "W";
            //this.map[1, 1] = "W";
            //this.map[3, 1] = "W";
            //this.map[4, 1] = "W";
            //this.map[5, 1] = "W";
            //this.map[7, 1] = "W";
            //this.map[8, 1] = "W";
            //this.map[1, 2] = "W";
            //this.map[2, 2] = "W";
            //this.map[3, 2] = "W";
            //this.map[4, 2] = "W";
            //this.map[5, 2] = "W";
            //this.map[8, 2] = "W";
            //this.map[9, 2] = "W";
            //this.map[10, 2] = "W";
            //this.map[4, 3] = "W";
            //this.map[5, 3] = "W";
            //this.map[6, 3] = "W";
            //this.map[7, 3] = "W";
            //this.map[8, 3] = "W";
            //this.map[9, 3] = "W";
            //this.map[1, 4] = "W";
            //this.map[2, 4] = "W";
            //this.map[3, 4] = "W";
            //this.map[4, 4] = "W";
            //this.map[5, 4] = "W";
            //this.map[6, 4] = "W";
            //this.map[0, 5] = "W";
            //this.map[1, 5] = "W";
            //this.map[2, 5] = "W";
            //this.map[3, 5] = "W";
            //this.map[5, 5] = "W";
            //this.map[9, 5] = "W";
            //this.map[1, 6] = "W";
            //this.map[2, 6] = "W";
            //this.map[4, 6] = "W";
            //this.map[7, 6] = "W";
            //this.map[3, 7] = "W";
            //this.map[4, 7] = "W";
            //this.map[5, 7] = "W";
            //this.map[6, 7] = "W";
            //this.map[0, 8] = "W";
            //this.map[1, 8] = "W";
            //this.map[2, 8] = "W";
            //this.map[4, 8] = "W";
            //this.map[5, 8] = "W";
            //this.map[7, 8] = "W";
            //this.map[10, 8] = "W";
            //this.map[0, 9] = "W";
            //this.map[5, 9] = "W";
            //this.map[9, 9] = "W";
            //this.map[10, 9] = "W";
            //this.map[6, 10] = "W";
            //this.map[8, 10] = "W";

            Point startingLocation = new Point(0, 1);
            int height = 9;
            int width = 9;
            InitializeMap(width, height, startingLocation);
            // 8 □ □ □ □ □ □ □ □ □
            // 7 □ □ □ □ □ □ □ □ □
            // 6 □ □ □ □ □ □ □ □ □
            // 5 □ □ □ □ □ □ □ □ □
            // 4 □ □ □ □ □ □ □ □ □
            // 3 □ □ □ □ □ □ □ □ □
            // 2 ■ ■ ■ ■ ■ ■ ■ ■ ■
            // 1 S □ □ □ □ □ □ □ □
            // 0 ■ ■ ■ ■ ■ ■ ■ ■ ■
            //   0 1 2 3 4 5 6 7 8
            this.map[0, 0] = "W";
            this.map[0, 2] = "W";
            this.map[1, 0] = "W";
            this.map[1, 2] = "W";
            this.map[2, 0] = "W";
            this.map[2, 2] = "W";
            this.map[3, 0] = "W";
            this.map[3, 2] = "W";
            this.map[4, 0] = "W";
            this.map[4, 2] = "W";
            this.map[5, 0] = "W";
            this.map[5, 2] = "W";
            this.map[6, 0] = "W";
            this.map[6, 2] = "W";
            this.map[7, 0] = "W";
            this.map[7, 2] = "W";
            this.map[8, 0] = "W";
            this.map[8, 2] = "W";


            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);
            txtMap.Text += ShowPossibleTiles("The algorithm should find a possible tiles, ignoring obstacles:", startingLocation, path);
            txtMap.Text += Environment.NewLine;
            txtMap.Text += "Possible tile count is: " + path.Count;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            txtMap.Text += Environment.NewLine;
            txtMap.Text += "Time elapsed is: " + elapsedMs + "ms";

        }

        /// <summary>
        /// Returns the map and path as a simple grid as a string
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private string ShowPossibleTiles(string title, Point startingLocation, IEnumerable<Point> path)
        {
            StringBuilder route = new StringBuilder();
            route.AppendFormat("{0}\r\n", title);
            for (int y = this.map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (startingLocation.Equals(new Point(x, y)))
                    {
                        // Show the start position
                        route.Append('S');
                    }
                    else if (this.map[x, y] != "")
                    {
                        // Show any barriers
                        route.Append('░');
                    }
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                    {
                        // Show the path in between
                        route.Append('*');
                    }
                    else
                    {
                        // Show nodes that aren't part of the path
                        route.Append('·');
                    }
                }
                route.Append(Environment.NewLine);
            }
            return route.ToString();
        }

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        private void InitializeMap(int xMax, int zMax, Point startingLocation)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ □ □
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

            //this.searchParameters = new SearchParameters(startingLocation, endLocation, map);
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
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Utility.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = "W";
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
                        Console.WriteLine(" this.map[" + x + ", " + z + "] = false;");
                    }
                }
            }
        }

        private void btnDebugPrint_Click(object sender, EventArgs e)
        {
            DebugPrintOutMap(11, 11);
        }

    }



}


