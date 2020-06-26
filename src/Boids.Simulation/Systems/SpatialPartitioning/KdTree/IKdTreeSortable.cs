namespace Boids.Simulation.Systems.SpatialPartitioning.KdTree
{
    public interface IKdTreeSortable<in TType>
    {
        /// <summary>
        /// Used by KdTree to check if a value is smaller in a given dimension.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <param name="dimension">The dimension the left of check is performed in.</param>
        /// <returns></returns>
        bool IsLeftOf(TType other, uint dimension);

        /// <summary>
        /// Used by KdTree to determine how dissimilar two values are in a given dimension.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <param name="dimension">The dimension of the value that will be examined.</param>
        /// <returns></returns>
        float Distance(TType other);
    }
}