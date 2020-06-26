using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Boids.Simulation.Helpers;

namespace Boids.Simulation.Systems.SpatialPartitioning.KdTree
{
    public class Vector2KdTree
    {
        private Vector2KdTreeNode? _root;

        public Vector2KdTree(IEnumerable<Vector2> points)
        {
            foreach (var point in points)
            {
                Insert(point);
            }
        }

        public void Insert(Vector2 point)
        {
            _root = InsertRecursive(_root, point, 0);
        }

        public float MinimumInDimension(uint dimension)
        {
            return MinimumValueInDimension(_root, dimension, 0);
        }

        public Vector2 NearestNeighbour(Vector2 point)
        {
            if (_root == null)
                return point;

            return NearestNeighbourByQueue(_root, point);
        }

        public IEnumerable<Vector2> NeighboursInRange(Vector2 point, float range)
        {
            if (_root == null || float.IsNaN(range))
                return new Vector2[] { };

            return NeighboursWithinRangeByQueue(_root, point, range);
        }

        private static Vector2KdTreeNode InsertRecursive(Vector2KdTreeNode? root, Vector2 point, uint depth)
        {
            if (root == null)
                return new Vector2KdTreeNode(point, depth);

            if (point.IsNan())
                return root;


            var currentDimension = depth % 2;

            if (point.IsLeftOf(root.Point, currentDimension))
                root.Left = InsertRecursive(root.Left, point, depth + 1);
            else
                root.Right = InsertRecursive(root.Right, point, depth + 1);

            return root;
        }

        private static float MinimumValueInDimension(Vector2KdTreeNode? root, uint dimension, uint depth)
        {
            if (root == null)
                return float.MaxValue;

            var currentDepth = depth % 2;

            if (currentDepth == dimension)
            {
                if (root.Left == null)
                    // Correct depth AND no more children in this dimension means we have smallest value
                    return root.Point.ValueAtDimension(dimension);
                return MinimumValueInDimension(root.Left, dimension, depth + 1);
            }

            return MinOfThree(root.Point.ValueAtDimension(dimension), MinimumValueInDimension(root.Left, dimension, depth + 1), MinimumValueInDimension(root.Right, dimension, depth + 1));
        }

        private static Vector2 NearestNeighbourByQueue(Vector2KdTreeNode root, Vector2 point)
        {
            var nodesToExplore = new Queue<Vector2KdTreeNode>();
            nodesToExplore.Enqueue(root);

            var bestPoint = root.Point;
            var bestDistance = Vector2.Distance(point, root.Point);
            uint depth = 0;

            while (nodesToExplore.Any())
            {
                var node = nodesToExplore.Dequeue();

                if (point.IsLeftOf(node.Point, node.Dimension))
                {
                    if (node.Left != null)
                    {
                        var distanceToChild = Vector2.Distance(point, node.Left.Point);
                        if (distanceToChild < bestDistance)
                        {
                            bestDistance = distanceToChild;
                            bestPoint = node.Left.Point;
                        }
                        nodesToExplore.Enqueue(node.Left);
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        var distanceToChild = Vector2.Distance(point, node.Right.Point);
                        if (distanceToChild < bestDistance)
                        {
                            bestDistance = distanceToChild;
                            bestPoint = node.Right.Point;
                        }
                        nodesToExplore.Enqueue(node.Right);
                    }
                }

                depth++;
                if (depth == uint.MaxValue)
                    throw new Exception();
            }

            return bestPoint;
        }

        private static IEnumerable<Vector2> NeighboursWithinRangeByQueue(Vector2KdTreeNode root, Vector2 point, float range)
        {
            var nodesToExplore = new Queue<Vector2KdTreeNode>();
            nodesToExplore.Enqueue(root);

            var nearbyNeighbours = new List<Vector2>();

            uint depth = 0;

            while (nodesToExplore.Any())
            {
                var node = nodesToExplore.Dequeue();

                if (point.IsLeftOf(node.Point, node.Dimension))
                {
                    if (node.Left != null)
                    {
                        var distanceToChild = Vector2.Distance(point, node.Left.Point);
                        if (distanceToChild < range)
                        {
                            nearbyNeighbours.Add(node.Left.Point);
                        }
                        nodesToExplore.Enqueue(node.Left);
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        var distanceToChild = Vector2.Distance(point, node.Right.Point);
                        if (distanceToChild < range)
                        {
                            nearbyNeighbours.Add(node.Right.Point);
                        }
                        nodesToExplore.Enqueue(node.Right);
                    }
                }

                depth++;
                if (depth == uint.MaxValue)
                    throw new Exception();
            }

            return nearbyNeighbours;
        }

        private static float MinOfThree(float v1, float v2, float v3)
        {
            return Math.Min(v1, Math.Min(v2, v3));
        }
    }
}