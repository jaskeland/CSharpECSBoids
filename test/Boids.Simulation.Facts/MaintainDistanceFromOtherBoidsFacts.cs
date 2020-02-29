using Boids.Simulation.Archetypes;
using Boids.Simulation.Components;
using Boids.Simulation.Helpers;
using Boids.Simulation.Systems;
using SFML.System;
using Xunit;

namespace Boids.Simulation.Facts
{
    public class MaintainDistanceFromOtherBoidsFacts
    {
        [Fact]
        public void Maintains_distance()
        {
            var boid = new Boid
            {
                BoidComponent = new BoidComponent
                {
                    Position = new Vector2f(10, 10),
                    NearestNeighbour = new Vector2f(5, 5)
                }
            };

            var sut = new MaintainDistanceFromOtherBoids(10.0f, 1.0f);

            sut.Mutate(boid, 1.0f);

            Assert.Equal(1.0f, boid.BoidComponent.Acceleration.Length());
            Assert.True(boid.BoidComponent.Acceleration.X > 0, "Acceleration must push away from neighbour");
            Assert.True(boid.BoidComponent.Acceleration.Y > 0, "Acceleration must push away from neighbour");
        }
    }
}