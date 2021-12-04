using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day04
{
    public class Day04 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();
            var drawNumbers = input[0].Split(',').Select(int.Parse).ToList();
            var bingoCards = input.Skip(1).Where(c => !string.IsNullOrWhiteSpace(c)).Chunk(5).Select(b => new BingoCard(b)).ToList();

            var part1 = 0;
            var drawnNumbers = new List<int>();
            foreach (var drawNumber in drawNumbers)
            {
                drawnNumbers.Add(drawNumber);
                var winningCard = bingoCards.FirstOrDefault(b => b.IsWinning(drawnNumbers));
                if (winningCard != null)
                {
                    var unmarked = winningCard.Numbers.SelectMany(n => n).Where(n => !drawnNumbers.Contains(n));
                    var unmarkedSum = unmarked.Sum();
                    part1 = unmarkedSum * drawNumber;
                    break;
                }
            }

            var part2 = "";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
        
        private class BingoCard
        {
            public List<List<int>> Numbers { get; }
            public List<List<int>> NumbersTransposed { get; }

            public BingoCard(IEnumerable<string> cardData)
            {
                Numbers = new List<List<int>>();
                NumbersTransposed = new List<List<int>>();
                foreach (var dataRow in cardData)
                {
                    Numbers.Add(dataRow.Split(' ')
                        .Where(c => !string.IsNullOrWhiteSpace(c))
                        .Select(int.Parse).ToList());
                }

                for (var i = 0; i < Numbers.Count; i++)
                {
                    var list = new List<int>();
                    for (var j = 0; j < Numbers[i].Count(); j++)
                    {
                        list.Add(Numbers[j][i]);
                    }
                    NumbersTransposed.Add(list);
                }
            }

            public bool IsWinning(List<int> drawnNumbers)
            {
                var rowWin = Numbers.Any(row => drawnNumbers.Intersect(row).Count() >= 5);
                var columnWin = NumbersTransposed.Any(column => drawnNumbers.Intersect(column).Count() >= 5);

                return rowWin || columnWin;
            }
        }
    }
}
