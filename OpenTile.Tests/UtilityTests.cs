using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;

namespace OpenTile.Tests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void TestRandom()
        {
            // Arrange
            int randomPercent = 40;
            int lowerBound = 0;
            int upperBound = 100;
            int counterResult = 0;

            // Act
            for (int i = lowerBound; i < upperBound; i++)
            {
                int result = Utility.GenerateRandomNumber(lowerBound, upperBound);
                if (result <= randomPercent)
                {
                    counterResult++;
                }
            }

            // Assert
            Assert.IsTrue(counterResult > 0);
            Assert.IsTrue(counterResult > 30);
            Assert.IsTrue(counterResult < 50);
        }

        [TestMethod]
        public void FindNearestTileTest()
        {
            // Arrange
            Point startingLocation = new Point(1, 1);
            Point enemy1 = new Point(10, 10);
            Point enemy2 = new Point(20, 20);
            List<Point> enemyList = new List<Point>();
            enemyList.Add(enemy1);
            enemyList.Add(enemy2);
            //here    

            // Act
            Point result = Utility.FindNearestTile(startingLocation, enemyList);

            // Assert
            Assert.IsTrue(result != Point.Empty);
            Assert.IsTrue(result == enemy1);
            Assert.IsTrue(result != enemy2);
        }


    }
}