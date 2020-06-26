using System;
using System.Collections.Generic;
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

        [Fact]
        public void Nearest_neighbour_returns_correct_value_for_2d_vectors()
        {
            var points = new List<KdVector2>
            {
                { new KdVector2(10, 10) },
                { new KdVector2(25,10) },
                { new KdVector2(75,10) },
                { new KdVector2(25,75) },
            };
            var tree = new KdTree<KdVector2>(points, 2);

            var searchPoint = new KdVector2(15, 15);
            var neigbhour = tree.NearestNeighbour(searchPoint);

            Assert.Equal(neigbhour.Value, points[0].Value);
        }

        [Fact]
        public void Nearest_neighbour_returns_correct_value_for_3d_vectors()
        {
            var points = new List<KdVector3>
            {
                { new KdVector3(10, 10, 10) },
                { new KdVector3(25,10, 25) },
                { new KdVector3(75,10, 75) },
                { new KdVector3(25,75, 25) },
            };
            var tree = new KdTree<KdVector3>(points, 3);

            var searchPoint = new KdVector3(15, 15, 15);
            var neigbhour = tree.NearestNeighbour(searchPoint);

            Assert.Equal(neigbhour.Value, points[0].Value);
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
                    1 => Value.X < other.Value.X,
                    2 => Value.X < other.Value.X,
                    _ => throw new Exception("This is a 3d vector, no fourth dimensions allowed.")
                };
            }

            public float Distance(KdVector3 other)
            {
                return Vector3.Distance(Value, other.Value);
            }
        }
    }
}
