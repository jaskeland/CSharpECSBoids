using Boids.Simulation.Systems.Collision;
using System.Numerics;
using Xunit;

namespace Boids.Simulation.Facts.Collisions
{
    public class IntersectingLineFacts
    {
        [Fact]
        public void Crossed_lines_intersect()
        {
            var firstLine = new CollidableLine(new Vector2(0.0f, 0.0f), new Vector2(10.0f, 10.0f));
            var secondLine = new CollidableLine(new Vector2(0.0f, 5.0f), new Vector2(10.0f, 5.0f));

            Assert.True(firstLine.Intersects(secondLine), "firstLine.Intersects(secondLine)");
        }

        [Fact]
        public void Equal_lines_intersect()
        {
            var firstLine = new CollidableLine(new Vector2(0.0f, 0.0f), new Vector2(10.0f, 10.0f));
            var secondLine = new CollidableLine(new Vector2(0.0f, 0.0f), new Vector2(10.0f, 10.0f)); ;

            Assert.True(firstLine.Intersects(secondLine), "firstLine.Intersects(secondLine)");
        }

        [Fact]
        public void Lines_are_not_considered_infinite_length()
        {
            var firstLine = new CollidableLine(new Vector2(0.0f, 0.0f), new Vector2(10.0f, 10.0f));
            var secondLine = new CollidableLine(new Vector2(0.0f, 5.0f), new Vector2(1.0f, 5.0f));

            Assert.False(firstLine.Intersects(secondLine), "firstLine.Intersects(secondLine)");
        }
    }
}