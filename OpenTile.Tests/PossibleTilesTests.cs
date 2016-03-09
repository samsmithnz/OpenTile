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
        public void Test_PossibleTiles_NoWalls_RangeOf1()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 1;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 
            //  □ □ S □ □ 
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 8);
        }

        [TestMethod]
        public void Test_PossibleTiles_NoWalls_RangeOf2()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 2;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 
            //  □ □ S □ □ 
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 20);
        }

        [TestMethod]
        public void Test_PossibleTiles_EastWall_RangeOf2()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 2;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ 
            //  □ □ □ ■ □ 
            //  □ □ S ■ □ 
            //  □ □ □ ■ □ 
            //  □ □ □ □ □ 
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 14);
        }

        [TestMethod]
        public void Test_PossibleTiles_EastWall_RangeOf3()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 3;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ ■ □ 
            //  □ □ □ ■ □ 
            //  □ □ S ■ □ 
            //  □ □ □ ■ □ 
            //  □ □ □ ■ □ 
            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 14);
        }

        [TestMethod]
        public void Test_PossibleTiles_EastDoglegWall_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(3, 3);
            int height = 7;
            int width = 7;
            int range = 7;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ ■ □ 
            //  □ □ □ □ □ ■ □ 
            //  □ □ □ □ ■ ■ □ 
            //  □ □ □ S ■ □ □ 
            //  □ □ □ □ ■ ■ □
            //  □ □ □ □ □ ■ □
            //  □ □ □ □ □ ■ □ 
            this.map[5, 6] = false;
            this.map[5, 5] = false;
            this.map[5, 4] = false;
            this.map[4, 4] = false;
            this.map[4, 3] = false;
            this.map[4, 2] = false;
            this.map[5, 2] = false;
            this.map[5, 1] = false;
            this.map[5, 0] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 31);
        }


        [TestMethod]
        public void Test_PossibleTiles_DeadspotWall_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(16, 16);
            int height = 40;
            int width = 70;
            int range = 7;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ □ □ 
            //  □ □ □ □ □ □ □ 
            //  □ □ □ □ ■ ■ ■ 
            //  □ □ □ S ■ □ ■ 
            //  □ □ □ □ ■ ■ ■
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □ 
            this.map[15, 15] = false;
            this.map[15, 14] = false;
            this.map[15, 13] = false;
            this.map[14, 15] = false;
            this.map[14, 13] = false;
            this.map[13, 15] = false;
            this.map[13, 14] = false;
            this.map[13, 13] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 166);
        }

        [TestMethod]
        public void Test_PossibleTiles_DeadspotWall_LargeMap_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(16, 16);
            int height = 40;
            int width = 70;
            int range = 7;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ □ □ 
            //  □ □ □ □ □ □ □ 
            //  □ □ □ □ ■ ■ ■ 
            //  □ □ □ S ■ □ ■ 
            //  □ □ □ □ ■ ■ ■
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □ 
            this.map[15, 15] = false;
            this.map[15, 14] = false;
            this.map[15, 13] = false;
            this.map[14, 15] = false;
            this.map[14, 13] = false;
            this.map[13, 15] = false;
            this.map[13, 14] = false;
            this.map[13, 13] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 166);
        }


        [TestMethod]
        public void Test_PossibleTiles_DeadspotWall_LargeMap_RangeOf15()
        {
            // Arrange
            Point startingLocation = new Point(20, 20);
            int height = 40;
            int width = 70;
            int range = 15;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ □ □ □ □ 
            //  □ □ □ □ □ □ □ 
            //  □ □ □ □ ■ ■ ■ 
            //  □ □ □ S ■ □ ■ 
            //  □ □ □ □ ■ ■ ■
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □ 
            this.map[15, 15] = false;
            this.map[15, 14] = false;
            this.map[15, 13] = false;
            this.map[14, 15] = false;
            this.map[14, 13] = false;
            this.map[13, 15] = false;
            this.map[13, 14] = false;
            this.map[13, 13] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 739);
        }


        [TestMethod]
        public void Test_PossibleTiles_EastWall_RangeOf1()
        {
            // Arrange
            Point startingLocation = new Point(1, 1);
            int height = 3;
            int width = 3;
            int range = 1;
            InitializeMap(width, height, startingLocation);
            //  □ □ □ 
            //  □ S ■ 
            //  □ □ □ 
            this.map[2, 1] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 7);
        }

        [TestMethod]
        public void Test_PossibleTiles_LShapedWall_RangeOf2()
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
            //  * * * □ □ □ □
            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, width, height, this.map);

            // Assert
            Assert.IsTrue(path.Count == 14);
        }



    }
}