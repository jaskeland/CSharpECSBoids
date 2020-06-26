using Boids.Simulation.Archetypes;
using Boids.Simulation.Systems.SpatialPartitioning;
using System.Collections.Generic;
using System.Numerics;

namespace Boids.Simulation.Systems
{
    public static class FindNeareastNeighbour
    {
        public static Vector2 NearestNeighbour(Boid boid, IEnumerable<Vector2> neighbours)
        {
            var closestPoint = boid.BoidComponent.Target;
            var closestDistance = Vector2.Distance(boid.BoidComponent.Position, closestPoint);

            foreach (var item in neighbours)
            {
                var distanceTo = Vector2.Distance(boid.BoidComponent.Position, item);
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