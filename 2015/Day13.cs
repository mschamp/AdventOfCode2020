using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace _2015
{
    public class Day13 : General.PuzzleWithStringArrayInput
    {
        public Day13():base(13)
        {

        }

        public override string SolvePart1(string[] input)
        {
            var relations = input
    .Select(s => System.Text.RegularExpressions.Regex.Match(s, @"^(\w+)\s\w+\s(\w+)\s(\d+)(?:\s\w+){6}\s(\w+)").Groups)
    .Select(g => new { Person1 = g[1].Value, Person2 = g[4].Value, Happiness = g[2].Value=="gain"?int.Parse(g[3].Value):-1* int.Parse(g[3].Value) })
    .ToList();

            List<string> persons = relations.SelectMany(d => new[] { d.Person1, d.Person2 }).Distinct().ToList();

            Func<string, string, int> getHappiness = (a, b) => relations
                  .Where(d => (d.Person1 == a && d.Person2 == b) ||
                                          (d.Person2 == a && d.Person1 == b)).Sum(x=> x.Happiness);
            var permutations = persons.Permutations().Select(perm => perm.ToList().Concat(perm.First()));
            var result = permutations
                .Select(route => route.Pairwise((from, to) => getHappiness(from, to)).Sum());
            return result.Max().ToString();
        }

        public override string SolvePart2(string[] input)
        {
            var relations = input
   .Select(s => System.Text.RegularExpressions.Regex.Match(s, @"^(\w+)\s\w+\s(\w+)\s(\d+)(?:\s\w+){6}\s(\w+)").Groups)
   .Select(g => new { Person1 = g[1].Value, Person2 = g[4].Value, Happiness = g[2].Value == "gain" ? int.Parse(g[3].Value) : -1 * int.Parse(g[3].Value) })
   .ToList();

            List<string> persons = relations.SelectMany(d => new[] { d.Person1, d.Person2 }).Distinct().ToList();

            persons.Add("Me");

            Func<string, string, int> getHappiness = (a, b) => relations
                  .Where(d => (d.Person1 == a && d.Person2 == b) ||
                                          (d.Person2 == a && d.Person1 == b)).Sum(x => x.Happiness);
            var permutations = persons.Permutations().Select(perm => perm.ToList().Concat(perm.First()));
            var result = permutations
                .Select(route => route.Pairwise((from, to) => getHappiness(from, to)).Sum());
            return result.Max().ToString();
        }

        public override void Tests()
        {
            System.Diagnostics.Debug.Assert(SolvePart1(@"Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol") == "330");
        }
    }
}
