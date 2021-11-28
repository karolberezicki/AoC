using System;

namespace AoC._2018.Day11
{
    public class Day11 : ISolution
    {
        private record Cell(int X, int Y, int Power, int Size);

        private const int GridSerialNumber = 5093;
        private const int GridSize = 300;

        public void Execute()
        {
            var grid = new int[GridSize, GridSize];

            GeneratePowerCell(grid);

            var cell = GetCellWithLargestPower(grid, 3);
            var part1 = $"{cell.X},{cell.Y}";

            for (var size = 3; size < 30; size++)
            {
                var biggerCell = GetCellWithLargestPower(grid, size);

                if (cell.Power < biggerCell.Power)
                {
                    cell = biggerCell;
                }
            }

            var part2 = $"{cell.X},{cell.Y},{cell.Size}";

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static void GeneratePowerCell(int[,] grid)
        {
            for (var x = 1; x < GridSize; x++)
            {
                for (var y = 1; y < GridSize; y++)
                {
                    var rackId = x + 10;
                    var power = rackId * y;
                    power += GridSerialNumber;
                    power *= rackId;
                    power = power > 100 ? Math.Abs(power / 100) % 10 : 0;
                    power -= 5;

                    grid[x, y] = power;
                }
            }
        }

        private static Cell GetCellWithLargestPower(int[,] grid, int size)
        {
            var cellWithLargestPower = new Cell(0, 0, 0, 0);
            for (var x = 1; x <= GridSize - size; x++)
            {
                for (var y = 1; y <= GridSize - size; y++)
                {
                    var power = 0;
                    for (var i = 0; i < size; i++)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            power += grid[x + i, y + j];
                        }
                    }

                    if (cellWithLargestPower.Power < power)
                    {
                        cellWithLargestPower = new Cell(x, y, power, size);
                    }
                }
            }

            return cellWithLargestPower;
        }
    }
}
