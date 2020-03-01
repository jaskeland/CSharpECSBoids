using Boids.Simulation.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using Boids.Simulation.Archetypes;
using Boids.Simulation.Helpers;
using Boids.Simulation.Systems;
using System.Numerics;

namespace Boids.Demo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var windowSize = new Vector2u(1920, 1080);
            uint numberOfBoids = 100;

            var window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y), "Boids");
            window.Closed += OnClose;

            var boids = EvenlySpacedBoids(windowSize, numberOfBoids).ToList();
            var followLeaderSystem = new FollowTheLeader(0.2f);
            var windSystem = new Wind(new Vector2(1, 0), 0.1f);
            var maxSpeedSystem = new LimitSpeed(5.0f);
            var maintainDistanceSystem = new StayAwayFromNearestBoid(10.0f, 1.0f);
            var frictionSystem = new Friction(0.1f);

            var clock = new Clock();
            var previousFrameTime = clock.ElapsedTime;

            while (window.IsOpen)
            {
                var deltaTime = (Time.FromSeconds(1) / previousFrameTime).AsSeconds();
                window.DispatchEvents();
                window.Clear();

                boids.ForEach(boid => boid.BoidComponent.NearestNeighbour = FindNeareastNeighbour.NearestNeighbour(boid, boids.Select(b => b.BoidComponent.Position)));
                boids.ForEach(boid => boid.BoidComponent.Target = AverageFlockCenter.Center(boids.Select(b => b.BoidComponent.Position)));
                boids.ForEach(boid => followLeaderSystem.Mutate(boid, deltaTime));
                //boids.ForEach(boid => windSystem.Mutate(boid, deltaTime));
                boids.ForEach(boid => maxSpeedSystem.Mutate(boid));
                boids.ForEach(boid => maintainDistanceSystem.Mutate(boid, deltaTime));
                boids.ForEach(boid => frictionSystem.Mutate(boid, deltaTime));

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
                var position = new Vector2(xSpacing * i, ySpacing * i);
                yield return new Boid
                {
                    BoidComponent = new BoidComponent
                    {
                        Acceleration = new Vector2(),
                        Position = position,
                        Target = new Vector2(windowSize.X / 2, windowSize.Y / 2)
                    },
                    DrawableBoidComponent = new DrawableBoidComponent
                    {
                        Position = position.ToVector2f()
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