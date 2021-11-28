using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day10
{
    public class Day10 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var points = input.Select(s => new Point(s, 0)).ToList();
            var requiredDeltaX = points.Where(p => p.DeltaX != 0).Select(p => p.X / p.DeltaX).Max();
            var requiredDeltaY = points.Where(p => p.DeltaY != 0).Select(p => p.Y / p.DeltaY).Max();
            var initTime = Math.Max(Math.Abs(requiredDeltaX), Math.Abs(requiredDeltaY));
            points = input.Select(s => new Point(s, initTime)).ToList();

            var passedSeconds = 0;

            while (!DetectEdge(points))
            {
                passedSeconds++;
                foreach (var point in points)
                {
                    point.Move();
                }
            }

            Normalize(points);
            Print(points);

            const string part1 = "EKALLKLB"; // Read from console output
            var part2 = passedSeconds + initTime;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static void Print(IReadOnlyCollection<Point> points)
        {
            Console.WriteLine();
            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 80; x++)
                {
                    Console.Write(points.Any(p => p.InPosition(x, y)) ? "█" : " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void Normalize(IReadOnlyCollection<Point> points)
        {
            var minX = points.Select(p => p.X).Min();
            var minY = points.Select(p => p.Y).Min();

            foreach (var point in points)
            {
                point.X -= minX;
                point.Y -= minY;
            }
        }

        private static bool DetectEdge(IReadOnlyCollection<Point> points)
        {
            return points.Any(point => new[]
            {
                points.Any(p => p.X == point.X && p.Y == point.Y - 1),
                points.Any(p => p.X == point.X && p.Y == point.Y - 2),
                points.Any(p => p.X == point.X && p.Y == point.Y - 3),
                points.Any(p => p.X == point.X + 1 && p.Y == point.Y),
                points.Any(p => p.X == point.X + 2 && p.Y == point.Y),
                points.Any(p => p.X == point.X + 3 && p.Y == point.Y)
            }.All(c => c));
        }
    }
}
