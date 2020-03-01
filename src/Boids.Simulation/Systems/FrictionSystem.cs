using Boids.Simulation.Archetypes;

namespace Boids.Simulation.Systems
{
    public class Friction
    {
        private readonly float _influence;

        public Friction(float influence)
        {
            _influence = influence;
        }

        public void Mutate(Boid boid, float deltaTime)
        {
            boid.BoidComponent.Acceleration -= boid.BoidComponent.Acceleration * _influence * deltaTime;
        }
    }
}