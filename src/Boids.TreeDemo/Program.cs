using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Boids.Demo;
using Boids.Simulation.Archetypes;
using Boids.Simulation.Components;
using Boids.Simulation.Helpers;
using Boids.Simulation.Systems.SpatialPartitioning.KdTree;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Boids.TreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var windowSize = new Vector2u(1920, 1080);
            uint numberOfBoids = 20;
            var window = new RenderWindow(new VideoMode(windowSize.X, windowSize.Y), "Boids");
            var step = false;

            window.Closed += OnClose;
            window.KeyPressed += delegate (object? sender, KeyEventArgs eventArgs)
            {
                if (eventArgs.Code == Keyboard.Key.Space)
                {
                    step = true;
                }
            };
            window.KeyReleased += delegate (object? sender, KeyEventArgs eventArgs)
            {
                if (eventArgs.Code == Keyboard.Key.Space)
                {
                    step = false;
                }
            };

            var points = new[] {new KdVector2(RandomPoint(windowSize)) };
            var tree = new KdTree<KdVector2>(points, 2);
            var treeDrawing = KdTreeDrawing.PointsOnGrid(new Vector2(windowSize.X, windowSize.Y), tree);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                if (step)
                {
                    tree.Insert(new KdVector2(RandomPoint(windowSize)));
                    treeDrawing = KdTreeDrawing.PointsOnGrid(new Vector2(windowSize.X, windowSize.Y), tree);
                    step = false;
                }

                window.Draw(treeDrawing);
                window.Display();
            }
        }

        private static Vector2 RandomPoint(Vector2u windowSize)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return new Vector2(random.Next((int)windowSize.X), random.Next((int)windowSize.Y));
        }

        public static void OnClose(object? sender, EventArgs args)
        {
            if (sender == null)
                throw new NullReferenceException("Window is null in OnClose event handler");

            ((Window)sender).Close();
        }
    }
}
