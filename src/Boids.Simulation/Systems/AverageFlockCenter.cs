using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Boids.Simulation.Systems
{
    public static class AverageFlockCenter
    {
        public static Vector2 Center(IEnumerable<Vector2> flock)
        {
            return new Vector2(flock.Average(v => v.X), flock.Average(v => v.Y));
        }
    }
}