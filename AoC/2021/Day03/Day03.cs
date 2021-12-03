using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2021.Day03
{
    public class Day03 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var mostCommonBitInPosition = new char[input[0].Length];
            var leastCommonBitInPosition = new char[input[0].Length];
            var inputForOxygen = input.ToList();
            var inputForCarbonDioxide = input.ToList();

            for (var i = 0; i < input[0].Length; i++)
            {
                var (mostCommon, leastCommon) = GetCommonBits(input.Select(b => b[i]));
                mostCommonBitInPosition[i] = mostCommon;
                leastCommonBitInPosition[i] = leastCommon;

                if (inputForOxygen.Count > 1)
                {
                    var (oxygenMostCommon, _) = GetCommonBits(inputForOxygen.Select(b => b[i]));
                    inputForOxygen = inputForOxygen.Where(b => b[i] == oxygenMostCommon).ToList();
                }

                if (inputForCarbonDioxide.Count > 1)
                {
                    var (_, carbonDioxideLeastCommon) = GetCommonBits(inputForCarbonDioxide.Select(b => b[i]));
                    inputForCarbonDioxide = inputForCarbonDioxide.Where(b => b[i] == carbonDioxideLeastCommon).ToList();
                }
            }

            var gamma = BinToInt(mostCommonBitInPosition);
            var epsilon = BinToInt(leastCommonBitInPosition);

            var oxygen = BinToInt(inputForOxygen[0]);
            var carbonDioxide = BinToInt(inputForCarbonDioxide[0]);

            var part1 = gamma * epsilon;
            var part2 = oxygen * carbonDioxide;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static (char MostCommon, char LeastCommon) GetCommonBits(IEnumerable<char> bits)
        {
            var bitsList = bits.ToList();
            var countOnes = bitsList.Count(b => b == '1');
            var countZeros = bitsList.Count(b => b == '0');
            var most = countOnes >= countZeros ? '1' : '0';
            var least = countOnes < countZeros ? '1' : '0';
            return (most, least);
        }

        private static int BinToInt(char[] binaryArray)
        {
            return BinToInt(new string(binaryArray));
        }

        private static int BinToInt(string value)
        {
            return Convert.ToInt32(value, 2);
        }
    }
}
