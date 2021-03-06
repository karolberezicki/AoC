﻿using System;
using System.Collections.Generic;
using System.Linq;
using IntCode;

namespace AoC._2019.Day17
{
    public class Day17 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInput();

            var memoryState = input.Split(",")
                .Select(long.Parse)
                .ToList();

            var part1 = Part1(memoryState);
            Console.WriteLine($"Part1 {part1}");

            var icc = new IntCodeComputer(memoryState);
            icc.SetAddress(0, 2);

            // TODO: Go through scaffolding via code
            const string subroutineA = "L,4,R,8,L,6,L,10";
            const string subroutineB = "L,6,R,8,R,10,L,6,L,6";
            const string subroutineC = "L,4,L,4,L,10";
            const string mainRoutine = "A,B,A,B,C,C,B,A,B,C";

            var robotInput = string.Join("\n", mainRoutine, subroutineA, subroutineB, subroutineC, "n", "");
            foreach (var i in robotInput)
            {
                icc.Inputs.Enqueue(i);
            }

            icc.RunTillHalt();

            var part2 = icc.Output.Last();
            Console.WriteLine($"Part2 {part2}");
        }

        private static int Part1(IEnumerable<long> memoryState)
        {
            var icc = new IntCodeComputer(memoryState);
            icc.RunTillHalt();

            var image = icc.Output.ToList();

            var map = GenerateMap(image);
            Display(map);

            var intersections = new HashSet<(int X, int Y)>();
            for (var y = 1; y < 40; y++)
            {
                for (var x = 1; x < 40; x++)
                {
                    var up = map.ContainsKey((x, y - 1)) && map[(x, y - 1)] == '#';
                    var down = map.ContainsKey((x, y + 1)) && map[(x, y + 1)] == '#';
                    var left = map.ContainsKey((x - 1, y)) && map[(x - 1, y)] == '#';
                    var right = map.ContainsKey((x + 1, y)) && map[(x + 1, y)] == '#';

                    if (up && down && left && right)
                    {
                        intersections.Add((x, y));
                    }
                }
            }

            return intersections.Select(i => i.X * i.Y).Sum();
        }

        private static Dictionary<(int X, int Y), char> GenerateMap(IEnumerable<long> image)
        {
            var map = new Dictionary<(int X, int Y), char>();

            var x = 0;
            var y = 0;
            foreach (var data in image)
            {
                if (data == 10)
                {
                    y++;
                    x = 0;
                    continue;
                }

                map[(x, y)] = (char)data;
                x++;
            }
            return map;
        }

        private static void Display(Dictionary<(int X, int Y), char> map)
        {
            Console.CursorVisible = false;
            Console.Clear();

            var xOffset = Math.Min(0, map.Keys.Select(m => m.X).Min()) * -1;
            var yOffset = Math.Min(0, map.Keys.Select(m => m.Y).Min()) * -1;

            var cursorEndPosition = Math.Max(1, map.Keys.Select(m => m.Y).Max()) + yOffset + 1;

            foreach (var ((x, y), data) in map)
            {
                Console.SetCursorPosition(x + xOffset, y + yOffset);
                Console.Write(data);
            }

            Console.SetCursorPosition(0, cursorEndPosition);
        }
    }
}
