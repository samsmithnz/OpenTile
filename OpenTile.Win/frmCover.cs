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
    public partial class frmCover : Form
    {
        public frmCover()
        {
            InitializeComponent();
        }

        private string[,] map;
        //private SearchParameters searchParameters;

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            txtMap.Text = "";
            int height = 5;
            int width = 7;

            // Start with a clear map (don't add any obstacles)
            InitializeMap(width, height, Point.Empty);
            //PathFinding pathFinder = new PathFinding(searchParameters);
            List<Point> path = new List<Point>();// pathFinder.FindPath();
            //txtMap.Text += ShowRoute("The algorithm should find a direct path without obstacles:", path);
            //txtMap.Text += Environment.NewLine;

            // Now add an obstacle
            Point startingLocation = new Point(1, 2);
            int range = 3;
            InitializeMap(width, height, startingLocation);
            AddWallWithGap();
            path = PossibleTiles.FindTiles(startingLocation, range, this.map);
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();
            txtMap.Text += ShowRoute("The algorithm should find a possible tiles, ignoring the obstacle:", startingLocation, path);
            txtMap.Text += Environment.NewLine;

            //// Create a barrier between the start and end points
            //InitializeMap(7, 5, Point.Empty, Point.Empty);
            //AddWallWithoutGap();
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();
            //txtMap.Text += ShowRoute("The algorithm should not be able to find a route around the barrier:", path);
            //txtMap.Text += Environment.NewLine;


            //// Create a maze with custom start and end points
            //InitializeMap(7, 5, new Point(0, 4), new Point(6, 4));
            //AddWallWithMaze();
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();
            //txtMap.Text += ShowRoute("The algorithm should be able to find a long route around the barrier:", path);
            //txtMap.Text += Environment.NewLine;


            //// Create a maze with custom start and end points
            //InitializeMap(7, 5, new Point(0, 4), new Point(4, 2));
            //AddWallWithSpinningMaze();
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();
            //txtMap.Text += ShowRoute("The algorithm should be able to find a long route around the barrier:", path);
            //txtMap.Text += Environment.NewLine;


            // Create a larger maze with custom start and end points
            //InitializeMap(70, 40, new Point(0, 0), new Point(69, 39), false);
            //AddRandomItems(70, 40, 40);            
            //pathFinder = new PathFinding(searchParameters);
            //path = pathFinder.FindPath();
            //txtMap.Text += ShowRoute("The algorithm should be able to find a long route around the random blocks:", path);
            //txtMap.Text += Environment.NewLine;

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }

        /// <summary>
        /// Returns the map and path as a simple grid as a string
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private string ShowRoute(string title, Point startingLocation, IEnumerable<Point> path)
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
                    if (((x != 0 && z != 0) || (x != xMax - 1 && z != zMax - 1)) && probOfMapBeingBlocked > Common.GenerateRandomNumber(1, 100))
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


