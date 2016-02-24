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

        private void InitializeMap(int xMax, int zMax, Point startLocation, Point endLocation)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    map[x, z] = true;
                }
            }

            if (startLocation == Point.Empty)
            {
                startLocation = new Point(1, 2);
            }
            if (endLocation == Point.Empty)
            {
                endLocation = new Point(5, 2);
            }
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
            InitializeMap(7, 5, Point.Empty, Point.Empty);
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
            InitializeMap(7, 5, Point.Empty, Point.Empty);
            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
        }

        private void AddWallWithMaze()
        {
            //  S ■ ■ □ ■ ■ F
            //  □ ■ □ ■ □ ■ □
            //  □ ■ □ ■ □ ■ □
            //  □ ■ □ ■ □ ■ □
            //  ■ □ ■ ■ ■ □ ■

            // No path
            InitializeMap(7, 5, new Point(0,4), new Point(6,4));
            this.map[0, 0] = false;
            this.map[1, 4] = false;
            this.map[1, 3] = false;
            this.map[1, 2] = false;
            this.map[1, 1] = false;
            this.map[2, 4] = false;
            this.map[2, 0] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
            this.map[4, 4] = false;
            this.map[4, 0] = false;
            this.map[5, 4] = false;
            this.map[5, 3] = false;
            this.map[5, 2] = false;
            this.map[5, 1] = false;
            this.map[6, 0] = false;
        }

        [TestMethod]
        public void Test_WithoutWalls_CanFindPath()
        {
            // Arrange
            PathFinding pathFinder = new PathFinding(searchParameters);

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


        [TestMethod]
        public void Test_WithMazeWall()
        {
            // Arrange
            AddWallWithMaze();
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            List<Point> path = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(16, path.Count);
        }
    }
}
