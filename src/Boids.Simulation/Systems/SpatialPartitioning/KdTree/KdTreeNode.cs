namespace Boids.Simulation.Systems.SpatialPartitioning.KdTree
{
    internal class KdTreeNode<TType>
        where TType : IKdTreeSortable<TType>
    {
        internal KdTreeNode<TType>? Left { get; set; }
        internal KdTreeNode<TType>? Right { get; set; }

        internal uint Dimension { get; }

        internal TType Value { get; }

        internal KdTreeNode(TType value, uint dimension)
        {
            Value = value;
            Dimension = dimension;
        }
    }
}