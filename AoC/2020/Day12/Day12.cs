using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day12
{
    public class Day12 : ISolution
    {
        public void Execute()
        {
            var instructions = GetInstructions();

            var part1 = Part1(instructions);
            var part2 = Part2(instructions);

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static char ChangeDirection(char currentDirection, char action, int value)
        {
            var direction = currentDirection;
            if (action == 'R' && value == 270)
            {
                action = 'L';
                value = 90;
            }

            if (action == 'R' && value == 180)
            {
                action = 'L';
                value = 180;
            }

            if (action == 'R' && value == 90)
            {
                action = 'L';
                value = 270;
            }

            direction = action switch
            {
                'L' when value == 90  && direction == 'N' => 'W',
                'L' when value == 90  && direction == 'W' => 'S',
                'L' when value == 90  && direction == 'S' => 'E',
                'L' when value == 90  && direction == 'E' => 'N',
                'L' when value == 180 && direction == 'N' => 'S',
                'L' when value == 180 && direction == 'W' => 'E',
                'L' when value == 180 && direction == 'S' => 'N',
                'L' when value == 180 && direction == 'E' => 'W',
                'L' when value == 270 && direction == 'N' => 'E',
                'L' when value == 270 && direction == 'W' => 'N',
                'L' when value == 270 && direction == 'S' => 'W',
                'L' when value == 270 && direction == 'E' => 'S',
                _ => direction
            };

            return direction;
        }

        private static int Part1(IEnumerable<(char Action, int Value)> instructions)
        {
            (int X, int Y) position = (0, 0);
            var direction = 'E';

            foreach (var instruction in instructions)
                switch (instruction)
                {
                    case ('N', var value):
                        position = (position.X, position.Y + value);
                        break;
                    case ('S', var value):
                        position = (position.X, position.Y - value);
                        break;
                    case ('E', var value):
                        position = (position.X + value, position.Y);
                        break;
                    case ('W', var value):
                        position = (position.X - value, position.Y);
                        break;
                    case ('L', var value):
                        direction = ChangeDirection(direction,'L', value);
                        break;
                    case ('R', var value):
                        direction = ChangeDirection(direction, 'R', value);
                        break;
                    case ('F', var value):
                        position = direction switch
                        {
                            'N' => (position.X, position.Y + value),
                            'S' => (position.X, position.Y - value),
                            'E' => (position.X + value, position.Y),
                            'W' => (position.X - value, position.Y),
                            _ => position
                        };
                        break;
                }

            var test1 = Math.Abs(position.X) + Math.Abs(position.Y);

            return test1;
        }

        private static int Part2(IEnumerable<(char Action, int Value)> instructions)
        {
            (int X, int Y) position = (0, 0);
            var waypoint = (X: 10, Y: 1);
            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case ('N', var value):
                        waypoint = (waypoint.X, waypoint.Y + value);
                        break;
                    case ('S', var value):
                        waypoint = (waypoint.X, waypoint.Y - value);
                        break;
                    case ('E', var value):
                        waypoint = (waypoint.X + value, waypoint.Y);
                        break;
                    case ('W', var value):
                        waypoint = (waypoint.X - value, waypoint.Y);
                        break;
                    case ('L', 90) or ('R', 270):
                        waypoint = (-waypoint.Y, waypoint.X);
                        break;
                    case ('R', 90) or ('L', 270):
                        waypoint = (waypoint.Y, -waypoint.X);
                        break;
                    case ('L' or 'R', 180):
                        waypoint = (-waypoint.X, -waypoint.Y);
                        break;
                    case ('F', var value):
                        position = (position.X + value * waypoint.X, position.Y + value * waypoint.Y);
                        break;
                }
            }

            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        private static List<(char Action, int Value)> GetInstructions()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();
            return input
                .Select(l => (Action: l[0], Value: int.Parse(l.Substring(1, l.Length - 1))))
                .ToList();
        }
    }
}
