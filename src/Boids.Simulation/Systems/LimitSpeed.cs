using System;
using System.Numerics;
using Boids.Simulation.Archetypes;

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
            var clampedSpeed = new Vector2
            {
                X = Math.Clamp(boid.BoidComponent.Acceleration.X, -_maxSpeed, _maxSpeed),
                Y = Math.Clamp(boid.BoidComponent.Acceleration.Y, -_maxSpeed, _maxSpeed)
            };

            boid.BoidComponent.Acceleration = clampedSpeed;
        }
    }
}