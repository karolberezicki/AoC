using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day05
{
    public class Day05 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1)
                .Select(l => l.Split(" -> ")
                    .Select(c=> c.Split(',').Select(int.Parse).ToList()).ToList())
                .Select(l => (X1: l[0][0], Y1: l[0][1], X2: l[1][0], Y2: l[1][1])).ToList();

            var part1 = OverlappingPointsCount(input);
            var part2 = OverlappingPointsCount(input, true);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int OverlappingPointsCount(List<(int X1, int Y1, int X2, int Y2)> input, bool part2 = false)
        {
            var allLinePoints = input.Select(tuple => CalcPointsInLine(tuple, part2)).ToList();
            var visitedPoint = new HashSet<(int X, int Y)>();
            var overlappingPoints = new HashSet<(int X, int Y)>();
            foreach (var point in allLinePoints.SelectMany(points => points))
            {
                if (visitedPoint.Contains(point))
                {
                    overlappingPoints.Add(point);
                }
                else
                {
                    visitedPoint.Add(point);
                }
            }

            return overlappingPoints.Count;
        }

        private static HashSet<(int X, int Y)> CalcPointsInLine((int X1, int Y1, int X2, int Y2) line, bool part2 = false)
        {
            var points = new HashSet<(int, int)>();

            if (line.X1 == line.X2)
            {
                var y1 = Math.Min(line.Y1, line.Y2);
                var y2 = Math.Max(line.Y1, line.Y2);
                for (var y = y1; y <= y2; y++)
                {
                    points.Add((line.X1, y));
                }
            }

            else if (line.Y1 == line.Y2)
            {
                var x1 = Math.Min(line.X1, line.X2);
                var x2 = Math.Max(line.X1, line.X2);
                for (var x = x1; x <= x2; x++)
                {
                    points.Add((x, line.Y1));
                }
            }

            else if (part2)
            {
                var x = line.X1;
                var y = line.Y1;
                var dx = Math.Sign(line.X2 - line.X1);
                var dy = Math.Sign(line.Y2 - line.Y1);
                do
                {
                    points.Add((x, y));
                    x += dx;
                    y += dy;
                   
                } while (x != line.X2 || y != line.Y2);
                points.Add((x, y));
            }

            return points;
        }
    }
}
