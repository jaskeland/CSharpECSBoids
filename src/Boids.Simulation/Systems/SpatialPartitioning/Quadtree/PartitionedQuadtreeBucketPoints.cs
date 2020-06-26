using System.Collections.Generic;
using System.Numerics;

namespace Boids.Simulation.Systems.SpatialPartitioning.Quadtree
{
    internal class PartitionedQuadtreeBucketPoints
    {
        private List<Vector2> _topLeft;
        private List<Vector2> _topRight;
        private List<Vector2> _bottomLeft;
        private List<Vector2> _bottomRight;

        public PartitionedQuadtreeBucketPoints(IEnumerable<Vector2> points, Vector2 center)
        {
            _topLeft = new List<Vector2>();
            _topRight = new List<Vector2>();
            _bottomLeft = new List<Vector2>();
            _bottomRight = new List<Vector2>();

            foreach (var point in points)
            {
                switch (DirectionFromCenter(point, center))
                {
                    case ChildBucketDirection.TopLeft:
                        _topLeft.Add(point);
                        break;

                    case ChildBucketDirection.TopRight:
                        _topRight.Add(point);
                        break;

                    case ChildBucketDirection.BottomLeft:
                        _bottomLeft.Add(point);
                        break;

                    case ChildBucketDirection.BottomRight:
                        _bottomRight.Add(point);
                        break;
                }
            }
        }

        public IReadOnlyCollection<Vector2> TopLeft => _topLeft.AsReadOnly();
        public IReadOnlyCollection<Vector2> TopRight => _topRight.AsReadOnly();
        public IReadOnlyCollection<Vector2> BottomLeft => _bottomLeft.AsReadOnly();
        public IReadOnlyCollection<Vector2> BottomRight => _bottomRight.AsReadOnly();

        private static ChildBucketDirection DirectionFromCenter(Vector2 point, Vector2 center)
        {
            // Is left of center?
            if (point.X < center.X)
            {
                // Is above?
                if (point.Y < center.Y)
                {
                    return ChildBucketDirection.TopLeft;
                }
                else
                {
                    return ChildBucketDirection.BottomLeft;
                }
            }
            else
            {
                // Is above?
                if (point.Y < center.Y)
                {
                    return ChildBucketDirection.TopRight;
                }
                else
                {
                    return ChildBucketDirection.BottomRight;
                }
            }
        }
    }
}