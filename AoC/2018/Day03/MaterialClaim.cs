using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AoC._2018.Day03
{
    [DebuggerDisplay("{Display}")]
    public class MaterialClaim
    {
        private readonly Regex _descriptionRegex = new(@"#(?<Id>\d+) @ (?<Left>\d+),(?<Top>\d+): (?<Width>\d+)x(?<Height>\d+)");

        public int Id { get; init; }
        public int Left { get; init; }
        public int Top { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }

        public string Display => $"#{Id} @ {Left},{Top}: {Width}x{Height}";

        public HashSet<(int X, int Y)> Points { get; }

        public MaterialClaim(string description)
        {
            var match = _descriptionRegex.Match(description);
            Id = int.Parse(match.Groups[nameof(Id)].Value);
            Left = int.Parse(match.Groups[nameof(Left)].Value);
            Top = int.Parse(match.Groups[nameof(Top)].Value);
            Width = int.Parse(match.Groups[nameof(Width)].Value);
            Height = int.Parse(match.Groups[nameof(Height)].Value);
            Points = new HashSet<(int X, int Y)>();
            for (var x = Left; x < Left + Width; x++)
            {
                for (var y = Top; y < Top + Height; y++)
                {
                    Points.Add((x, y));
                }
            }
        }
    }
}
