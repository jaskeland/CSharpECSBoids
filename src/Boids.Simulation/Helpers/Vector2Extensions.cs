using SFML.System;
using System.Numerics;

namespace Boids.Simulation.Helpers
{
    public static class Vector2Extensions
    {
        public static Vector2f ToVector2f(this Vector2 vector)
        {
            return new Vector2f(vector.X, vector.Y);
        }
    }
}