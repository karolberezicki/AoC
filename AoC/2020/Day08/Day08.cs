using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day08
{
    public class Day08 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var part1 = ExecuteBootCode(input).Value;
            var part2 = 0;

            var instructionsToCheck = input
                .Select((instruction, index) => (instruction, index))
                .Where(x => x.instruction != null && (x.instruction.Contains("jmp") || x.instruction.Contains("nop")))
                .Select(x => x.index).ToList();

            foreach (var index in instructionsToCheck)
            {
                var currentInput = new List<string>(input);

                currentInput[index] = currentInput[index].Contains("jmp")
                    ? currentInput[index].Replace("jmp", "nop")
                    : currentInput[index].Replace("nop", "jmp");

                var (terminated, value) = ExecuteBootCode(currentInput);

                if (terminated)
                {
                    part2 = value;
                    break;
                }
            }

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static (bool Terminated, int Value) ExecuteBootCode(IReadOnlyList<string> input)
        {
            var accumulator = 0;
            var visited = new HashSet<int>();

            for (var i = 0; i < input.Count; i++)
            {
                var instruction = input[i].Split(" ");
                var operation = instruction[0];
                var argument = int.Parse(instruction[1]);

                if (visited.Contains(i))
                {
                    return (false, accumulator);
                }

                visited.Add(i);

                switch (operation)
                {
                    case "acc":
                        accumulator += argument;
                        break;
                    case "jmp":
                        i += argument - 1;
                        break;
                }
            }

            return (true, accumulator);
        }
    }
}
