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

        public static bool IsLeftOf(this Vector2 thisPoint, Vector2 otherPoint, uint dimension)
        {
            return dimension == 0
                ? thisPoint.X < otherPoint.X
                : thisPoint.Y < otherPoint.Y;
        }

        public static float ValueAtDimension(this Vector2 point, uint dimension)
        {
            return dimension == 0
                ? point.X
                : point.Y;
        }

        public static bool IsNan(this Vector2 vector)
        {
            return float.IsNaN(vector.X) || float.IsNaN(vector.Y);
        }
    }
}