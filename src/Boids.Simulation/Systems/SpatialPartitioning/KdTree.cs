using Boids.Simulation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Boids.Simulation.Systems.SpatialPartitioning
{
    public class KdTree
    {
        private Node? _root;

        public KdTree(IEnumerable<Vector2> points)
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

        private Node InsertRecursive(Node? root, Vector2 point, uint depth)
        {
            if (root == null)
                return new Node(point, depth);

            var currentDimension = depth % 2;

            if (point.IsLeftOf(root.Point, currentDimension))
                root.Left = InsertRecursive(root.Left, point, depth + 1);
            else
                root.Right = InsertRecursive(root.Right, point, depth + 1);

            return root;
        }

        private float MinimumValueInDimension(Node? root, uint dimension, uint depth)
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

        private Vector2 NearestNeighbourByQueue(Node root, Vector2 point)
        {
            var nodesToExplore = new Queue<Node>();
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

        private float MinOfThree(float v1, float v2, float v3)
        {
            return Math.Min(v1, Math.Min(v2, v3));
        }
    }
}