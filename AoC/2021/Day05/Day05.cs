using System;
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

            var horizontalAndVerticalLines = input.Where(l => l.X1 == l.X2 || l.Y1 == l.Y2).ToList();

            var part1 = "";
            var part2 = "";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
