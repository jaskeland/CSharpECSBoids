using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Boids.Simulation.Archetypes;
using Boids.Simulation.Helpers;
using Boids.Simulation.Systems.SpatialPartitioning;
using Boids.Simulation.Systems.SpatialPartitioning.KdTree;

namespace Boids.Simulation.Systems
{
    public static class AverageFlockCenter
    {
        public static Vector2 Center(Vector2KdTree tree, Vector2 position, float range)
        {
            var flock = tree.NeighboursInRange(position, range).ToList();

            if (!flock.Any())
                return position;

            var average = new Vector2(flock.Average(v => v.X), flock.Average(v => v.Y));
            return !average.IsNan()
                ? average
                : position;
        }
    }
}