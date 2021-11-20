using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace _2015
{
    public class Day9 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            IEnumerable<int> routeLengths = getPossibleRoutes(input.Split(Environment.NewLine));

            return routeLengths.Min().ToString();
        }

        private IEnumerable<int> getPossibleRoutes(string[] lines)
        {
            var distances = lines
    .Select(s => System.Text.RegularExpressions.Regex.Match(s, @"^(\w+) to (\w+) = (\d+)").Groups)
    .Select(g => new { From = g[1].Value, To = g[2].Value, Distance = int.Parse(g[3].Value) })
    .ToList();

            List<string> places = distances.SelectMany(d => new[] { d.From, d.To }).Distinct().ToList();

            Func<string, string, int> getDistance = (a, b) => distances
                  .FirstOrDefault(d => (d.From == a && d.To == b) ||
                                          (d.To == a && d.From == b)).Distance;
            // Try all routes
            return places.Permutations()
                .Select(route => route.Pairwise((from, to) => getDistance(from, to)).Sum());
        }

        public string SolvePart2(string input = null)
        {
            IEnumerable<int> routeLengths = getPossibleRoutes(input.Split(Environment.NewLine));

            return routeLengths.Max().ToString();
        }

        public void Tests()
        {
            System.Diagnostics.Debug.Assert(SolvePart1(@"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141") == "605");
            System.Diagnostics.Debug.Assert(SolvePart2(@"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141") == "982");
        }
    }
}
