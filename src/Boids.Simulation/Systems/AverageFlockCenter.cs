using SFML.System;
using System.Collections.Generic;
using System.Linq;

namespace Boids.Simulation.Systems
{
    public static class AverageFlockCenter
    {
        public static Vector2f Center(IEnumerable<Vector2f> flock)
        {
            return new Vector2f(flock.Average(v => v.X), flock.Average(v => v.Y));
        }
    }
}