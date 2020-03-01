using Boids.Simulation.Archetypes;
using Boids.Simulation.Systems.Collision;
using System.Collections.Generic;

namespace Boids.Simulation.Systems
{
    public class LineCollision
    {
        private IEnumerable<CollidableLine> _lines;

        public LineCollision(params CollidableLine[] collidableLines)
        {
            _lines = collidableLines;
        }

        public LineCollision(IEnumerable<CollidableLine> collidableLines)
        {
            _lines = collidableLines;
        }

        public void Mutate(Boid boid)
        {
            foreach (var line in _lines)
            {
                var projectedPath = new CollidableLine(boid.BoidComponent.Position, boid.BoidComponent.Position + boid.BoidComponent.Acceleration);
                if (projectedPath.Intersects(line))
                {
                    return;
                }
            }
        }
    }
}