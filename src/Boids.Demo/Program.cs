using Boids.Simulation.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using Boids.Simulation.Archetypes;

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

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                foreach (var boid in boids)
                {
                    window.Draw(boid.DrawableBoidComponent);
                }
                window.Display();
            }
        }

        private static IEnumerable<Boid> EvenlySpacedBoids(Vector2u windowSize, uint numberOfBoids)
        {
            var xSpacing = windowSize.X / numberOfBoids;
            var ySpacing = windowSize.Y / numberOfBoids;

            for (var i = 0; i <= numberOfBoids; i++)
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