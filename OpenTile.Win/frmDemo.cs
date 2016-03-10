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
    public partial class frmDemo : Form
    {
        public frmDemo()
        {
            InitializeComponent();
        }

        private bool[,] map;
        private SearchParameters searchParameters;

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            txtMap.Text = "";
            int width = int.Parse(txtWidth.Text);
            int height = int.Parse(txtHeight.Text);
            int range = int.Parse(txtRange.Text);
            int startingWidth = 3;
            int startingHeight = 3;

            // Create a larger maze with custom start and end points
            InitializeMap(width, height, new Point(0, 0), new Point(width - 1, height - 1), false);
            AddRandomItems(width, height, 40);
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();

            PathFinding pathFinder = new PathFinding(searchParameters);
            PathFindingResult pathResult = pathFinder.FindPath();
            pathResult.Path.AddRange(PossibleTiles.FindTiles(new Point(0, 0), range, width, height, this.map));
            txtMap.Text += ShowRoute("The algorithm should be able to find a long route around the random blocks:", pathResult.Path, startingWidth, startingHeight);
            txtMap.Text += Environment.NewLine;
        }

        /// <summary>
        /// Returns the map and path as a simple grid as a string
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private string ShowRoute(string title, IEnumerable<Point> path, int startingWidth, int startingHeight)
        {
            StringBuilder route = new StringBuilder();
            route.AppendFormat("{0}\r\n", title);
            for (int y = this.map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (this.searchParameters.startingLocation.Equals(new Point(x, y)) || (x < startingWidth && y < startingHeight))
                    {
                        // Show the start position
                        route.Append('S');
                    }
                    else if (this.searchParameters.EndLocation.Equals(new Point(x, y)))
                    {
                        // Show the end position
                        route.Append('F');
                    }
                    else if (this.map[x, y] == false)
                    {
                        // Show any barriers
                        route.Append('░');
                    }
                    else if (path != null && path.Where(p => p.X == x && p.Y == y).Any())
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
        private void InitializeMap(int xMax, int zMax, Point startingLocation, Point endLocation, bool locationsNotSet)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[x, z] = true;
                }
            }

            if (locationsNotSet == true)
            {
                startingLocation = new Point(1, 2);
                endLocation = new Point(5, 2);
            }
            this.searchParameters = new SearchParameters(startingLocation, endLocation, map);
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

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;
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

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
        }

        private void AddWallWithMaze()
        {
            //  S ■ ■ □ ■ ■ F
            //  □ ■ □ ■ □ ■ □
            //  □ ■ □ ■ □ ■ □
            //  □ ■ □ ■ □ ■ □
            //  ■ □ ■ ■ ■ □ ■

            // long path
            this.map[0, 0] = false;
            this.map[1, 4] = false;
            this.map[1, 3] = false;
            this.map[1, 2] = false;
            this.map[1, 1] = false;
            this.map[2, 4] = false;
            this.map[2, 0] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
            this.map[4, 4] = false;
            this.map[4, 0] = false;
            this.map[5, 4] = false;
            this.map[5, 3] = false;
            this.map[5, 2] = false;
            this.map[5, 1] = false;
            this.map[6, 0] = false;
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
            this.map[1, 1] = false;
            this.map[1, 2] = false;
            this.map[1, 3] = false;
            this.map[1, 4] = false;
            this.map[2, 1] = false;
            this.map[3, 1] = false;
            this.map[3, 3] = false;
            this.map[4, 1] = false;
            this.map[4, 3] = false;
            this.map[5, 1] = false;
            this.map[5, 2] = false;
            this.map[5, 3] = false;
        }

        private void AddRandomItems(int xMax, int zMax, int probOfMapBeingBlocked)
        {
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Utility.GenerateRandomNumber(1, 100))
                    {
                        this.map[x, z] = false;
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
                    if (this.map[x, z] == false)
                    {
                        Console.WriteLine(" this.map[" + x + ", " + z + "] = false;");
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


