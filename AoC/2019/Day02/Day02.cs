using System;
using System.Linq;
using IntCode;

namespace AoC._2019.Day02
{
    public class Day02 : ISolution
    {
        private const long ApolloLandingDate = 19690720;

        public void Execute()
        {
            var input = Utils.LoadInput();
            
            var memoryState = input.Split(",")
                .Select(long.Parse)
                .ToList();

            var noun = 12;
            var verb = 2;

            var icc = new IntCodeComputer(memoryState);
            icc.SetNoun(noun);
            icc.SetVerb(verb);
            icc.RunTillHalt();

            var part1 = icc.ReadIntCode(0);

            long zeroMemoryCell;

            do
            {
                noun++;
                icc = new IntCodeComputer(memoryState);
                icc.SetNoun(noun);
                icc.SetVerb(verb);
                icc.RunTillHalt();
                zeroMemoryCell = icc.ReadIntCode(0);
            } while (zeroMemoryCell < ApolloLandingDate);

            noun--;

            do
            {
                verb++;
                icc = new IntCodeComputer(memoryState);
                icc.SetNoun(noun);
                icc.SetVerb(verb);
                icc.RunTillHalt();
                zeroMemoryCell = icc.ReadIntCode(0);
            } while (zeroMemoryCell < ApolloLandingDate);

            var part2 = 100 * noun + verb;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
