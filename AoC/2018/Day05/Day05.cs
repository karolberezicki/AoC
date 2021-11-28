using System;
using System.Text;

namespace AoC._2018.Day05
{
    public class Day05 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput().Trim();

            var sourcePolymerAfterReaction = PerformReaction(new StringBuilder(input));

            var shortestPolymerLength = int.MaxValue;
            for (var i = 65; i <= 90; i++)
            {
                var polymer = new StringBuilder(sourcePolymerAfterReaction);
                polymer.Replace(((char)i).ToString(), "");
                polymer.Replace(((char)(i + 32)).ToString(), "");

                var polymerAfterReaction = PerformReaction(polymer);

                if (polymerAfterReaction.Length < shortestPolymerLength)
                {
                    shortestPolymerLength = polymerAfterReaction.Length;
                }
            }

            var part1 = sourcePolymerAfterReaction.Length;
            var part2 = shortestPolymerLength;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static string PerformReaction(StringBuilder polymer)
        {
            var reacted = true;
            while (reacted)
            {
                reacted = false;
                for (var index = 0; index < polymer.Length - 1; index++)
                {
                    var unit = polymer[index];
                    var nextUnit = polymer[index + 1];

                    if (Math.Abs(unit - nextUnit) - 32 != 0)
                    {
                        continue;
                    }

                    reacted = true;
                    polymer.Remove(index, 2);

                    break;
                }
            }

            return polymer.ToString();
        }
    }
}
