using Boids.Simulation.Archetypes;
using System.Numerics;

namespace Boids.Simulation.Systems
{
    public class StayAwayFromNearestBoid
    {
        private readonly float _minDistance;
        private readonly float _influence;

        public StayAwayFromNearestBoid(float minDistance, float influence)
        {
            _minDistance = minDistance;
            _influence = influence;
        }

        public void Mutate(Boid boid, float deltaTime)
        {
            if (Vector2.Distance(boid.BoidComponent.Position, boid.BoidComponent.NearestNeighbour) > _minDistance)
                return;

            var direction = Vector2.Normalize(boid.BoidComponent.Position - boid.BoidComponent.NearestNeighbour);
            var influence = direction * _influence * deltaTime;
            boid.BoidComponent.Acceleration += influence;
        }
    }
}