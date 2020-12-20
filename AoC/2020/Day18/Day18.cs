using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC._2020.Day18
{
    public class Day18 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines()
                .SkipLast(1)
                .Select(line => Regex.Replace(line, @"\s+", ""))
                .ToList();

            var part1 = input.Sum(line => EvaluateExpression(line));
            var part2 = input.Sum(line => EvaluateExpression(line, true));

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }

        private static long EvaluateExpression(string line, bool part2 = false)
        {
            var operations = new Stack<char>();
            var values = new Stack<long>();

            foreach (var symbol in line)
            {
                switch (symbol)
                {
                    case '(':
                        operations.Push('(');
                        break;
                    case ')':
                        Eval(operations, values);
                        operations.Pop();
                        break;
                    case '*':
                        Eval(operations, values);
                        operations.Push('*');
                        break;
                    case '+':
                        Eval(operations, values, part2 ? "(*" : "(");
                        operations.Push('+');
                        break;
                    default:
                        values.Push((long)char.GetNumericValue(symbol));
                        break;
                }
            }

            Eval(operations, values);

            return values.First();
        }

        private static void Eval(Stack<char> operations, Stack<long> values, string precedenceOperators = "(")
        {
            while (operations.Count > 0 && !precedenceOperators.Contains(operations.Peek()))
            {
                var value = operations.Pop() == '+' ? values.Pop() + values.Pop() : values.Pop() * values.Pop();
                values.Push(value);
            }
        }
    }

}

