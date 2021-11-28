using System.Text.RegularExpressions;

namespace AoC._2018.Day10
{
    public class Point
    {
        private static readonly Regex DescriptionRegex
            = new Regex(@"position=<\s*(?<X>-?\d+),\s*(?<Y>-?\d+)> velocity=<\s*(?<DeltaX>-?\d+),\s*(?<DeltaY>-?\d+)>");

        public int X { get; set; }
        public int Y { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public Point(string description, int initTime)
        {
            var match = DescriptionRegex.Match(description);

            X = int.Parse(match.Groups[nameof(X)].Value);
            Y = int.Parse(match.Groups[nameof(Y)].Value);
            DeltaX = int.Parse(match.Groups[nameof(DeltaX)].Value);
            DeltaY = int.Parse(match.Groups[nameof(DeltaY)].Value);

            X += DeltaX * initTime;
            Y += DeltaY * initTime;
        }

        public void Move()
        {
            X += DeltaX;
            Y += DeltaY;
        }

        public bool InPosition(int x, int y) => X == x && Y == y;
    }
}
