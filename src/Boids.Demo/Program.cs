using Boids.Simulation.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var boids = EvenlySpacedBoids(windowSize, numberOfBoids);
            var drawableBoids = new List<DrawableBoid>();
            drawableBoids.AddRange(boids.Select(b => new DrawableBoid(b.Position)));

            while (window.IsOpen)
            {
                window.DispatchEvents();

                foreach (var (boid, index) in boids.Select((v, i) => (v, i)))
                {
                }

                window.Clear();
                foreach (var drawable in drawableBoids)
                {
                    window.Draw(drawable);
                }
                window.Display();
            }
        }

        private static IEnumerable<Boid> EvenlySpacedBoids(Vector2u windowSize, uint numberOfBoids)
        {
            var xSpacing = windowSize.X / numberOfBoids;
            var ySpacing = windowSize.Y / numberOfBoids;

            for (int i = 0; i <= numberOfBoids; i++)
            {
                yield return new Boid
                {
                    Acceleration = new Vector2f(),
                    Position = new Vector2f(xSpacing * i, ySpacing * i),
                    Target = new Vector2f(windowSize.X / 2, windowSize.Y / 2)
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