using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day05
{
    public class Day05 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            //input = new List<string>{ "FBFBBFFRLR" };

            var seatIds = new List<int>();

            foreach (var line in input)
            {
                var rowLowerRange = 0;
                var rowUpperRange = 127;

                for (var i = 0; i < 6; i++)
                {
                    if (line[i] == 'F')
                    {
                        rowUpperRange = (rowLowerRange + rowUpperRange) / 2;
                        continue;
                    }

                    if (line[i] == 'B')
                    {
                        rowLowerRange = (rowLowerRange + rowUpperRange) / 2 + 1;
                        continue;
                    }
                }

                var row = line[6] == 'F' ? rowLowerRange : rowUpperRange;

                var columnLowerRange = 0;
                var columnUpperRange = 7;

                for (var i = 7; i < 9; i++)
                {
                    if (line[i] == 'L')
                    {
                        columnUpperRange = (columnLowerRange + columnUpperRange) / 2;
                        continue;
                    }

                    if (line[i] == 'R')
                    {
                        columnLowerRange = (columnLowerRange + columnUpperRange) / 2 + 1;
                        continue;
                    }
                }

                var column = line[9] == 'L' ? columnLowerRange : columnUpperRange;

                var seatId = row * 8 + column;

                seatIds.Add(seatId);

            }

            var part1 = seatIds.Max();
            var part2 = Enumerable.Range(0, 128 * 8).Except(seatIds)
                .First(i => i > 100 && i < 800);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
