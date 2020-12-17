using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day17
{
    public class Day17 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var part1 = Part1(input);
            var part2 = Part2(input);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int Part1(IReadOnlyList<string> input)
        {
            var grid = new Dictionary<(int X, int Y, int Z), bool>();

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        grid[(x, y, 0)] = true;
                    }
                }
            }

            for (var i = 0; i < 6; i++)
            {
                var surroundings = new HashSet<(int X, int Y, int Z)>();
                foreach (var cube in grid)
                {
                    var cubeSurroundings = cube.Key.Around();
                    surroundings.UnionWith(cubeSurroundings);
                }

                foreach (var cube in surroundings)
                {
                    if (!grid.ContainsKey(cube))
                    {
                        grid.Add(cube, false);
                    }
                }

                var workingGrid = grid.ToDictionary(entry => entry.Key,
                    entry => entry.Value);
                foreach (var cube in grid)
                {
                    var active = cube.Key.Around().Count(c => Get3DGridValue(grid, c));

                    if (cube.Value && (active == 2 || active == 3))
                    {
                        workingGrid[cube.Key] = true;
                    }
                    else if (cube.Value)
                    {
                        workingGrid[cube.Key] = false;
                    }
                    else if (!cube.Value && active == 3)
                    {
                        workingGrid[cube.Key] = true;
                    }
                }

                grid = workingGrid;
            }


            return grid.Count(c => c.Value);
        }

        private static int Part2(IReadOnlyList<string> input)
        {
            var grid = new Dictionary<(int X, int Y, int Z, int W), bool>();

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        grid[(x, y, 0, 0)] = true;
                    }
                }
            }

            for (var i = 0; i < 6; i++)
            {
                var surroundings = new HashSet<(int X, int Y, int Z, int W)>();
                foreach (var cube in grid)
                {
                    var cubeSurroundings = cube.Key.Around();
                    surroundings.UnionWith(cubeSurroundings);
                }

                foreach (var cube in surroundings)
                {
                    if (!grid.ContainsKey(cube))
                    {
                        grid.Add(cube, false);
                    }
                }

                var workingGrid = grid.ToDictionary(entry => entry.Key,
                    entry => entry.Value);
                foreach (var cube in grid)
                {
                    var active = cube.Key.Around().Count(c => Get4DGridValue(grid, c));

                    if (cube.Value && (active == 2 || active == 3))
                    {
                        workingGrid[cube.Key] = true;
                    }
                    else if (cube.Value)
                    {
                        workingGrid[cube.Key] = false;
                    }
                    else if (!cube.Value && active == 3)
                    {
                        workingGrid[cube.Key] = true;
                    }
                }

                grid = workingGrid;
            }


            return grid.Count(c => c.Value);
        }

        private static bool Get3DGridValue(IReadOnlyDictionary<(int X, int Y, int Z), bool> grid, (int X, int Y, int Z) position)
        {
            return grid.ContainsKey(position) && grid[position];
        }

        private static bool Get4DGridValue(IReadOnlyDictionary<(int X, int Y, int Z, int W), bool> grid, (int X, int Y, int Z, int W) position)
        {
            return grid.ContainsKey(position) && grid[position];
        }
    }
}
