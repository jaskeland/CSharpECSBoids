using System;
using System.Numerics;

namespace Boids.Simulation.Systems.Collision
{
    public struct CollidableLine
    {
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public CollidableLine(Vector2 start, Vector2 end)
        {
            _startPosition = start;
            _endPosition = end;
        }

        public bool Intersects(CollidableLine otherLine)
        {
            return DoIntersect(_startPosition, _endPosition, otherLine._startPosition, otherLine._endPosition);
        }

        private static bool DoIntersect(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
        {
            // Find the four orientations needed for general and special cases
            var o1 = DetermineOrientation(p1, q1, p2);
            var o2 = DetermineOrientation(p1, q1, q2);
            var o3 = DetermineOrientation(p2, q2, p1);
            var o4 = DetermineOrientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && OnSegment(p1, p2, q1))
                return true;

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && OnSegment(p1, q2, q1))
                return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && OnSegment(p2, p1, q2))
                return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && OnSegment(p2, q1, q2))
                return true;

            return false; // Doesn't fall in any of the above cases
        }

        public static Orientation DetermineOrientation(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            var val = (p2.Y - p1.Y) * (p3.X - p2.X) - (p2.X - p1.X) * (p3.Y - p2.Y);

            if (val == 0)
                return Orientation.Colinear;

            return (val > 0) ? Orientation.Clockwise : Orientation.CounterClockwise;
        }

        private static bool OnSegment(Vector2 p, Vector2 q, Vector2 r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }
    }
}