using System;
using System.Linq;

namespace AoC._2021.Day02
{
    public class Day02 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .SkipLast(1)
                .Select(c => c.Split(' '))
                .Select(c => (Action: c[0], Value: int.Parse(c[1])))
                .ToList();

            var horizontal = 0;
            var aim = 0;
            var depth = 0;

            foreach (var (action, value) in input)
            {
                switch (action)
                {
                    case "forward":
                        horizontal += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }

            var part1 = horizontal * aim;
            var part2 = horizontal * depth;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
