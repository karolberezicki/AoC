using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2020.Day14
{
    public class Day14 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var part1 = Part1(input);
            var part2 = Part2(input);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static long Part1(IEnumerable<string> input)
        {
            var memory = new Dictionary<long, long>();
            var mask = "";

            foreach (var instruction in input.Select(line => line.Split(" = ")))
            {
                if (instruction[0] == "mask")
                {
                    mask = instruction[1];
                    continue;
                }

                var address = long.Parse(Regex.Replace(instruction[0], "[^0-9]", ""));
                var value = long.Parse(instruction[1]);

                var binValue = Convert.ToString(value, 2).PadLeft(mask.Length, '0').ToArray();

                for (var i = 0; i < mask.Length; i++)
                {
                    if (mask[i] != 'X')
                    {
                        binValue[i] = mask[i];
                    }
                }

                var maskedValue = Convert.ToInt64(new string(binValue), 2);
                memory[address] = maskedValue;
            }

            return memory.Values.Sum();
        }

        private static long Part2(IEnumerable<string> input)
        {
            var memory = new Dictionary<long, long>();
            var mask = "";

            foreach (var instruction in input.Select(line => line.Split(" = ")))
            {
                if (instruction[0] == "mask")
                {
                    mask = instruction[1];
                    continue;
                }

                var address = long.Parse(Regex.Replace(instruction[0], "[^0-9]", ""));
                var value = long.Parse(instruction[1]);

                var addressBin = Convert.ToString(address, 2).PadLeft(mask.Length, '0').ToArray();

                for (var i = 0; i < mask.Length; i++)
                {
                    addressBin[i] = mask[i] == '0' ? addressBin[i] : mask[i];
                }

                var floating = mask
                    .Select((character, index) => (Character: character, Index: index))
                    .Where(x => x.Character == 'X')
                    .Select(x => x.Index).ToList();


                for (var i = 0; i < (int)Math.Pow(2, floating.Count); i++)
                {
                    var updateBits = Convert.ToString(i, 2).PadLeft(floating.Count, '0').ToArray();


                    for (var j = 0; j < floating.Count; j++)
                    {
                        addressBin[floating[j]] = updateBits[j];
                    }

                    var address2 = Convert.ToInt64(new string(addressBin), 2);
                    memory[address2] = value;
                }
            }

            return memory.Values.Sum();
        }
    }
}
