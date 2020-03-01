using Boids.Simulation.Components;
using Boids.Simulation.Helpers;

namespace Boids.Simulation.Systems
{
    public static class UpdateBoidRender
    {
        public static void Mutate(DrawableBoidComponent drawable, BoidComponent boid)
        {
            drawable.Position = boid.Position.ToVector2f();
        }
    }
}