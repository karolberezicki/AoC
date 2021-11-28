using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day03
{
    public class Day03 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines();

            //input = new[] {"#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2"}; // Sample data

            var claims = input
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(c => new MaterialClaim(c))
                .ToHashSet();

            var allPoints = claims.SelectMany(c => c.Points).ToList();

            var seenPoints = new HashSet<(int X, int Y)>();
            var overlappingPoints = new HashSet<(int X, int Y)>();

            foreach (var point in allPoints)
            {
                if (seenPoints.Contains(point))
                {
                    overlappingPoints.Add(point);
                }
                else
                {
                    seenPoints.Add(point);
                }
            }

            var part1 = overlappingPoints.Count;
            var part2 = claims.First(claim => !claim.Points.Any(overlappingPoints.Contains)).Id;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
