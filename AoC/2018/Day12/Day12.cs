using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day12
{
    public class Day12 : ISolution
    {
        private record Note(string Pattern, char Result);

        private record Pot(int Index, char Plant);

        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var initialState = input.First().Replace("initial state: ", "").Trim().ToList();
            var notes = input.Skip(2).Select(c => c.Split(" => "))
                .Select(n => new Note(n[0], n[1].First())).ToList();

            var part1 = SumAfterTwentyGenerations(initialState, notes);
            var part2 = SumAfterFiftyBillionGenerations(initialState, notes);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static long SumAfterFiftyBillionGenerations(IEnumerable<char> initialState, IReadOnlyCollection<Note> notes)
        {
            var pots = InitPots(initialState);

            var results = new List<(int Iteration, int Result)>();

            var iteration = 0;
            while (results.Count < 2)
            {
                iteration++;

                pots = UpdatePots(notes, pots);

                if (iteration % 1000 != 0)
                {
                    continue;
                }

                var sumOfPots = pots.Where(p => p.Plant == '#').Select(p => p.Index).Sum();
                results.Add((iteration, sumOfPots));
            }

            const long generations = 50000000000;

            var sumOfPotsAfterFiftyBillionGenerations =
                (generations - 1000) / 1000 * (results[1].Result - results[0].Result) + results[0].Result;
            return sumOfPotsAfterFiftyBillionGenerations;
        }

        private static int SumAfterTwentyGenerations(IEnumerable<char> initialState, IReadOnlyCollection<Note> notes)
        {
            var pots = InitPots(initialState);

            for (var i = 1; i <= 20; i++)
            {
                pots = UpdatePots(notes, pots);
            }

            return pots.Where(p => p.Plant == '#').Select(p => p.Index).Sum();
        }

        private static List<Pot> UpdatePots(IReadOnlyCollection<Note> notes, List<Pot> pots)
        {
            var updatedPots = new List<Pot>();

            for (var index = 0; index < pots.Count; index++)
            {
                var leftEdgeIndex = index - 2;
                var leftIndex = index - 1;
                var rightIndex = index + 1;
                var rightEdgeIndex = index + 2;

                var leftEdge = pots.ElementAtOrDefault(leftEdgeIndex)?.Plant ?? '.';
                var left = pots.ElementAtOrDefault(leftIndex)?.Plant ?? '.';
                var right = pots.ElementAtOrDefault(rightIndex)?.Plant ?? '.';
                var rightEdge = pots.ElementAtOrDefault(rightEdgeIndex)?.Plant ?? '.';

                var pot = pots[index];

                var pattern = $"{leftEdge}{left}{pot.Plant}{right}{rightEdge}";

                var updatedPot = new Pot(pot.Index, notes.FirstOrDefault(n => n.Pattern == pattern)?.Result ?? '.');
                updatedPots.Add(updatedPot);
            }

            pots = updatedPots;
            var lastPot = pots.Last();
            updatedPots.Add(new Pot(lastPot.Index + 1, '.'));
            return pots;
        }

        private static List<Pot> InitPots(IEnumerable<char> initialState)
        {
            var pots = initialState.Select((plant, index) => new Pot(index, plant)).ToList();
            for (var i = 1; i < 10; i++)
            {
                pots.Add(new Pot(-1 * i, '.'));
            }

            return pots.OrderBy(p => p.Index).ToList();
        }
    }
}
