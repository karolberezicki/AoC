using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day08
{
    public class Day08 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .SkipLast(1)
                .Select(l => l.Split(" | "))
                .Select(c => (Signal: c[0].Split(" "), Output: c[1].Split(" "))).ToList();

            var uniqueDigitsSegmentCounts = new List<int> { 2, 4, 3, 7 };
            var part1 = input.SelectMany(l => l.Output).Count(o => uniqueDigitsSegmentCounts.Contains(o.Length));

            var outputWithDigitsMap = input.Select(l => (l.Output, Map: MapSignalToDigits(l.Signal.Concat(l.Output).ToList()))).ToList();

            var part2 = outputWithDigitsMap.Sum(d => int.Parse(new string(d.Output.Select(o => d.Map[SortString(o)]).ToArray())));

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static Dictionary<string, char> MapSignalToDigits(IReadOnlyCollection<string> signal)
        {
            var one = signal.First(s => s.Length == 2);
            var four = signal.First(s => s.Length == 4);
            var seven = signal.First(s => s.Length == 3);
            var eight = signal.First(s => s.Length == 7);

            var three = signal.First(s => s.Length == 5 && seven.All(s.Contains));
            var five = signal.First(s => s.Length == 5 && s != three && four.Intersect(s).Count() == 3);
            var two = signal.First(s => s.Length == 5 && s != three && s != five);

            var nine = signal.First(s => s.Length == 6 && four.All(s.Contains));
            var zero = signal.First(s => s.Length == 6 && s != nine && seven.All(s.Contains));
            var six = signal.First(s => s.Length == 6 && s != nine && s != zero);
            
            var map = new Dictionary<string, char>
            {
                [SortString(zero)] = '0',
                [SortString(one)] = '1',
                [SortString(two)] = '2',
                [SortString(three)] = '3',
                [SortString(four)] = '4',
                [SortString(five)] = '5',
                [SortString(six)] = '6',
                [SortString(seven)] = '7',
                [SortString(eight)] = '8',
                [SortString(nine)] = '9'
            };

            return map;
        }

        private static string SortString(string input)
        {
            // Learned:
            // https://stackoverflow.com/a/6441603/7454424
            var characters = input.ToCharArray(); // https://stackoverflow.com/a/37139124/7454424
            Array.Sort(characters);
            return new string(characters);
        }
    }
}
