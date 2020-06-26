using System.Numerics;

namespace Boids.Simulation.Systems.SpatialPartitioning.KdTree
{
    internal class Vector2KdTreeNode
    {
        internal Vector2KdTreeNode? Left { get; set; }
        internal Vector2KdTreeNode? Right { get; set; }
        internal Vector2 Point { get; }

        internal uint Dimension { get; }

        public Vector2KdTreeNode(Vector2 point, uint dimension)
        {
            Point = point;
            Dimension = dimension % 2;
        }
    }
}