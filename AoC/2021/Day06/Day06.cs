using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day06
{
    public class Day06 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput().Split(',').Select(int.Parse);
            var lanternfish = Enumerable.Range(0, 9)
                .Select(i => (long)input.Count(f => f == i))
                .ToList();

            const int days = 80;
            const int moreDays = 256 - days;

            for (var cycle = 0; cycle < days; cycle++)
            {
                lanternfish = RunCycle(lanternfish);
            }

            var part1 = lanternfish.Sum();

            for (var i = 0; i < moreDays; i++)
            {
                lanternfish = RunCycle(lanternfish);
            }

            var part2 = lanternfish.Sum();

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static List<long> RunCycle(List<long> lanternfish)
        {
            var born = lanternfish[0];
            lanternfish = Enumerable.Range(1, 8).Select(i => lanternfish[i]).ToList();
            lanternfish[6] += born;
            lanternfish.Add(born);
            return lanternfish;
        }
    }
}
