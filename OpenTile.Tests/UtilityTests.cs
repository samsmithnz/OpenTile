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

    }
}