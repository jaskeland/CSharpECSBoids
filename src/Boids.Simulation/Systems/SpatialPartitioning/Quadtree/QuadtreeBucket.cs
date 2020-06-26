using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Boids.Simulation.Systems.SpatialPartitioning.Quadtree
{
    public class QuadtreeBucket
    {
        // Each bucket can have 4 children
        private QuadtreeBucket[] _children = new QuadtreeBucket[4];

        private readonly List<Vector2> _contents;
        private readonly Vector2 _topLeft;
        private readonly Vector2 _bottomRight;

        public QuadtreeBucket(IEnumerable<Vector2> points, Vector2 topLeft, Vector2 bottomRight)
        {
            if (topLeft.X + bottomRight.X == 0)
                throw new ArgumentException("QuadtreeBucket width can not be 0.");
            if (topLeft.Y + bottomRight.Y == 0)
                throw new ArgumentException("QuadtreeBucket height can not be 0.");

            _contents = points.ToList();
            _topLeft = topLeft;
            _bottomRight = bottomRight;
        }

        private QuadtreeBucket(Vector2 topLeft, Vector2 bottomRight) : this(new List<Vector2>(), topLeft, bottomRight)
        {
        }

        public (Vector2 topLeft, Vector2 bottomRight) Dimensions => (_topLeft, _bottomRight);

        public bool IsEmpty => !_contents.Any() || _children.All(b => b == null);

        private bool IsTerminal => _contents.Count() <= 1;

        public void AddPoint(Vector2 point)
        {
            _contents.Add(point);
        }

        public void GetAllChildrenRecursively(ref List<QuadtreeBucket> outList)
        {
            foreach (var bucket in _children)
            {
                if (bucket == null)
                    continue;
                outList.Add(bucket);
                bucket.GetAllChildrenRecursively(ref outList);
            }
        }

        public void Partition()
        {
            if (_contents.Count() <= 1)
                return;

            var center = new Vector2((_topLeft.X + _bottomRight.X) / 2, (_topLeft.Y + _bottomRight.Y) / 2);

            _children[(int)ChildBucketDirection.TopLeft] = new QuadtreeBucket(_topLeft, center);

            var aboveCenter = new Vector2(center.X, _topLeft.Y);
            var rightOfCenter = new Vector2(_bottomRight.X, center.Y);
            _children[(int)ChildBucketDirection.TopRight] = new QuadtreeBucket(aboveCenter, rightOfCenter);

            var leftOfCenter = new Vector2(_topLeft.X, center.Y);
            var belowCenter = new Vector2(center.X, _bottomRight.Y);
            _children[(int)ChildBucketDirection.BottomLeft] = new QuadtreeBucket(leftOfCenter, belowCenter);

            _children[(int)ChildBucketDirection.BottomRight] = new QuadtreeBucket(center, _bottomRight);

            foreach (var point in _contents)
            {
                _children[(int)DirectionFromCenter(point, center)].AddPoint(point);
            }

            foreach (var child in _children)
            {
                if (child.IsTerminal)
                    continue;
                child.Partition();
            }
        }

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