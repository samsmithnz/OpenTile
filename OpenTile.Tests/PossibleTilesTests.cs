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
        private string[,] map;

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
            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[4, 1] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[3, 0] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[5, 6] = "W";
            this.map[5, 5] = "W";
            this.map[5, 4] = "W";
            this.map[4, 4] = "W";
            this.map[4, 3] = "W";
            this.map[4, 2] = "W";
            this.map[5, 2] = "W";
            this.map[5, 1] = "W";
            this.map[5, 0] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[15, 15] = "W";
            this.map[15, 14] = "W";
            this.map[15, 13] = "W";
            this.map[14, 15] = "W";
            this.map[14, 13] = "W";
            this.map[13, 15] = "W";
            this.map[13, 14] = "W";
            this.map[13, 13] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[15, 15] = "W";
            this.map[15, 14] = "W";
            this.map[15, 13] = "W";
            this.map[14, 15] = "W";
            this.map[14, 13] = "W";
            this.map[13, 15] = "W";
            this.map[13, 14] = "W";
            this.map[13, 13] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[15, 15] = "W";
            this.map[15, 14] = "W";
            this.map[15, 13] = "W";
            this.map[14, 15] = "W";
            this.map[14, 13] = "W";
            this.map[13, 15] = "W";
            this.map[13, 14] = "W";
            this.map[13, 13] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[2, 1] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

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
            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[4, 1] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 14);
        }

        [TestMethod]
        public void Test_PossibleTiles_Diag_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(0, 6);
            int height = 7;
            int width = 7;
            int range = 7;
            InitializeMap(width, height, startingLocation);
            // 6 S ■ ■ □ □ □ □
            // 5 ■ □ ■ ■ □ □ □
            // 4 ■ ■ □ ■ ■ □ □
            // 3 □ ■ ■ □ ■ ■ □
            // 2 □ □ ■ ■ □ ■ ■
            // 1 □ □ □ ■ ■ □ ■
            // 0 □ □ □ □ ■ ■ □
            //   0 1 2 3 4 5 6
            this.map[0, 4] = "W";
            this.map[0, 5] = "W";
            this.map[1, 3] = "W";
            this.map[1, 4] = "W";
            this.map[1, 6] = "W";
            this.map[2, 2] = "W";
            this.map[2, 3] = "W";
            this.map[2, 5] = "W";
            this.map[2, 6] = "W";
            this.map[3, 1] = "W";
            this.map[3, 2] = "W";
            this.map[3, 4] = "W";
            this.map[3, 5] = "W";
            this.map[4, 0] = "W";
            this.map[4, 1] = "W";
            this.map[4, 3] = "W";
            this.map[4, 4] = "W";
            this.map[5, 0] = "W";
            this.map[5, 2] = "W";
            this.map[5, 3] = "W";
            this.map[6, 1] = "W";
            this.map[6, 2] = "W";
            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 5);
        }

        [TestMethod]
        public void Test_PossibleTiles_StraightEast_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(0, 1);
            int height = 9;
            int width = 9;
            int range = 7;
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
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 7);
        }

        [TestMethod]
        public void Test_PossibleTiles_Contained_RangeOf1_NoPath()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 3;
            InitializeMap(width, height, startingLocation);
            // 4 □ □ □ □ □ 
            // 3 □ ■ ■ ■ □ 
            // 2 □ ■ S ■ □ 
            // 1 □ ■ ■ ■ □ 
            // 0 □ □ □ □ □ 
            //   0 1 2 3 4 
            this.map[1, 1] = "W";
            this.map[1, 2] = "W";
            this.map[1, 3] = "W";
            this.map[2, 1] = "W";
            this.map[2, 3] = "W";
            this.map[3, 1] = "W";
            this.map[3, 2] = "W";
            this.map[3, 3] = "W";
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 0);
        }

    }
}