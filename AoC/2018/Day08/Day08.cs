using System;
using System.Linq;

namespace AoC._2018.Day08
{
    public class Day08 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput();
            
            var license = input.Split(" ").Select(int.Parse).ToList();

            var (node, _) = Node.CreateNode(license, 0);
            var part1 = node.SumMetadata();
            var part2 = node.CalcValue();

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
