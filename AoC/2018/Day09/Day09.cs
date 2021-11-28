using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2018.Day09
{
    public class Day09 : ISolution
    {
        public void Execute()
        {
            var part1 = CalculateHighScore(GetGameData());
            var part2 = CalculateHighScore(GetGameData(100));

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
        
        private static long CalculateHighScore((int Players, int LastMarbleWorth) game)
        {
            var (players, lastMarbleWorth) = game;

            var elfScores = Enumerable.Range(0, players).Select(p => new List<long>()).ToList();
            var marbleCircle = new LinkedList<int>(new []{0});
            var currentMarble = marbleCircle.First;

            for (var pickedMarble = 1; pickedMarble < lastMarbleWorth; pickedMarble++)
            {
                if (pickedMarble % 23 == 0)
                {
                    currentMarble = currentMarble.CircleBackward(marbleCircle, 7);
                    elfScores[(pickedMarble - 1) % players].AddRange(new long[] { pickedMarble, currentMarble.Value });
                    var nextMarble = currentMarble.CircleForward(marbleCircle);
                    marbleCircle.Remove(currentMarble);
                    currentMarble = nextMarble;
                }
                else
                {
                    currentMarble = currentMarble.CircleForward(marbleCircle);
                    var addedMarble = new LinkedListNode<int>(pickedMarble);
                    marbleCircle.AddAfter(currentMarble, addedMarble);
                    currentMarble = addedMarble;
                }
            }

            return elfScores.Select(s => s.Sum()).Max();
        }

        private static (int Players, int LastMarbleWorth) GetGameData(int scoreMultiplier = 1)
        {
            var input = Utils.LoadInput();
            var descriptionRegex = new Regex(@"(?<Players>\d+) players; last marble is worth (?<Points>\d+) points");
            var match = descriptionRegex.Match(input);
            return (int.Parse(match.Groups["Players"].Value), int.Parse(match.Groups["Points"].Value)*scoreMultiplier);
        }
    }
}
