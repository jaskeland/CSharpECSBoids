using System;
using Boids.Simulation.Archetypes;
using SFML.System;

namespace Boids.Simulation.Systems
{
    public class LimitSpeed
    {
        private readonly float _maxSpeed;

        public LimitSpeed(float maxSpeed)
        {
            _maxSpeed = maxSpeed;
        }

        public void Mutate(Boid boid)
        {
            var clampedSpeed = new Vector2f
            {
                X = Math.Clamp(boid.BoidComponent.Acceleration.X, -_maxSpeed, _maxSpeed),
                Y = Math.Clamp(boid.BoidComponent.Acceleration.Y, -_maxSpeed, _maxSpeed)
            };

            boid.BoidComponent.Acceleration = clampedSpeed;
        }
    }
}