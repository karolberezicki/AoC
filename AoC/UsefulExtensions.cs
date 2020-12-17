using System.Collections.Generic;

namespace AoC
{
    public static class UsefulExtensions
    {
        public static IEnumerable<(int x, int y)> Around(this (int x, int y) point)
        {
            var (x, y) = point;
            yield return (x, y - 1);
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x, y + 1);
        }

        public static IEnumerable<(int x, int y, int z)> Around(this (int x, int y, int z) point)
        {
            var (x, y, z) = point;
            for (var dX = -1; dX <= 1; dX++)
            {
                for (var dY = -1; dY <= 1; dY++)
                {
                    for (var dZ = -1; dZ <= 1; dZ++)
                    {
                        if (dX == 0 && dY == 0 && dZ == 0)
                        {
                            continue;
                        }
                        yield return (x + dX, y + dY, z + dZ);
                    }
                }
            }
        }

        public static IEnumerable<(int x, int y, int z, int w)> Around(this (int x, int y, int z, int w) point)
        {
            var (x, y, z, w) = point;
            for (var dX = -1; dX <= 1; dX++)
            {
                for (var dY = -1; dY <= 1; dY++)
                {
                    for (var dZ = -1; dZ <= 1; dZ++)
                    {
                        for (var dW = -1; dW <= 1; dW++)
                        {
                            if (dX == 0 && dY == 0 && dZ == 0 && dW == 0)
                            {
                                continue;
                            }
                            yield return (x + dX, y + dY, z + dZ, w + dW);
                        }
                    }
                }
            }
        }

        public static IEnumerable<(int x, int y)> Adjacent(this (int x, int y) point)
        {
            var (x, y) = point;
            yield return (x, y - 1);
            yield return (x - 1, y);
            yield return (x + 1, y);
            yield return (x, y + 1);

            yield return (x - 1, y - 1);
            yield return (x + 1, y - 1);
            yield return (x - 1, y + 1);
            yield return (x + 1, y + 1);
        }
    }
}
