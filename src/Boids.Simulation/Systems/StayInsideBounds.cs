using Boids.Simulation.Archetypes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Boids.Simulation.Systems
{
    public class StayInsideBounds
    {
        private Vector2 _topLeft;
        private Vector2 _bottomRight;

        public StayInsideBounds(Vector2 topLeft, Vector2 bottomRight)
        {
            _topLeft = topLeft;
            _bottomRight = bottomRight;
        }

        public void Mutate(Boid boid)
        {
            var projectedPosition = boid.BoidComponent.Position + boid.BoidComponent.Acceleration;

            if (projectedPosition.X > _bottomRight.X)
                boid.BoidComponent.Acceleration = new Vector2(-boid.BoidComponent.Acceleration.X, boid.BoidComponent.Acceleration.Y);
            if (projectedPosition.X < _topLeft.X)
                boid.BoidComponent.Acceleration = new Vector2(-boid.BoidComponent.Acceleration.X, boid.BoidComponent.Acceleration.Y);

            if (projectedPosition.Y > _bottomRight.Y)
                boid.BoidComponent.Acceleration = new Vector2(boid.BoidComponent.Acceleration.X, -boid.BoidComponent.Acceleration.Y);
            if (projectedPosition.Y < _topLeft.Y)
                boid.BoidComponent.Acceleration = new Vector2(boid.BoidComponent.Acceleration.X, -boid.BoidComponent.Acceleration.Y);
        }
    }
}