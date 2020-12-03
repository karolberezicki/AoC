using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day03
{
    public class Day03 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var part1 = Traverse(input, (3, 1));
            var part2 = part1 * Traverse(input, (1, 1)) * Traverse(input, (5, 1)) * Traverse(input, (7, 1)) * Traverse(input, (1, 2));

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static long Traverse(IReadOnlyList<string> map, (int Right, int Down) slope)
        {
            var (right, down) = slope;
            var counter = 0L;
            var x = 0;
            for (var y = 0; y < map.Count; y += down)
            {
                var mapLine = map[y];
                if (mapLine[x] == '#')
                {
                    counter++;
                }

                x += right;
                x = x >= mapLine.Length ? x - mapLine.Length : x;
            }

            return counter;
        }
    }
}
