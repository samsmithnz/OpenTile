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

        private void InitializeMap(int xMax, int zMax, Point startLocation, Point endLocation, bool locationsNotSet = true)
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

            if (locationsNotSet == true)
            {
                if (startLocation == Point.Empty)
                {
                    startLocation = new Point(1, 2);
                }
                if (endLocation == Point.Empty)
                {
                    endLocation = new Point(5, 2);
                }
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

            // long path
            InitializeMap(7, 5, new Point(0, 4), new Point(6, 4));
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
            InitializeMap(7, 5, Point.Empty, Point.Empty);
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


        [TestMethod]
        public void Test_GiantRandomMap_WithInefficentPath()
        {
            // Arrange
            AddGiantRandomWalls();
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            List<Point> path = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(75, path.Count);
            //CreateDebugPictureOfMapAndRoute(70, 40, path);
        }

        private void CreateDebugPictureOfMapAndRoute(int xMax, int zMax, List<Point> path)
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □


            //□ ■ ■ ■ ■ ■ ■ ■ ■ □ ■ □ □ □ ■ □ ■ ■ □ □ □ □ □ ■ □ ■ □ ■ ■ □ ■ ■ ■ □ ■ ■ □ □ □ □ □ □ □ □ □ □ ■ □ ■ □ □ ■ □ ■ □ □ □ ■ ■ □ ■ □ ■ ■ □ ■ □ ■ □ □
            //□ ■ ■ □ ■ □ □ □ □ □ □ ■ ■ □ □ ■ □ ■ ■ □ □ □ □ □ □ □ □ □ □ ■ □ □ □ □ ■ □ □ □ □ ■ ■ □ □ □ □ □ □ □ □ □ □ ■ □ □ □ □ □ □ ■ □ □ ■ ■ □ □ □ ■ □ □ □
            //□ ■ ■ □ □ ■ □ □ □ □ □ □ ■ □ ■ □ □ ■ ■ □ □ □ ■ □ □ ■ □ ■ □ □ □ □ ■ □ ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ ■ ■ □ ■ ■ □ □ ■ □ □ □ ■ □ ■ □ □ □ □ ■
            //□ □ S □ □ ■ ■ □ □ □ ■ □ □ ■ □ □ ■ ■ □ ■ □ ■ □ ■ □ □ □ □ ■ ■ ■ □ ■ ■ □ ■ □ □ ■ □ ■ ■ □ □ □ □ □ ■ ■ □ ■ ■ □ ■ ■ □ ■ ■ ■ ■ □ □ □ □ □ □ □ ■ ■ □
            //□ □ ■ * * □ ■ ■ □ □ □ ■ ■ ■ □ □ □ □ ■ ■ □ ■ □ □ ■ ■ □ □ □ □ □ □ ■ □ □ □ ■ ■ ■ ■ ■ ■ □ ■ □ □ □ ■ □ □ □ ■ ■ □ □ □ ■ □ □ □ □ □ ■ □ □ □ ■ ■ □ ■
            //□ ■ ■ □ ■ * □ ■ ■ □ □ ■ □ ■ ■ □ □ ■ □ ■ ■ □ □ □ □ □ ■ ■ □ □ □ ■ □ □ ■ □ □ ■ □ ■ ■ ■ ■ □ □ ■ □ □ □ ■ ■ □ □ ■ ■ □ □ □ ■ ■ □ ■ □ ■ ■ □ ■ ■ ■ ■
            //□ ■ □ □ ■ ■ * ■ □ □ ■ ■ □ ■ □ □ □ □ □ ■ ■ ■ ■ □ □ □ ■ ■ □ □ ■ ■ □ ■ ■ ■ □ □ □ □ □ □ □ □ □ ■ □ □ □ □ □ □ □ □ ■ □ ■ □ □ □ ■ ■ □ ■ ■ □ ■ ■ ■ □
            //■ □ □ □ □ □ □ * □ □ ■ □ ■ ■ □ □ □ ■ □ □ □ □ ■ ■ □ ■ □ □ ■ □ ■ ■ ■ □ ■ □ □ □ □ □ ■ □ ■ ■ □ ■ ■ ■ □ □ ■ ■ □ □ □ □ □ □ ■ □ □ ■ ■ □ □ □ □ □ □ □
            //□ □ ■ □ ■ ■ □ ■ * * □ □ □ □ □ □ * * * □ □ □ □ ■ □ ■ ■ □ □ □ □ ■ □ □ □ ■ ■ □ ■ ■ ■ ■ □ □ □ ■ ■ □ □ □ □ □ □ □ □ ■ ■ ■ □ ■ ■ □ □ ■ □ □ ■ ■ □ □
            //□ ■ ■ □ ■ ■ ■ □ ■ ■ * ■ □ □ ■ * □ ■ ■ * ■ ■ ■ ■ □ ■ □ □ □ ■ ■ □ □ □ □ □ □ □ ■ ■ □ ■ □ □ □ □ ■ □ ■ □ ■ ■ ■ □ ■ □ ■ ■ □ □ □ □ ■ ■ ■ ■ □ ■ □ □
            //■ □ □ □ ■ □ ■ □ □ □ ■ * □ ■ ■ ■ * * ■ ■ * □ ■ ■ □ □ ■ □ □ ■ ■ ■ □ □ □ □ ■ ■ □ □ □ □ □ □ □ □ ■ □ ■ □ □ ■ □ □ □ □ □ □ □ ■ □ □ □ ■ □ □ □ □ □ □
            //■ □ ■ □ □ □ □ ■ ■ □ □ □ * □ □ ■ □ ■ * ■ □ * * ■ ■ □ □ ■ ■ ■ ■ □ ■ □ □ □ □ ■ □ ■ □ □ ■ □ □ ■ ■ ■ □ □ ■ ■ ■ □ □ □ □ ■ ■ ■ ■ □ □ ■ ■ □ ■ ■ □ □
            //□ ■ □ □ □ □ ■ □ □ ■ □ ■ □ * * □ ■ * □ ■ ■ □ ■ * * * □ ■ □ □ □ ■ □ □ ■ ■ □ □ ■ ■ □ □ □ ■ □ □ ■ □ ■ ■ □ ■ ■ □ □ ■ ■ □ □ □ ■ ■ ■ □ ■ ■ □ ■ ■ □
            //■ □ □ □ □ ■ □ □ □ ■ □ □ □ ■ ■ * * ■ □ ■ ■ □ ■ □ ■ ■ * □ ■ □ ■ □ ■ □ ■ ■ ■ □ □ □ ■ ■ □ ■ □ ■ ■ ■ □ ■ □ □ □ ■ □ ■ ■ ■ □ □ □ □ □ □ ■ □ □ ■ □ □
            //□ □ ■ ■ □ □ □ ■ □ ■ ■ ■ ■ □ ■ ■ ■ ■ ■ ■ ■ ■ □ ■ □ □ □ * ■ □ ■ □ ■ □ □ □ ■ □ ■ □ ■ ■ □ □ ■ □ □ □ □ □ □ □ ■ □ □ □ □ ■ □ ■ □ □ □ □ □ ■ ■ ■ □ ■
            //□ □ □ □ ■ □ ■ ■ □ ■ □ □ ■ ■ ■ □ □ □ ■ □ □ ■ □ ■ □ □ ■ □ * □ ■ ■ ■ □ ■ □ □ □ □ □ ■ ■ ■ ■ ■ □ □ □ □ ■ □ □ ■ ■ □ □ □ □ □ ■ □ ■ □ □ □ □ □ □ ■ □
            //□ ■ ■ ■ □ ■ □ □ □ ■ ■ □ □ □ ■ □ ■ □ □ ■ □ ■ □ ■ ■ □ □ ■ □ * * * * □ ■ □ □ □ □ □ □ ■ □ □ ■ ■ □ □ □ □ ■ □ □ □ □ □ ■ □ ■ □ ■ □ ■ □ □ □ ■ □ ■ □
            //□ □ □ ■ □ □ ■ ■ ■ □ □ ■ □ □ □ □ ■ □ ■ □ ■ □ ■ ■ ■ ■ ■ ■ ■ □ ■ ■ ■ * □ □ □ □ ■ ■ ■ □ □ □ □ □ ■ □ ■ □ ■ ■ ■ □ ■ □ ■ □ □ □ ■ ■ ■ □ □ □ □ □ □ □
            //■ □ ■ ■ ■ □ ■ □ ■ □ □ □ □ □ ■ □ ■ □ □ □ □ ■ □ □ □ ■ □ □ ■ □ ■ ■ □ □ * * □ □ ■ □ □ ■ ■ □ □ □ □ □ □ □ ■ ■ ■ ■ ■ ■ ■ □ □ ■ ■ ■ □ □ ■ □ □ ■ ■ □
            //□ ■ □ □ □ ■ □ ■ ■ ■ □ □ □ ■ ■ □ ■ ■ ■ □ □ ■ □ □ □ □ □ □ ■ ■ □ □ □ □ □ ■ * * ■ ■ ■ ■ □ ■ □ □ □ □ □ ■ □ □ □ □ ■ □ □ □ ■ □ □ □ □ ■ ■ □ □ □ □ □
            //■ □ □ ■ ■ ■ □ ■ ■ ■ □ ■ □ □ □ ■ □ ■ □ ■ □ ■ □ □ ■ □ ■ □ ■ ■ □ ■ □ □ □ □ □ ■ * * □ ■ □ □ □ □ □ □ □ ■ □ □ □ □ □ ■ ■ ■ □ □ □ ■ ■ ■ □ □ ■ □ □ □
            //□ □ □ □ ■ ■ □ □ ■ □ □ □ ■ ■ ■ □ ■ □ □ □ □ □ □ □ □ □ ■ □ □ □ ■ ■ □ □ □ □ ■ □ ■ ■ * ■ □ ■ □ □ □ ■ □ ■ □ □ ■ ■ □ ■ ■ □ ■ ■ □ ■ □ □ □ □ □ ■ □ □
            //□ ■ ■ ■ ■ □ □ ■ ■ □ ■ ■ □ □ ■ □ □ □ ■ ■ ■ ■ □ □ ■ ■ □ ■ □ □ □ □ □ ■ □ □ □ ■ □ ■ ■ * □ □ ■ □ □ □ ■ □ □ □ ■ ■ □ ■ ■ □ □ □ □ □ □ □ □ □ □ ■ ■ □
            //■ □ □ □ ■ □ □ ■ □ ■ □ □ □ □ □ ■ □ □ □ ■ ■ ■ ■ ■ ■ □ ■ ■ ■ □ □ □ □ □ □ ■ ■ ■ ■ □ □ ■ * □ □ ■ □ □ □ □ □ □ ■ ■ □ ■ □ □ □ ■ ■ □ □ □ □ □ ■ □ □ □
            //□ ■ ■ □ □ □ ■ □ □ ■ □ ■ □ □ □ □ ■ ■ □ □ ■ ■ □ ■ □ □ ■ □ □ □ ■ ■ □ □ □ □ □ ■ ■ □ □ ■ ■ * □ □ □ ■ ■ □ □ □ □ ■ □ □ □ ■ ■ □ □ □ □ □ □ □ □ □ ■ □
            //□ □ ■ □ □ ■ ■ ■ ■ □ □ □ □ ■ ■ ■ □ □ ■ □ ■ ■ ■ □ ■ ■ □ ■ □ ■ ■ □ ■ ■ □ □ □ □ ■ ■ □ ■ □ ■ * □ □ □ □ □ ■ □ □ ■ □ □ □ □ □ □ □ ■ □ ■ □ □ □ □ □ □
            //□ □ ■ □ □ ■ ■ □ □ □ □ □ ■ □ ■ □ ■ □ □ ■ □ ■ ■ □ □ □ □ □ □ □ □ ■ □ □ ■ ■ □ □ □ □ ■ □ □ □ ■ * □ □ □ ■ □ ■ □ □ □ ■ □ □ □ ■ ■ □ ■ □ □ □ ■ □ □ □
            //□ ■ □ ■ □ □ ■ ■ □ □ □ ■ □ □ ■ □ ■ ■ □ ■ □ ■ □ □ ■ □ □ □ □ □ □ □ ■ □ □ □ ■ □ □ □ □ □ □ □ □ ■ * □ ■ ■ ■ ■ ■ □ ■ □ □ ■ □ ■ ■ □ ■ □ □ □ □ ■ ■ □
            //□ ■ ■ □ ■ ■ ■ □ □ □ ■ □ ■ □ □ □ □ □ □ □ □ ■ ■ □ □ ■ ■ ■ □ □ □ □ □ □ □ □ □ ■ ■ ■ ■ □ □ □ ■ ■ □ * ■ □ □ ■ ■ ■ ■ □ ■ □ ■ □ □ ■ ■ □ □ □ ■ □ □ □
            //□ □ □ □ □ □ ■ ■ □ □ □ □ □ ■ □ □ ■ □ □ □ □ ■ ■ ■ ■ ■ □ ■ □ □ ■ ■ ■ □ □ ■ □ □ □ ■ ■ ■ ■ □ □ □ ■ □ * * * □ □ ■ □ □ ■ ■ ■ ■ □ ■ □ ■ □ ■ ■ □ □ □
            //■ ■ ■ ■ ■ □ □ □ □ □ □ ■ ■ □ □ ■ ■ □ □ ■ ■ ■ □ ■ □ □ □ ■ □ ■ ■ □ ■ □ □ ■ □ □ □ □ □ ■ □ □ ■ □ □ □ ■ ■ ■ * ■ ■ □ □ □ □ □ □ ■ □ □ □ □ □ □ □ □ □
            //□ □ □ □ □ □ □ □ ■ □ □ □ □ □ ■ □ ■ ■ ■ ■ ■ □ □ □ ■ □ □ □ ■ □ □ □ ■ □ □ □ □ □ □ □ □ ■ □ □ □ □ □ □ ■ □ ■ * ■ □ □ □ □ ■ ■ □ ■ □ □ ■ ■ □ ■ □ □ □
            //■ □ □ □ ■ ■ ■ □ ■ □ □ □ □ ■ □ □ ■ □ □ □ □ ■ □ □ □ ■ □ □ □ ■ ■ ■ □ □ □ □ ■ □ □ □ ■ ■ ■ ■ □ □ ■ □ □ ■ ■ □ * * ■ □ □ ■ □ ■ □ □ ■ □ □ □ □ □ ■ ■
            //□ □ ■ □ ■ □ □ □ □ □ □ ■ □ □ □ ■ □ □ □ ■ □ □ □ ■ □ □ □ ■ ■ ■ ■ □ ■ □ ■ □ □ □ □ □ □ ■ □ ■ ■ □ ■ □ □ □ □ ■ □ ■ * ■ □ □ □ □ □ □ ■ ■ □ ■ □ □ ■ □
            //□ □ ■ □ ■ ■ ■ □ ■ □ □ □ ■ □ □ □ □ □ ■ ■ □ □ □ □ ■ ■ ■ □ □ ■ □ □ □ □ □ ■ □ □ □ ■ □ □ □ ■ ■ ■ ■ □ □ □ ■ ■ □ □ □ * ■ □ □ ■ ■ □ □ □ ■ □ □ □ □ □
            //□ ■ ■ □ ■ ■ ■ □ ■ ■ □ □ □ □ □ □ ■ □ □ ■ □ □ ■ □ □ □ □ □ ■ □ ■ ■ □ ■ □ ■ □ □ □ □ ■ □ ■ ■ ■ □ □ □ ■ □ □ ■ □ □ □ ■ * ■ □ □ □ □ □ □ ■ □ ■ □ ■ □
            //□ □ ■ □ □ □ □ ■ □ ■ □ □ □ □ □ □ □ □ □ □ ■ □ □ □ □ □ □ ■ ■ □ □ ■ □ □ □ ■ ■ □ □ ■ □ □ □ □ ■ □ ■ □ □ ■ □ ■ □ □ ■ □ ■ * ■ ■ ■ ■ □ ■ □ □ ■ □ □ □
            //□ ■ □ □ □ □ □ □ □ ■ □ □ ■ □ □ ■ □ ■ □ ■ □ □ □ □ □ □ □ □ ■ □ ■ □ □ ■ □ □ ■ ■ □ □ □ ■ □ ■ ■ □ □ ■ □ ■ □ ■ □ ■ □ □ ■ ■ * * * * * * * ■ ■ ■ □ □
            //□ ■ □ □ ■ □ □ □ □ ■ ■ □ □ □ □ □ □ ■ ■ ■ □ ■ □ □ ■ ■ □ ■ ■ ■ □ □ ■ □ □ □ □ ■ ■ ■ □ □ □ □ □ □ □ □ ■ □ □ ■ ■ □ ■ □ □ □ □ □ ■ □ ■ ■ □ * * ■ ■ □
            //□ ■ □ □ □ □ □ ■ □ ■ ■ □ □ ■ ■ ■ □ ■ ■ □ □ ■ ■ ■ ■ ■ □ □ □ ■ ■ □ □ □ ■ □ ■ ■ □ ■ □ □ □ □ □ ■ □ □ ■ ■ ■ □ □ □ □ □ ■ □ □ □ □ ■ □ □ □ ■ ■ * * F


            string[,] mapDebug = new string[xMax, zMax];
            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (map[x, z] == true)
                    {
                        mapDebug[x, z] = " □";
                    }
                    else if (map[x, z] == false)
                    {
                        mapDebug[x, z] = " ■";
                    }
                }
            }

            int i = 0;
            foreach (Point item in path)
            {
                if (i == 0)
                {
                    mapDebug[item.X, item.Y] = " S";
                }
                else if (i == path.Count - 1)
                {
                    mapDebug[item.X, item.Y] = " F";
                }
                else
                {
                    mapDebug[item.X, item.Y] = " *";
                }
                i++;
            }

            for (int z = 0; z < zMax; z++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    System.Diagnostics.Debug.Write(mapDebug[x, z]);
                }
                System.Diagnostics.Debug.WriteLine("");
            }

        }

        private void AddGiantRandomWalls()
        {

            InitializeMap(70, 40, new Point(0, 0), new Point(69, 39));
            this.map[1, 0] = false;
            this.map[2, 0] = false;
            this.map[3, 0] = false;
            this.map[4, 0] = false;
            this.map[5, 0] = false;
            this.map[6, 0] = false;
            this.map[7, 0] = false;
            this.map[8, 0] = false;
            this.map[10, 0] = false;
            this.map[14, 0] = false;
            this.map[16, 0] = false;
            this.map[17, 0] = false;
            this.map[23, 0] = false;
            this.map[25, 0] = false;
            this.map[27, 0] = false;
            this.map[28, 0] = false;
            this.map[30, 0] = false;
            this.map[31, 0] = false;
            this.map[32, 0] = false;
            this.map[34, 0] = false;
            this.map[35, 0] = false;
            this.map[46, 0] = false;
            this.map[48, 0] = false;
            this.map[51, 0] = false;
            this.map[53, 0] = false;
            this.map[57, 0] = false;
            this.map[58, 0] = false;
            this.map[60, 0] = false;
            this.map[62, 0] = false;
            this.map[63, 0] = false;
            this.map[65, 0] = false;
            this.map[67, 0] = false;
            this.map[1, 1] = false;
            this.map[2, 1] = false;
            this.map[4, 1] = false;
            this.map[11, 1] = false;
            this.map[12, 1] = false;
            this.map[15, 1] = false;
            this.map[17, 1] = false;
            this.map[18, 1] = false;
            this.map[29, 1] = false;
            this.map[34, 1] = false;
            this.map[39, 1] = false;
            this.map[40, 1] = false;
            this.map[51, 1] = false;
            this.map[58, 1] = false;
            this.map[61, 1] = false;
            this.map[62, 1] = false;
            this.map[66, 1] = false;
            this.map[1, 2] = false;
            this.map[2, 2] = false;
            this.map[5, 2] = false;
            this.map[12, 2] = false;
            this.map[14, 2] = false;
            this.map[17, 2] = false;
            this.map[18, 2] = false;
            this.map[22, 2] = false;
            this.map[25, 2] = false;
            this.map[27, 2] = false;
            this.map[32, 2] = false;
            this.map[34, 2] = false;
            this.map[51, 2] = false;
            this.map[52, 2] = false;
            this.map[54, 2] = false;
            this.map[55, 2] = false;
            this.map[58, 2] = false;
            this.map[62, 2] = false;
            this.map[64, 2] = false;
            this.map[69, 2] = false;
            this.map[5, 3] = false;
            this.map[6, 3] = false;
            this.map[10, 3] = false;
            this.map[13, 3] = false;
            this.map[16, 3] = false;
            this.map[17, 3] = false;
            this.map[19, 3] = false;
            this.map[21, 3] = false;
            this.map[23, 3] = false;
            this.map[28, 3] = false;
            this.map[29, 3] = false;
            this.map[30, 3] = false;
            this.map[32, 3] = false;
            this.map[33, 3] = false;
            this.map[35, 3] = false;
            this.map[38, 3] = false;
            this.map[40, 3] = false;
            this.map[41, 3] = false;
            this.map[47, 3] = false;
            this.map[48, 3] = false;
            this.map[50, 3] = false;
            this.map[51, 3] = false;
            this.map[53, 3] = false;
            this.map[54, 3] = false;
            this.map[56, 3] = false;
            this.map[57, 3] = false;
            this.map[58, 3] = false;
            this.map[59, 3] = false;
            this.map[67, 3] = false;
            this.map[68, 3] = false;
            this.map[2, 4] = false;
            this.map[6, 4] = false;
            this.map[7, 4] = false;
            this.map[11, 4] = false;
            this.map[12, 4] = false;
            this.map[13, 4] = false;
            this.map[18, 4] = false;
            this.map[19, 4] = false;
            this.map[21, 4] = false;
            this.map[24, 4] = false;
            this.map[25, 4] = false;
            this.map[32, 4] = false;
            this.map[36, 4] = false;
            this.map[37, 4] = false;
            this.map[38, 4] = false;
            this.map[39, 4] = false;
            this.map[40, 4] = false;
            this.map[41, 4] = false;
            this.map[43, 4] = false;
            this.map[47, 4] = false;
            this.map[51, 4] = false;
            this.map[52, 4] = false;
            this.map[56, 4] = false;
            this.map[62, 4] = false;
            this.map[66, 4] = false;
            this.map[67, 4] = false;
            this.map[69, 4] = false;
            this.map[1, 5] = false;
            this.map[2, 5] = false;
            this.map[4, 5] = false;
            this.map[7, 5] = false;
            this.map[8, 5] = false;
            this.map[11, 5] = false;
            this.map[13, 5] = false;
            this.map[14, 5] = false;
            this.map[17, 5] = false;
            this.map[19, 5] = false;
            this.map[20, 5] = false;
            this.map[26, 5] = false;
            this.map[27, 5] = false;
            this.map[31, 5] = false;
            this.map[34, 5] = false;
            this.map[37, 5] = false;
            this.map[39, 5] = false;
            this.map[40, 5] = false;
            this.map[41, 5] = false;
            this.map[42, 5] = false;
            this.map[45, 5] = false;
            this.map[49, 5] = false;
            this.map[50, 5] = false;
            this.map[53, 5] = false;
            this.map[54, 5] = false;
            this.map[58, 5] = false;
            this.map[59, 5] = false;
            this.map[61, 5] = false;
            this.map[63, 5] = false;
            this.map[64, 5] = false;
            this.map[66, 5] = false;
            this.map[67, 5] = false;
            this.map[68, 5] = false;
            this.map[69, 5] = false;
            this.map[1, 6] = false;
            this.map[4, 6] = false;
            this.map[5, 6] = false;
            this.map[7, 6] = false;
            this.map[10, 6] = false;
            this.map[11, 6] = false;
            this.map[13, 6] = false;
            this.map[19, 6] = false;
            this.map[20, 6] = false;
            this.map[21, 6] = false;
            this.map[22, 6] = false;
            this.map[26, 6] = false;
            this.map[27, 6] = false;
            this.map[30, 6] = false;
            this.map[31, 6] = false;
            this.map[33, 6] = false;
            this.map[34, 6] = false;
            this.map[35, 6] = false;
            this.map[45, 6] = false;
            this.map[54, 6] = false;
            this.map[56, 6] = false;
            this.map[60, 6] = false;
            this.map[61, 6] = false;
            this.map[63, 6] = false;
            this.map[64, 6] = false;
            this.map[66, 6] = false;
            this.map[67, 6] = false;
            this.map[68, 6] = false;
            this.map[0, 7] = false;
            this.map[10, 7] = false;
            this.map[12, 7] = false;
            this.map[13, 7] = false;
            this.map[17, 7] = false;
            this.map[22, 7] = false;
            this.map[23, 7] = false;
            this.map[25, 7] = false;
            this.map[28, 7] = false;
            this.map[30, 7] = false;
            this.map[31, 7] = false;
            this.map[32, 7] = false;
            this.map[34, 7] = false;
            this.map[40, 7] = false;
            this.map[42, 7] = false;
            this.map[43, 7] = false;
            this.map[45, 7] = false;
            this.map[46, 7] = false;
            this.map[47, 7] = false;
            this.map[50, 7] = false;
            this.map[51, 7] = false;
            this.map[58, 7] = false;
            this.map[61, 7] = false;
            this.map[62, 7] = false;
            this.map[2, 8] = false;
            this.map[4, 8] = false;
            this.map[5, 8] = false;
            this.map[7, 8] = false;
            this.map[23, 8] = false;
            this.map[25, 8] = false;
            this.map[26, 8] = false;
            this.map[31, 8] = false;
            this.map[35, 8] = false;
            this.map[36, 8] = false;
            this.map[38, 8] = false;
            this.map[39, 8] = false;
            this.map[40, 8] = false;
            this.map[41, 8] = false;
            this.map[45, 8] = false;
            this.map[46, 8] = false;
            this.map[55, 8] = false;
            this.map[56, 8] = false;
            this.map[57, 8] = false;
            this.map[59, 8] = false;
            this.map[60, 8] = false;
            this.map[63, 8] = false;
            this.map[66, 8] = false;
            this.map[67, 8] = false;
            this.map[1, 9] = false;
            this.map[2, 9] = false;
            this.map[4, 9] = false;
            this.map[5, 9] = false;
            this.map[6, 9] = false;
            this.map[8, 9] = false;
            this.map[9, 9] = false;
            this.map[11, 9] = false;
            this.map[14, 9] = false;
            this.map[17, 9] = false;
            this.map[18, 9] = false;
            this.map[20, 9] = false;
            this.map[21, 9] = false;
            this.map[22, 9] = false;
            this.map[23, 9] = false;
            this.map[25, 9] = false;
            this.map[29, 9] = false;
            this.map[30, 9] = false;
            this.map[38, 9] = false;
            this.map[39, 9] = false;
            this.map[41, 9] = false;
            this.map[46, 9] = false;
            this.map[48, 9] = false;
            this.map[50, 9] = false;
            this.map[51, 9] = false;
            this.map[52, 9] = false;
            this.map[54, 9] = false;
            this.map[56, 9] = false;
            this.map[57, 9] = false;
            this.map[62, 9] = false;
            this.map[63, 9] = false;
            this.map[64, 9] = false;
            this.map[65, 9] = false;
            this.map[67, 9] = false;
            this.map[0, 10] = false;
            this.map[4, 10] = false;
            this.map[6, 10] = false;
            this.map[10, 10] = false;
            this.map[13, 10] = false;
            this.map[14, 10] = false;
            this.map[15, 10] = false;
            this.map[18, 10] = false;
            this.map[19, 10] = false;
            this.map[22, 10] = false;
            this.map[23, 10] = false;
            this.map[26, 10] = false;
            this.map[29, 10] = false;
            this.map[30, 10] = false;
            this.map[31, 10] = false;
            this.map[36, 10] = false;
            this.map[37, 10] = false;
            this.map[46, 10] = false;
            this.map[48, 10] = false;
            this.map[51, 10] = false;
            this.map[59, 10] = false;
            this.map[63, 10] = false;
            this.map[0, 11] = false;
            this.map[2, 11] = false;
            this.map[7, 11] = false;
            this.map[8, 11] = false;
            this.map[15, 11] = false;
            this.map[17, 11] = false;
            this.map[19, 11] = false;
            this.map[23, 11] = false;
            this.map[24, 11] = false;
            this.map[27, 11] = false;
            this.map[28, 11] = false;
            this.map[29, 11] = false;
            this.map[30, 11] = false;
            this.map[32, 11] = false;
            this.map[37, 11] = false;
            this.map[39, 11] = false;
            this.map[42, 11] = false;
            this.map[45, 11] = false;
            this.map[46, 11] = false;
            this.map[47, 11] = false;
            this.map[50, 11] = false;
            this.map[51, 11] = false;
            this.map[52, 11] = false;
            this.map[57, 11] = false;
            this.map[58, 11] = false;
            this.map[59, 11] = false;
            this.map[60, 11] = false;
            this.map[63, 11] = false;
            this.map[64, 11] = false;
            this.map[66, 11] = false;
            this.map[67, 11] = false;
            this.map[1, 12] = false;
            this.map[6, 12] = false;
            this.map[9, 12] = false;
            this.map[11, 12] = false;
            this.map[16, 12] = false;
            this.map[19, 12] = false;
            this.map[20, 12] = false;
            this.map[22, 12] = false;
            this.map[27, 12] = false;
            this.map[31, 12] = false;
            this.map[34, 12] = false;
            this.map[35, 12] = false;
            this.map[38, 12] = false;
            this.map[39, 12] = false;
            this.map[43, 12] = false;
            this.map[46, 12] = false;
            this.map[48, 12] = false;
            this.map[49, 12] = false;
            this.map[51, 12] = false;
            this.map[52, 12] = false;
            this.map[55, 12] = false;
            this.map[56, 12] = false;
            this.map[60, 12] = false;
            this.map[61, 12] = false;
            this.map[62, 12] = false;
            this.map[64, 12] = false;
            this.map[65, 12] = false;
            this.map[67, 12] = false;
            this.map[68, 12] = false;
            this.map[0, 13] = false;
            this.map[5, 13] = false;
            this.map[9, 13] = false;
            this.map[13, 13] = false;
            this.map[14, 13] = false;
            this.map[17, 13] = false;
            this.map[19, 13] = false;
            this.map[20, 13] = false;
            this.map[22, 13] = false;
            this.map[24, 13] = false;
            this.map[25, 13] = false;
            this.map[28, 13] = false;
            this.map[30, 13] = false;
            this.map[32, 13] = false;
            this.map[34, 13] = false;
            this.map[35, 13] = false;
            this.map[36, 13] = false;
            this.map[40, 13] = false;
            this.map[41, 13] = false;
            this.map[43, 13] = false;
            this.map[45, 13] = false;
            this.map[46, 13] = false;
            this.map[47, 13] = false;
            this.map[49, 13] = false;
            this.map[53, 13] = false;
            this.map[55, 13] = false;
            this.map[56, 13] = false;
            this.map[57, 13] = false;
            this.map[64, 13] = false;
            this.map[67, 13] = false;
            this.map[2, 14] = false;
            this.map[3, 14] = false;
            this.map[7, 14] = false;
            this.map[9, 14] = false;
            this.map[10, 14] = false;
            this.map[11, 14] = false;
            this.map[12, 14] = false;
            this.map[14, 14] = false;
            this.map[15, 14] = false;
            this.map[16, 14] = false;
            this.map[17, 14] = false;
            this.map[18, 14] = false;
            this.map[19, 14] = false;
            this.map[20, 14] = false;
            this.map[21, 14] = false;
            this.map[23, 14] = false;
            this.map[28, 14] = false;
            this.map[30, 14] = false;
            this.map[32, 14] = false;
            this.map[36, 14] = false;
            this.map[38, 14] = false;
            this.map[40, 14] = false;
            this.map[41, 14] = false;
            this.map[44, 14] = false;
            this.map[52, 14] = false;
            this.map[57, 14] = false;
            this.map[59, 14] = false;
            this.map[65, 14] = false;
            this.map[66, 14] = false;
            this.map[67, 14] = false;
            this.map[69, 14] = false;
            this.map[4, 15] = false;
            this.map[6, 15] = false;
            this.map[7, 15] = false;
            this.map[9, 15] = false;
            this.map[12, 15] = false;
            this.map[13, 15] = false;
            this.map[14, 15] = false;
            this.map[18, 15] = false;
            this.map[21, 15] = false;
            this.map[23, 15] = false;
            this.map[26, 15] = false;
            this.map[30, 15] = false;
            this.map[31, 15] = false;
            this.map[32, 15] = false;
            this.map[34, 15] = false;
            this.map[40, 15] = false;
            this.map[41, 15] = false;
            this.map[42, 15] = false;
            this.map[43, 15] = false;
            this.map[44, 15] = false;
            this.map[49, 15] = false;
            this.map[52, 15] = false;
            this.map[53, 15] = false;
            this.map[59, 15] = false;
            this.map[61, 15] = false;
            this.map[68, 15] = false;
            this.map[1, 16] = false;
            this.map[2, 16] = false;
            this.map[3, 16] = false;
            this.map[5, 16] = false;
            this.map[9, 16] = false;
            this.map[10, 16] = false;
            this.map[14, 16] = false;
            this.map[16, 16] = false;
            this.map[19, 16] = false;
            this.map[21, 16] = false;
            this.map[23, 16] = false;
            this.map[24, 16] = false;
            this.map[27, 16] = false;
            this.map[34, 16] = false;
            this.map[41, 16] = false;
            this.map[44, 16] = false;
            this.map[45, 16] = false;
            this.map[50, 16] = false;
            this.map[56, 16] = false;
            this.map[58, 16] = false;
            this.map[60, 16] = false;
            this.map[62, 16] = false;
            this.map[66, 16] = false;
            this.map[68, 16] = false;
            this.map[3, 17] = false;
            this.map[6, 17] = false;
            this.map[7, 17] = false;
            this.map[8, 17] = false;
            this.map[11, 17] = false;
            this.map[16, 17] = false;
            this.map[18, 17] = false;
            this.map[20, 17] = false;
            this.map[22, 17] = false;
            this.map[23, 17] = false;
            this.map[24, 17] = false;
            this.map[25, 17] = false;
            this.map[26, 17] = false;
            this.map[27, 17] = false;
            this.map[28, 17] = false;
            this.map[30, 17] = false;
            this.map[31, 17] = false;
            this.map[32, 17] = false;
            this.map[38, 17] = false;
            this.map[39, 17] = false;
            this.map[40, 17] = false;
            this.map[46, 17] = false;
            this.map[48, 17] = false;
            this.map[50, 17] = false;
            this.map[51, 17] = false;
            this.map[52, 17] = false;
            this.map[54, 17] = false;
            this.map[56, 17] = false;
            this.map[60, 17] = false;
            this.map[61, 17] = false;
            this.map[62, 17] = false;
            this.map[0, 18] = false;
            this.map[2, 18] = false;
            this.map[3, 18] = false;
            this.map[4, 18] = false;
            this.map[6, 18] = false;
            this.map[8, 18] = false;
            this.map[14, 18] = false;
            this.map[16, 18] = false;
            this.map[21, 18] = false;
            this.map[25, 18] = false;
            this.map[28, 18] = false;
            this.map[30, 18] = false;
            this.map[31, 18] = false;
            this.map[38, 18] = false;
            this.map[41, 18] = false;
            this.map[42, 18] = false;
            this.map[50, 18] = false;
            this.map[51, 18] = false;
            this.map[52, 18] = false;
            this.map[53, 18] = false;
            this.map[54, 18] = false;
            this.map[55, 18] = false;
            this.map[56, 18] = false;
            this.map[59, 18] = false;
            this.map[60, 18] = false;
            this.map[61, 18] = false;
            this.map[64, 18] = false;
            this.map[67, 18] = false;
            this.map[68, 18] = false;
            this.map[1, 19] = false;
            this.map[5, 19] = false;
            this.map[7, 19] = false;
            this.map[8, 19] = false;
            this.map[9, 19] = false;
            this.map[13, 19] = false;
            this.map[14, 19] = false;
            this.map[16, 19] = false;
            this.map[17, 19] = false;
            this.map[18, 19] = false;
            this.map[21, 19] = false;
            this.map[28, 19] = false;
            this.map[29, 19] = false;
            this.map[35, 19] = false;
            this.map[38, 19] = false;
            this.map[39, 19] = false;
            this.map[40, 19] = false;
            this.map[41, 19] = false;
            this.map[43, 19] = false;
            this.map[49, 19] = false;
            this.map[54, 19] = false;
            this.map[58, 19] = false;
            this.map[63, 19] = false;
            this.map[64, 19] = false;
            this.map[0, 20] = false;
            this.map[3, 20] = false;
            this.map[4, 20] = false;
            this.map[5, 20] = false;
            this.map[7, 20] = false;
            this.map[8, 20] = false;
            this.map[9, 20] = false;
            this.map[11, 20] = false;
            this.map[15, 20] = false;
            this.map[17, 20] = false;
            this.map[19, 20] = false;
            this.map[21, 20] = false;
            this.map[24, 20] = false;
            this.map[26, 20] = false;
            this.map[28, 20] = false;
            this.map[29, 20] = false;
            this.map[31, 20] = false;
            this.map[37, 20] = false;
            this.map[41, 20] = false;
            this.map[49, 20] = false;
            this.map[55, 20] = false;
            this.map[56, 20] = false;
            this.map[57, 20] = false;
            this.map[61, 20] = false;
            this.map[62, 20] = false;
            this.map[63, 20] = false;
            this.map[66, 20] = false;
            this.map[4, 21] = false;
            this.map[5, 21] = false;
            this.map[8, 21] = false;
            this.map[12, 21] = false;
            this.map[13, 21] = false;
            this.map[14, 21] = false;
            this.map[16, 21] = false;
            this.map[26, 21] = false;
            this.map[30, 21] = false;
            this.map[31, 21] = false;
            this.map[36, 21] = false;
            this.map[38, 21] = false;
            this.map[39, 21] = false;
            this.map[41, 21] = false;
            this.map[43, 21] = false;
            this.map[47, 21] = false;
            this.map[49, 21] = false;
            this.map[52, 21] = false;
            this.map[53, 21] = false;
            this.map[55, 21] = false;
            this.map[56, 21] = false;
            this.map[58, 21] = false;
            this.map[59, 21] = false;
            this.map[61, 21] = false;
            this.map[67, 21] = false;
            this.map[1, 22] = false;
            this.map[2, 22] = false;
            this.map[3, 22] = false;
            this.map[4, 22] = false;
            this.map[7, 22] = false;
            this.map[8, 22] = false;
            this.map[10, 22] = false;
            this.map[11, 22] = false;
            this.map[14, 22] = false;
            this.map[18, 22] = false;
            this.map[19, 22] = false;
            this.map[20, 22] = false;
            this.map[21, 22] = false;
            this.map[24, 22] = false;
            this.map[25, 22] = false;
            this.map[27, 22] = false;
            this.map[33, 22] = false;
            this.map[37, 22] = false;
            this.map[39, 22] = false;
            this.map[40, 22] = false;
            this.map[44, 22] = false;
            this.map[48, 22] = false;
            this.map[52, 22] = false;
            this.map[53, 22] = false;
            this.map[55, 22] = false;
            this.map[56, 22] = false;
            this.map[67, 22] = false;
            this.map[68, 22] = false;
            this.map[0, 23] = false;
            this.map[4, 23] = false;
            this.map[7, 23] = false;
            this.map[9, 23] = false;
            this.map[15, 23] = false;
            this.map[19, 23] = false;
            this.map[20, 23] = false;
            this.map[21, 23] = false;
            this.map[22, 23] = false;
            this.map[23, 23] = false;
            this.map[24, 23] = false;
            this.map[26, 23] = false;
            this.map[27, 23] = false;
            this.map[28, 23] = false;
            this.map[35, 23] = false;
            this.map[36, 23] = false;
            this.map[37, 23] = false;
            this.map[38, 23] = false;
            this.map[41, 23] = false;
            this.map[45, 23] = false;
            this.map[52, 23] = false;
            this.map[53, 23] = false;
            this.map[55, 23] = false;
            this.map[59, 23] = false;
            this.map[60, 23] = false;
            this.map[66, 23] = false;
            this.map[1, 24] = false;
            this.map[2, 24] = false;
            this.map[6, 24] = false;
            this.map[9, 24] = false;
            this.map[11, 24] = false;
            this.map[16, 24] = false;
            this.map[17, 24] = false;
            this.map[20, 24] = false;
            this.map[21, 24] = false;
            this.map[23, 24] = false;
            this.map[26, 24] = false;
            this.map[30, 24] = false;
            this.map[31, 24] = false;
            this.map[37, 24] = false;
            this.map[38, 24] = false;
            this.map[41, 24] = false;
            this.map[42, 24] = false;
            this.map[47, 24] = false;
            this.map[48, 24] = false;
            this.map[53, 24] = false;
            this.map[57, 24] = false;
            this.map[58, 24] = false;
            this.map[68, 24] = false;
            this.map[2, 25] = false;
            this.map[5, 25] = false;
            this.map[6, 25] = false;
            this.map[7, 25] = false;
            this.map[8, 25] = false;
            this.map[13, 25] = false;
            this.map[14, 25] = false;
            this.map[15, 25] = false;
            this.map[18, 25] = false;
            this.map[20, 25] = false;
            this.map[21, 25] = false;
            this.map[22, 25] = false;
            this.map[24, 25] = false;
            this.map[25, 25] = false;
            this.map[27, 25] = false;
            this.map[29, 25] = false;
            this.map[30, 25] = false;
            this.map[32, 25] = false;
            this.map[33, 25] = false;
            this.map[38, 25] = false;
            this.map[39, 25] = false;
            this.map[41, 25] = false;
            this.map[43, 25] = false;
            this.map[50, 25] = false;
            this.map[53, 25] = false;
            this.map[61, 25] = false;
            this.map[63, 25] = false;
            this.map[2, 26] = false;
            this.map[5, 26] = false;
            this.map[6, 26] = false;
            this.map[12, 26] = false;
            this.map[14, 26] = false;
            this.map[16, 26] = false;
            this.map[19, 26] = false;
            this.map[21, 26] = false;
            this.map[22, 26] = false;
            this.map[31, 26] = false;
            this.map[34, 26] = false;
            this.map[35, 26] = false;
            this.map[40, 26] = false;
            this.map[44, 26] = false;
            this.map[49, 26] = false;
            this.map[51, 26] = false;
            this.map[55, 26] = false;
            this.map[59, 26] = false;
            this.map[60, 26] = false;
            this.map[62, 26] = false;
            this.map[66, 26] = false;
            this.map[1, 27] = false;
            this.map[3, 27] = false;
            this.map[6, 27] = false;
            this.map[7, 27] = false;
            this.map[11, 27] = false;
            this.map[14, 27] = false;
            this.map[16, 27] = false;
            this.map[17, 27] = false;
            this.map[19, 27] = false;
            this.map[21, 27] = false;
            this.map[24, 27] = false;
            this.map[32, 27] = false;
            this.map[36, 27] = false;
            this.map[45, 27] = false;
            this.map[48, 27] = false;
            this.map[49, 27] = false;
            this.map[50, 27] = false;
            this.map[51, 27] = false;
            this.map[52, 27] = false;
            this.map[54, 27] = false;
            this.map[57, 27] = false;
            this.map[59, 27] = false;
            this.map[60, 27] = false;
            this.map[62, 27] = false;
            this.map[67, 27] = false;
            this.map[68, 27] = false;
            this.map[1, 28] = false;
            this.map[2, 28] = false;
            this.map[4, 28] = false;
            this.map[5, 28] = false;
            this.map[6, 28] = false;
            this.map[10, 28] = false;
            this.map[12, 28] = false;
            this.map[21, 28] = false;
            this.map[22, 28] = false;
            this.map[25, 28] = false;
            this.map[26, 28] = false;
            this.map[27, 28] = false;
            this.map[37, 28] = false;
            this.map[38, 28] = false;
            this.map[39, 28] = false;
            this.map[40, 28] = false;
            this.map[44, 28] = false;
            this.map[45, 28] = false;
            this.map[48, 28] = false;
            this.map[51, 28] = false;
            this.map[52, 28] = false;
            this.map[53, 28] = false;
            this.map[54, 28] = false;
            this.map[56, 28] = false;
            this.map[58, 28] = false;
            this.map[61, 28] = false;
            this.map[62, 28] = false;
            this.map[66, 28] = false;
            this.map[6, 29] = false;
            this.map[7, 29] = false;
            this.map[13, 29] = false;
            this.map[16, 29] = false;
            this.map[21, 29] = false;
            this.map[22, 29] = false;
            this.map[23, 29] = false;
            this.map[24, 29] = false;
            this.map[25, 29] = false;
            this.map[27, 29] = false;
            this.map[30, 29] = false;
            this.map[31, 29] = false;
            this.map[32, 29] = false;
            this.map[35, 29] = false;
            this.map[39, 29] = false;
            this.map[40, 29] = false;
            this.map[41, 29] = false;
            this.map[42, 29] = false;
            this.map[46, 29] = false;
            this.map[53, 29] = false;
            this.map[56, 29] = false;
            this.map[57, 29] = false;
            this.map[58, 29] = false;
            this.map[59, 29] = false;
            this.map[61, 29] = false;
            this.map[63, 29] = false;
            this.map[65, 29] = false;
            this.map[66, 29] = false;
            this.map[0, 30] = false;
            this.map[1, 30] = false;
            this.map[2, 30] = false;
            this.map[3, 30] = false;
            this.map[4, 30] = false;
            this.map[11, 30] = false;
            this.map[12, 30] = false;
            this.map[15, 30] = false;
            this.map[16, 30] = false;
            this.map[19, 30] = false;
            this.map[20, 30] = false;
            this.map[21, 30] = false;
            this.map[23, 30] = false;
            this.map[27, 30] = false;
            this.map[29, 30] = false;
            this.map[30, 30] = false;
            this.map[32, 30] = false;
            this.map[35, 30] = false;
            this.map[41, 30] = false;
            this.map[44, 30] = false;
            this.map[48, 30] = false;
            this.map[49, 30] = false;
            this.map[50, 30] = false;
            this.map[52, 30] = false;
            this.map[53, 30] = false;
            this.map[60, 30] = false;
            this.map[8, 31] = false;
            this.map[14, 31] = false;
            this.map[16, 31] = false;
            this.map[17, 31] = false;
            this.map[18, 31] = false;
            this.map[19, 31] = false;
            this.map[20, 31] = false;
            this.map[24, 31] = false;
            this.map[28, 31] = false;
            this.map[32, 31] = false;
            this.map[41, 31] = false;
            this.map[48, 31] = false;
            this.map[50, 31] = false;
            this.map[52, 31] = false;
            this.map[57, 31] = false;
            this.map[58, 31] = false;
            this.map[60, 31] = false;
            this.map[63, 31] = false;
            this.map[64, 31] = false;
            this.map[66, 31] = false;
            this.map[0, 32] = false;
            this.map[4, 32] = false;
            this.map[5, 32] = false;
            this.map[6, 32] = false;
            this.map[8, 32] = false;
            this.map[13, 32] = false;
            this.map[16, 32] = false;
            this.map[21, 32] = false;
            this.map[25, 32] = false;
            this.map[29, 32] = false;
            this.map[30, 32] = false;
            this.map[31, 32] = false;
            this.map[36, 32] = false;
            this.map[40, 32] = false;
            this.map[41, 32] = false;
            this.map[42, 32] = false;
            this.map[43, 32] = false;
            this.map[46, 32] = false;
            this.map[49, 32] = false;
            this.map[50, 32] = false;
            this.map[54, 32] = false;
            this.map[57, 32] = false;
            this.map[59, 32] = false;
            this.map[62, 32] = false;
            this.map[68, 32] = false;
            this.map[69, 32] = false;
            this.map[2, 33] = false;
            this.map[4, 33] = false;
            this.map[11, 33] = false;
            this.map[15, 33] = false;
            this.map[19, 33] = false;
            this.map[23, 33] = false;
            this.map[27, 33] = false;
            this.map[28, 33] = false;
            this.map[29, 33] = false;
            this.map[30, 33] = false;
            this.map[32, 33] = false;
            this.map[34, 33] = false;
            this.map[41, 33] = false;
            this.map[43, 33] = false;
            this.map[44, 33] = false;
            this.map[46, 33] = false;
            this.map[51, 33] = false;
            this.map[53, 33] = false;
            this.map[55, 33] = false;
            this.map[62, 33] = false;
            this.map[63, 33] = false;
            this.map[65, 33] = false;
            this.map[68, 33] = false;
            this.map[2, 34] = false;
            this.map[4, 34] = false;
            this.map[5, 34] = false;
            this.map[6, 34] = false;
            this.map[8, 34] = false;
            this.map[12, 34] = false;
            this.map[18, 34] = false;
            this.map[19, 34] = false;
            this.map[24, 34] = false;
            this.map[25, 34] = false;
            this.map[26, 34] = false;
            this.map[29, 34] = false;
            this.map[35, 34] = false;
            this.map[39, 34] = false;
            this.map[43, 34] = false;
            this.map[44, 34] = false;
            this.map[45, 34] = false;
            this.map[46, 34] = false;
            this.map[50, 34] = false;
            this.map[51, 34] = false;
            this.map[56, 34] = false;
            this.map[59, 34] = false;
            this.map[60, 34] = false;
            this.map[64, 34] = false;
            this.map[1, 35] = false;
            this.map[2, 35] = false;
            this.map[4, 35] = false;
            this.map[5, 35] = false;
            this.map[6, 35] = false;
            this.map[8, 35] = false;
            this.map[9, 35] = false;
            this.map[16, 35] = false;
            this.map[19, 35] = false;
            this.map[22, 35] = false;
            this.map[28, 35] = false;
            this.map[30, 35] = false;
            this.map[31, 35] = false;
            this.map[33, 35] = false;
            this.map[35, 35] = false;
            this.map[40, 35] = false;
            this.map[42, 35] = false;
            this.map[43, 35] = false;
            this.map[44, 35] = false;
            this.map[48, 35] = false;
            this.map[51, 35] = false;
            this.map[55, 35] = false;
            this.map[57, 35] = false;
            this.map[64, 35] = false;
            this.map[66, 35] = false;
            this.map[68, 35] = false;
            this.map[2, 36] = false;
            this.map[7, 36] = false;
            this.map[9, 36] = false;
            this.map[20, 36] = false;
            this.map[27, 36] = false;
            this.map[28, 36] = false;
            this.map[31, 36] = false;
            this.map[35, 36] = false;
            this.map[36, 36] = false;
            this.map[39, 36] = false;
            this.map[44, 36] = false;
            this.map[46, 36] = false;
            this.map[49, 36] = false;
            this.map[51, 36] = false;
            this.map[54, 36] = false;
            this.map[56, 36] = false;
            this.map[58, 36] = false;
            this.map[59, 36] = false;
            this.map[60, 36] = false;
            this.map[61, 36] = false;
            this.map[63, 36] = false;
            this.map[66, 36] = false;
            this.map[1, 37] = false;
            this.map[9, 37] = false;
            this.map[12, 37] = false;
            this.map[15, 37] = false;
            this.map[17, 37] = false;
            this.map[19, 37] = false;
            this.map[28, 37] = false;
            this.map[30, 37] = false;
            this.map[33, 37] = false;
            this.map[36, 37] = false;
            this.map[37, 37] = false;
            this.map[41, 37] = false;
            this.map[43, 37] = false;
            this.map[44, 37] = false;
            this.map[47, 37] = false;
            this.map[49, 37] = false;
            this.map[51, 37] = false;
            this.map[53, 37] = false;
            this.map[56, 37] = false;
            this.map[57, 37] = false;
            this.map[65, 37] = false;
            this.map[66, 37] = false;
            this.map[67, 37] = false;
            this.map[1, 38] = false;
            this.map[4, 38] = false;
            this.map[9, 38] = false;
            this.map[10, 38] = false;
            this.map[17, 38] = false;
            this.map[18, 38] = false;
            this.map[19, 38] = false;
            this.map[21, 38] = false;
            this.map[24, 38] = false;
            this.map[25, 38] = false;
            this.map[27, 38] = false;
            this.map[28, 38] = false;
            this.map[29, 38] = false;
            this.map[32, 38] = false;
            this.map[37, 38] = false;
            this.map[38, 38] = false;
            this.map[39, 38] = false;
            this.map[48, 38] = false;
            this.map[51, 38] = false;
            this.map[52, 38] = false;
            this.map[54, 38] = false;
            this.map[60, 38] = false;
            this.map[62, 38] = false;
            this.map[63, 38] = false;
            this.map[67, 38] = false;
            this.map[68, 38] = false;
            this.map[1, 39] = false;
            this.map[7, 39] = false;
            this.map[9, 39] = false;
            this.map[10, 39] = false;
            this.map[13, 39] = false;
            this.map[14, 39] = false;
            this.map[15, 39] = false;
            this.map[17, 39] = false;
            this.map[18, 39] = false;
            this.map[21, 39] = false;
            this.map[22, 39] = false;
            this.map[23, 39] = false;
            this.map[24, 39] = false;
            this.map[25, 39] = false;
            this.map[29, 39] = false;
            this.map[30, 39] = false;
            this.map[34, 39] = false;
            this.map[36, 39] = false;
            this.map[37, 39] = false;
            this.map[39, 39] = false;
            this.map[45, 39] = false;
            this.map[48, 39] = false;
            this.map[49, 39] = false;
            this.map[50, 39] = false;
            this.map[56, 39] = false;
            this.map[61, 39] = false;
            this.map[65, 39] = false;
            this.map[66, 39] = false;

        }
    }
}
