using System;
using System.Linq;
using IntCode;

namespace AoC._2019.Day09
{
    public class Day09 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput();

            var memoryState = input.Split(",")
                .Select(long.Parse)
                .ToList();

            var icc = new IntCodeComputer(memoryState, 1);
            icc.RunTillHalt();

            var part1 = icc.Output.First();

            icc = new IntCodeComputer(memoryState, 2);
            icc.RunTillHalt();

            var part2 = icc.Output.First();

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
