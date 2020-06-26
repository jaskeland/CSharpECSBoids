using System;
using System.Collections.Generic;
using System.Linq;

namespace Boids.Simulation.Systems.SpatialPartitioning.KdTree
{
    public class KdTree<TType>
        where TType : IKdTreeSortable<TType>
    {
        private readonly uint _dimensionality;
        private KdTreeNode<TType>? _root;

        public KdTree(IEnumerable<TType> values, uint dimensionality)
        {
            _dimensionality = dimensionality;

            foreach (var item in values)
            {
                Insert(item);
            }
        }

        public void Insert(TType point)
        {
            _root = InsertRecursive(_root, point, 0, _dimensionality);
        }

        private static KdTreeNode<TType> InsertRecursive(KdTreeNode<TType>? root, TType point, uint depth, uint dimensionality)
        {
            if (root == null)
                return new KdTreeNode<TType>(point, depth);

            var currentDimension = depth % dimensionality;

            if (point.IsLeftOf(root.Value, currentDimension))
                root.Left = InsertRecursive(root.Left, point, depth + 1, dimensionality);
            else
                root.Right = InsertRecursive(root.Right, point, depth + 1, dimensionality);

            return root;
        }

        /// <summary>
        /// Returns the @TType with the smallest Distance.
        /// </summary>
        /// <param name="value">Value to search against</param>
        /// <returns></returns>
        public TType NearestNeighbour(TType value)
        {
            // _root is always initialized in the constructor
            return NearestNeighbourByQueue(_root!, value);
        }

        private static TType NearestNeighbourByQueue(KdTreeNode<TType> root, TType point)
        {
            var nodesToExplore = new Queue<KdTreeNode<TType>>();
            nodesToExplore.Enqueue(root);

            var bestSoFar = root.Value;
            var bestDistance = point.Distance(root.Value);
            uint depth = 0;

            while (nodesToExplore.Any())
            {
                var node = nodesToExplore.Dequeue();

                if (point.IsLeftOf(node.Value, node.Dimension))
                {
                    if (node.Left != null)
                    {
                        var distanceToChild = point.Distance(node.Left.Value);
                        if (distanceToChild < bestDistance)
                        {
                            bestDistance = distanceToChild;
                            bestSoFar = node.Left.Value;
                        }
                        nodesToExplore.Enqueue(node.Left);
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        var distanceToChild = point.Distance(node.Right.Value);
                        if (distanceToChild < bestDistance)
                        {
                            bestDistance = distanceToChild;
                            bestSoFar = node.Right.Value;
                        }
                        nodesToExplore.Enqueue(node.Right);
                    }
                }

                depth++;
                if (depth == uint.MaxValue)
                    throw new Exception();
            }

            return bestSoFar;
        }
    }
}