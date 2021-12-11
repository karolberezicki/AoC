using System;
using System.Linq;

namespace AoC._2021.Day07
{
    public class Day07 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput().Split(',').Select(int.Parse).ToList();

            var move1 = input.OrderBy(i => i).ToList()[input.Count / 2];
            var part1 = input.Select(position => Math.Abs(position - move1)).Sum();

            var move2 = (int)Math.Floor(input.Average());
            var part2 = input.Select(position => Enumerable.Range(1, Math.Abs(position - move2)).Sum()).Sum();

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
