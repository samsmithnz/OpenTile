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

        private void InitializeMap(int xMax, int zMax)
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
            InitializeMap(width, height);
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
            InitializeMap(width, height);
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
        public void Test_PossibleTiles_NoWalls_RangeOf2With1Action()
        {
            // Arrange
            int range = 2;
            Point startingLocation = new Point(10, 10);
            int height = 20;
            int width = 20;
            InitializeMap(width, height);
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 
            //  □ □ S □ □ 
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 24);
        }

        [TestMethod]
        public void Test_PossibleTiles_NoWalls_RangeOf2With2Actions()
        {
            // Arrange
            int range = 2;
            Point startingLocation = new Point(10, 10);
            int height = 20;
            int width = 20;
            InitializeMap(width, height);
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 
            //  □ □ S □ □ 
            //  □ □ □ □ □ 
            //  □ □ □ □ □ 

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);
            List<Point> path2 = PossibleTiles.FindTiles(startingLocation, range * 2, this.map);

            // Assert
            Assert.IsTrue(path.Count + (path2.Count - path.Count) == 80);

        }

        [TestMethod]
        public void Test_PossibleTiles_EastWall_RangeOf2()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 2;
            InitializeMap(width, height);
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
            Assert.IsTrue(path.Count == 16);
        }

        [TestMethod]
        public void Test_PossibleTiles_EastWall_RangeOf3()
        {
            // Arrange
            Point startingLocation = new Point(2, 2);
            int height = 5;
            int width = 5;
            int range = 3;
            InitializeMap(width, height);
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
            InitializeMap(width, height);
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
        public void Test_PossibleTiles_DeadspotWall_RangeOf5()
        {
            // Arrange
            int range = 5;
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

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 61);
        }

        [TestMethod]
        public void Test_PossibleTiles_DeadspotWall_LargeMap_RangeOf7()
        {
            // Arrange
            int range = 5;
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

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 61);
        }


        [TestMethod]
        public void Test_PossibleTiles_DeadspotWall_LargeMap_RangeOf6With1Action()
        {
            // Arrange
            int range = 6;
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

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 80);
        }

        [TestMethod]
        public void Test_PossibleTiles_DeadspotWall_LargeMap_RangeOf3With2Actions()
        {
            // Arrange
            int range = 3;
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

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);
            List<Point> path2 = PossibleTiles.FindTiles(startingLocation, range * 2, this.map);


            // Assert
            Assert.IsTrue(path.Count + (path2.Count - path.Count) == 80);
        }


        [TestMethod]
        public void Test_PossibleTiles_EastWall_RangeOf1()
        {
            // Arrange
            int range = 1;
            Point startingLocation = new Point(1, 1);
            int height = 3;
            int width = 3;
            InitializeMap(width, height);
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
            InitializeMap(width, height);
            //  * * * ■ □ □ □
            //  * * * ■ □ □ □
            //  * S * ■ □ F □
            //  * * * ■ ■ □ □
            //  * * * * □ □ □
            this.map[3, 4] = "W";
            this.map[3, 3] = "W";
            this.map[3, 2] = "W";
            this.map[3, 1] = "W";
            this.map[4, 1] = "W";

            // Act
            List<Point> path = PossibleTiles.FindTiles(startingLocation, range, this.map);

            // Assert
            Assert.IsTrue(path.Count == 15);
        }

        [TestMethod]
        public void Test_PossibleTiles_Diag_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(0, 6);
            int height = 7;
            int width = 7;
            int range = 7;
            InitializeMap(width, height);
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
            Assert.IsTrue(path.Count == 6);
        }

        [TestMethod]
        public void Test_PossibleTiles_StraightEast_RangeOf7()
        {
            // Arrange
            Point startingLocation = new Point(0, 1);
            int height = 9;
            int width = 9;
            int range = 7;
            InitializeMap(width, height);
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
            InitializeMap(width, height);
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