using Boids.Simulation.Archetypes;
using Boids.Simulation.Components;
using Boids.Simulation.Systems;
using Boids.Simulation.Systems.SpatialPartitioning.KdTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace Boids.Simulation.Facts
{
    public class FindNearestNeighbourFacts
    {
        [Fact]
        public void Nearest_neighbour()
        {
            var boid = new Boid
            {
                BoidComponent = new BoidComponent
                {
                    Position = new Vector2(10, 10)
                }
            };
            var neighbours = new[]
            {
                new Vector2(0,0),
                new Vector2(5,5)
            };

            var nearestNeighbour = FindNeareastNeighbour.NearestNeighbour(boid, neighbours);

            Assert.Equal(neighbours[1], nearestNeighbour);
        }

        [Fact]
        public void Nearest_neighbour_from_large_set()
        {
            var rand = new Random();
            int randInt() => rand.Next(0, 1000);
            Vector2 randVector2() => new Vector2(randInt(), randInt());
            var neighbours = Enumerable.Range(0, 100).Select(_ => randVector2()).ToList();

            var boid = new Boid
            {
                BoidComponent = new BoidComponent
                {
                    Position = new Vector2(250, 750)
                }
            };

            var kdTree = new Vector2KdTree(neighbours);
            var nearestByKdTree = kdTree.NearestNeighbour(boid.BoidComponent.Position);
            var nearestNeighbour = FindNeareastNeighbour.NearestNeighbour(boid, neighbours);
            var bruteForceNearestNeighbour = BruteForceNearestNeighbour(neighbours, boid.BoidComponent.Position);

            Assert.Equal(nearestNeighbour, bruteForceNearestNeighbour);
        }

        internal Vector2 BruteForceNearestNeighbour(IEnumerable<Vector2> points, Vector2 target)
        {
            return points.OrderBy(vec => Vector2.Distance(vec, target)).First();
        }
    }
}