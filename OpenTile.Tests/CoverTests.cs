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

        #region " Private setup functions"

        private bool[,] map;

        private void InitializeMap(int xMax, int zMax, Point startingLocation, List<Point> coverLocations)
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

        #endregion

        #region " Initial pathfinding tests"

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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        #endregion

        #region " North Cover tests"

        [TestMethod]
        public void Test_WithNorthCover_1_O_Clock_Enemy()
        {
            // Arrange
            //  In Cover
            //  □ □ E □
            //  □ ■ □ □ 
            //  □ S □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNorthCover_2_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ ■ E □ 
            //  □ S □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithNorthCover_3_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ ■ □ □ 
            //  □ S E □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithNorthCover_5_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ ■ □ □ 
            //  □ S □ □ 
            //  □ □ E □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithNorthCover_6_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ ■ □ □ 
            //  □ S □ □ 
            //  □ E □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithNorthCover_7_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ ■ □ □ 
            //  □ S □ □ 
            //  E □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithNorthCover_8_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ ■ □ □ 
            //  E S □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }


        [TestMethod]
        public void Test_WithNorthCover_9_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  E ■ □ □ 
            //  □ S □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithNorthCover_11_O_Clock_Enemy()
        {
            // Arrange
            //  In Cover
            //  E □ □ □
            //  □ ■ □ □ 
            //  □ S □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNorthCover_12_O_Clock_Enemy()
        {
            // Arrange
            //  In Cover
            //  □ E □ □
            //  □ ■ □ □ 
            //  □ S □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }


        #endregion

        #region " East Cover tests"

        [TestMethod]
        public void Test_WithEastCover_2_OClock_Enemy()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_3_OClock_Enemy()
        {
            // Arrange
            //  In Cover
            // 2 □ □ □ □ 
            // 1 □ S ■ E 
            // 0 □ □ □ □
            //   0 1 2 3
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_4_OClock_Enemy()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithEastCover_5_OClock_Enemy_Flanking()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_6_OClock_Enemy2()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_7_OClock_Enemy()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_9_OClock_Enemy()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_10_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            // 2 E □ □ □ 
            // 1 □ S ■ □ 
            // 0 □ □ □ □     
            //   0 1 2 3          
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_12_OClock_Enemy2()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover_1_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            // 2 □ □ E □ 
            // 1 □ S ■ □ 
            // 0 □ □ □ □   
            //   0 1 2 3          
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        #endregion

        #region " South Cover tests"

        [TestMethod]
        public void Test_WithSouthCover_1_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ E □
            //  □ S □ □ 
            //  □ ■ □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithSouthCover_3_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ S E □ 
            //  □ ■ □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithSouthCover_4_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ S □ □ 
            //  □ ■ E □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithSouthCover_5_O_Clock_Enemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ □
            //  □ S □ □ 
            //  □ ■ □ □ 
            //  □ □ E □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithSouthCover_6_O_Clock_Enemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ □
            //  □ S □ □ 
            //  □ ■ □ □ 
            //  □ E □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithSouthCover_7_O_Clock_Enemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ □
            //  □ S □ □ 
            //  □ ■ □ □ 
            //  E □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithSouthCover_8_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ S □ □ 
            //  E ■ □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }


        [TestMethod]
        public void Test_WithSouthCover_9_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  E S □ □ 
            //  □ ■ □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithSouthCover_10_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  E □ □ □
            //  □ S □ □ 
            //  □ ■ □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithSouthCover_12_O_Clock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ E □ □
            //  □ S □ □ 
            //  □ ■ □ □ 
            //  □ □ □ □
            Point startingLocation = new Point(1, 2);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        #endregion

        #region " West Cover tests"

        [TestMethod]
        public void Test_WithWestCover_2_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ E 
            //  □ ■ S □ 
            //  □ □ □ □
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_3_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            // 2 □ □ □ □ 
            // 1 □ ■ S E 
            // 0 □ □ □ □
            //   0 1 2 3
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_4_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ ■ S □ 
            //  □ □ □ E
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_5_OClock__Flanking()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ ■ S □ 
            //  □ □ E □
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_6_OClock_Enemy2()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ ■ S □ 
            //  □ E □ □
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_7_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ ■ S □ 
            //  E □ □ □
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(3, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_9_OClock_Enemy()
        {
            // Arrange
            //  In Cover
            //  □ □ □ □ 
            //  E ■ S □ 
            //  □ □ □ □            
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithWestCover_10_OClock_Enemy()
        {
            // Arrange
            //  In Cover
            //  E □ □ □ 
            //  □ ■ S □ 
            //  □ □ □ □               
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithWestCover_11_OClock_Enemy2()
        {
            // Arrange
            //  Flanked
            //  □ E □ □ 
            //  □ ■ S □ 
            //  □ □ □ □             
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithWestCover_12_OClock_Enemy()
        {
            // Arrange
            //  Flanked
            // 2 □ □ E □ 
            // 1 □ ■ S □ 
            // 0 □ □ □ □   
            //   0 1 2 3          
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        #endregion

        #region " Advanced cover scenario tests"


        [TestMethod]
        public void Test_WithNortheastCovers_TwoEnemysInCover_PlayerStillInCover()
        {
            // Arrange
            //  In Cover
            // 3 □ E □ □
            // 2 □ ■ □ □ 
            // 1 □ S ■ E 
            // 0 □ □ □ □
            //   0 1 2 3          
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNortheastCovers_TwoEnemysNotInCover_PlayerStillInCover()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNortheastCovers_TwoEnemysNotInCover_PlayerFlanked()
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }


        [TestMethod]
        public void Test_WithNorthwestCovers_TwoEnemysInCover_PlayerStillInCover()
        {
            // Arrange
            //  In Cover
            // 3 □ □ E □
            // 2 □ □ ■ □ 
            // 1 E ■ S □ 
            // 0 □ □ □ □
            //   0 1 2 3          
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            coverLocations.Add(new Point(2, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 1));
            enemyLocations.Add(new Point(2, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNorthwestCovers_TwoEnemysNotInCover_PlayerStillInCover()
        {
            // Arrange
            //  In Cover
            //  □ □ □ E
            //  □ □ ■ □ 
            //  □ ■ S □
            //  E □ □ □            
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            coverLocations.Add(new Point(2, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(0, 0));
            enemyLocations.Add(new Point(3, 3));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNorthwestCovers_TwoEnemysNotInCover_PlayerFlanked()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □
            //  □ □ ■ E 
            //  □ ■ S □
            //  □ E □ □            
            Point startingLocation = new Point(2, 1);
            int width = 4;
            int height = 4;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(1, 1));
            coverLocations.Add(new Point(2, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 0));
            enemyLocations.Add(new Point(3, 2));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }


        [TestMethod]
        public void Test_WithTwoCover_SouthEnemy()
        {
            // Arrange
            //  Flanked
            //  □ □ □ □ 
            //  □ S ■ □ 
            //  □ ■ E □
            Point startingLocation = new Point(1, 1);
            int width = 4;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(2, 0));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
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
            int height = 5;
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
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithFourCovers_FourEnemysInCover_PlayerInCover()
        {
            // Arrange
            // In Cover
            // 4 □ □ □ □ □
            // 3 □ E ■ E □  
            // 2 □ ■ S ■ □
            // 1 □ E ■ E □
            // 0 □ □ □ □ □ 
            //   0 1 2 3 4            
            Point startingLocation = new Point(2, 2);
            int width = 5;
            int height = 5;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 3));
            coverLocations.Add(new Point(3, 2));
            coverLocations.Add(new Point(2, 1));
            coverLocations.Add(new Point(1, 2));
            List<Point> enemyLocations = new List<Point>();
            enemyLocations.Add(new Point(1, 1));
            enemyLocations.Add(new Point(1, 3));
            enemyLocations.Add(new Point(3, 3));
            enemyLocations.Add(new Point(3, 1));

            // Act
            InitializeMap(width, height, startingLocation, coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map, enemyLocations);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        #endregion

    }
}