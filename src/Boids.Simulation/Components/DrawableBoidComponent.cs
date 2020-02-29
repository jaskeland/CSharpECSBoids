using System;
using SFML.Graphics;

namespace Boids.Simulation.Components
{
    public class DrawableBoidComponent : Transformable, Drawable
    {
        private readonly CircleShape _shape;

        public DrawableBoidComponent()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            _shape = new CircleShape(3.0f)
            {
                FillColor = new Color((byte)random.Next(15, 255), (byte)random.Next(15, 255), (byte)random.Next(50, 255))
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            target.Draw(_shape, states);
        }
    }
}