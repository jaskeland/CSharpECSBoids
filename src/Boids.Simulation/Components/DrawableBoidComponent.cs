using System;
using SFML.Graphics;
using SFML.System;

namespace Boids.Simulation.Components
{
    public class DrawableBoidComponent : Transformable, Drawable
    {
        private readonly Drawable _shape;

        public DrawableBoidComponent()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            var colour = new Color((byte) random.Next(15, 255), (byte) random.Next(15, 255), (byte) random.Next(50, 255));
            _shape = new VertexArray(PrimitiveType.LineStrip, 5)
            {
                [0] = new Vertex(new Vector2f(-9, -6), colour),
                [1] = new Vertex(new Vector2f(0, 0), colour),
                [2] = new Vertex(new Vector2f(-9, 6), colour),
                [3] = new Vertex(new Vector2f(0, 0), colour),
                [4] = new Vertex(new Vector2f(-15, 0), colour),
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            target.Draw(_shape, states);
        }
    }
}