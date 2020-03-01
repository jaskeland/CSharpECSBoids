using SFML.System;
using System.Numerics;

namespace Boids.Simulation.Components
{
    public class BoidComponent
    {
        public Vector2 Position { get; set; }

        public Vector2 Acceleration { get; set; }

        public Vector2 Target { get; set; }

        public Vector2 NearestNeighbour { get; set; }
    }
}