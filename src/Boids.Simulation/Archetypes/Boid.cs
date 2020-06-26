using Boids.Simulation.Components;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Boids.Simulation.Archetypes
{
    public class Boid
    {
        public Boid()
        {
            Id = EntityId.NewId();
            BoidComponent = new BoidComponent();
            DrawableBoidComponent = new DrawableBoidComponent();
        }

        public EntityId Id { get; }

        public BoidComponent BoidComponent { get; set; }

        public DrawableBoidComponent DrawableBoidComponent { get; set; }
    }
}