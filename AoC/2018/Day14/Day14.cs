using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day14
{
    public class Day14 : ISolution
    {
        public void Execute()
        {
            const int input = 652601;
            var part1 = "";
            var part2 = "";

            var inputDigits = input.ToString().Select(digit => int.Parse(digit.ToString())).ToList();

            var firstElf = 0;
            var secondElf = 1;

            var recipes = new List<int> { 3, 7 };

            var part1Resolved = false;
            var part2Resolved = false;

            while (true)
            {
                // Display(recipes, firstElf, secondElf);

                var firstElfScore = recipes[firstElf];
                var secondElfScore = recipes[secondElf];
                var scoreSum = firstElfScore + secondElfScore;
                if (scoreSum >= 10)
                {
                    recipes.Add(1);
                    recipes.Add(scoreSum % 10);
                }
                else
                {
                    recipes.Add(scoreSum);
                }

                firstElf = (firstElf + 1 + firstElfScore) % recipes.Count;
                secondElf = (secondElf + 1 + secondElfScore) % recipes.Count;

                if (recipes.Count == 10 + input)
                {
                    part1 = string.Join("", recipes.Skip(Math.Max(0, recipes.Count - 10)));
                    part1Resolved = true;
                }

                var digitsIndex = 0;
                for (var recipesIndex = recipes.Count - (inputDigits.Count + 1);
                    recipesIndex < recipes.Count;
                    recipesIndex++)
                {
                    if (recipes.Count < inputDigits.Count + 1)
                    {
                        break;
                    }

                    if (digitsIndex >= inputDigits.Count)
                    {
                        part2 = (recipesIndex - inputDigits.Count).ToString();
                        part2Resolved = true;
                        break;
                    }

                    if (recipes[recipesIndex] != inputDigits[digitsIndex])
                    {
                        break;
                    }

                    digitsIndex++;
                }

                if (part1Resolved && part2Resolved)
                {
                    break;
                }
            }


            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static void Display(IReadOnlyList<int> recipes, int firstElf, int secondElf)
        {
            for (var index = 0; index < recipes.Count; index++)
            {
                var recipe = recipes[index];
                if (index == firstElf)
                {
                    Console.Write($"({recipe})");
                }
                else if (index == secondElf)
                {
                    Console.Write($"[{recipe}]");
                }
                else
                {
                    Console.Write($" {recipe} ");
                }
            }

            Console.WriteLine();
        }
    }
}
