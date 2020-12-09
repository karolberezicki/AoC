using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day09
{
    public class Day09 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .SkipLast(1)
                .Select(long.Parse)
                .ToList();

            var part1 = 0L;
            for (var index = 25; index < input.Count; index++)
            {
                var number = input[index];

                var isValid = false;
                for (var i = index - 25; i < index + 25; i++)
                {
                    for (var j = index - 25; j < index + 25; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (input[index] == input[i] + input[j])
                        {
                            isValid = true;
                        }
                    }
                }

                if (!isValid)
                {
                    part1 = number;
                    break;
                }
            }

            var part2 = 0L;

            for (var index = 0; index < input.Count; index++)
            {

                long sum = 0;
                var possibleContiguousRange = new List<long>();

                for (var i = index; i < input.Count; i++)
                {
                    sum += input[i];
                    possibleContiguousRange.Add(input[i]);

                    if (sum == part1)
                    {
                        possibleContiguousRange = possibleContiguousRange.OrderBy(c => c).ToList();
                        var p1 = possibleContiguousRange.First();
                        var p2 = possibleContiguousRange.Last();
                        part2 = p1 + p2;
                        break;
                    }

                    if (sum > part1)
                    {
                        break;
                    }
                }

                if (part2 != 0)
                {
                    break;
                }

            }

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
