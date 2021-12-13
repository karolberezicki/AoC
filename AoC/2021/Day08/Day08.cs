using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day08
{
    public class Day08 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .SkipLast(1)
                .Select(l => l.Split(" | "))
                .Select(c => (Signal: c[0].Split(" "), Output: c[1].Split(" "))).ToList();

            var uniqueDigitsSegmentCounts = new List<int> { 2, 4, 3, 7 };
            var part1 = input.SelectMany(l => l.Output).Count(o => uniqueDigitsSegmentCounts.Contains(o.Length));

            var part2 = "";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
