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

        [TestMethod]
        public void Test_WithoutWalls_CanFindPath()
        {
            // Arrange
            InitializeMap(7, 5, Point.Empty, Point.Empty);
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            PathFindingResult pathResult = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(pathResult);
            Assert.IsNotNull(pathResult.Path);
            Assert.IsTrue(pathResult.Path.Any());
            Assert.AreEqual(4, pathResult.Path.Count);
        }

        [TestMethod]
        public void Test_WithOpenWall_CanFindPathAroundWall()
        {
            // Arrange
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ * ■ ■ * □
            //  □ □ □ * * □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2
            InitializeMap(7, 5, Point.Empty, Point.Empty);
            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            PathFindingResult pathResult = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(pathResult);
            Assert.IsNotNull(pathResult.Path);
            Assert.IsTrue(pathResult.Path.Any());
            Assert.IsTrue(pathResult.LastTile.TraversalCost == 6);
            Assert.IsTrue(pathResult.Path.Count == 5);
        }

        [TestMethod]
        public void Test_WithClosedWall_CannotFindPath()
        {
            // Arrange
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

            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            PathFindingResult pathResult = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(pathResult);
            Assert.IsNotNull(pathResult.Path);
            Assert.IsFalse(pathResult.Path.Any());
            Assert.IsFalse(pathResult.Tiles.Any());
        }


        [TestMethod]
        public void Test_WithMazeWall()
        {
            // Arrange
            //  S ■ ■ □ ■ ■ F
            //  * ■ □ ■ □ ■ □
            //  * ■ □ ■ □ ■ □
            //  * ■ □ ■ □ ■ □
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

            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            PathFindingResult pathResult = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(pathResult);
            Assert.IsNotNull(pathResult.Path);
            Assert.IsTrue(pathResult.Path.Any());
            Assert.IsTrue(pathResult.LastTile.TraversalCost == 18);
            Assert.AreEqual(16, pathResult.Path.Count);
        }


        [TestMethod]
        public void Test_GiantRandomMap_WithInefficentPath()
        {
            // Arrange
            AddGiantRandomWalls();
            PathFinding pathFinder = new PathFinding(searchParameters);

            // Act
            PathFindingResult pathResult = pathFinder.FindPath();

            // Assert
            Assert.IsNotNull(pathResult);
            Assert.IsNotNull(pathResult.Path);
            Assert.IsTrue(pathResult.Path.Any());
            Assert.AreEqual(97, pathResult.Path.Count);
            CreateDebugPictureOfMapAndRoute(70, 40, pathResult.Path);
        }


        #region "private helper functions"

        private void InitializeMap(int xMax, int zMax, Point startingLocation, Point endLocation, bool locationsNotSet = true)
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
                if (startingLocation == Point.Empty)
                {
                    startingLocation = new Point(1, 2);
                }
                if (endLocation == Point.Empty)
                {
                    endLocation = new Point(5, 2);
                }
            }
            this.searchParameters = new SearchParameters(startingLocation, endLocation, map);
        }
        

        private void CreateDebugPictureOfMapAndRoute(int xMax, int zMax, List<Point> path)
        {


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
                    mapDebug[0, 0] = " S";
                }
                if (i == path.Count - 1)
                {
                    mapDebug[item.X, item.Y] = " F";
                }
                else
                {
                    mapDebug[item.X, item.Y] = " *";
                }
                i++;
            }

            //for (int z = 0; z < zMax; z++)
            for (int z = zMax - 1; z >= 0; z--)
            {
                for (int x = 0; x < xMax; x++)
                {
                    System.Diagnostics.Debug.Write(mapDebug[x, z]);
                }
                System.Diagnostics.Debug.WriteLine("");
            }

        }


        #region "Crazy long working for huge random map"

        private void AddGiantRandomWalls()
        {
            //□ ■ □ □ □ ■ ■ □ ■ ■ ■ ■ □ □ □ ■ □ ■ □ □ □ ■ ■ □ □ ■ □ □ □ □ ■ □ □ □ ■ □ ■ □ ■ □ ■ ■ ■ ■ ■ □ ■ ■ ■ □ □ ■ ■ ■ □ □ □ □ ■ □ □ □ □ □ ■ ■ □ □ □ F
            //■ ■ □ ■ □ ■ ■ □ □ □ ■ ■ □ ■ □ □ □ □ □ ■ □ □ □ □ □ □ □ ■ □ □ □ ■ □ ■ □ □ □ ■ □ □ □ □ □ □ ■ □ □ ■ ■ □ □ □ ■ ■ ■ □ □ □ ■ □ □ ■ ** □ ■ □ □ * ■
            //■ □ ■ □ ■ ■ ■ □ ■ □ □ □ ■ ■ □ □ □ □ □ ■ □ ■ □ □ ■ □ ■ □ □ □ □ □ □ □ □ □ □ □ □ □ ■ □ ■ □ □ □ □ □ □ □ ■ □ ■ ■ ■ □ □ □ □ □ ** ■ ■ * ■ ■ * ■ □
            //■ □ ■ □ ■ ■ ■ □ □ □ □ □ ■ ■ ■ □ □ ■ □ □ □ □ ■ □ □ □ □ □ □ ■ □ □ □ □ □ ■ □ □ □ □ □ ■ ■ ■ □ ■ ■ ■ □ ■ □ □ □ ■ □ □ □ ■ □ * ■ □ ■ □ □ * * □ ■ □
            //□ ■ □ □ □ ■ □ □ □ □ □ ■ ■ ■ □ □ □ □ ■ ■ ■ ■ ■ □ □ ■ □ □ ■ □ □ ■ ■ ■ ■ □ □ ■ ■ □ ■ ■ ■ □ ■ □ □ □ □ ■ □ □ ■ ■ □ □ □ □ * ■ □ ■ ■ □ ■ ■ □ ■ □ ■
            //■ ■ □ ■ □ ■ □ □ □ ■ ■ □ ■ □ □ □ □ ■ ■ □ □ ■ □ □ □ □ ■ □ □ ■ ■ □ ■ □ ■ □ □ □ □ □ ■ □ ■ ■ ■ ■ □ □ ■ ■ □ ■ □ □ □ ■ □ * ■ □ ■ □ ■ □ ■ ■ □ ■ □ □
            //□ □ ■ □ ■ ■ ■ ■ □ ■ ■ ■ □ □ ■ □ □ □ ■ ■ □ □ □ ■ □ □ ■ □ ■ □ □ ■ □ ■ ■ □ ■ ■ □ □ □ □ □ ■ ■ □ □ □ ■ □ ■ □ □ ■ ■ □ □ * ■ ■ □ □ □ □ □ ■ □ □ ■ □
            //□ ■ □ ■ ■ □ ■ □ ■ □ □ □ □ ■ □ □ ■ □ □ ■ □ ■ □ □ ■ □ ■ ■ ■ □ □ ■ □ □ □ □ ■ □ □ ■ ■ □ ■ □ □ ■ ■ ■ ■ □ □ ■ □ ■ ■ □ * ■ □ □ □ ■ ■ □ ■ □ □ ■ □ ■
            //□ ■ □ □ □ □ □ □ ■ □ □ □ ■ ■ □ ■ □ ■ □ □ □ ■ ■ □ □ □ □ ■ □ □ ■ □ □ □ □ □ □ □ ■ □ □ ■ □ □ □ □ ■ ■ □ □ □ ■ □ ■ □ □ * ■ ■ □ □ □ ■ □ □ □ ■ □ □ ■
            //□ ■ □ □ ■ ■ □ □ □ ■ ■ ■ □ ■ □ ■ ■ □ □ □ ■ □ ■ ■ □ ■ □ ■ ■ ■ □ □ □ ■ ■ ■ □ □ □ ■ ■ □ □ ■ □ ■ ■ □ ■ □ ■ ■ ■ □ ■ * ■ ■ □ ■ □ ■ □ □ □ □ ■ □ □ □
            //■ □ □ □ □ □ □ □ ■ ■ □ ■ □ ■ ■ □ □ □ □ ■ □ □ ■ □ □ □ □ □ □ ■ ■ ■ ■ □ □ □ □ □ ■ ■ ■ □ ■ ■ ■ ■ □ □ □ ■ * □ ■ □ ■ □ * ■ ■ ■ ■ ■ ■ □ ■ ■ □ □ □ □
            //■ ■ □ □ □ ■ ■ □ ■ □ ■ □ □ □ ■ ■ ■ □ □ ■ ■ □ □ □ ■ □ □ □ ■ ■ □ ■ ■ □ □ ■ □ * * * ■ ■ ■ ■ □ ■ ■ ■ * * ■ * ■ ■ ■ * ■ ■ □ □ □ □ □ □ □ ■ □ □ □ ■
            //□ □ □ □ ■ ■ □ □ ■ □ ■ □ □ □ ■ ■ ■ ■ ■ □ □ ■ □ □ □ ■ □ □ □ ■ ■ □ □ □ * * * ■ ■ ■ * * * ■ * * * * ■ ■ ■ □ * * ■ * □ ■ ■ □ □ □ ■ ■ ■ □ □ □ □ ■
            //□ ■ □ □ ■ □ ■ □ ■ ■ □ □ ■ □ □ ■ □ □ ■ ■ □ □ □ ■ □ ■ □ □ ■ □ □ □ □ * ■ ■ ■ □ □ □ ■ □ ■ * □ □ □ □ □ □ □ □ □ ■ * □ ■ ■ □ ■ □ □ ■ ■ ■ □ ■ □ □ □
            //□ ■ □ □ □ ■ ■ ■ □ □ □ □ □ ■ □ □ ■ ■ ■ ■ ■ ■ □ □ □ ■ □ ■ ■ ■ □ ■ □ ■ * ■ □ □ □ ■ ■ □ □ ■ ■ □ □ ■ □ □ ■ □ □ □ ■ □ ■ □ ■ □ ■ ■ □ □ □ ■ □ □ □ ■
            //□ □ ■ □ □ □ □ □ □ □ □ ■ ■ ■ ■ □ □ ■ □ ■ □ ■ □ □ □ ■ ■ ■ ■ ■ ■ □ □ □ * ■ □ ■ ■ ■ □ ■ □ □ ■ □ □ ■ □ ■ □ ■ □ ■ □ ■ □ □ □ □ □ □ ■ □ ■ ■ □ □ □ ■
            //□ □ □ □ ■ □ □ □ □ ■ ■ □ ■ ■ ■ □ □ ■ ■ □ □ □ □ □ ■ □ ■ □ ■ □ □ ■ □ □ * ■ □ ■ □ □ ■ □ ■ ■ ■ □ ■ ■ □ ■ □ ■ □ □ □ ■ □ □ ■ ■ □ □ □ □ □ □ □ □ □ □
            //■ □ □ □ □ ■ ■ □ ■ □ ■ □ □ □ □ ■ □ □ □ ■ ■ □ □ □ □ ■ ■ □ □ □ ■ ■ □ * ■ ■ □ ■ ■ □ ■ ■ ■ □ ■ □ ■ □ □ □ □ □ □ □ □ □ □ □ □ ■ ■ ■ ■ □ □ ■ ■ □ □ ■
            //□ □ □ □ □ □ ■ □ □ ■ □ □ □ ■ ■ ■ □ □ □ □ ■ □ □ ■ □ □ □ ■ ■ ■ ■ ■ □ * ■ ■ ■ ■ ■ ■ ■ □ ■ ■ ■ □ ■ □ □ ■ □ ■ □ □ □ ■ □ □ □ ■ □ □ □ □ □ ■ □ ■ □ □
            //□ □ □ □ □ □ □ ■ ■ □ ■ □ □ □ ■ ■ ■ □ □ □ □ ■ ■ □ ■ ■ ■ □ ■ ■ ■ □ * ■ ■ ■ □ ■ □ ■ □ □ ■ □ □ ■ □ □ □ ■ ■ □ □ □ ■ ■ ■ ■ □ □ ■ ■ ■ □ ■ □ □ □ □ □
            //■ □ □ ■ ■ ■ □ ■ □ □ ■ □ ■ □ □ ■ □ ■ □ □ □ □ □ ■ ■ ■ □ □ □ ■ □ * □ ■ ■ □ □ ■ □ □ □ □ □ □ ■ □ □ ■ ■ □ □ ■ □ □ □ □ □ ■ □ ■ □ □ □ □ ■ ■ □ ■ □ □
            //■ ■ ■ ■ □ ■ □ □ ■ □ □ □ □ □ □ □ ■ ■ □ ■ ■ □ □ □ □ □ □ □ □ □ * ■ ■ ■ ■ ■ □ ■ □ □ ■ □ ■ □ □ ■ ■ ■ □ □ □ ■ ■ □ □ ■ □ □ ■ ■ ■ ■ ■ □ ■ □ □ ■ □ □
            //■ ■ □ ■ ■ □ ■ □ □ □ ■ □ □ □ □ □ □ □ □ ■ ■ □ □ □ □ ■ □ ■ ■ ■ □ * * * * * □ ■ □ □ □ ■ □ □ □ ■ ■ ■ □ □ □ □ ■ □ □ ■ □ □ □ □ □ ■ ■ ■ ■ □ □ ■ ■ ■
            //□ □ □ □ □ □ □ ■ ■ □ ■ ■ ■ □ □ □ ■ ■ □ ■ □ □ ■ ■ □ □ ■ ■ □ □ □ ■ □ ■ □ ■ * ■ □ □ ■ □ □ □ □ ■ □ □ □ □ □ ■ □ □ ■ □ □ □ □ □ □ □ □ ■ ■ ■ □ ■ □ □
            //■ ■ □ ■ □ ■ □ ■ ■ □ ■ □ □ □ ■ □ ■ □ □ ■ □ ■ ■ ■ □ □ ■ □ ■ □ □ ■ ■ □ ■ * □ ■ □ ■ □ □ □ □ □ □ □ ■ □ ■ □ ■ □ ■ □ □ ■ □ ■ □ ■ □ ■ □ ■ ■ ■ □ □ □
            //■ ■ ■ □ □ ■ □ ■ ■ ■ ■ □ □ ■ ■ ■ □ □ ■ ■ □ □ □ □ □ ■ □ ■ ■ □ * * ■ ■ * ■ □ □ ■ ■ □ ■ ■ ■ □ ■ □ □ ■ □ □ ■ □ □ □ □ □ □ □ ■ ■ □ □ ■ ■ □ □ □ ■ □
            //■ ■ ■ □ ■ □ ■ □ ■ ■ ■ □ □ ■ □ ■ ■ ■ ■ □ □ □ ■ ■ ■ □ ■ ■ ■ * □ □ * * □ □ □ □ □ □ □ □ □ □ ■ □ ■ □ ■ □ □ ■ ■ □ □ ■ ■ ■ □ □ □ □ □ □ □ ■ ■ □ □ □
            //□ □ ■ □ □ □ □ □ □ □ □ ■ ■ * * ■ □ ■ □ □ □ ■ □ * ■ ■ ■ * * □ □ □ □ □ □ ■ □ ■ □ □ ■ ■ ■ ■ □ ■ ■ ■ ■ □ ■ ■ □ □ □ ■ ■ ■ ■ □ □ □ □ ■ ■ □ ■ ■ ■ □
            //□ □ ■ ■ □ ■ □ ■ ■ □ □ * * □ ■ * □ ■ □ ■ □ □ * ■ * ■ * □ □ □ ■ □ □ ■ ■ □ ■ □ ■ □ □ ■ □ □ □ □ □ □ □ □ ■ □ ■ □ □ □ □ □ □ ■ ■ ■ ■ □ □ □ ■ □ □ □
            //■ □ □ ■ □ □ ■ ■ □ ■ * ■ □ ■ □ ■ * ■ ■ ■ ■ □ * ■ □ * □ □ □ ■ ■ ■ □ ■ □ ■ □ ■ □ ■ □ ■ ■ □ □ ■ ■ ■ □ □ ■ □ □ □ □ ■ ■ □ ■ □ □ □ ■ □ □ □ ■ □ □ □
            //□ ■ □ □ □ ■ ■ □ * * ■ ■ □ □ □ * ■ ■ □ ■ □ * ■ □ □ ■ ■ ■ ■ □ □ ■ □ □ □ ■ □ □ □ ■ □ □ ■ ■ ■ □ □ ■ □ □ ■ ■ □ □ □ ■ ■ □ □ □ ■ ■ □ □ ■ □ □ □ ■ ■
            //□ □ ■ □ ■ ■ ■ * ■ ■ □ ■ □ ■ □ * ■ * * ■ * □ □ □ ■ ■ ■ □ ■ ■ ■ ■ □ □ ■ □ ■ □ ■ □ □ □ ■ □ ■ ■ ■ ■ □ □ □ □ □ ■ □ ■ □ □ □ ■ □ ■ □ ■ □ □ □ □ □ □
            //■ □ □ □ ■ * * ■ □ ■ □ ■ ■ □ □ □ * □ □ * □ ■ □ □ □ □ □ □ □ □ □ □ □ □ ■ ■ □ ■ □ □ □ □ □ □ □ □ □ ■ ■ □ ■ ■ □ ■ □ □ □ ■ □ □ □ □ ■ ■ ■ □ ■ □ □ ■
            //□ □ ■ □ * ■ ■ □ □ ■ □ ■ □ ■ □ ■ ■ □ ■ □ □ □ □ ■ ■ □ ■ ■ ■ ■ □ □ □ ■ ■ □ ■ ■ □ □ □ □ □ □ ■ ■ □ ■ □ □ ■ □ □ □ □ ■ □ □ □ ■ □ ■ ■ □ □ ■ ■ □ ■ ■
            //□ ■ ■ □ * ■ □ □ □ ■ □ □ □ □ □ ■ □ □ ■ □ □ □ □ □ □ ■ □ □ ■ □ □ ■ ■ □ □ ■ ■ ■ □ □ □ □ □ ■ □ □ □ □ ■ ■ ■ ■ ■ □ □ □ □ □ □ □ ■ □ ■ □ □ □ □ □ □ ■
            //■ ■ □ * ■ □ □ □ □ ■ □ □ ■ ■ □ □ □ □ ■ ■ ■ □ □ □ ■ ■ ■ ■ ■ □ □ □ □ □ □ □ ■ ■ □ □ ■ ■ □ □ ■ □ □ □ □ □ ■ □ □ □ □ □ ■ ■ □ ■ □ □ ■ □ ■ ■ ■ ■ □ ■
            //□ □ □ * ■ □ □ □ □ □ □ ■ □ ■ □ □ □ □ □ □ □ ■ ■ □ □ □ ■ □ □ □ ■ ■ □ □ ■ □ □ □ □ □ □ ■ ■ □ ■ □ □ □ □ □ □ ■ □ □ □ ■ □ ■ □ □ □ □ □ ■ □ □ □ □ □ □
            //□ □ * ■ □ ■ □ ■ ■ □ ■ □ □ □ ■ ■ □ □ ■ □ □ □ ■ □ ■ □ □ □ □ ■ ■ □ ■ ■ ■ □ □ ■ □ ■ □ □ □ □ □ □ □ ■ ■ □ □ ■ ■ □ ■ □ □ ■ □ ■ □ ■ □ ■ □ ■ □ □ □ □
            //■ * ■ □ □ □ □ □ ■ ■ □ □ ■ ■ ■ ■ ■ ■ □ □ □ □ □ ■ □ □ □ □ □ □ □ □ □ □ □ □ ■ ■ □ □ □ ■ □ ■ □ ■ □ ■ □ ■ □ ■ ■ □ □ □ ■ □ ■ □ □ □ □ ■ □ □ ■ □ □ □
            //S □ □ □ □ □ ■ □ ■ □ ■ □ □ □ □ □ □ ■ ■ □ ■ □ □ ■ ■ □ □ □ □ □ ■ □ □ ■ ■ □ ■ □ □ □ ■ □ ■ □ ■ □ ■ □ □ □ □ □ ■ ■ □ □ □ □ ■ □ □ ■ ■ ■ □ □ □ ■ □ □



            InitializeMap(70, 40, new Point(0, 0), new Point(69, 39), false);
            this.map[6, 0] = false;
            this.map[8, 0] = false;
            this.map[10, 0] = false;
            this.map[17, 0] = false;
            this.map[18, 0] = false;
            this.map[20, 0] = false;
            this.map[23, 0] = false;
            this.map[24, 0] = false;
            this.map[30, 0] = false;
            this.map[33, 0] = false;
            this.map[34, 0] = false;
            this.map[36, 0] = false;
            this.map[40, 0] = false;
            this.map[42, 0] = false;
            this.map[44, 0] = false;
            this.map[46, 0] = false;
            this.map[52, 0] = false;
            this.map[53, 0] = false;
            this.map[58, 0] = false;
            this.map[61, 0] = false;
            this.map[62, 0] = false;
            this.map[63, 0] = false;
            this.map[67, 0] = false;
            this.map[0, 1] = false;
            this.map[2, 1] = false;
            this.map[8, 1] = false;
            this.map[9, 1] = false;
            this.map[12, 1] = false;
            this.map[13, 1] = false;
            this.map[14, 1] = false;
            this.map[15, 1] = false;
            this.map[16, 1] = false;
            this.map[17, 1] = false;
            this.map[23, 1] = false;
            this.map[36, 1] = false;
            this.map[37, 1] = false;
            this.map[41, 1] = false;
            this.map[43, 1] = false;
            this.map[45, 1] = false;
            this.map[47, 1] = false;
            this.map[49, 1] = false;
            this.map[51, 1] = false;
            this.map[52, 1] = false;
            this.map[56, 1] = false;
            this.map[58, 1] = false;
            this.map[63, 1] = false;
            this.map[66, 1] = false;
            this.map[3, 2] = false;
            this.map[5, 2] = false;
            this.map[7, 2] = false;
            this.map[8, 2] = false;
            this.map[10, 2] = false;
            this.map[14, 2] = false;
            this.map[15, 2] = false;
            this.map[18, 2] = false;
            this.map[22, 2] = false;
            this.map[24, 2] = false;
            this.map[29, 2] = false;
            this.map[30, 2] = false;
            this.map[32, 2] = false;
            this.map[33, 2] = false;
            this.map[34, 2] = false;
            this.map[37, 2] = false;
            this.map[39, 2] = false;
            this.map[47, 2] = false;
            this.map[48, 2] = false;
            this.map[51, 2] = false;
            this.map[52, 2] = false;
            this.map[54, 2] = false;
            this.map[57, 2] = false;
            this.map[59, 2] = false;
            this.map[61, 2] = false;
            this.map[63, 2] = false;
            this.map[65, 2] = false;
            this.map[4, 3] = false;
            this.map[11, 3] = false;
            this.map[13, 3] = false;
            this.map[21, 3] = false;
            this.map[22, 3] = false;
            this.map[26, 3] = false;
            this.map[30, 3] = false;
            this.map[31, 3] = false;
            this.map[34, 3] = false;
            this.map[41, 3] = false;
            this.map[42, 3] = false;
            this.map[44, 3] = false;
            this.map[51, 3] = false;
            this.map[55, 3] = false;
            this.map[57, 3] = false;
            this.map[63, 3] = false;
            this.map[0, 4] = false;
            this.map[1, 4] = false;
            this.map[4, 4] = false;
            this.map[9, 4] = false;
            this.map[12, 4] = false;
            this.map[13, 4] = false;
            this.map[18, 4] = false;
            this.map[19, 4] = false;
            this.map[20, 4] = false;
            this.map[24, 4] = false;
            this.map[25, 4] = false;
            this.map[26, 4] = false;
            this.map[27, 4] = false;
            this.map[28, 4] = false;
            this.map[36, 4] = false;
            this.map[37, 4] = false;
            this.map[40, 4] = false;
            this.map[41, 4] = false;
            this.map[44, 4] = false;
            this.map[50, 4] = false;
            this.map[56, 4] = false;
            this.map[57, 4] = false;
            this.map[59, 4] = false;
            this.map[62, 4] = false;
            this.map[64, 4] = false;
            this.map[65, 4] = false;
            this.map[66, 4] = false;
            this.map[67, 4] = false;
            this.map[69, 4] = false;
            this.map[1, 5] = false;
            this.map[2, 5] = false;
            this.map[5, 5] = false;
            this.map[9, 5] = false;
            this.map[15, 5] = false;
            this.map[18, 5] = false;
            this.map[25, 5] = false;
            this.map[28, 5] = false;
            this.map[31, 5] = false;
            this.map[32, 5] = false;
            this.map[35, 5] = false;
            this.map[36, 5] = false;
            this.map[37, 5] = false;
            this.map[43, 5] = false;
            this.map[48, 5] = false;
            this.map[49, 5] = false;
            this.map[50, 5] = false;
            this.map[51, 5] = false;
            this.map[52, 5] = false;
            this.map[60, 5] = false;
            this.map[62, 5] = false;
            this.map[69, 5] = false;
            this.map[2, 6] = false;
            this.map[5, 6] = false;
            this.map[6, 6] = false;
            this.map[9, 6] = false;
            this.map[11, 6] = false;
            this.map[13, 6] = false;
            this.map[15, 6] = false;
            this.map[16, 6] = false;
            this.map[18, 6] = false;
            this.map[23, 6] = false;
            this.map[24, 6] = false;
            this.map[26, 6] = false;
            this.map[27, 6] = false;
            this.map[28, 6] = false;
            this.map[29, 6] = false;
            this.map[33, 6] = false;
            this.map[34, 6] = false;
            this.map[36, 6] = false;
            this.map[37, 6] = false;
            this.map[44, 6] = false;
            this.map[45, 6] = false;
            this.map[47, 6] = false;
            this.map[50, 6] = false;
            this.map[55, 6] = false;
            this.map[59, 6] = false;
            this.map[61, 6] = false;
            this.map[62, 6] = false;
            this.map[65, 6] = false;
            this.map[66, 6] = false;
            this.map[68, 6] = false;
            this.map[69, 6] = false;
            this.map[0, 7] = false;
            this.map[4, 7] = false;
            this.map[7, 7] = false;
            this.map[9, 7] = false;
            this.map[11, 7] = false;
            this.map[12, 7] = false;
            this.map[21, 7] = false;
            this.map[34, 7] = false;
            this.map[35, 7] = false;
            this.map[37, 7] = false;
            this.map[47, 7] = false;
            this.map[48, 7] = false;
            this.map[50, 7] = false;
            this.map[51, 7] = false;
            this.map[53, 7] = false;
            this.map[57, 7] = false;
            this.map[62, 7] = false;
            this.map[63, 7] = false;
            this.map[64, 7] = false;
            this.map[66, 7] = false;
            this.map[69, 7] = false;
            this.map[2, 8] = false;
            this.map[4, 8] = false;
            this.map[5, 8] = false;
            this.map[6, 8] = false;
            this.map[8, 8] = false;
            this.map[9, 8] = false;
            this.map[11, 8] = false;
            this.map[13, 8] = false;
            this.map[16, 8] = false;
            this.map[19, 8] = false;
            this.map[24, 8] = false;
            this.map[25, 8] = false;
            this.map[26, 8] = false;
            this.map[28, 8] = false;
            this.map[29, 8] = false;
            this.map[30, 8] = false;
            this.map[31, 8] = false;
            this.map[34, 8] = false;
            this.map[36, 8] = false;
            this.map[38, 8] = false;
            this.map[42, 8] = false;
            this.map[44, 8] = false;
            this.map[45, 8] = false;
            this.map[46, 8] = false;
            this.map[47, 8] = false;
            this.map[53, 8] = false;
            this.map[55, 8] = false;
            this.map[59, 8] = false;
            this.map[61, 8] = false;
            this.map[63, 8] = false;
            this.map[1, 9] = false;
            this.map[5, 9] = false;
            this.map[6, 9] = false;
            this.map[10, 9] = false;
            this.map[11, 9] = false;
            this.map[16, 9] = false;
            this.map[17, 9] = false;
            this.map[19, 9] = false;
            this.map[22, 9] = false;
            this.map[25, 9] = false;
            this.map[26, 9] = false;
            this.map[27, 9] = false;
            this.map[28, 9] = false;
            this.map[31, 9] = false;
            this.map[35, 9] = false;
            this.map[39, 9] = false;
            this.map[42, 9] = false;
            this.map[43, 9] = false;
            this.map[44, 9] = false;
            this.map[47, 9] = false;
            this.map[50, 9] = false;
            this.map[51, 9] = false;
            this.map[55, 9] = false;
            this.map[56, 9] = false;
            this.map[60, 9] = false;
            this.map[61, 9] = false;
            this.map[64, 9] = false;
            this.map[68, 9] = false;
            this.map[69, 9] = false;
            this.map[0, 10] = false;
            this.map[3, 10] = false;
            this.map[6, 10] = false;
            this.map[7, 10] = false;
            this.map[9, 10] = false;
            this.map[11, 10] = false;
            this.map[13, 10] = false;
            this.map[15, 10] = false;
            this.map[17, 10] = false;
            this.map[18, 10] = false;
            this.map[19, 10] = false;
            this.map[20, 10] = false;
            this.map[23, 10] = false;
            this.map[29, 10] = false;
            this.map[30, 10] = false;
            this.map[31, 10] = false;
            this.map[33, 10] = false;
            this.map[35, 10] = false;
            this.map[37, 10] = false;
            this.map[39, 10] = false;
            this.map[41, 10] = false;
            this.map[42, 10] = false;
            this.map[45, 10] = false;
            this.map[46, 10] = false;
            this.map[47, 10] = false;
            this.map[50, 10] = false;
            this.map[55, 10] = false;
            this.map[56, 10] = false;
            this.map[58, 10] = false;
            this.map[62, 10] = false;
            this.map[66, 10] = false;
            this.map[2, 11] = false;
            this.map[3, 11] = false;
            this.map[5, 11] = false;
            this.map[7, 11] = false;
            this.map[8, 11] = false;
            this.map[14, 11] = false;
            this.map[17, 11] = false;
            this.map[19, 11] = false;
            this.map[23, 11] = false;
            this.map[25, 11] = false;
            this.map[30, 11] = false;
            this.map[33, 11] = false;
            this.map[34, 11] = false;
            this.map[36, 11] = false;
            this.map[38, 11] = false;
            this.map[41, 11] = false;
            this.map[50, 11] = false;
            this.map[52, 11] = false;
            this.map[59, 11] = false;
            this.map[60, 11] = false;
            this.map[61, 11] = false;
            this.map[62, 11] = false;
            this.map[66, 11] = false;
            this.map[2, 12] = false;
            this.map[11, 12] = false;
            this.map[12, 12] = false;
            this.map[15, 12] = false;
            this.map[17, 12] = false;
            this.map[21, 12] = false;
            this.map[24, 12] = false;
            this.map[25, 12] = false;
            this.map[26, 12] = false;
            this.map[35, 12] = false;
            this.map[37, 12] = false;
            this.map[40, 12] = false;
            this.map[41, 12] = false;
            this.map[42, 12] = false;
            this.map[43, 12] = false;
            this.map[45, 12] = false;
            this.map[46, 12] = false;
            this.map[47, 12] = false;
            this.map[48, 12] = false;
            this.map[50, 12] = false;
            this.map[51, 12] = false;
            this.map[55, 12] = false;
            this.map[56, 12] = false;
            this.map[57, 12] = false;
            this.map[58, 12] = false;
            this.map[63, 12] = false;
            this.map[64, 12] = false;
            this.map[66, 12] = false;
            this.map[67, 12] = false;
            this.map[68, 12] = false;
            this.map[0, 13] = false;
            this.map[1, 13] = false;
            this.map[2, 13] = false;
            this.map[4, 13] = false;
            this.map[6, 13] = false;
            this.map[8, 13] = false;
            this.map[9, 13] = false;
            this.map[10, 13] = false;
            this.map[13, 13] = false;
            this.map[15, 13] = false;
            this.map[16, 13] = false;
            this.map[17, 13] = false;
            this.map[18, 13] = false;
            this.map[22, 13] = false;
            this.map[23, 13] = false;
            this.map[24, 13] = false;
            this.map[26, 13] = false;
            this.map[27, 13] = false;
            this.map[28, 13] = false;
            this.map[44, 13] = false;
            this.map[46, 13] = false;
            this.map[48, 13] = false;
            this.map[51, 13] = false;
            this.map[52, 13] = false;
            this.map[55, 13] = false;
            this.map[56, 13] = false;
            this.map[57, 13] = false;
            this.map[65, 13] = false;
            this.map[66, 13] = false;
            this.map[0, 14] = false;
            this.map[1, 14] = false;
            this.map[2, 14] = false;
            this.map[5, 14] = false;
            this.map[7, 14] = false;
            this.map[8, 14] = false;
            this.map[9, 14] = false;
            this.map[10, 14] = false;
            this.map[13, 14] = false;
            this.map[14, 14] = false;
            this.map[15, 14] = false;
            this.map[18, 14] = false;
            this.map[19, 14] = false;
            this.map[25, 14] = false;
            this.map[27, 14] = false;
            this.map[28, 14] = false;
            this.map[32, 14] = false;
            this.map[33, 14] = false;
            this.map[35, 14] = false;
            this.map[38, 14] = false;
            this.map[39, 14] = false;
            this.map[41, 14] = false;
            this.map[42, 14] = false;
            this.map[43, 14] = false;
            this.map[45, 14] = false;
            this.map[48, 14] = false;
            this.map[51, 14] = false;
            this.map[59, 14] = false;
            this.map[60, 14] = false;
            this.map[63, 14] = false;
            this.map[64, 14] = false;
            this.map[68, 14] = false;
            this.map[0, 15] = false;
            this.map[1, 15] = false;
            this.map[3, 15] = false;
            this.map[5, 15] = false;
            this.map[7, 15] = false;
            this.map[8, 15] = false;
            this.map[10, 15] = false;
            this.map[14, 15] = false;
            this.map[16, 15] = false;
            this.map[19, 15] = false;
            this.map[21, 15] = false;
            this.map[22, 15] = false;
            this.map[23, 15] = false;
            this.map[26, 15] = false;
            this.map[28, 15] = false;
            this.map[31, 15] = false;
            this.map[32, 15] = false;
            this.map[34, 15] = false;
            this.map[37, 15] = false;
            this.map[39, 15] = false;
            this.map[47, 15] = false;
            this.map[49, 15] = false;
            this.map[51, 15] = false;
            this.map[53, 15] = false;
            this.map[56, 15] = false;
            this.map[58, 15] = false;
            this.map[60, 15] = false;
            this.map[62, 15] = false;
            this.map[64, 15] = false;
            this.map[65, 15] = false;
            this.map[66, 15] = false;
            this.map[7, 16] = false;
            this.map[8, 16] = false;
            this.map[10, 16] = false;
            this.map[11, 16] = false;
            this.map[12, 16] = false;
            this.map[16, 16] = false;
            this.map[17, 16] = false;
            this.map[19, 16] = false;
            this.map[22, 16] = false;
            this.map[23, 16] = false;
            this.map[26, 16] = false;
            this.map[27, 16] = false;
            this.map[31, 16] = false;
            this.map[33, 16] = false;
            this.map[35, 16] = false;
            this.map[37, 16] = false;
            this.map[40, 16] = false;
            this.map[45, 16] = false;
            this.map[51, 16] = false;
            this.map[54, 16] = false;
            this.map[63, 16] = false;
            this.map[64, 16] = false;
            this.map[65, 16] = false;
            this.map[67, 16] = false;
            this.map[0, 17] = false;
            this.map[1, 17] = false;
            this.map[3, 17] = false;
            this.map[4, 17] = false;
            this.map[6, 17] = false;
            this.map[10, 17] = false;
            this.map[19, 17] = false;
            this.map[20, 17] = false;
            this.map[25, 17] = false;
            this.map[27, 17] = false;
            this.map[28, 17] = false;
            this.map[29, 17] = false;
            this.map[37, 17] = false;
            this.map[41, 17] = false;
            this.map[45, 17] = false;
            this.map[46, 17] = false;
            this.map[47, 17] = false;
            this.map[52, 17] = false;
            this.map[55, 17] = false;
            this.map[61, 17] = false;
            this.map[62, 17] = false;
            this.map[63, 17] = false;
            this.map[64, 17] = false;
            this.map[67, 17] = false;
            this.map[68, 17] = false;
            this.map[69, 17] = false;
            this.map[0, 18] = false;
            this.map[1, 18] = false;
            this.map[2, 18] = false;
            this.map[3, 18] = false;
            this.map[5, 18] = false;
            this.map[8, 18] = false;
            this.map[16, 18] = false;
            this.map[17, 18] = false;
            this.map[19, 18] = false;
            this.map[20, 18] = false;
            this.map[31, 18] = false;
            this.map[32, 18] = false;
            this.map[33, 18] = false;
            this.map[34, 18] = false;
            this.map[35, 18] = false;
            this.map[37, 18] = false;
            this.map[40, 18] = false;
            this.map[42, 18] = false;
            this.map[45, 18] = false;
            this.map[46, 18] = false;
            this.map[47, 18] = false;
            this.map[51, 18] = false;
            this.map[52, 18] = false;
            this.map[55, 18] = false;
            this.map[58, 18] = false;
            this.map[59, 18] = false;
            this.map[60, 18] = false;
            this.map[61, 18] = false;
            this.map[62, 18] = false;
            this.map[64, 18] = false;
            this.map[67, 18] = false;
            this.map[0, 19] = false;
            this.map[3, 19] = false;
            this.map[4, 19] = false;
            this.map[5, 19] = false;
            this.map[7, 19] = false;
            this.map[10, 19] = false;
            this.map[12, 19] = false;
            this.map[15, 19] = false;
            this.map[17, 19] = false;
            this.map[23, 19] = false;
            this.map[24, 19] = false;
            this.map[25, 19] = false;
            this.map[29, 19] = false;
            this.map[33, 19] = false;
            this.map[34, 19] = false;
            this.map[37, 19] = false;
            this.map[44, 19] = false;
            this.map[47, 19] = false;
            this.map[48, 19] = false;
            this.map[51, 19] = false;
            this.map[57, 19] = false;
            this.map[59, 19] = false;
            this.map[64, 19] = false;
            this.map[65, 19] = false;
            this.map[67, 19] = false;
            this.map[7, 20] = false;
            this.map[8, 20] = false;
            this.map[10, 20] = false;
            this.map[14, 20] = false;
            this.map[15, 20] = false;
            this.map[16, 20] = false;
            this.map[21, 20] = false;
            this.map[22, 20] = false;
            this.map[24, 20] = false;
            this.map[25, 20] = false;
            this.map[26, 20] = false;
            this.map[28, 20] = false;
            this.map[29, 20] = false;
            this.map[30, 20] = false;
            this.map[33, 20] = false;
            this.map[34, 20] = false;
            this.map[35, 20] = false;
            this.map[37, 20] = false;
            this.map[39, 20] = false;
            this.map[42, 20] = false;
            this.map[45, 20] = false;
            this.map[49, 20] = false;
            this.map[50, 20] = false;
            this.map[54, 20] = false;
            this.map[55, 20] = false;
            this.map[56, 20] = false;
            this.map[57, 20] = false;
            this.map[60, 20] = false;
            this.map[61, 20] = false;
            this.map[62, 20] = false;
            this.map[64, 20] = false;
            this.map[6, 21] = false;
            this.map[9, 21] = false;
            this.map[13, 21] = false;
            this.map[14, 21] = false;
            this.map[15, 21] = false;
            this.map[20, 21] = false;
            this.map[23, 21] = false;
            this.map[27, 21] = false;
            this.map[28, 21] = false;
            this.map[29, 21] = false;
            this.map[30, 21] = false;
            this.map[31, 21] = false;
            this.map[34, 21] = false;
            this.map[35, 21] = false;
            this.map[36, 21] = false;
            this.map[37, 21] = false;
            this.map[38, 21] = false;
            this.map[39, 21] = false;
            this.map[40, 21] = false;
            this.map[42, 21] = false;
            this.map[43, 21] = false;
            this.map[44, 21] = false;
            this.map[46, 21] = false;
            this.map[49, 21] = false;
            this.map[51, 21] = false;
            this.map[55, 21] = false;
            this.map[59, 21] = false;
            this.map[65, 21] = false;
            this.map[67, 21] = false;
            this.map[0, 22] = false;
            this.map[5, 22] = false;
            this.map[6, 22] = false;
            this.map[8, 22] = false;
            this.map[10, 22] = false;
            this.map[15, 22] = false;
            this.map[19, 22] = false;
            this.map[20, 22] = false;
            this.map[25, 22] = false;
            this.map[26, 22] = false;
            this.map[30, 22] = false;
            this.map[31, 22] = false;
            this.map[34, 22] = false;
            this.map[35, 22] = false;
            this.map[37, 22] = false;
            this.map[38, 22] = false;
            this.map[40, 22] = false;
            this.map[41, 22] = false;
            this.map[42, 22] = false;
            this.map[44, 22] = false;
            this.map[46, 22] = false;
            this.map[59, 22] = false;
            this.map[60, 22] = false;
            this.map[61, 22] = false;
            this.map[62, 22] = false;
            this.map[65, 22] = false;
            this.map[66, 22] = false;
            this.map[69, 22] = false;
            this.map[4, 23] = false;
            this.map[9, 23] = false;
            this.map[10, 23] = false;
            this.map[12, 23] = false;
            this.map[13, 23] = false;
            this.map[14, 23] = false;
            this.map[17, 23] = false;
            this.map[18, 23] = false;
            this.map[24, 23] = false;
            this.map[26, 23] = false;
            this.map[28, 23] = false;
            this.map[31, 23] = false;
            this.map[35, 23] = false;
            this.map[37, 23] = false;
            this.map[40, 23] = false;
            this.map[42, 23] = false;
            this.map[43, 23] = false;
            this.map[44, 23] = false;
            this.map[46, 23] = false;
            this.map[47, 23] = false;
            this.map[49, 23] = false;
            this.map[51, 23] = false;
            this.map[55, 23] = false;
            this.map[58, 23] = false;
            this.map[59, 23] = false;
            this.map[2, 24] = false;
            this.map[11, 24] = false;
            this.map[12, 24] = false;
            this.map[13, 24] = false;
            this.map[14, 24] = false;
            this.map[17, 24] = false;
            this.map[19, 24] = false;
            this.map[21, 24] = false;
            this.map[25, 24] = false;
            this.map[26, 24] = false;
            this.map[27, 24] = false;
            this.map[28, 24] = false;
            this.map[29, 24] = false;
            this.map[30, 24] = false;
            this.map[35, 24] = false;
            this.map[37, 24] = false;
            this.map[38, 24] = false;
            this.map[39, 24] = false;
            this.map[41, 24] = false;
            this.map[44, 24] = false;
            this.map[47, 24] = false;
            this.map[49, 24] = false;
            this.map[51, 24] = false;
            this.map[53, 24] = false;
            this.map[55, 24] = false;
            this.map[62, 24] = false;
            this.map[64, 24] = false;
            this.map[65, 24] = false;
            this.map[69, 24] = false;
            this.map[1, 25] = false;
            this.map[5, 25] = false;
            this.map[6, 25] = false;
            this.map[7, 25] = false;
            this.map[13, 25] = false;
            this.map[16, 25] = false;
            this.map[17, 25] = false;
            this.map[18, 25] = false;
            this.map[19, 25] = false;
            this.map[20, 25] = false;
            this.map[21, 25] = false;
            this.map[25, 25] = false;
            this.map[27, 25] = false;
            this.map[28, 25] = false;
            this.map[29, 25] = false;
            this.map[31, 25] = false;
            this.map[33, 25] = false;
            this.map[35, 25] = false;
            this.map[39, 25] = false;
            this.map[40, 25] = false;
            this.map[43, 25] = false;
            this.map[44, 25] = false;
            this.map[47, 25] = false;
            this.map[50, 25] = false;
            this.map[54, 25] = false;
            this.map[56, 25] = false;
            this.map[58, 25] = false;
            this.map[60, 25] = false;
            this.map[61, 25] = false;
            this.map[65, 25] = false;
            this.map[69, 25] = false;
            this.map[1, 26] = false;
            this.map[4, 26] = false;
            this.map[6, 26] = false;
            this.map[8, 26] = false;
            this.map[9, 26] = false;
            this.map[12, 26] = false;
            this.map[15, 26] = false;
            this.map[18, 26] = false;
            this.map[19, 26] = false;
            this.map[23, 26] = false;
            this.map[25, 26] = false;
            this.map[28, 26] = false;
            this.map[34, 26] = false;
            this.map[35, 26] = false;
            this.map[36, 26] = false;
            this.map[40, 26] = false;
            this.map[42, 26] = false;
            this.map[53, 26] = false;
            this.map[56, 26] = false;
            this.map[57, 26] = false;
            this.map[59, 26] = false;
            this.map[62, 26] = false;
            this.map[63, 26] = false;
            this.map[64, 26] = false;
            this.map[66, 26] = false;
            this.map[4, 27] = false;
            this.map[5, 27] = false;
            this.map[8, 27] = false;
            this.map[10, 27] = false;
            this.map[14, 27] = false;
            this.map[15, 27] = false;
            this.map[16, 27] = false;
            this.map[17, 27] = false;
            this.map[18, 27] = false;
            this.map[21, 27] = false;
            this.map[25, 27] = false;
            this.map[29, 27] = false;
            this.map[30, 27] = false;
            this.map[37, 27] = false;
            this.map[38, 27] = false;
            this.map[39, 27] = false;
            this.map[43, 27] = false;
            this.map[48, 27] = false;
            this.map[49, 27] = false;
            this.map[50, 27] = false;
            this.map[54, 27] = false;
            this.map[57, 27] = false;
            this.map[58, 27] = false;
            this.map[62, 27] = false;
            this.map[63, 27] = false;
            this.map[64, 27] = false;
            this.map[69, 27] = false;
            this.map[0, 28] = false;
            this.map[1, 28] = false;
            this.map[5, 28] = false;
            this.map[6, 28] = false;
            this.map[8, 28] = false;
            this.map[10, 28] = false;
            this.map[14, 28] = false;
            this.map[15, 28] = false;
            this.map[16, 28] = false;
            this.map[19, 28] = false;
            this.map[20, 28] = false;
            this.map[24, 28] = false;
            this.map[28, 28] = false;
            this.map[29, 28] = false;
            this.map[31, 28] = false;
            this.map[32, 28] = false;
            this.map[35, 28] = false;
            this.map[40, 28] = false;
            this.map[41, 28] = false;
            this.map[42, 28] = false;
            this.map[43, 28] = false;
            this.map[45, 28] = false;
            this.map[46, 28] = false;
            this.map[47, 28] = false;
            this.map[50, 28] = false;
            this.map[52, 28] = false;
            this.map[53, 28] = false;
            this.map[54, 28] = false;
            this.map[56, 28] = false;
            this.map[57, 28] = false;
            this.map[65, 28] = false;
            this.map[69, 28] = false;
            this.map[0, 29] = false;
            this.map[8, 29] = false;
            this.map[9, 29] = false;
            this.map[11, 29] = false;
            this.map[13, 29] = false;
            this.map[14, 29] = false;
            this.map[19, 29] = false;
            this.map[22, 29] = false;
            this.map[29, 29] = false;
            this.map[30, 29] = false;
            this.map[31, 29] = false;
            this.map[32, 29] = false;
            this.map[38, 29] = false;
            this.map[39, 29] = false;
            this.map[40, 29] = false;
            this.map[42, 29] = false;
            this.map[43, 29] = false;
            this.map[44, 29] = false;
            this.map[45, 29] = false;
            this.map[49, 29] = false;
            this.map[52, 29] = false;
            this.map[54, 29] = false;
            this.map[57, 29] = false;
            this.map[58, 29] = false;
            this.map[59, 29] = false;
            this.map[60, 29] = false;
            this.map[61, 29] = false;
            this.map[62, 29] = false;
            this.map[64, 29] = false;
            this.map[65, 29] = false;
            this.map[1, 30] = false;
            this.map[4, 30] = false;
            this.map[5, 30] = false;
            this.map[9, 30] = false;
            this.map[10, 30] = false;
            this.map[11, 30] = false;
            this.map[13, 30] = false;
            this.map[15, 30] = false;
            this.map[16, 30] = false;
            this.map[20, 30] = false;
            this.map[22, 30] = false;
            this.map[23, 30] = false;
            this.map[25, 30] = false;
            this.map[27, 30] = false;
            this.map[28, 30] = false;
            this.map[29, 30] = false;
            this.map[33, 30] = false;
            this.map[34, 30] = false;
            this.map[35, 30] = false;
            this.map[39, 30] = false;
            this.map[40, 30] = false;
            this.map[43, 30] = false;
            this.map[45, 30] = false;
            this.map[46, 30] = false;
            this.map[48, 30] = false;
            this.map[50, 30] = false;
            this.map[51, 30] = false;
            this.map[52, 30] = false;
            this.map[54, 30] = false;
            this.map[56, 30] = false;
            this.map[57, 30] = false;
            this.map[59, 30] = false;
            this.map[61, 30] = false;
            this.map[66, 30] = false;
            this.map[1, 31] = false;
            this.map[8, 31] = false;
            this.map[12, 31] = false;
            this.map[13, 31] = false;
            this.map[15, 31] = false;
            this.map[17, 31] = false;
            this.map[21, 31] = false;
            this.map[22, 31] = false;
            this.map[27, 31] = false;
            this.map[30, 31] = false;
            this.map[38, 31] = false;
            this.map[41, 31] = false;
            this.map[46, 31] = false;
            this.map[47, 31] = false;
            this.map[51, 31] = false;
            this.map[53, 31] = false;
            this.map[57, 31] = false;
            this.map[58, 31] = false;
            this.map[62, 31] = false;
            this.map[66, 31] = false;
            this.map[69, 31] = false;
            this.map[1, 32] = false;
            this.map[3, 32] = false;
            this.map[4, 32] = false;
            this.map[6, 32] = false;
            this.map[8, 32] = false;
            this.map[13, 32] = false;
            this.map[16, 32] = false;
            this.map[19, 32] = false;
            this.map[21, 32] = false;
            this.map[24, 32] = false;
            this.map[26, 32] = false;
            this.map[27, 32] = false;
            this.map[28, 32] = false;
            this.map[31, 32] = false;
            this.map[36, 32] = false;
            this.map[39, 32] = false;
            this.map[40, 32] = false;
            this.map[42, 32] = false;
            this.map[45, 32] = false;
            this.map[46, 32] = false;
            this.map[47, 32] = false;
            this.map[48, 32] = false;
            this.map[51, 32] = false;
            this.map[53, 32] = false;
            this.map[54, 32] = false;
            this.map[57, 32] = false;
            this.map[61, 32] = false;
            this.map[62, 32] = false;
            this.map[64, 32] = false;
            this.map[67, 32] = false;
            this.map[69, 32] = false;
            this.map[2, 33] = false;
            this.map[4, 33] = false;
            this.map[5, 33] = false;
            this.map[6, 33] = false;
            this.map[7, 33] = false;
            this.map[9, 33] = false;
            this.map[10, 33] = false;
            this.map[11, 33] = false;
            this.map[14, 33] = false;
            this.map[18, 33] = false;
            this.map[19, 33] = false;
            this.map[23, 33] = false;
            this.map[26, 33] = false;
            this.map[28, 33] = false;
            this.map[31, 33] = false;
            this.map[33, 33] = false;
            this.map[34, 33] = false;
            this.map[36, 33] = false;
            this.map[37, 33] = false;
            this.map[43, 33] = false;
            this.map[44, 33] = false;
            this.map[48, 33] = false;
            this.map[50, 33] = false;
            this.map[53, 33] = false;
            this.map[54, 33] = false;
            this.map[58, 33] = false;
            this.map[59, 33] = false;
            this.map[65, 33] = false;
            this.map[68, 33] = false;
            this.map[0, 34] = false;
            this.map[1, 34] = false;
            this.map[3, 34] = false;
            this.map[5, 34] = false;
            this.map[9, 34] = false;
            this.map[10, 34] = false;
            this.map[12, 34] = false;
            this.map[17, 34] = false;
            this.map[18, 34] = false;
            this.map[21, 34] = false;
            this.map[26, 34] = false;
            this.map[29, 34] = false;
            this.map[30, 34] = false;
            this.map[32, 34] = false;
            this.map[34, 34] = false;
            this.map[40, 34] = false;
            this.map[42, 34] = false;
            this.map[43, 34] = false;
            this.map[44, 34] = false;
            this.map[45, 34] = false;
            this.map[48, 34] = false;
            this.map[49, 34] = false;
            this.map[51, 34] = false;
            this.map[55, 34] = false;
            this.map[58, 34] = false;
            this.map[60, 34] = false;
            this.map[62, 34] = false;
            this.map[64, 34] = false;
            this.map[65, 34] = false;
            this.map[67, 34] = false;
            this.map[1, 35] = false;
            this.map[5, 35] = false;
            this.map[11, 35] = false;
            this.map[12, 35] = false;
            this.map[13, 35] = false;
            this.map[18, 35] = false;
            this.map[19, 35] = false;
            this.map[20, 35] = false;
            this.map[21, 35] = false;
            this.map[22, 35] = false;
            this.map[25, 35] = false;
            this.map[28, 35] = false;
            this.map[31, 35] = false;
            this.map[32, 35] = false;
            this.map[33, 35] = false;
            this.map[34, 35] = false;
            this.map[37, 35] = false;
            this.map[38, 35] = false;
            this.map[40, 35] = false;
            this.map[41, 35] = false;
            this.map[42, 35] = false;
            this.map[44, 35] = false;
            this.map[49, 35] = false;
            this.map[52, 35] = false;
            this.map[53, 35] = false;
            this.map[59, 35] = false;
            this.map[61, 35] = false;
            this.map[62, 35] = false;
            this.map[64, 35] = false;
            this.map[65, 35] = false;
            this.map[67, 35] = false;
            this.map[69, 35] = false;
            this.map[0, 36] = false;
            this.map[2, 36] = false;
            this.map[4, 36] = false;
            this.map[5, 36] = false;
            this.map[6, 36] = false;
            this.map[12, 36] = false;
            this.map[13, 36] = false;
            this.map[14, 36] = false;
            this.map[17, 36] = false;
            this.map[22, 36] = false;
            this.map[29, 36] = false;
            this.map[35, 36] = false;
            this.map[41, 36] = false;
            this.map[42, 36] = false;
            this.map[43, 36] = false;
            this.map[45, 36] = false;
            this.map[46, 36] = false;
            this.map[47, 36] = false;
            this.map[49, 36] = false;
            this.map[53, 36] = false;
            this.map[57, 36] = false;
            this.map[60, 36] = false;
            this.map[62, 36] = false;
            this.map[68, 36] = false;
            this.map[0, 37] = false;
            this.map[2, 37] = false;
            this.map[4, 37] = false;
            this.map[5, 37] = false;
            this.map[6, 37] = false;
            this.map[8, 37] = false;
            this.map[12, 37] = false;
            this.map[13, 37] = false;
            this.map[19, 37] = false;
            this.map[21, 37] = false;
            this.map[24, 37] = false;
            this.map[26, 37] = false;
            this.map[40, 37] = false;
            this.map[42, 37] = false;
            this.map[50, 37] = false;
            this.map[52, 37] = false;
            this.map[53, 37] = false;
            this.map[54, 37] = false;
            this.map[62, 37] = false;
            this.map[63, 37] = false;
            this.map[65, 37] = false;
            this.map[66, 37] = false;
            this.map[68, 37] = false;
            this.map[0, 38] = false;
            this.map[1, 38] = false;
            this.map[3, 38] = false;
            this.map[5, 38] = false;
            this.map[6, 38] = false;
            this.map[10, 38] = false;
            this.map[11, 38] = false;
            this.map[13, 38] = false;
            this.map[19, 38] = false;
            this.map[27, 38] = false;
            this.map[31, 38] = false;
            this.map[33, 38] = false;
            this.map[37, 38] = false;
            this.map[44, 38] = false;
            this.map[47, 38] = false;
            this.map[48, 38] = false;
            this.map[52, 38] = false;
            this.map[53, 38] = false;
            this.map[54, 38] = false;
            this.map[58, 38] = false;
            this.map[61, 38] = false;
            this.map[65, 38] = false;
            this.map[69, 38] = false;
            this.map[1, 39] = false;
            this.map[5, 39] = false;
            this.map[6, 39] = false;
            this.map[8, 39] = false;
            this.map[9, 39] = false;
            this.map[10, 39] = false;
            this.map[11, 39] = false;
            this.map[15, 39] = false;
            this.map[17, 39] = false;
            this.map[21, 39] = false;
            this.map[22, 39] = false;
            this.map[25, 39] = false;
            this.map[30, 39] = false;
            this.map[34, 39] = false;
            this.map[36, 39] = false;
            this.map[38, 39] = false;
            this.map[40, 39] = false;
            this.map[41, 39] = false;
            this.map[42, 39] = false;
            this.map[43, 39] = false;
            this.map[44, 39] = false;
            this.map[46, 39] = false;
            this.map[47, 39] = false;
            this.map[48, 39] = false;
            this.map[51, 39] = false;
            this.map[52, 39] = false;
            this.map[53, 39] = false;
            this.map[58, 39] = false;
            this.map[64, 39] = false;
            this.map[65, 39] = false;

        }
        #endregion

        #endregion

    }
}
