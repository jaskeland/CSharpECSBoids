using Boids.Simulation.Helpers;
using Boids.Simulation.SFML_helpers;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Boids.Simulation.Systems.Quadtree
{
    /// <summary>
    /// Partitoned space from top-left to bottom-right.
    /// </summary>
    public class Quadtree : Drawable
    {
        private QuadtreeBucket _rootBucket;
        private readonly ListOfDrawables _drawableTree;
        private readonly Random _random;

        public Quadtree(IEnumerable<Vector2> points, Vector2 topLeft, Vector2 bottomRight)
        {
            _random = new Random((int)bottomRight.X);
            _rootBucket = new QuadtreeBucket(points, topLeft, bottomRight);
            _rootBucket.Partition();

            _drawableTree = new ListOfDrawables();
            var allBuckets = new List<QuadtreeBucket> { _rootBucket };
            _rootBucket.GetAllChildrenRecursively(ref allBuckets);
            foreach (var bucket in allBuckets)
            {
                //if (bucket.IsEmpty)
                //    continue;
                _drawableTree.Add(BucketShape(bucket.Dimensions));
            }
        }

        private Drawable BucketShape(ValueTuple<Vector2, Vector2> tuple)
        {
            return BucketShape(tuple.Item1, tuple.Item2);
        }

        private Drawable BucketShape(Vector2 topLeft, Vector2 bottomRight)
        {
            return new RectangleShape(bottomRight.ToVector2f())
            {
                Position = topLeft.ToVector2f(),
                OutlineThickness = 1,
                OutlineColor = new Color((byte)_random.Next(15, 255), (byte)_random.Next(15, 255), (byte)_random.Next(50, 255)),
                FillColor = Color.Transparent
            };
        }

        public void Draw(RenderTarget target, RenderStates states) => _drawableTree.Draw(target, states);

        public void Clear() => _drawableTree.Clear();
    }
}