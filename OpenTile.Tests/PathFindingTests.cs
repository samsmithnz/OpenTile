using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using OpenTile;

namespace OpenTile.Tests
{
    [TestClass]
    public class PathFindingTests
    {
        private bool[,] map;
        private SearchParameters searchParameters;

        [TestInitialize]
        public void Initialize()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[7, 5];
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    map[x, y] = true;
                }
            }

            var startLocation = new Point(1, 2);
            var endLocation = new Point(5, 2);
            this.searchParameters = new SearchParameters(startLocation, endLocation, map);
        }

        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;
        }

        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
        }

        [TestMethod]
        public void Test_WithoutWalls_CanFindPath()
        {
            // Arrange
            PathFinding  pathFinder = new PathFinding(searchParameters);

            // Act
            List<Point> path = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(4, path.Count);
        }

        [TestMethod]
        public void Test_WithOpenWall_CanFindPathAroundWall()
        {
            // Arrange
            AddWallWithGap();
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            List<Point> path = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(5, path.Count);
        }

        [TestMethod]
        public void Test_WithClosedWall_CannotFindPath()
        {
            // Arrange
            AddWallWithoutGap();
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            List<Point> path = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(path);
            Assert.IsFalse(path.Any());
        }
    }
}
