using Boids.Simulation.Archetypes;
using Boids.Simulation.Helpers;

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
            var direction = (boid.BoidComponent.Target - boid.BoidComponent.Position).Normalize();
            var influence = direction * _influence * deltaTime;
            boid.BoidComponent.Acceleration += influence;
        }
    }
}