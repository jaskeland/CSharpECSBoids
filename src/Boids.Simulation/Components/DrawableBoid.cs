using SFML.Graphics;
using SFML.System;

namespace Boids.Simulation.Components
{
    public class DrawableBoid : Transformable, Drawable
    {
        private RectangleShape _shape;

        public DrawableBoid(Vector2f position)
        {
            _shape = new RectangleShape(new Vector2f(10, 10));
            Position = position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            target.Draw(_shape, states);
        }
    }
}