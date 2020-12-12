using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day10
{
    public class Day10 : ISolution
    {
        private List<int> _input;
        private int _maxValue;
        private readonly Dictionary<(int, int), long> _cache = new Dictionary<(int, int), long>();

        public void Execute()
        {
            var inputLines = Utils.LoadInputLines();

            _input = inputLines.SkipLast(1).Select(int.Parse).ToList();
            _input.Sort();
            _maxValue = _input.Max();

            var diff1 = 1;
            var diff3 = 1;

            for (var i = 1; i < _input.Count; i++)
            {
                var delta = _input[i] - _input[i-1];
                switch (delta)
                {
                    case 1:
                        diff1++;
                        break;
                    case 3:
                        diff3++;
                        break;
                }
            }

            var part1 = diff1 * diff3;
            var part2 = Part2(_input.Count, 0);
            
            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private long Part2(int joltsLeft, int last)
        {
            if (joltsLeft < 0)
            {
                return 0;
            }

            if (last == _maxValue)
            {
                return 1;
            }

            var total = 0L;

            for (var i = 1; i < 4; i++)
            {
                var next = last + i;

                if (!_input.Contains(next))
                {
                    continue;
                }

                if (!_cache.ContainsKey((joltsLeft - 1, next)))
                {
                    _cache[(joltsLeft - 1, next)] = Part2(joltsLeft - 1, next);
                }

                total += _cache[(joltsLeft - 1, next)];
            }

            return total;
        }
    }
}
