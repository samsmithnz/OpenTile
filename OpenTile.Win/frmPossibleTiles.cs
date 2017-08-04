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
            int range = 0;
            if (txtRange.Text != "")
            {
                range = int.Parse(txtRange.Text);
            }
            int actionPoints = 1;
            if (txtActionPoints.Text != "")
            {
                actionPoints = int.Parse(txtActionPoints.Text);
            }
            Point startingLocation = new Point(0, 0);
            //startingLocation = AddBasicTestMap();
            //startingLocation = AddWallWithGap();
            //startingLocation = AddWallWithSpinningMaze();
            //startingLocation = AddRandomMap();
            //startingLocation = AddDeadspotWallLargeMap();
            startingLocation = AddMediumBlankMap();

            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);
            List<Point> path2 = null;
            if (actionPoints == 2)
            {
                path2 = PossibleTiles.FindTiles(startingLocation, range * 2, this.map);
            }
            txtMap.Text += ShowPossibleTiles("The algorithm should find a possible tiles, ignoring obstacles:", startingLocation, path, path2);
            txtMap.Text += Environment.NewLine;
            int pathCount = path.Count;
            if (path2 != null && path2.Count > path.Count)
            {
                pathCount += (path2.Count - path.Count);
            }
            txtMap.Text += "Possible tile count is: " + pathCount;
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
        private string ShowPossibleTiles(string title, Point startingLocation, IEnumerable<Point> path, IEnumerable<Point> path2)
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
                    else if (path.Where(p => p.X == x && p.Y == y).Any() || (path2 != null && path2.Where(p => p.X == x && p.Y == y).Any()))
                    {
                        // Show the path in between
                        if (path.Where(p => p.X == x && p.Y == y).Any() == true)
                        {
                            route.Append('■');
                        }
                        else if (path2.Where(p => p.X == x && p.Y == y).Any() == true)
                        {
                            route.Append('□');
                        }
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

        private Point AddBasicTestMap()
        {
            Point startingLocation = new Point(1, 1);
            int height = 10;
            int width = 10;
            InitializeMap(width, height);
            this.map[5, 5] = "W";
            this.map[3, 5] = "W";
            return startingLocation;
        }

        private Point AddDeadspotWallLargeMap()
        {
            // Arrange
            Point startingLocation = new Point(3, 3);
            int height = 10;
            int width = 10;
            InitializeMap(width, height);
            // 6 □ □ □ □ □ □ □ 
            // 5 □ □ □ □ □ □ □ 
            // 4 □ □ □ □ ■ ■ ■ 
            // 3 □ □ □ S ■ □ ■ 
            // 2 □ □ □ □ ■ ■ ■
            // 1 □ □ □ □ □ □ □
            // 0 □ □ □ □ □ □ □ 
            //   0 1 2 3 4 5 6 x

            this.map[4, 2] = "W";
            this.map[5, 2] = "W";
            this.map[6, 2] = "W";
            this.map[4, 3] = "W";
            this.map[6, 3] = "W";
            this.map[4, 4] = "W";
            this.map[5, 4] = "W";
            this.map[6, 4] = "W";
            return startingLocation;
        }

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        private void InitializeMap(int xMax, int zMax)
        {
            // z/y
            // 4 □ □ □ □ □ □ □
            // 3 □ □ □ □ □ □ □
            // 2 □ S □ □ □ □ □
            // 1 □ □ □ □ □ □ □
            // 0 □ □ □ □ □ □ □
            //   0 1 2 3 4 5 6 x

            this.map = new string[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[x, z] = "";
                }
            }
        }

        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private Point AddWallWithGap()
        {
            // z/y
            // 4 □ □ □ ■ □ □ □
            // 3 □ □ □ ■ □ □ □
            // 2 □ S □ ■ □ F □
            // 1 □ □ □ ■ ■ □ □
            // 0 □ □ □ □ □ □ □
            //   0 1 2 3 4 5 6 x
            Point startingLocation = new Point(1, 2);
            int height = 5;
            int width = 7;
            InitializeMap(width, height);
            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[4, 1] = "W";
            return startingLocation;
        }

        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        private Point AddWallWithoutGap()
        {
            // z/y
            // 4 □ □ □ ■ □ □ □
            // 3 □ □ □ ■ □ □ □
            // 2 □ S □ ■ □ F □
            // 1 □ □ □ ■ □ □ □
            // 0 □ □ □ ■ □ □ □
            //   0 1 2 3 4 5 6 x

            // No path
            Point startingLocation = new Point(1, 2);
            int height = 5;
            int width = 7;
            InitializeMap(width, height);
            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[3, 0] = "W";
            return startingLocation;
        }

        private Point AddWallWithMaze()
        {
            //  z/y
            //  4 S ■ ■ □ ■ ■ F
            //  3 □ ■ □ ■ □ ■ □
            //  2 □ ■ □ ■ □ ■ □
            //  1 □ ■ □ ■ □ ■ □
            //  0 ■ □ ■ ■ ■ □ ■
            //    0 1 2 3 4 5 6 x

            // long path
            Point startingLocation = new Point(0, 4);
            int height = 5;
            int width = 7;
            InitializeMap(width, height);
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
            return startingLocation;
        }

        private Point AddWallWithSpinningMaze()
        {
            // z/y
            //  4 S ■ □ □ □ □ □
            //  3 □ ■ □ ■ ■ ■ □
            //  2 □ ■ □ □ F ■ □
            //  1 □ ■ ■ ■ ■ ■ □
            //  0 □ □ □ □ □ □ □
            //    0 1 2 3 4 5 6 x

            // long path
            Point startingLocation = new Point(0, 4);
            int height = 5;
            int width = 7;
            InitializeMap(width, height);
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
            return startingLocation;
        }

        private Point AddMediumBlankMap()
        {
            // Arrange
            Point startingLocation = new Point(10, 10);
            int height = 20;
            int width = 20;
            InitializeMap(width, height);
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 
            //  □ □ S □ □ 
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 

            return startingLocation;
        }

        private Point AddRandomMap()
        {
            Point startingLocation = new Point(40, 20);
            int height = 40;
            int width = 80;
            InitializeMap(width, height);
            AddRandomItems(width, height, 40);
            return startingLocation;
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


