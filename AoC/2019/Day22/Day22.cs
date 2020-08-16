using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AoC._2019.Day22
{
    public class Day22 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .ToList();

            var shuffleInstructions = ParseInput(input);

            var part1 = Part1(shuffleInstructions);
            Console.WriteLine($"Part1 {part1}");

            var part2 = Part2(shuffleInstructions);
            Console.WriteLine($"Part2 {part2}");
        }

        private static long Part1(IEnumerable<(Techniques Technique, int Value)> shuffleInstructions)
        {
            const long card = 2019;
            const long cardsCount = 10007;
            var cardIndex = card;

            foreach (var (technique, value) in shuffleInstructions)
            {
                switch (technique)
                {
                    case Techniques.DealIntoNewStack:
                        cardIndex = cardsCount - cardIndex - 1;
                        break;
                    case Techniques.Cut:
                        if (value > 0)
                        {
                            cardIndex = cardsCount - value + cardIndex;
                        }
                        else
                        {
                            cardIndex -= value;
                        }
                        if (cardIndex > cardsCount)
                        {
                            cardIndex -= cardsCount;
                        }
                        break;
                    case Techniques.DealWithIncrement:
                        var a = cardIndex * value;
                        var b = (long) (1.0d * value * cardIndex / cardsCount);
                        cardIndex = a - b * cardsCount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return cardIndex;
        }

        /// <summary>
        ///     Based on explanation by /u/mcpower_/
        ///     https://www.reddit.com/r/adventofcode/comments/ee0rqi/2019_day_22_solutions/fbnkaju/
        /// </summary>
        private static BigInteger Part2(IEnumerable<(Techniques Technique, int Value)> shuffleInstructions)
        {
            const int card = 2020;
            var size = new BigInteger(119315717514047L);
            var iterations = new BigInteger(101741582076661L);

            var incrementMul = new BigInteger(1);
            var offsetDiff = new BigInteger(0);

            foreach (var (technique, value) in shuffleInstructions)
            {
                switch (technique)
                {
                    case Techniques.Cut:
                        offsetDiff += value * incrementMul;
                        break;
                    case Techniques.DealIntoNewStack:
                        incrementMul *= -1;
                        offsetDiff += incrementMul;
                        break;
                    case Techniques.DealWithIncrement:
                        incrementMul *= BigInteger.ModPow(value, size - 2, size);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                incrementMul = Modulus(incrementMul, size);
                offsetDiff = Modulus(offsetDiff, size);
            }

            var increment = BigInteger.ModPow(incrementMul, iterations, size);
            var offset = offsetDiff * (1 - increment);
            offset *= BigInteger.ModPow((1 - incrementMul) % size, size - 2, size);
            offset = Modulus(offset, size);
            var cardIndex = Modulus(offset + card * increment, size);
            return cardIndex;
        }

        private static BigInteger Modulus(BigInteger a, BigInteger b)
        {
            return (BigInteger.Abs(a * b) + a) % b;
        }

        private static List<(Techniques Technique, int Value)> ParseInput(IEnumerable<string> input)
        {
            return input
                // Sanitize input... https://xkcd.com/327/
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i =>
                {
                    if (i.Contains("cut "))
                    {
                        return (Techniques.Cut, int.Parse(i.Split(" ").Last()));
                    }
                    if (i.Contains("deal with increment "))
                    {
                        return (Techniques.DealWithIncrement, int.Parse(i.Split(" ").Last()));
                    }
                    return (Techniques.DealIntoNewStack, 0);
                }).ToList();
        }
    }
}
