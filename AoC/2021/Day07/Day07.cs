using System;
using System.Linq;

namespace AoC._2021.Day07
{
    public class Day07 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput().Split(',').Select(int.Parse).ToList();
            var min = input.Min();
            var max = input.Max();

            var part1 = Enumerable.Range(min, max - min)
                .Select(move => input.Select(position => Math.Abs(position - move)).Sum()).Min();

            var part2 = Enumerable.Range(min, max - min)
                .Select(move => input.Select(position => Enumerable.Range(1, Math.Abs(position - move)).Sum()).Sum()).Min();

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
