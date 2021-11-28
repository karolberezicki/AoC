using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2018.Day13
{
    public class Day13 : ISolution
    {
        private enum Turn
        {
            Left,
            Straight,
            Right
        }

        private record Cart(int X, int Y, char Symbol, Turn NextTurn, bool Crashed);

        private const char Up = '^';
        private const char Down = 'v';
        private const char Left = '<';
        private const char Right = '>';

        private static readonly Dictionary<(char symbol, char track), char> CurveLogic = new()
        {
            { (Left, '/'), Down },
            { (Up, '/'), Right },
            { (Right, '/'), Up },
            { (Down, '/'), Left },
            { (Left, '\\'), Up },
            { (Up, '\\'), Left },
            { (Right, '\\'), Down },
            { (Down, '\\'), Right },
        };

        private static readonly Dictionary<(char symbol, Turn turn), char> TurnLogic = new()
        {
            { (Right, Turn.Left), Up },
            { (Right, Turn.Straight), Right },
            { (Right, Turn.Right), Down },

            { (Left, Turn.Left), Down },
            { (Left, Turn.Straight), Left },
            { (Left, Turn.Right), Up },

            { (Up, Turn.Left), Left },
            { (Up, Turn.Straight), Up },
            { (Up, Turn.Right), Right },

            { (Down, Turn.Left), Right },
            { (Down, Turn.Straight), Down },
            { (Down, Turn.Right), Left }
        };

        private static readonly Dictionary<Turn, Turn> IntersectionLogic = new()
        {
            { Turn.Left, Turn.Straight },
            { Turn.Straight, Turn.Right },
            { Turn.Right, Turn.Left },
        };

        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();

            var height = input.Count;
            var width = input[0].Length;

            var grid = new char[width, height];

            var cartSymbols = new[] { Up, Down, Left, Right };

            var carts = new List<Cart>();


            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var track = input[y][x];

                    if (cartSymbols.Contains(track))
                    {
                        grid[x, y] = track == Up || track == Down ? '|' : '-';
                        carts.Add(new Cart(x, y, track, Turn.Left, false));
                    }
                    else
                    {
                        grid[x, y] = track;
                    }
                }
            }

            var part1 = "";
            var part2 = "";

            while (true)
            {
                if (!string.IsNullOrWhiteSpace(part2))
                {
                    break;
                }

                carts = carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();

                for (var index = 0; index < carts.Count; index++)
                {
                    if (carts[index].Crashed)
                    {
                        continue;
                    }

                    var cart = CartMove(carts[index], grid);

                    if (carts.Any(c => c.X == cart.X && c.Y == cart.Y && c.Crashed == cart.Crashed))
                    {
                        if (string.IsNullOrWhiteSpace(part1))
                        {
                            part1 = $"{cart.X},{cart.Y}";
                        }

                        cart = MarkAsCrashed(cart);
                    }

                    carts[index] = cart;

                    if (cart.Crashed)
                    {
                        for (var i = 0; i < carts.Count; i++)
                        {
                            if (carts[i].X == cart.X && carts[i].Y == cart.Y)
                            {
                                carts[i] = MarkAsCrashed(carts[i]);
                            }
                        }
                    }

                    if (carts.Count(c => c.Crashed == false) == 1)
                    {
                        var lastCart = carts.First(c => c.Crashed == false);
                        part2 = $"{lastCart.X},{lastCart.Y}";
                    }
                }
            }

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static Cart MarkAsCrashed(Cart cart)
        {
            return new Cart(cart.X, cart.Y, cart.Symbol, cart.NextTurn, true);
        }

        private static Cart CartMove(Cart cart, char[,] grid)
        {
            int x;
            int y;

            switch (cart.Symbol)
            {
                case Up:
                    x = cart.X;
                    y = cart.Y - 1;
                    break;
                case Down:
                    x = cart.X;
                    y = cart.Y + 1;
                    break;
                case Left:
                    x = cart.X - 1;
                    y = cart.Y;
                    break;
                case Right:
                    x = cart.X + 1;
                    y = cart.Y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cart));
            }

            var track = grid[x, y];

            if (track == '+')
            {
                return new Cart(x, y, TurnLogic[(cart.Symbol, cart.NextTurn)], IntersectionLogic[cart.NextTurn],
                    cart.Crashed);
            }

            var key = (cart.Symbol, track);
            return new Cart(x, y, CurveLogic.ContainsKey(key) ? CurveLogic[key] : cart.Symbol, cart.NextTurn,
                cart.Crashed);
        }
    }
}
