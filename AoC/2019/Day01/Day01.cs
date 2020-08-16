using System;
using System.Linq;

namespace AoC._2019.Day01
{
    public class Day01 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .Select(int.Parse)
                .ToList();

            var part1 = input.Sum(GetFuelRequirement);
            var part2 = input.Sum(GetFuelRequirementWithRequiredFuel);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int GetFuelRequirement(int moduleMass)
        {
            return moduleMass / 3 - 2;
        }

        private static int GetFuelRequirementWithRequiredFuel(int moduleMass)
        {
            var fuelRequirement = GetFuelRequirement(moduleMass);

            if (fuelRequirement > 0) return fuelRequirement + GetFuelRequirementWithRequiredFuel(fuelRequirement);

            return 0;
        }
    }
}
