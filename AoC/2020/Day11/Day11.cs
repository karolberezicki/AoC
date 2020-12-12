using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day11
{
    public class Day11 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var grid = CreateGrid(input);

            var part1 = Part1(CopyGrid(grid));
            var part2 = Part2(CopyGrid(grid));

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static int Part1(Dictionary<(int X, int Y), char> grid)
        {
            var changed = true;

            while (changed)
            {
                changed = false;
                var workingGrid = CopyGrid(grid);

                foreach (var (key, value) in grid)
                {
                    if (value == '.')
                    {
                        continue;
                    }

                    var occupied = key.Adjacent()
                        .Where(k => grid.ContainsKey(k))
                        .Select(k => grid[k])
                        .Count(s => s == '#');

                    switch (value)
                    {
                        case 'L' when occupied == 0:
                            workingGrid[key] = '#';
                            changed = true;
                            break;
                        case '#' when occupied >= 4:
                            workingGrid[key] = 'L';
                            changed = true;
                            break;
                    }
                }

                grid = workingGrid;
            }

            return grid.Count(s => s.Value == '#');
        }

        private static int Part2(Dictionary<(int X, int Y), char> grid)
        {
            var changed = true;
            while (changed)
            {
                changed = false;
                var workingGrid = CopyGrid(grid);

                foreach (var (key, value) in grid)
                {
                    if (value == '.')
                    {
                        continue;
                    }

                    var occupied = VisibleOccupied(grid, key);

                    switch (value)
                    {
                        case 'L' when occupied == 0:
                            workingGrid[key] = '#';
                            changed = true;
                            break;
                        case '#' when occupied >= 5:
                            workingGrid[key] = 'L';
                            changed = true;
                            break;
                    }
                }

                grid = workingGrid;
            }

            return grid.Count(s => s.Value == '#');
        }

        private static Dictionary<(int X, int Y), char> CopyGrid(Dictionary<(int X, int Y), char> grid)
        {
            return grid.ToDictionary(entry => entry.Key,
                entry => entry.Value);
        }

        private static Dictionary<(int X, int Y), char> CreateGrid(IReadOnlyList<string> input)
        {
            var grid = new Dictionary<(int X, int Y), char>();

            for (var y = 0; y < input.Count; y++)
            {
                var line = input[y];
                for (var x = 0; x < line.Length; x++)
                {
                    grid[(x, y)] = line[x];
                }
            }

            return grid;
        }


        private static int VisibleOccupied(IReadOnlyDictionary<(int X, int Y), char> grid, (int x, int y) key)
        {
            var occupied = 0;

            foreach (var xDelta in new[] { -1, 0, 1 })
            {
                foreach (var yDelta in new[] { -1, 0, 1 })
                {
                    if (xDelta == 0 && yDelta == 0)
                    {
                        continue;
                    }

                    var x = key.x;
                    var y = key.y;

                    do
                    {
                        x += xDelta;
                        y += yDelta;
                    } while (grid.ContainsKey((x, y)) && grid[(x, y)] == '.');


                    if ((x, y) != key && grid.ContainsKey((x, y)) && grid[(x, y)] == '#')
                    {
                        occupied++;
                    }
                }
            }

            return occupied;

        }

        private static void PrintGrid(IReadOnlyDictionary<(int X, int Y), char> grid)
        {
            var maxX = grid.Select(k => k.Key.X).Max();
            var maxY = grid.Select(k => k.Key.Y).Max();

            Console.Write(' ');
            for (var x = 0; x <= maxX; x++)
            {
                Console.Write(x);
            }

            Console.WriteLine();
            for (var y = 0; y <= maxY; y++)
            {
                Console.Write(y);
                for (var x = 0; x <= maxX; x++)
                {
                    Console.Write(grid[(x, y)]);
                }

                Console.WriteLine();
            }

            Console.WriteLine(new string('-', maxX + 2));
        }
    }
}
