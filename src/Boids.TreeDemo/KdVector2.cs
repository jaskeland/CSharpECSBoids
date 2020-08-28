using System.Numerics;
using Boids.Simulation.Systems.SpatialPartitioning.KdTree;

namespace Boids.Demo
{
    public class KdVector2 : IKdTreeSortable<KdVector2>
    {
        public Vector2 Value { get; }

        public KdVector2(float x, float y)
        {
            Value = new Vector2(x, y);
        }

        public KdVector2(Vector2 vector)
        {
            Value = vector;
        }
        
        public bool IsLeftOf(KdVector2 other, uint dimension)
        {
            return dimension == 0
                ? Value.X < other.Value.X
                : Value.Y < other.Value.Y;
        }

        public float Distance(KdVector2 other)
        {
            return Vector2.Distance(Value, other.Value);
        }
    }
}
