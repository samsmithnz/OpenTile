using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;

namespace OpenTile.Tests
{
    [TestClass]
    public class PossibleTilesTests
    {
        private bool[,] map;

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

        }

        [TestMethod]
        public void Test_PossibleTiles_LWall_RangeOf1()
        {
            // Arrange
            Point startingLocation = new Point(1, 2);
            int height = 5;
            int width = 7;
            int range = 1;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □
            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 8);
        }


        [TestMethod]
        public void Test_PossibleTiles_EWall_RangeOf1()
        {
            // Arrange
            Point startingLocation = new Point(1, 12);
            int height = 3;
            int width = 3;
            int range = 1;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ 
            //  □ S ■ 
            //  □ □ □ 
            //this.map[2, 1] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 7);
        }

        [TestMethod]
        public void Test_PossibleTiles_LWall_RangeOf2()
        {
            // Arrange
            Point startingLocation = new Point(1, 2);
            int height = 5;
            int width = 7;
            int range = 2;
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

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 15);
        }



    }
}