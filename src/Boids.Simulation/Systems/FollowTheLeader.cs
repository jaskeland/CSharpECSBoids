using Boids.Simulation.Archetypes;
using System.Numerics;

namespace Boids.Simulation.Systems
{
    public class FollowTheLeader
    {
        private readonly float _influence;

        public FollowTheLeader(float influence)
        {
            _influence = influence;
        }

        public void Mutate(Boid boid, float deltaTime)
        {
            var direction = Vector2.Normalize(boid.BoidComponent.Target - boid.BoidComponent.Position);
            var influence = direction * _influence * deltaTime;
            boid.BoidComponent.Acceleration += influence;
        }
    }
}