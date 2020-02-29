using Boids.Simulation.Archetypes;
using Boids.Simulation.Helpers;
using SFML.System;
using System.Collections.Generic;

namespace Boids.Simulation.Systems
{
    public static class FindNeareastNeighbour
    {
        public static Vector2f NearestNeighbour(Boid boid, IEnumerable<Vector2f> neighbours)
        {
            var closestPoint = boid.BoidComponent.Target;
            var closestDistance = boid.BoidComponent.Position.DistanceTo(closestPoint);

            foreach (var item in neighbours)
            {
                var distanceTo = boid.BoidComponent.Position.DistanceTo(item);
                if (distanceTo < closestDistance && distanceTo > 0)
                {
                    closestPoint = item;
                    closestDistance = distanceTo;
                }
            }

            return closestPoint;
        }
    }
}