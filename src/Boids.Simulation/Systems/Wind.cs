using Boids.Simulation.Archetypes;
using System.Numerics;

namespace Boids.Simulation.Systems
{
    public class Wind
    {
        private readonly Vector2 _direction;
        private readonly float _influence;

        public Wind(Vector2 direction, float influence)
        {
            _direction = Vector2.Normalize(direction);
            _influence = influence;
        }

        public void Mutate(Boid boid, float deltaTime)
        {
            boid.BoidComponent.Acceleration += _direction * _influence * deltaTime;
        }
    }
}