using Boids.Simulation.Components;

namespace Boids.Simulation.Systems
{
    public static class UpdateBoidRender
    {
        public static void Mutate(DrawableBoidComponent drawable, BoidComponent boid)
        {
            drawable.Position = boid.Position;
        }
    }
}