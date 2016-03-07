﻿using System;
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

        private bool[,] map;
        //private SearchParameters searchParameters;

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            txtMap.Text = "";
            
            //CRITERIA
            Point startingLocation = new Point(1, 2);
            int height = 5;
            int width = 7;
            int range = 3;
            InitializeMap(width, height, startingLocation);
            //  * * * ■ □ □ □
            //  * * * ■ □ □ □
            //  * S * ■ □ F □
            //  * * * ■ ■ □ □
            //  * * * * □ □ □
            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;

            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);
            txtMap.Text += ShowPossibleTiles("The algorithm should find a possible tiles, ignoring the obstacle:", startingLocation, path);
            txtMap.Text += Environment.NewLine;
            txtMap.Text += "Path length is: " + path.Count;

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
                    else if (this.map[x, y] == false)
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

            this.map = new bool[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[x, z] = true;
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


