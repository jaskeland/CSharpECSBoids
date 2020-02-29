using System;
using SFML.System;

namespace Boids.Simulation.Helpers
{
    public static class Vector2fExtensions
    {
        public static float Length(this Vector2f vector)
        {
            return (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        public static Vector2f Normalize(this Vector2f vector)
        {
            return vector / vector.Length();
        }

        public static float DistanceTo(this Vector2f vector, Vector2f target)
        {
            return (vector - target).Length();
        }
    }
}