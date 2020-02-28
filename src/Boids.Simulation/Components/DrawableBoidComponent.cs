using System;
using SFML.Graphics;
using SFML.System;

namespace Boids.Simulation.Components
{
    public class DrawableBoidComponent : Transformable, Drawable
    {
        private readonly RectangleShape _shape;

        public DrawableBoidComponent()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            _shape = new RectangleShape(new Vector2f(2, 2))
            {
                FillColor = new Color((byte)random.Next(), (byte)random.Next(), (byte)random.Next())
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            target.Draw(_shape, states);
        }
    }
}