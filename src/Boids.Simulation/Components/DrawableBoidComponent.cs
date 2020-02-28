using SFML.Graphics;
using SFML.System;

namespace Boids.Simulation.Components
{
    public class DrawableBoidComponent : Transformable, Drawable
    {
        private readonly RectangleShape _shape;

        public DrawableBoidComponent()
        {
            _shape = new RectangleShape(new Vector2f(10, 10));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            target.Draw(_shape, states);
        }
    }
}