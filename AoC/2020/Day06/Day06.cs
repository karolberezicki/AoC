using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day06
{
    public class Day06 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines();

            var lettersDictionary = GetLettersDict();
            var group = 0;
            var part1 = 0;
            var part2 = 0;

            foreach (var line in input)
            {
                if (line == string.Empty)
                {
                    part1 += lettersDictionary.Count(c => c.Value != 0);

                    part2 += lettersDictionary.Count(c => c.Value == group);

                    lettersDictionary = GetLettersDict();
                    group = 0;
                    continue;
                }

                group++;

                foreach (var c in line)
                {
                    lettersDictionary[c] = lettersDictionary[c] + 1;
                }
            }


            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static Dictionary<char, int> GetLettersDict()
        {
            var lettersDictionary = new Dictionary<char, int>();
            for (var i = 'a'; i <= 'z'; i++)
            {
                lettersDictionary[i] = 0;
            }

            return lettersDictionary;
        }
    }
}
