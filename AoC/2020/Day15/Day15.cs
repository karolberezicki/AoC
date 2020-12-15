using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day15
{
    public class Day15 : ISolution
    {
        public void Execute()
        {
            const string input = "12,1,16,3,11,0";
            var numbers = input
                .Split(",")
                .Select(int.Parse)
                .ToList();

            var part1 = FindNumberSpoken(numbers, 2020);
            var part2 = FindNumberSpoken(numbers, 30000000);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int FindNumberSpoken(IReadOnlyCollection<int> startingNumbers, int to)
        {
            var lastNumberSpoken = startingNumbers.Last();

            var memory = startingNumbers
                .SkipLast(1)
                .Select((s, i) => new { s, i })
                .ToDictionary(x => x.s, x => x.i + 1);

            var turn = startingNumbers.Count;

            while (turn < to)
            {
                if (!memory.ContainsKey(lastNumberSpoken) || memory[lastNumberSpoken] == 0)
                {
                    memory[lastNumberSpoken] = turn;
                    lastNumberSpoken = 0;
                }
                else
                {
                    var diff = turn - memory[lastNumberSpoken];
                    memory[lastNumberSpoken] = turn;
                    lastNumberSpoken = diff;
                }

                turn++;
            }

            return lastNumberSpoken;
        }
    }
}
