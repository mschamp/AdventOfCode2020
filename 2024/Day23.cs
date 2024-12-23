using Interfaces.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day23:PuzzleWithObjectInput<(HashSet<string> computers, HashSet<(string,string)> networks)>
    {
        public Day23():base(23,2024)
        {
            
        }

        public override string SolvePart1((HashSet<string> computers, HashSet<(string, string)> networks) input)
        {
            var combinations = input.computers.DifferentCombinations(3).Where(x => x.Any(y => y.StartsWith("t", StringComparison.Ordinal)))
                .Select(x => x.ToArray())
                .Where(x => input.networks.Contains((x[0], x[1])) && input.networks.Contains((x[1], x[2])) && input.networks.Contains((x[0], x[2])));

            return combinations.Count().ToString();
        }

        public override string SolvePart2((HashSet<string> computers, HashSet<(string, string)> networks) input)
        {
            var networks = input.computers.Select(c => new HashSet<string> { c }).ToList();
            foreach (var n in networks)
            {
                foreach (var c in input.computers)
                {
                    if (n.All(d => input.networks.Contains((d, c))))
                    {
                        n.Add(c);
                    }
                }
            }

            var largestNetwork = networks.OrderByDescending(n => n.Count).First();
            return string.Join(",", largestNetwork.OrderBy(x => x));
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn") == "7");

            Debug.Assert(SolvePart2(@"kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn") == "co,de,ka,ta");
        }

        protected override (HashSet<string> computers, HashSet<(string, string)> networks) CastToObject(string RawData)
        {
            HashSet<string> computers = new();
            HashSet<(string, string)> networks = new();

            foreach (var line in RawData.Split(Environment.NewLine))
            {
                string[] parts = line.Split('-');

                computers.Add(parts[0]);
                computers.Add(parts[1]);
                networks.Add((parts[0], parts[1]));
                networks.Add((parts[1], parts[0]));
            }

            return (computers, networks);
        }
    }
}
