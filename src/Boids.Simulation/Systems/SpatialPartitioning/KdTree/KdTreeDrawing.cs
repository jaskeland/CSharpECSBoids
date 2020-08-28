using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Boids.Simulation.Helpers;
using Boids.Simulation.SFML_helpers;
using SFML.Graphics;

namespace Boids.Simulation.Systems.SpatialPartitioning.KdTree
{
    public static class KdTreeDrawing
    {
        public static ListOfDrawables PointsOnGrid<TType>(Vector2 boxSize, KdTree<TType> tree)
            where TType : IKdTreeSortable<TType>
        {
            return new ListOfDrawables(MakeCircles(boxSize, tree));
        }

        private static IEnumerable<Drawable> MakeCircles<TType>(Vector2 boxSize, KdTree<TType> tree)
            where TType : IKdTreeSortable<TType>
        {
            // Number of elements in each direction
            var treeSize = tree.Size();
            var elements = tree.GetAllWithPositionInTree().ToArray();

            // Double tree width because the elements position are are split in the middle with negative values when going left of center.
            var xStep = Math.Round(boxSize.X / (treeSize.X * 2), MidpointRounding.AwayFromZero);
            var yStep = Math.Round(boxSize.Y / treeSize.Y, MidpointRounding.AwayFromZero);

            var count = elements.Count();
            for (var i = 0; i < count; i++)
            {
                // Adjusting for centered tree positions
                var xPos = xStep * (elements[i].Value.X + treeSize.X);
                var yPos = yStep * elements[i].Value.Y;
                yield return Circle(new Vector2((float) xPos, (float) yPos));
            }
        }

        private static Drawable Circle(Vector2 position)
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            var colour = new Color((byte)random.Next(15, 255), (byte)random.Next(15, 255), (byte)random.Next(50, 255));

            return new CircleShape
            {
                FillColor = colour,
                Radius = 5,
                Position = position.ToVector2f()
            };
        }
    }
}
