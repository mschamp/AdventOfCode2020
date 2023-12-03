using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2021
{
	public class Day14:General.PuzzleWithObjectInput<(string template,Dictionary<string, string[]> rules)>
    {
        public Day14() : base(14, 2021) { }
        protected override (string, Dictionary<string, string[]>) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine+Environment.NewLine);
            Dictionary<string, string[]> dict = parts[1].Split(Environment.NewLine).Select(x => resultProducts(x.Split(" -> "))).ToDictionary(x => x.Item1, x => x.Item2);

            return (parts[0], dict);
        }

        private (string,string[]) resultProducts(string[] parts)
        {
            return (parts[0], new[] { parts[0][0] + parts[1], parts[1]+ parts[0][1] });
        }

        public override string SolvePart1((string, Dictionary<string, string[]>) input)
        {
            Dictionary<string, long> pairs = new();
            for (int i = 0; i < input.Item1.Length-1; i++)
            {
                pairs.Add(input.Item1.Substring(i, 2), 1);
            }

            for (int i = 0; i < 10; i++)
            {
                pairs = Step(pairs, input.Item2);
            }

            Dictionary<char, long> Counts = CountLetter(pairs, input.Item1);

            return (Counts.Max(x => x.Value)-Counts.Min(x => x.Value)).ToString();
        }

        private Dictionary<char,long> CountLetter(Dictionary<string, long> pairs, string original)
        {
            Dictionary<char, long> result = new();
            foreach (var pair in pairs)
            {
                if (!result.ContainsKey(pair.Key[0])) result.Add(pair.Key[0], 0);
                result[pair.Key[0]] += pair.Value;
            }

            result[original.Last()]++;
            return result;
        }


        private Dictionary<string, long> Step(Dictionary<string, long> pairs, Dictionary<string, string[]> rules)
        {
            Dictionary<string, long> result = new();

            foreach (var pair in pairs)
            {
                foreach (var newPair in rules[pair.Key])
                {
                    if (!result.ContainsKey(newPair)) result.Add(newPair, 0);
                    result[newPair] += pair.Value;
                }
            }

            return result;
        }

        public override string SolvePart2((string, Dictionary<string, string[]>) input)
        {
            Dictionary<string, long> pairs = new();
            for (int i = 0; i < input.Item1.Length - 1; i++)
            {
                pairs.Add(input.Item1.Substring(i, 2), 1);
            }

            for (int i = 0; i < 40; i++)
            {
                pairs = Step(pairs, input.Item2);
            }

            Dictionary<char, long> Counts = CountLetter(pairs, input.Item1);

            return (Counts.Max(x => x.Value) - Counts.Min(x => x.Value)).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C") == "1588");
        }
    }
}
