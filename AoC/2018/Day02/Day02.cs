using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day02
{
    public class Day02 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().ToArray();

            var frequencies = input.Select(CountLetterFrequencies).ToList();

            var countIdsWithTwoLetters = frequencies.Count(f => f.ContainsValue(2));
            var countIdsWithThreeLetters = frequencies.Count(f => f.ContainsValue(3));

            var part1 = countIdsWithTwoLetters * countIdsWithThreeLetters;
            var part2 = FindCommonLetters(input);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static string FindCommonLetters(string[] ids)
        {
            foreach (var id in ids)
            {
                foreach (var comparingId in ids)
                {
                    var commonLetters = new List<char>();
                    for (var i = 0; i < comparingId.Length; i++)
                    {
                        if (comparingId[i] == id[i])
                        {
                            commonLetters.Add(id[i]);
                        }
                    }

                    if (commonLetters.Count == id.Length - 1)
                    {
                        return string.Concat(commonLetters);
                    }
                }
            }

            throw new Exception("Not found.");
        }

        private static Dictionary<char, int> CountLetterFrequencies(string id)
        {
            var frequencies = new Dictionary<char, int>();

            foreach (var letter in id)
            {
                frequencies[letter] = frequencies.ContainsKey(letter) ? frequencies[letter] + 1 : 1;
            }

            return frequencies;
        }
    }
}
