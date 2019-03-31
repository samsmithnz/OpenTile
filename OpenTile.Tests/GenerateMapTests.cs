using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;

namespace OpenTile.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class GenerateMapTests
    {

        [TestMethod]
        public void Test_TestMap()
        {
            // Arrange
            int xMax = 10;
            int zMax = 10;
            string[,] map = new string[xMax, zMax];

            // Act
            map = GenerateMap.GenerateTestMap(map, xMax, zMax);
            //GenerateMap.DebugPrintOutMap(map, xMax, zMax);

            // Assert
            Assert.IsTrue(map.Length > 0);
            Assert.IsTrue(map[5, 5] == "HW");
            Assert.IsTrue(map[3, 5] == "LW");
        }

        [TestMethod]
        public void Test_RandomMap()
        {
            // Arrange
            int xMax = 10;
            int zMax = 10;
            string[,] map = new string[xMax, zMax];

            // Act
            map = GenerateMap.GenerateRandomMap(map, xMax, zMax, 50);
            GenerateMap.DebugPrintOutMap(map, xMax, zMax);

            // Assert
            Assert.IsTrue(map.Length > 0);
            //Assert.IsTrue(map[5, 5] == "HW");
            //Assert.IsTrue(map[3, 5] == "LW");
        }

    }
}