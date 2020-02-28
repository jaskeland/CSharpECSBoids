using Boids.Simulation.Archetypes;
using Boids.Simulation.Helpers;
using SFML.System;

namespace Boids.Simulation.Systems
{
    public class Wind
    {
        private readonly Vector2f _direction;
        private readonly float _influence;

        public Wind(Vector2f direction, float influence)
        {
            _direction = direction.Normalize();
            _influence = influence;
        }

        public void Mutate(Boid boid, float deltaTime)
        {
            boid.BoidComponent.Acceleration += _direction * _influence * deltaTime;
        }
    }
}