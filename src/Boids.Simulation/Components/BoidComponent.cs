using SFML.System;

namespace Boids.Simulation.Components
{
    public class BoidComponent
    {
        public Vector2f Position { get; set; }

        public Vector2f Acceleration { get; set; }

        public Vector2f Target { get; set; }
    }
}