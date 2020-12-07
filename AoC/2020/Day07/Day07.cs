using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day07
{
    public class Day07 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1);

            var bags = new List<Bag>();
            foreach (var line in input)
            {
                if (line.Contains(" bags contain no other bags."))
                {
                    var color = line.Replace(" bags contain no other bags.", "");
                    bags.Add(new Bag(color));
                    continue;
                }

                var clearedLine = line
                    .Replace(" bags contain ", " ")
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace(" bags", " ")
                    .Replace(" bag", "")
                    .Replace("  ", " ")
                    .Trim();

                var splitLine = clearedLine.Split(" ");
                
                var b = new Bag($"{splitLine[0]} {splitLine[1]}");
                for (var i = 2; i < splitLine.Length; i+=3)
                {
                    b.ContainingBags.Add(new Bag($"{splitLine[i+1]} {splitLine[i+2]}", int.Parse(splitLine[i])));
                }
                bags.Add(b);
            }

            var directGoldBagsContainers = bags
                .Where(b => b.ContainingBags.Any(c => c.BagColor == "shiny gold"))
                .ToList();

            var colorsThatHoldShinyGold = new HashSet<string>();
            foreach (var directGoldBagsContainer in directGoldBagsContainers)
            {
                colorsThatHoldShinyGold.Add(directGoldBagsContainer.BagColor);
            }

            var anyColorAdded = true;
            while (anyColorAdded)
            {
                anyColorAdded = false;
                var newColorsThatHoldShinyGold = new HashSet<string>();
                foreach (var color in colorsThatHoldShinyGold)
                {

                    var colorsToAdd = bags
                        .Where(b => b.ContainingBags.Any(c => c.BagColor == color)).Select(b => b.BagColor)
                        .Except(colorsThatHoldShinyGold)
                        .ToList();

                    if (colorsToAdd.Count > 0)
                    {
                        anyColorAdded = true;
                    }

                    foreach (var color2 in colorsToAdd)
                    {
                        newColorsThatHoldShinyGold.Add(color2);
                    }
                }
                colorsThatHoldShinyGold.UnionWith(newColorsThatHoldShinyGold);
            }

            var part1 = colorsThatHoldShinyGold.Count;

            var q = new Queue<string>(new []{ "shiny gold" });

            var part2 = 0;
            while (q.Count > 0)
            {
                var bagColor = q.Dequeue();
                var containingBags = bags
                    .Where(b => b.BagColor == bagColor)
                    .SelectMany(b => b.ContainingBags);

                foreach (var bag in containingBags)
                {
                    part2 += bag.Count;
                    for (var i = 0; i < bag.Count; i++)
                    {
                        q.Enqueue(bag.BagColor);
                    }
                }

            }

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private class Bag
        {
            public string BagColor { get; }
            public int Count { get; }
            public List<Bag> ContainingBags { get; }

            public Bag(string color, int count = 1)
            {
                BagColor = color;
                Count = count;
                ContainingBags = new List<Bag>();
            }
        }
    }
}
