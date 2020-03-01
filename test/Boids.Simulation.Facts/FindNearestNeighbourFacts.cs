using Boids.Simulation.Archetypes;
using Boids.Simulation.Components;
using Boids.Simulation.Systems;
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
    }
}