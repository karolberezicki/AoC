using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2018.Day04
{
    public class Day04 : ISolution
    {
        private record Guard(int Id, List<int> MinutesAsleep);

        public void Execute()
        {
            var input = Utils.LoadInputLines();

            var guards = CalcGuardsSleep(input);

            var selectedGuardByStrategyOne = guards.OrderByDescending(g => g.MinutesAsleep.Count).First();
            var mostFrequentMinute = GetMostFrequentSleepMinute(selectedGuardByStrategyOne).Minute;

            var part1 = selectedGuardByStrategyOne.Id * mostFrequentMinute;

            var selectedGuardByStrategyTwo = guards.Where(g => g.MinutesAsleep.Count > 0)
                .Select(g => new { g.Id, MinuteCount = GetMostFrequentSleepMinute(g) })
                .OrderByDescending(g => g.MinuteCount.Count)
                .First();

            var part2 = selectedGuardByStrategyTwo.Id * selectedGuardByStrategyTwo.MinuteCount.Minute;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
        
        private static HashSet<Guard> CalcGuardsSleep(IEnumerable<string> log)
        {
            var guards = new HashSet<Guard>();
            var chronologicalLog = log.OrderBy(c => c).ToList();
            var currentGuardId = 0;
            var currentSleepStart = 0;
            var guardIdRegex = new Regex(@"^.{19}Guard.{2}(?<Id>\d+)");
            var minutesRegex = new Regex(@"^.{15}(?<Minutes>\d{2})");

            foreach (var entry in chronologicalLog)
            {
                if (entry.Contains("Guard"))
                {
                    currentGuardId = int.Parse(guardIdRegex.Match(entry).Groups["Id"].Value);
                    if (guards.All(g => g.Id != currentGuardId))
                    {
                        guards.Add(new Guard(currentGuardId, new List<int>()));
                    }
                    continue;
                }

                if (entry.Contains("falls asleep"))
                {
                    currentSleepStart = int.Parse(minutesRegex.Match(entry).Groups["Minutes"].Value);
                    continue;
                }

                if (entry.Contains("wakes up"))
                {
                    var currentSleepEnd = int.Parse(minutesRegex.Match(entry).Groups["Minutes"].Value);
                    var currentSleepMinutes = Enumerable.Range(currentSleepStart, currentSleepEnd - currentSleepStart);
                    guards.First(g => g.Id == currentGuardId).MinutesAsleep.AddRange(currentSleepMinutes);
                }
            }

            return guards;
        }

        private static (int Minute, int Count) GetMostFrequentSleepMinute(Guard guard)
        {
            var i = guard.MinutesAsleep
                .GroupBy(s => s)
                .Select(g => new {Item = g.Key, Count = g.Count()})
                .OrderByDescending(g => g.Count)
                .First();

            return (i.Item, i.Count);
        }
    }
}
