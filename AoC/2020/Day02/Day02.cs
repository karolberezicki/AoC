using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2020.Day02
{
    public class Day02 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines();

            var passwords = input
                .SkipLast(1)
                .Select(p => new Password(p))
                .ToList();

            var part1 = passwords.Count(p => p.IsValid);
            var part2 = passwords.Count(p => p.IsValid2);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private class Password
        {
            private char LetterRule { get; }
            private int Min { get; }
            private int Max { get; }
            private string Value { get; }

            public Password(string str)
            {
                const string pattern = @"^(\d+)-(\d+) ([a-z]): ([a-z]+)";
                var match = Regex.Match(str, pattern);
                Min = int.Parse(match.Groups[1].Value);
                Max = int.Parse(match.Groups[2].Value);
                LetterRule = match.Groups[3].Value.First();
                Value = match.Groups[4].Value;
            }

            private int Counter => Value.Count(c => c == LetterRule);
            public bool IsValid => Counter >= Min && Counter <= Max;
            public bool IsValid2 => Value[Min - 1] == LetterRule ^ Value[Max - 1] == LetterRule;
        }
    }
}
