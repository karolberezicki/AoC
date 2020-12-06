using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day05
{
    public class Day05 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var seatIds = input.Select(GetSeatId).ToList();

            var part1 = seatIds.Max();
            var part2 = Enumerable.Range(0, 128 * 8).Except(seatIds)
                .First(i => i > 100 && i < 800);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int GetSeatId(string line)
        {
            var row = Convert.ToInt32(string.Join("", line.Take(7)).Replace('F', '0').Replace('B', '1'), 2);
            var column = Convert.ToInt32(string.Join("", line.Skip(7).Take(3)).Replace('L', '0').Replace('R', '1'), 2);
            return row * 8 + column;
        }
    }
}
