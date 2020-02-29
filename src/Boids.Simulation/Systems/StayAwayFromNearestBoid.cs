﻿using Boids.Simulation.Archetypes;
using Boids.Simulation.Helpers;

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
            if (boid.BoidComponent.Position.DistanceTo(boid.BoidComponent.NearestNeighbour) > _minDistance)
                return;

            var direction = (boid.BoidComponent.Position - boid.BoidComponent.NearestNeighbour).Normalize();
            var influence = direction * _influence * deltaTime;
            boid.BoidComponent.Acceleration += influence;
        }
    }
}