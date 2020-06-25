using System.Collections.Generic;
using System.Numerics;
using Xunit;
using Boids.Simulation.Systems.Quadtree;
using System;

namespace Boids.Simulation.Facts.QuadtreeFacts
{
    public class ConstructionFacts
    {
        [Fact]
        public void CanCreateQuadTree()
        {
            var random = new Random();
            var points = new List<Vector2>();
            for (int i = 0; i < 10; i++)
            {
                points.Add(new Vector2(random.Next(1, 19), random.Next(1, 19)));
            }

            var tree = new Quadtree(points, new Vector2(), new Vector2(20, 20));
            tree.Clear();
        }
    }
}