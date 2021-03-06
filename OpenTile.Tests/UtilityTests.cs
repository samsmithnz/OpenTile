﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;

namespace OpenTile.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
                int result = Common.GenerateRandomNumber(lowerBound, upperBound);
                if (result <= randomPercent)
                {
                    counterResult++;
                }
            }

            // Assert
            Assert.IsTrue(counterResult > 0);
            Assert.IsTrue(counterResult > 25);
            Assert.IsTrue(counterResult < 55);
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

            // Act
            Point result = Common.FindNearestTile(startingLocation, enemyList);

            // Assert
            Assert.IsTrue(result != Point.Empty);
            Assert.IsTrue(result == enemy1);
            Assert.IsTrue(result != enemy2);
        }

        [TestMethod]
        public void FindNearestTileTest2()
        {
            // Arrange
            Point startingLocation = new Point(1, 1);
            Point enemy1 = new Point(-5, -5);
            Point enemy2 = new Point(-6 - 6);
            List<Point> enemyList = new List<Point>();
            enemyList.Add(enemy1);
            enemyList.Add(enemy2);

            // Act
            Point result = Common.FindNearestTile(startingLocation, enemyList);

            // Assert
            Assert.IsTrue(result != Point.Empty);
            Assert.IsTrue(result == enemy1);
            Assert.IsTrue(result != enemy2);
        }

        [TestMethod]
        public void HighCoverProbability()
        {
            // Arrange
            int probOfHighCover = 10;
            int probOfLowCover = 20;
            int prob = 10;
            bool isHighResult = false;
            bool isLowResult = false;

            // Act
            if (probOfHighCover + probOfLowCover >= prob)
            {
                if (prob <= probOfHighCover)
                {
                    isHighResult = true;
                }
                else if (prob - probOfHighCover <= probOfLowCover)
                {
                    isLowResult = true;
                }
            }

            // Assert
            Assert.IsTrue(isHighResult == true);
            Assert.IsTrue(isLowResult == false);
        }


        [TestMethod]
        public void LowCoverProbability()
        {
            // Arrange
            int probOfHighCover = 10;
            int probOfLowCover = 20;
            int prob = 30;
            bool isHighResult = false;
            bool isLowResult = false;

            // Act
            if (probOfHighCover + probOfLowCover >= prob)
            {
                if (prob <= probOfHighCover)
                {
                    isHighResult = true;
                }
                else if (prob - probOfHighCover <= probOfLowCover)
                {
                    isLowResult = true;
                }
            }

            // Assert
            Assert.IsTrue(isHighResult == false);
            Assert.IsTrue(isLowResult == true);
        }

        [TestMethod]
        public void NoCoverProbability()
        {
            // Arrange
            int probOfHighCover = 10;
            int probOfLowCover = 20;
            int prob = 31;
            bool isHighResult = false;
            bool isLowResult = false;

            // Act
            if (probOfHighCover + probOfLowCover >= prob)
            {
                if (prob <= probOfHighCover)
                {
                    isHighResult = true;
                }
                else if (prob - probOfHighCover <= probOfLowCover)
                {
                    isLowResult = true;
                }
            }

            // Assert
            Assert.IsTrue(isHighResult == false);
            Assert.IsTrue(isLowResult == false);
        }

        [TestMethod]
        public void ASCIIPiecesTests()
        {
            //Arrange & Act
            string northWestCorner = ASCIIPieces.GetNorthWestCorner();
            string westEastSide = ASCIIPieces.GetWestEastSide();
            string northEastCorner = ASCIIPieces.GetNorthEastCorner();
            string northSideWithSouthJoin = ASCIIPieces.GetNorthSideWithSouthJoin();
            string northSouthSide = ASCIIPieces.GetNorthSouthSide();
            string northSouthSideWithEastJoin = ASCIIPieces.GetNorthSouthSideWithEastJoin();
            string northSouthSideWithWestEastJoins = ASCIIPieces.GetNorthSouthSideWithWestEastJoins();
            string northSouthSideWithWestJoin = ASCIIPieces.GetNorthSouthSideWithWestJoin();
            string southWestCorner = ASCIIPieces.GetSouthWestCorner();
            string westEastSideWithNorthJoin = ASCIIPieces.GetWestEastSideWithNorthJoin();
            string southEastCorner = ASCIIPieces.GetSouthEastCorner();
            string space = ASCIIPieces.GetSpace();

            //Assert
            Assert.AreEqual("╔", northWestCorner);
            Assert.AreEqual("═", westEastSide);
            Assert.AreEqual("╗", northEastCorner);
            Assert.AreEqual("╦", northSideWithSouthJoin);
            Assert.AreEqual("║", northSouthSide);
            Assert.AreEqual("╠", northSouthSideWithEastJoin);
            Assert.AreEqual("╬", northSouthSideWithWestEastJoins);
            Assert.AreEqual("╣", northSouthSideWithWestJoin);
            Assert.AreEqual("╚", southWestCorner);
            Assert.AreEqual("╩", westEastSideWithNorthJoin);
            Assert.AreEqual("╝", southEastCorner);
            Assert.AreEqual(" ", space);
        }
    }
}