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
        public void Test_WithoutCover()
        {
            // Arrange
            //  □ □ □ 
            //  □ S □ 
            //  □ □ □ 
            Point startingLocation = new Point(1, 1);
            int width = 3;
            int height = 3;

            // Act
            InitializeMap(width, height, startingLocation, null);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map);

            // Assert
            Assert.IsTrue(unitIsInCover == false);
        }

        [TestMethod]
        public void Test_WithEastCover()
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

            // Act
            InitializeMap(3, 3, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }

        [TestMethod]
        public void Test_WithNorthEastCover()
        {
            // Arrange
            //  □ □ ■ 
            //  □ S □ 
            //  □ □ □ 
            Point startingLocation = new Point(1, 1);
            int width = 3;
            int height = 3;
            List<Point> coverLocations = new List<Point>();
            coverLocations.Add(new Point(2, 1));

            // Act
            InitializeMap(3, 3, new Point(1, 1), coverLocations);
            bool unitIsInCover = Cover.CalculateCover(startingLocation, width, height, this.map);

            // Assert
            Assert.IsTrue(unitIsInCover == true);
        }
    

    }
}
