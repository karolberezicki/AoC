using System;
using System.Diagnostics;
using System.Linq;

namespace AoC._2022.Day02
{
    public class Day02 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1)
                .Select(line =>
                {
                    var lineSplit = line.Split(" ");
                    return (Opponent: (byte)(lineSplit[0].First() - 64), Player: (byte)(lineSplit[1].First() - 87));
                }).ToList();

            var part1 = input.Sum(c => CalculateScore(c.Opponent, c.Player));
            var part2 = input.Sum(c => CalculateStrategy(c.Opponent, c.Player));

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int CalculateScore(int opponent, int player)
        {
            return player + CalculateOutcome(opponent,player);
        }
        
        private static int CalculateOutcome(int opponent, int player)
        {
            if (player == opponent)
            {
                return 3; // draw
            }

            if (player == 1 && opponent == 3 || player == 2 && opponent == 1 || player == 3 && opponent == 2)
            {
                return 6; // win
            }

            return 0; // lose
        }
        
        private static int CalculateStrategy(int opponent, int outcome)
        {
            if (outcome == 2)
            {
                return opponent + 3; // draw
            }

            return (opponent, outcome) switch
            {
                (1, 1) => 3, // rock     - lose (use scissors 3)
                (1, 3) => 8, // rock     - win (2+6)
                (2, 1) => 1, // paper    - lose (use rock 1)
                (2, 3) => 9, // paper    - win (3+6)
                (3, 1) => 2, // scissors - lose (use paper 2)
                (3, 3) => 7, // scissors - win (1+6)
                _ => throw new UnreachableException(),
            };
        }
    }
}
