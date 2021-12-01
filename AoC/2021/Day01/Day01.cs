using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day01
{
    public class Day01 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).Select(int.Parse).ToList();

            var part1 = Solve(input);
            var part2 = Solve(input, 3);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int Solve(IReadOnlyCollection<int> input, int shift = 1)
        {
            return input
                .Skip(shift)
                .Zip(input, (curr, prev) => curr > prev ? 1 : 0)
                .Sum();
        }
    }
}
