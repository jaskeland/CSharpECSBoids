using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Boids.Simulation.Systems.SpatialPartitioning.KdTree;
using Xunit;

namespace Boids.Simulation.Facts
{
    public class GenericKdTreeFacts
    {
        [Fact]
        public void CanBuildTree()
        {
            var points = new List<KdVector2>
            {
                { new KdVector2(10, 10) },
                { new KdVector2(25,10) },
                { new KdVector2(75,10) },
                { new KdVector2(25,75) },
            };
            var tree = new KdTree<KdVector2>(points, 2);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void Nearest_neighbour_returns_correct_value_for_2d_vectors(int numPoints)
        {
            var rand = new Random();
            int randInt() => rand.Next(0, 1000);
            KdVector2 randVector2() => new KdVector2(randInt(), randInt());
            var points = Enumerable.Range(0, numPoints).Select(_ => randVector2()).ToList();

            var tree = new KdTree<KdVector2>(points, 2);

            var searchPoint = new KdVector2(250, 750);
            var neigbhour = tree.NearestNeighbour(searchPoint);
            var bruteForcedNeighbour = points.OrderBy(vec => vec.Distance(searchPoint)).First();

            Assert.Equal(bruteForcedNeighbour.Value, neigbhour.Value);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void Nearest_neighbour_returns_correct_value_for_3d_vectors(int numPoints)
        {
            var rand = new Random();
            int randInt() => rand.Next(0, 1000);
            KdVector3 randVector2() => new KdVector3(randInt(), randInt(), randInt());
            var points = Enumerable.Range(0, numPoints).Select(_ => randVector2()).ToList();

            var tree = new KdTree<KdVector3>(points, 3);

            var searchPoint = new KdVector3(250, 750, 0);
            var neigbhour = tree.NearestNeighbour(searchPoint);
            var bruteForcedNeighbour = points.OrderBy(vec => vec.Distance(searchPoint)).First();

            Assert.Equal(bruteForcedNeighbour.Value, neigbhour.Value);
        }

        [Fact]
        public void Forced_to_explore_both_children_of_root()
        {
            // Drawing these out illustrates a diffcult case where we cannot simply discard half of the tree
            var vectors = new[]
            {
                new Vector2(51, 75),
                new Vector2(25, 40),
                new Vector2(70, 70),
                new Vector2(10, 30),
                new Vector2(35, 90),
                new Vector2(55, 1),
                new Vector2(60, 80),
                new Vector2(1, 10),
                new Vector2(50, 50),
            };

            // The space is first split on x = 51, and x = 52 is right of that. But the nearest neighbour has x = 50 and lies in the left subtree.
            var target = new Vector2(52, 52);
            // 50, 2 is also a difficult spot. These points force us to explore in both the left and right side of the root
            var otherTarget = new Vector2(50, 2);

            var points = vectors.Select(v => new KdVector2(v.X, v.Y));
            var tree = new KdTree<KdVector2>(points, 2);

            var nearest = tree.NearestNeighbour(new KdVector2(target.X, target.Y));
            var bruteForcedNeighbour = vectors.OrderBy(v => Vector2.Distance(v, target)).First();

            var nearest2 = tree.NearestNeighbour(new KdVector2(otherTarget.X, otherTarget.Y));
            var bruteForcedNeighbour2 = vectors.OrderBy(v => Vector2.Distance(v, otherTarget)).First();

            Assert.Equal(bruteForcedNeighbour, nearest.Value);
            Assert.Equal(bruteForcedNeighbour2, nearest2.Value);
        }

        [Fact]
        public void Gets_correct_size()
        {
            var points = new List<KdVector2>
            {
                { new KdVector2(10, 10) },
                { new KdVector2(25,10) },
                { new KdVector2(75,10) },
                { new KdVector2(25,75) },
            };
            var tree = new KdTree<KdVector2>(points, 2);

            var size = tree.Size();
            
            Assert.Equal(1, size.X);
            Assert.Equal(3, size.Y);
        }

        [Fact]
        public void Gets_correct_size_for_wide_tree()
        {
            var vectors = new[]
            {
                new Vector2(51, 75),
                new Vector2(25, 40),
                new Vector2(70, 70),
                new Vector2(10, 30),
                new Vector2(35, 90),
                new Vector2(55, 1),
                new Vector2(60, 80),
                new Vector2(1, 10),
                new Vector2(50, 50),
            };

            var points = vectors.Select(v => new KdVector2(v.X, v.Y));
            var tree = new KdTree<KdVector2>(points, 2);

            var size = tree.Size();

            Assert.Equal(4, size.X);
            Assert.Equal(3, size.Y);
        }

        [Fact]
        public void Gets_with_tree_position()
        {
            var vectors = new[]
            {
                new Vector2(51, 75),
                new Vector2(25, 40),
                new Vector2(70, 70),
                new Vector2(10, 30),
                new Vector2(35, 90),
                new Vector2(55, 1),
                new Vector2(60, 80),
                new Vector2(1, 10),
                new Vector2(50, 50),
            };

            var points = vectors.Select(v => new KdVector2(v.X, v.Y)).ToList();
            var tree = new KdTree<KdVector2>(points, 2);

            var elementsWithTreePosition = tree.GetAllWithPositionInTree();

            Assert.NotEmpty(elementsWithTreePosition);
            Assert.Equal(Vector2.Zero, elementsWithTreePosition[points[0]]);

            var positionOfSixthElement = elementsWithTreePosition[points[5]];
            Assert.Equal(2, positionOfSixthElement.X);
            Assert.Equal(2, positionOfSixthElement.Y);
        }

        private class KdVector2 : IKdTreeSortable<KdVector2>
        {
            public Vector2 Value { get; }

            public KdVector2(float x, float y)
            {
                Value = new Vector2(x, y);
            }

            public bool IsLeftOf(KdVector2 other, uint dimension)
            {
                return dimension == 0
                    ? Value.X < other.Value.X
                    : Value.Y < other.Value.Y;
            }

            public float Distance(KdVector2 other)
            {
                return Vector2.Distance(Value, other.Value);
            }
        }

        private class KdVector3 : IKdTreeSortable<KdVector3>
        {
            public Vector3 Value { get; }

            public KdVector3(float x, float y, float z)
            {
                Value = new Vector3(x, y, z);
            }

            public bool IsLeftOf(KdVector3 other, uint dimension)
            {
                return dimension switch
                {
                    0 => Value.X < other.Value.X,
                    1 => Value.Y < other.Value.Y,
                    2 => Value.Z < other.Value.Z,
                    _ => throw new Exception($"This is a 3d vector, no fourth dimensions allowed.")
                };
            }

            public float Distance(KdVector3 other)
            {
                return Vector3.Distance(Value, other.Value);
            }
        }
    }
}