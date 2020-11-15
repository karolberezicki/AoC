using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2019.Day18
{
    public class Day18 : ISolution
    {
        private static readonly Dictionary<char, int> KeysCodeLookup = Enumerable.Range(0, 26)
            .ToDictionary(key => (char) (key + 'a'), key => (int) Math.Pow(2, key));

        public void Execute()
        {
            var map = GenerateMap();

            var part1 = Part1(map);
            Console.WriteLine($"Part1 {part1}");

            var part2 = Part2(map);
            Console.WriteLine($"Part2 {part2}");
        }

        private static int Part1(IReadOnlyDictionary<(int X, int Y), char> map)
        {
            var (entrancePosition, _) = map.FirstOrDefault(c => c.Value == '@');
            var keys = map.Where(c => char.IsLetter(c.Value) && char.IsLower(c.Value))
                .ToDictionary(x => x.Value, x => x.Key);

            var keyPaths = new Dictionary<(int X, int Y), List<(char Key, int Distance, int Obstacles)>>
            {
                [entrancePosition] = FindPathToKeys(map, entrancePosition)
            };

            foreach (var (_, position) in keys)
            {
                keyPaths[position] = FindPathToKeys(map, position);
            }

            var robots = new Dictionary<char, (int X, int Y)> {{'@', entrancePosition}};
            return CollectKeys(keyPaths, keys, robots);
        }

        private static int Part2(Dictionary<(int X, int Y), char> map)
        {
            ModifyMapForPart2(map);

            var keys = map.Where(c => char.IsLetter(c.Value) && char.IsLower(c.Value))
                .ToDictionary(x => x.Value, x => x.Key);
            var robots = map.Where(c => char.IsDigit(c.Value)).ToDictionary(x => x.Value, x => x.Key);

            var keyPaths = new Dictionary<(int X, int Y), List<(char Key, int Distance, int Obstacles)>>();

            foreach (var (_, robotPosition) in robots)
            {
                keyPaths[robotPosition] = FindPathToKeys(map, robotPosition);
            }

            foreach (var (_, keyPosition) in keys)
            {
                keyPaths[keyPosition] = FindPathToKeys(map, keyPosition);
            }

            return CollectKeys(keyPaths, keys, robots);
        }

        private static Dictionary<(int, int), char> GenerateMap()
        {
            var input = Utils.LoadInputLines()
                .ToList();

            var map = new Dictionary<(int, int), char>();

            for (var i = 0; i < input.Count; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    map[(i, j)] = input[i][j];
                }
            }

            return map;
        }

        private static void ModifyMapForPart2(IDictionary<(int X, int Y), char> map)
        {
            var (entrancePosition, _) = map.FirstOrDefault(c => c.Value == '@');
            map[entrancePosition] = '#';
            map[(entrancePosition.X + 1, entrancePosition.Y)] = '#';
            map[(entrancePosition.X - 1, entrancePosition.Y)] = '#';
            map[(entrancePosition.X, entrancePosition.Y + 1)] = '#';
            map[(entrancePosition.X, entrancePosition.Y - 1)] = '#';

            map[(entrancePosition.X - 1, entrancePosition.Y - 1)] = '1';
            map[(entrancePosition.X + 1, entrancePosition.Y - 1)] = '2';
            map[(entrancePosition.X - 1, entrancePosition.Y + 1)] = '3';
            map[(entrancePosition.X + 1, entrancePosition.Y + 1)] = '4';
        }

        private static bool IsOpenPassage(char passage)
        {
            return passage switch
            {
                '.' => true,
                '@' => true,
                '1' => true,
                '2' => true,
                '3' => true,
                '4' => true,
                _   => false
            };
        }

        private static long EncodePositions(IEnumerable<(int X, int Y)> positions)
        {
            long holder = 0;
            foreach (var (x, y) in positions)
            {
                holder *= 100;
                holder += x;
                holder *= 100;
                holder += y;
            }

            return holder;
        }

        private static IEnumerable<(int X, int Y)> DecodePositions(long holder, int positionsCount)
        {
            var positions = new List<(int X, int Y)>();

            for (var i = 0; i < positionsCount; i++)
            {
                var y = holder % 100;
                holder /= 100;
                var x = holder % 100;
                holder /= 100;

                positions.Add(((int) x, (int) y));
            }

            positions.Reverse();

            return positions;
        }

        private static List<(char Key, int Distance, int Obstacles)> FindPathToKeys(
            IReadOnlyDictionary<(int X, int Y), char> map, (int X, int Y) start)
        {
            var list = new List<(char Key, int Distance, int Obstacles)>();
            var visited = new HashSet<(int X, int Y)>();

            var queue = new Queue<((int X, int Y), int Distance, int Obstacles)>();
            queue.Enqueue((start, 0, 0));

            while (queue.Any())
            {
                var (position, distance, obstacles) = queue.Dequeue();

                if (map[position] == '#')
                {
                    visited.Add(position);
                }

                if (visited.Contains(position))
                {
                    continue;
                }

                visited.Add(position);

                var current = map[position];

                if (IsOpenPassage(current))
                {
                    foreach (var p in position.Around())
                    {
                        queue.Enqueue((p, distance + 1, obstacles));
                    }

                    continue;
                }

                if (char.IsLower(current))
                {
                    list.Add((Key: current, Distance: distance, Obstacles: obstacles));

                    foreach (var p in position.Around())
                    {
                        queue.Enqueue((p, distance + 1, obstacles));
                    }
                }
                else if (char.IsUpper(current))
                {
                    foreach (var p in position.Around())
                    {
                        var updatedObstacles = obstacles |= KeysCodeLookup[char.ToLower(current)];
                        queue.Enqueue((p, distance + 1, updatedObstacles));
                    }
                }
            }

            return list;
        }

        private static int CollectKeys(
            IReadOnlyDictionary<(int X, int Y), List<(char Key, int Distance, int Obstacles)>> keyPaths,
            IReadOnlyDictionary<char, (int X, int Y)> keys, Dictionary<char, (int X, int Y)> robots)
        {
            var currentMinimum = int.MaxValue;

            var startingSet = EncodePositions(robots.Select(r => r.Value));
            var queue = new Queue<(long Positions, int OwnedKeys, int Steps)>();
            queue.Enqueue((startingSet, 0, 0));

            var visited = new Dictionary<(long, int), int>();
            var allKeysCollectedValue = (int) Math.Pow(2, keys.Count) - 1;

            while (queue.Any())
            {
                var (positions, ownedKeys, steps) = queue.Dequeue();

                var visitedStateKey = (positions, ownedKeys);
                if (visited.TryGetValue(visitedStateKey, out var visitedStateStepCost))
                {
                    // longer path to already visited state, skip
                    if (visitedStateStepCost <= steps)
                    {
                        continue;
                    }

                    // shorter path to already visited state, override
                    visited[visitedStateKey] = steps;
                }
                else
                {
                    visited.Add((positions, ownedKeys), steps);
                }

                if (ownedKeys == allKeysCollectedValue)
                {
                    currentMinimum = Math.Min(currentMinimum, steps);
                    continue;
                }

                for (var i = 0; i < robots.Count; i++)
                {
                    var decodedPositions = DecodePositions(positions, robots.Count).ToList();
                    foreach (var (key, distance, obstacles) in keyPaths[decodedPositions[i]])
                    {
                        var keyIndicator = KeysCodeLookup[key];
                        if ((ownedKeys & keyIndicator) == keyIndicator || (obstacles & ownedKeys) != obstacles)
                        {
                            continue;
                        }

                        var newOwned = ownedKeys | keyIndicator;
                        var newPosition = decodedPositions;
                        newPosition[i] = keys[key];
                        queue.Enqueue((EncodePositions(newPosition), newOwned, steps + distance));
                    }
                }
            }

            return currentMinimum;
        }
    }
}
