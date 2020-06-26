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
using Boids.Simulation.Systems.SpatialPartitioning;

namespace Boids.Demo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var windowSize = new Vector2u(1920, 1080);
            uint numberOfBoids = 200;
            var timeScale = 1f;

            var window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y), "Boids");
            var running = true;

            window.Closed += OnClose;
            window.KeyPressed += delegate (object? sender, KeyEventArgs eventArgs)
            {
                if (eventArgs.Code == Keyboard.Key.Space)
                {
                    running = true;
                }
            };
            window.KeyReleased += delegate (object? sender, KeyEventArgs eventArgs)
            {
                if (eventArgs.Code == Keyboard.Key.Space)
                {
                    running = false;
                }
            };

            //var boids = EvenlySpacedBoids(windowSize, numberOfBoids).ToList();
            var boids = RandomplyPlacedBoids(windowSize, numberOfBoids).ToList();
            var followLeaderSystem = new FollowTheLeader(15.0f);
            var windSystem = new Wind(new Vector2(-1, -1), 0.1f);
            var maxSpeedSystem = new LimitSpeed(0.5f);
            var maintainDistanceSystem = new StayAwayFromNearestBoid(10.0f, 20.0f);
            var frictionSystem = new Friction(0.1f);
            var insideBoundsSystem = new StayInsideBounds(new Vector2(0.0f), new Vector2(windowSize.X, windowSize.Y));
            //var quadTree = new Quadtree(boids.Select(b => b.BoidComponent.Position), new Vector2(0, 0), new Vector2(windowSize.X, windowSize.Y));

            var clock = new Clock();
            var previousFrameTime = clock.ElapsedTime;

            while (window.IsOpen)
            {
                var deltaTime = (Time.FromSeconds(1) / previousFrameTime).AsSeconds();
                deltaTime = deltaTime * timeScale;

                window.DispatchEvents();
                window.Clear();

                if (running)
                {
                    var kdTree = new KdTree(boids.Select(b => b.BoidComponent.Position));
                    boids.ForEach(boid => boid.BoidComponent.NearestNeighbour = kdTree.NearestNeighbour(boid.BoidComponent.Position));
                    boids.ForEach(boid => boid.BoidComponent.Target = AverageFlockCenter.Center(kdTree, boid.BoidComponent.Position, 150.0f));
                    boids.ForEach(boid => followLeaderSystem.Mutate(boid, deltaTime));
                    boids.ForEach(boid => windSystem.Mutate(boid, deltaTime));
                    boids.ForEach(boid => maintainDistanceSystem.Mutate(boid, deltaTime));
                    boids.ForEach(boid => frictionSystem.Mutate(boid, deltaTime));

                    boids.ForEach(boid => maxSpeedSystem.Mutate(boid));
                    boids.ForEach(boid => insideBoundsSystem.Mutate(boid));

                    boids.ForEach(boid => boid.BoidComponent.Position += boid.BoidComponent.Acceleration);
                }

                //quadTree = new Quadtree(boids.Select(b => b.BoidComponent.Position), new Vector2(0, 0), new Vector2(windowSize.X, windowSize.Y));
                //window.Draw(quadTree);

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

        private static IEnumerable<Boid> RandomplyPlacedBoids(Vector2u windowSize, uint numberOfBoids)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            for (var i = 0; i < numberOfBoids; i++)
            {
                var position = new Vector2(random.Next((int)windowSize.X), random.Next((int)windowSize.Y));
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