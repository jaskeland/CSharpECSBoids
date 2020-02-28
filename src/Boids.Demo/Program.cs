using Boids.Simulation.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using Boids.Simulation.Archetypes;
using Boids.Simulation.Systems;

namespace Boids.Demo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var windowSize = new Vector2u(800, 600);
            uint numberOfBoids = 20;

            var window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y), "Boids");
            window.Closed += OnClose;

            var boids = EvenlySpacedBoids(windowSize, numberOfBoids).ToList();
            var followLeaderSystem = new FollowTheLeader(0.2f);
            var windSystem = new Wind(new Vector2f(1, 0), 0.1f);

            var clock = new Clock();
            var previousFrameTime = clock.ElapsedTime;

            while (window.IsOpen)
            {
                var deltaTime = (Time.FromSeconds(1) / previousFrameTime).AsSeconds();
                window.DispatchEvents();
                window.Clear();

                boids.ForEach(boid => followLeaderSystem.Mutate(boid, deltaTime));
                boids.ForEach(boid => windSystem.Mutate(boid, deltaTime));
                boids.ForEach(boid => boid.BoidComponent.Position += boid.BoidComponent.Acceleration);

                boids.ForEach(boid => UpdateBoidRender.Mutate(boid.DrawableBoidComponent, boid.BoidComponent));
                foreach (var boid in boids)
                {
                    window.Draw(boid.DrawableBoidComponent);
                }
                window.Display();
                previousFrameTime = clock.Restart();
            }
        }

        private static IEnumerable<Boid> EvenlySpacedBoids(Vector2u windowSize, uint numberOfBoids)
        {
            var xSpacing = windowSize.X / numberOfBoids;
            var ySpacing = windowSize.Y / numberOfBoids;

            for (var i = 0; i < numberOfBoids; i++)
            {
                var position = new Vector2f(xSpacing * i, ySpacing * i);
                yield return new Boid
                {
                    BoidComponent = new BoidComponent
                    {
                        Acceleration = new Vector2f(),
                        Position = position,
                        Target = new Vector2f(windowSize.X / 2, windowSize.Y / 2)
                    },
                    DrawableBoidComponent = new DrawableBoidComponent
                    {
                        Position = position
                    }
                };
            }
        }

        public static void OnClose(object? sender, EventArgs args)
        {
            if (sender == null)
                throw new NullReferenceException("Window is null in OnClose event handler");

            ((Window)sender).Close();
        }
    }
}