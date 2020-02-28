﻿using Boids.Simulation.Components;

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