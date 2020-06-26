using System;
using Boids.Simulation.Components;
using Boids.Simulation.Helpers;

namespace Boids.Simulation.Systems
{
    public static class UpdateBoidRender
    {
        public static void Mutate(DrawableBoidComponent drawable, BoidComponent boid)
        {
            drawable.Position = boid.Position.ToVector2f();
            var direction = MathF.Atan2(boid.Acceleration.Y, boid.Acceleration.X);
            drawable.Rotation = direction * 180 / MathF.PI;
        }
    }
}