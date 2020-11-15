using System.Collections.Generic;

namespace AoC
{
    public static class UsefulExtensions
    {
        public static IEnumerable<(int x, int y)> Around(this (int x, int y) point)
        {
            yield return (point.x, point.y - 1);
            yield return (point.x - 1, point.y);
            yield return (point.x + 1, point.y);
            yield return (point.x, point.y + 1);
        }
    }
}
