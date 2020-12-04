using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day04
{
    public class Day04 : ISolution
    {
        private static readonly List<string> RequiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        private static readonly List<string> ValidEyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public void Execute()
        {
            var input = string.Join("\n", Utils.LoadInput().Split(" "))
                .Split("\n")
                .ToList();

            var passports = ExtractPassports(input);

            var passportsWithRequiredFields = passports.Where(p => p.Entries.Keys.Intersect(RequiredFields).Count() >= RequiredFields.Count).ToList();

            var part1 = passportsWithRequiredFields.Count;
            var part2 = passportsWithRequiredFields.Count(IsValid);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static IEnumerable<Passport> ExtractPassports(IEnumerable<string> input)
        {

            var passports = new List<Passport>();

            var newPassport = new Passport();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(newPassport);
                    newPassport = new Passport();
                    continue;
                }

                var lineSplit = line.Trim().Split(":");
                newPassport.Entries.Add(lineSplit[0], lineSplit[1]);
            }

            return passports;
        }

        private static bool IsValid(Passport passport)
        {
            var byr = int.Parse(passport.Entries["byr"]);
            if (byr < 1920 || byr > 2002)
            {
                return false;
            }

            var iyr = int.Parse(passport.Entries["iyr"]);
            if (iyr < 2010 || iyr > 2020)
            {
                return false;
            }

            var eyr = int.Parse(passport.Entries["eyr"]);
            if (eyr < 2020 || eyr > 2030)
            {
                return false;
            }

            var heightValue = int.Parse(string.Join("", passport.Entries["hgt"].Where(char.IsDigit)));
            var heightIsCm = passport.Entries["hgt"].Contains("cm");

            if (heightIsCm)
            {
                if (heightValue < 150 || heightValue > 193)
                {
                    return false;
                }
            }
            else
            {
                if (heightValue < 59 || heightValue > 76)
                {
                    return false;
                }
            }

            if (!passport.Entries["hcl"].Contains('#') || passport.Entries["hcl"].Trim().Length != 7)
            {
                return false;
            }

            
            if (!ValidEyeColors.Contains(passport.Entries["ecl"].Trim()))
            {
                return false;
            }

            return passport.Entries["pid"].Trim().Count(char.IsDigit) == 9;
        }

        private class Passport
        {
            public Dictionary<string, string> Entries { get; }

            public Passport()
            {
                Entries = new Dictionary<string, string>();
            }
        }
    }
}
