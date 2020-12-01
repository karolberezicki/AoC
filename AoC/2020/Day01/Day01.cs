using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AoC._2020.Day01
{
    public class Day01 : ISolution
    {
        public void Execute()
        {
            var expenses= Utils.LoadInputLines()
                .SkipLast(1)
                .Select(int.Parse)
                .ToList();

            var (part1, part2) = Solve(expenses);

            Console.WriteLine($"Solve {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        private static (int part1, int part2) Solve(IReadOnlyList<int> expenses)
        {
            var part1 = 0;
            var part2 = 0;
            for (var i = 0; i < expenses.Count; i++)
            {
                for (var j = 0; j < expenses.Count; j++)
                {
                    if (expenses[i] + expenses[j] == 2020)
                    {
                        part1 = expenses[i] * expenses[j];
                    }

                    for (var k = 0; k < expenses.Count; k++)
                    {

                        if (expenses[i] + expenses[j] + expenses[k] == 2020)
                        {
                            part2 = expenses[i] * expenses[j] * expenses[k];
                        }

                        if (part1 != 0 && part2 != 0)
                        {
                            return (part1, part2);
                        }
                    }
                }
            }

            return (part1, part2);
        }
    }
}
