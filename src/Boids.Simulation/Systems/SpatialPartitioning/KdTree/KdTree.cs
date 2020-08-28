using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            var currentDimension = depth % dimensionality;

            if (root == null)
                return new KdTreeNode<TType>(point, currentDimension);

            if (point.IsLeftOf(root.Value, currentDimension))
                root.Left = InsertRecursive(root.Left, point, depth + 1, dimensionality);
            else
                root.Right = InsertRecursive(root.Right, point, depth + 1, dimensionality);

            return root;
        }

        /// <summary>
        /// Gets all elements in the tree with their associated position in the tree. The root is at (0,0), its left child is (-1,1) and its right child is (1,1)
        /// </summary>
        /// <returns></returns>
        public Dictionary<TType, Vector2> GetAllWithPositionInTree()
        {
            if (_root == null)
                return new Dictionary<TType, Vector2>();


            var elementsAtLevel = new Dictionary<int, int>();
            SizeRecursive(_root, 0, ref elementsAtLevel);

            var nodesToExplore = new Queue<KdTreeNode<TType>>();
            nodesToExplore.Enqueue(_root);

            var currentDepth = 0;
            var currentWidth = 0;
            var all = new Dictionary<TType, Vector2>();
            while (nodesToExplore.Any())
            {
                var node = nodesToExplore.Dequeue();
                all.Add(node.Value, new Vector2(currentWidth, currentDepth));

                // Because we traverse the tree width first we know that the depth only increases after we've explored all elements at the current depth
                currentWidth++;
                if (currentWidth >= elementsAtLevel[currentDepth])
                {
                    currentDepth++;
                    currentWidth = 0;
                }

                if (node.Left != null)
                    nodesToExplore.Enqueue(node.Left);
                if (node.Right != null)
                    nodesToExplore.Enqueue(node.Right);
            }

            return all;
        }

        public Vector2 Size()
        {
            if (_root == null)
                return new Vector2();

            var elementsAtLevel = new Dictionary<int, int>();
            SizeRecursive(_root, 0, ref elementsAtLevel);

            var deepest = elementsAtLevel.Max(kv => kv.Key);
            var widest = elementsAtLevel.Max(kv => kv.Value);

            return new Vector2(widest, deepest);
        }

        private static void SizeRecursive(KdTreeNode<TType> node, int currentDepth, ref Dictionary<int, int> elementsAtLevel)
        {
            if (elementsAtLevel.ContainsKey(currentDepth))
            {
                elementsAtLevel[currentDepth]++;
            }
            else
            {
                elementsAtLevel.Add(currentDepth, 1);
            }

            if (node == null)
                return;

            if (node.Left != null)
            {
                SizeRecursive(node.Left, currentDepth + 1, ref elementsAtLevel);
            }

            if (node.Right != null)
            {
                SizeRecursive(node.Right, currentDepth + 1, ref elementsAtLevel);
            }
        }

        /// <summary>
        /// Returns the @TType with the smallest Distance.
        /// </summary>
        /// <param name="value">Value to search against</param>
        /// <returns></returns>
        public TType NearestNeighbour(TType value)
        {
            // _root is always initialized in the constructor
            return NearestNeighbour(_root!, value);
        }

        private static TType NearestNeighbour(KdTreeNode<TType> root, TType point)
        {
            var nodesToExplore = new Stack<KdTreeNode<TType>>();
            nodesToExplore.Push(root);

            var bestSoFar = root.Value;
            var bestDistance = float.MaxValue;
            uint depth = 0;

            while (nodesToExplore.Any())
            {
                var node = nodesToExplore.Pop();
                var distance = point.Distance(node.Value);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestSoFar = node.Value;
                }

                if (point.IsLeftOf(node.Value, node.Dimension))
                {
                    if (node.Left != null)
                    {
                        nodesToExplore.Push(node.Left);
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        nodesToExplore.Push(node.Right);
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