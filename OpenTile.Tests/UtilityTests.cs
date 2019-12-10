using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;
using UnityEngine;

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
                int result = Common.GenerateRandomNumber(lowerBound, upperBound);
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
            Vector3 startingLocation = new Vector3(1, 0, 1);
            Vector3 enemy1 = new Vector3(1, 0, 10);
            Vector3 enemy2 = new Vector3(2, 0, 20);
            List<Vector3> enemyList = new List<Vector3>();
            enemyList.Add(enemy1);
            enemyList.Add(enemy2);

            // Act
            Vector3 result = Common.FindNearestTile(startingLocation, enemyList);

            // Assert
            Assert.IsTrue(result != Vector3.zero);
            Assert.IsTrue(result == enemy1);
            Assert.IsTrue(result != enemy2);
        }

        [TestMethod]
        public void FindNearestTileTest2()
        {
            // Arrange
            Vector3 startingLocation = new Vector3(1, 0, 1);
            Vector3 enemy1 = new Vector3(-5, 0, -5);
            Vector3 enemy2 = new Vector3(-6, 0, -6);
            List<Vector3> enemyList = new List<Vector3>();
            enemyList.Add(enemy1);
            enemyList.Add(enemy2);

            // Act
            Vector3 result = Common.FindNearestTile(startingLocation, enemyList);

            // Assert
            Assert.IsTrue(result != Vector3.zero);
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
    }
}