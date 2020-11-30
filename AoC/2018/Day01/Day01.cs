using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day01
{
    public class Day01 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .SkipLast(1)
                .Select(int.Parse)
                .ToList();

            var part1 = input.Sum();
            var part2 = FindFirstRepeatedFrequency(input);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int FindFirstRepeatedFrequency(IReadOnlyCollection<int> deltas)
        {
            var currentFrequency = 0;
            var seenFrequencies = new HashSet<int> {currentFrequency};
            while (true)
            {
                foreach (var delta in deltas)
                {
                    currentFrequency += delta;
                    if (seenFrequencies.Contains(currentFrequency))
                    {
                        return currentFrequency;
                    }

                    seenFrequencies.Add(currentFrequency);
                }
            }
        }
    }
}
