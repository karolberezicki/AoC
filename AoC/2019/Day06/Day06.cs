using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2019.Day06
{
    public class Day06 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .Select(o => o.Split(")"))
                .Select(o => (Center: o[0], Orbiting: o[1]));

            var orbits = new HashSet<(string, string)>(input);
            var spaceObjects = new HashSet<SpaceObject>{ new SpaceObject("COM", 0) };
            FindDirectOrbits(spaceObjects, orbits);
            var part1 = CountAllOrbits(spaceObjects);

            var yourPath = FindPath(orbits, "YOU", "COM");
            var santaPath = FindPath(orbits, "SAN", "COM");

            while (yourPath.Last() == santaPath.Last())
            {
                yourPath.Remove(yourPath.Last());
                santaPath.Remove(santaPath.Last());
            }

            var part2 = yourPath.Count + santaPath.Count - 2;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static List<string> FindPath(HashSet<(string Center, string Orbiting)> orbits, string from, string to)
        {
            var list = new List<string> { from };
            var (center, _) = orbits.First(o => o.Orbiting == from);
            if (center != to)
            {
                list.AddRange(FindPath(orbits, center, to));
            }

            return list;
        }

        private static void FindDirectOrbits(HashSet<SpaceObject> spaceObjects, HashSet<(string Center, string Orbiting)> orbits)
        {
            foreach (var spaceObject in spaceObjects)
            {
                var knownDirectOrbits = new HashSet<string>(spaceObject.DirectOrbits.Select(o => o.Name));
                var directOrbits = orbits.Where(o => o.Center == spaceObject.Name && !knownDirectOrbits.Contains(o.Orbiting)).ToList();
                if (directOrbits.Count > 0)
                {
                    foreach (var directOrbit in directOrbits)
                    {
                        spaceObject.DirectOrbits.Add(new SpaceObject(directOrbit.Orbiting, spaceObject.Level+1));
                        FindDirectOrbits(spaceObject.DirectOrbits, orbits);
                    }
                }
            }
        }

        private static int CountAllOrbits(HashSet<SpaceObject> spaceObjects)
        {
            var count = 0;
            foreach (var spaceObject in spaceObjects)
            {
                count += spaceObject.Level;
                count += CountAllOrbits(spaceObject.DirectOrbits);
            }
            return count;
        }
    }
}
