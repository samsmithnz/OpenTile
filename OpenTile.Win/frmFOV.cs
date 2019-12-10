﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityEngine;

namespace OpenTile.Win
{
    public partial class frmFOV : Form
    {
        public frmFOV()
        {
            InitializeComponent();
        }

        private string[,] map;

        private void InitializeMap(int xMax, int zMax)
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
        }

        private string ShowFOV(string title, Vector3 startingLocation, Vector3 endLocation)
        {
            StringBuilder route = new StringBuilder();
            route.AppendFormat("{0}\r\n", title);
            for (int y = this.map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (startingLocation == new Vector3(x,0, y))
                    {
                        // Show the start position
                        route.Append('S');
                    }
                    else if (endLocation == new Vector3(x,0, y))
                    {
                        // Show the end position
                        route.Append('F');
                    }
                    else if (this.map[x, y] != "")
                    {
                        // Show any barriers
                        route.Append('░');
                    }
                    //else if (path.Where(p => p.x == x && p.z == y).Any())
                    //{
                    //    // Show the path in between
                    //    route.Append('*');
                    //}
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

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            Vector3 startingLocation = new Vector3(1, 0, 2);
            Vector3 endLocation = new Vector3(5, 0, 2);

            InitializeMap(7, 5);
            this.map[3, 1] = "W";
            this.map[3, 3] = "W";
            ShowFOV("Testing", startingLocation, endLocation);
            txtMap.Text = "";
            txtMap.Text += ShowFOV("The algorithm should be able to show FOV on a blank map:", startingLocation, endLocation);
            txtMap.Text += Environment.NewLine;
        }
    }
}
