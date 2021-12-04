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

            var drawnNumbers = new List<int>();
            var cardsThatWon = new List<(Guid Id, List<int> DrawnCards)>();
            foreach (var drawNumber in drawNumbers)
            {
                drawnNumbers.Add(drawNumber);
                var winningCards = bingoCards
                    .Where(b => !cardsThatWon.Select(c => c.Id).Contains(b.Id) && b.IsWinning(drawnNumbers))
                    .ToList();
                cardsThatWon.AddRange(winningCards.Select(winningCard => (winningCard.Id, drawnNumbers.ToList())));
            }

            var part1 = GetScore(bingoCards.First(b => b.Id == cardsThatWon.First().Id), cardsThatWon.First().DrawnCards);
            var part2 = GetScore(bingoCards.First(b => b.Id == cardsThatWon.Last().Id), cardsThatWon.Last().DrawnCards);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int GetScore(BingoCard winningCard, ICollection<int> drawnNumbers)
        {
            var unmarked = winningCard.Numbers.SelectMany(n => n).Where(n => !drawnNumbers.Contains(n));
            return unmarked.Sum() * drawnNumbers.Last();
        }

        private class BingoCard
        {
            public Guid Id { get; }
            public List<List<int>> Numbers { get; }
            private List<List<int>> NumbersTransposed { get; }

            public BingoCard(IEnumerable<string> cardData)
            {
                Id = Guid.NewGuid();
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
                    for (var j = 0; j < Numbers[i].Count; j++)
                    {
                        list.Add(Numbers[j][i]);
                    }
                    NumbersTransposed.Add(list);
                }
            }

            public bool IsWinning(IReadOnlyCollection<int> drawnNumbers)
            {
                var rowWin = Numbers.Any(row => drawnNumbers.Intersect(row).Count() == row.Count);
                var columnWin = NumbersTransposed.Any(column => drawnNumbers.Intersect(column).Count() == column.Count);
                return rowWin || columnWin;
            }
        }
    }
}
