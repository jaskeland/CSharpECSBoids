using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Boids.Simulation.Systems.SpatialPartitioning.Quadtree;
using Xunit;

namespace Boids.Simulation.Facts.QuadtreeFacts
{
    public class QuadtreeBucketFacts
    {
        [Fact]
        public void Test()
        {
            var topLeft = new Vector2(0, 0);
            var bottomRight = new Vector2(100, 100);
            var points = new List<Vector2>
            {
                { new Vector2(10, 10) },
                { new Vector2(25,10) },
                { new Vector2(75,10) },
                { new Vector2(25,75) },
            };

            var bucket = new QuadtreeBucket(points, topLeft, bottomRight);
            bucket.Partition();
        }
    }
}