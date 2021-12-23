using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day09
{
    public class Day09 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var map = new Dictionary<(int x, int y), int>();

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    map[(x, y)] = input[y][x] - '0';
                }
            }

            var risk = 0;
            foreach (var point in map)
            {
                var pointsAroundCoordinates = point.Key.Around()
                    .Where(p => p.x >= 0 && p.y >= 0 && p.x < input[0].Length && p.y < input.Count);

                var pointsAroundValues = pointsAroundCoordinates.Select(p => map[p]).ToList();

                var lowPoint = pointsAroundValues.All(p => p > point.Value);
                if (lowPoint)
                {
                    risk += point.Value + 1;
                }
            }

            var part1 = risk;
            var part2 = "";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
