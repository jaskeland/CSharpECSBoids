using System.Numerics;

namespace Boids.Simulation.Systems.SpatialPartitioning
{
    internal class Node
    {
        internal Node? Left { get; set; }
        internal Node? Right { get; set; }
        internal Vector2 Point { get; }

        internal uint Dimension { get; }

        public Node(Vector2 point, uint dimension)
        {
            Point = point;
            Dimension = dimension % 2;
        }
    }
}