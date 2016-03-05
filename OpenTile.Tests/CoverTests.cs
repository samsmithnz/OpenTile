using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;

namespace OpenTile.Tests
{
    [TestClass]
    public class CoverTests
    {
        private bool[,] map;

        private void InitializeMap(int xMax, int zMax, Point startLocation, List<Point> coverLocations)
        {
            //  □ □ □ 
            //  □ S □ 
            //  □ □ □ 

            this.map = new bool[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[x, z] = true;
                }
            }

            if (coverLocations != null && coverLocations.Count > 0)
            {
                foreach (Point item in coverLocations)
                {
                    this.map[item.X, item.Y] = false;
                }
            }
        }

        [TestMethod]
        public void Test_WithoutCover_NoEnemy()
        {
            // Arrange
            //  □ □ □ 
            //  □ S □ 
            //  □ □ □ 
            Point startingLocation = new Point(1, 1);
            int width = 3;
            int height = 3;
            List<Point> enemyLocations = null;

            // Act
            InitializeMap(width, height, startingLocation, null);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_NoEnemy()
        {
            // Arrange
            //  □ □ □ 
            //  □ S ■ 
            //  □ □ □ 
            Point startingLocation = new Point(1, 1);
            int width = 3;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = null;

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_NorthEastEnemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ E 
            //  □ S ■ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 2));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_EastEnemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ □ 
            //  □ S ■ E 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 1));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_SouthEastEnemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ □ 
            //  □ S ■ □ 
            //  □ □ □ E
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 0));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_SouthEnemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ S ■ □ 
            //  □ □ E □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_SouthEnemy2()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ S ■ □ 
            //  □ E □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 0));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_SouthWestEnemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ S ■ □ 
            //  E □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(0, 0));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 1));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_WestEnemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  E S ■ □ 
            //  □ □ □ □            
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 1));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_NorthWestEnemy()
        {
            // Arrange
            //  Flanked
            //  E □ □ □ 
            //  □ S ■ □ 
            //  □ □ □ □               
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 2));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_NorthEnemy2()
        {
            // Arrange
            //  Flanked
            //  □ E □ □ 
            //  □ S ■ □ 
            //  □ □ □ □             
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 2));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_NorthEnemy()
        {
            // Arrange
            //  Flanked
            //  □ □ E □ 
            //  □ S ■ □ 
            //  □ □ □ □             
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 2));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithTwoCovers_TwoEnemysInCover_PlayerStillInCover()
        {
            // Arrange
            //  In Cover
            //  □ E □ □
            //  □ ■ □ □ 
            //  □ S ■ E 
            //  □ □ □ □             
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 1));
            enemyLocations.Add(new Point(1, 3));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithTwoCovers_TwoEnemysNotInCover_PlayerStillInCover()
        {
            // Arrange
            //  In Cover
            //  E □ □ □
            //  □ ■ □ □ 
            //  □ S ■ □
            //  □ □ □ E             
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 0));
            enemyLocations.Add(new Point(0, 3));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithTwoCovers_TwoEnemysNotInCover_PlayerFlanked()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  E ■ □ □  
            //  □ S ■ □
            //  □ □ E □             
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));
            enemyLocations.Add(new Point(0, 2));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithFourCovers_FourEnemysNotInCover_PlayerInCover()
        {
            // Arrange
            // In Cover
            // 4 □ □ E □ □
            // 3 □ □ ■ □ □  
            // 2 E ■ S ■ E
            // 1 □ □ ■ □ □
            // 0 □ □ E □ □ 
            //   0 1 2 3 4            
            Point startingLocation = new Point(2, 2);
            int width = 5;
            int height =5;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 3));
            coverLocations.Add(new Point(3, 2));
            coverLocations.Add(new Point(2, 1));
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));
            enemyLocations.Add(new Point(0, 2));
            enemyLocations.Add(new Point(2, 4));
            enemyLocations.Add(new Point(4, 2));

            // Act
            InitializeMap(width, height, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }


    }
}