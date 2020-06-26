using SFML.Graphics;
using System.Collections.Generic;

namespace Boids.Simulation.SFML_helpers
{
    public class ListOfDrawables : Drawable
    {
        private readonly List<Drawable> _drawables;

        public ListOfDrawables()
        {
            _drawables = new List<Drawable>();
        }

        public void AddRange(IEnumerable<Drawable> drawables)
        {
            foreach (var item in drawables)
            {
                Add(item);
            }
        }

        public void Add(Drawable drawable)
        {
            _drawables.Add(drawable);
        }

        public void Clear()
        {
            _drawables.Clear();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var item in _drawables)
            {
                target.Draw(item, states);
            }
        }
    }
}