using System;
using System.Linq;

namespace AoC._2020.Day13
{
    public class Day13 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().ToList();

            var timestamp = int.Parse(input[0]);
            var timetable = input[1]
                .Split(",")
                .Where(c => c != "x")
                .Select(int.Parse)
                .ToList();


            var departureList = timetable.Select(id => (BusId: id, WaitTime: id - timestamp % id)).ToList();

            var (busId, waitTime) = departureList.OrderBy(d => d.WaitTime).First();
            var part1 = busId * waitTime;



            var part2 = "";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
