using Boids.Simulation.Archetypes;
using Boids.Simulation.Components;
using Boids.Simulation.Systems;
using SFML.System;
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
                    Position = new Vector2f(10, 10)
                }
            };
            var neighbours = new[]
            {
                new Vector2f(0,0),
                new Vector2f(5,5)
            };

            var nearestNeighbour = FindNeareastNeighbour.NearestNeighbour(boid, neighbours);

            Assert.Equal(neighbours[1], nearestNeighbour);
        }
    }
}