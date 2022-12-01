using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2022.Day01
{
    public class Day01 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines();

            var elfFood = new List<int>();
            var tempElf = new List<int>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elfFood.Add(tempElf.Sum());
                    tempElf.Clear();
                }
                else
                {
                    tempElf.Add(int.Parse(line));
                }
            }

            var part1 = elfFood.Max();
            var part2 = elfFood.OrderDescending().Take(3).Sum();

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
