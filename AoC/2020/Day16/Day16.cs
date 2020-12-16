using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC._2020.Day16
{
    public class Day16 : ISolution
    {
        public void Execute()
        {
            var input = Utils.LoadInputLines().SkipLast(1).ToList();
            var rules = input.Where(l => l.Contains(" or "))
                .Select(r => r.Split(":").Last())
                .ToList();

            var rulesNumerical = new List<HashSet<int>>();
            var allValidNumbers = new HashSet<int>();
            foreach (var rule in rules)
            {
                var ruleRanges = rule.Split(" or ");
                var lowerRange = ruleRanges[0].Split("-").Select(int.Parse).ToList();
                var upperRange = ruleRanges[1].Split("-").Select(int.Parse).ToList();

                var ruleNumbers = 
                    new HashSet<int>(Enumerable.Range(lowerRange[0], lowerRange[1] - lowerRange[0] + 1)
                        .Union(Enumerable.Range(upperRange[0], upperRange[1] - upperRange[0] + 1)));
                rulesNumerical.Add(ruleNumbers);
                allValidNumbers.UnionWith(ruleNumbers);
            }

            var nearbyTicketsIndex = input.IndexOf("nearby tickets:") + 1;

            var ticketScanningErrorRate = 0;

            var validTickets = new List<List<int>>();
            for (var i = nearbyTicketsIndex; i < input.Count; i++)
            {
                var ticket = input[i].Split(",").Select(int.Parse).ToList();
                var isValid = true;
                foreach (var numberInTicket in ticket)
                {
                    if (!allValidNumbers.Contains(numberInTicket))
                    {
                        ticketScanningErrorRate += numberInTicket;
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    validTickets.Add(ticket);
                }
            }

            var possibleRulesMatch = new List<(int Rule, int Col)>();

            for (var i = 0; i < validTickets[0].Count; i++)
            {
                var values = validTickets.Select(t => t[i]).ToList();
                for (var ruleIndex = 0; ruleIndex < rulesNumerical.Count; ruleIndex++)
                {
                    var departureRule = rulesNumerical[ruleIndex];
                    if (values.All(v => departureRule.Contains(v)))
                    {
                        possibleRulesMatch.Add((ruleIndex, i));
                    }
                }
            }

            var ruleToColumnMatch = new List<(int Rule, int Col)>();
            for (var i = 0; i < rulesNumerical.Count; i++)
            {
                var matchedColumn = possibleRulesMatch
                    .GroupBy(c => c.Col).Select(c => (Col: c.Key, Count: c.Count()))
                    .First(c => c.Count == 1).Col;

                var matchedRule = possibleRulesMatch.First(c => c.Col == matchedColumn).Rule;
                ruleToColumnMatch.Add((matchedRule, matchedColumn));
                possibleRulesMatch = possibleRulesMatch.Where(c => c.Rule != matchedRule).ToList();
            }

            var yourTicketIndex = input.IndexOf("your ticket:") + 1;
            var yourTicket = input[yourTicketIndex].Split(",").Select(int.Parse).ToList();

            var departureValuesProduct = ruleToColumnMatch
                .Where(c => c.Rule < 6)
                .Select(c => c.Col)
                .Select(d => yourTicket[d]).Aggregate(1L, (current, v) => current * v);

            var part1 = ticketScanningErrorRate;
            var part2 = departureValuesProduct;

            Console.WriteLine($"Part1 {part1}");
            Console.WriteLine($"Part2 {part2}");
        }
    }
}
