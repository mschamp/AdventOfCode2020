using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day3 : General.PuzzleWithStringArrayInput
    {
        public Day3() : base(3, 2022)
        {
        }

        public override string SolvePart1(string[] input)
        {
            return input.Select(x => GetScorePack(x)).Sum().ToString();           
        }

        private int GetScorePack(string x)
        {
            return GetValue(x.Substring(0, x.Length / 2).Intersect(x.Substring(x.Length / 2, x.Length / 2)).First());
        }

        private int GetValue(char c)
        {
            if (char.IsLower(c)) return c - 'a' + 1;
            return c - 'A' + 27;
        }

        public override string SolvePart2(string[] input)
        {
            int score = 0;
            for (int i = 0; i < input.Length; i+=3)
            {
                score += GetValue(getCommon(input[i], input[i+1], input[i+2]));
            }
            return score.ToString();
        }

        private char getCommon(string v1, string v2, string v3)
        {
           return v1.Intersect(v2).Intersect(v3).First();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw") == "157");

            Debug.Assert(SolvePart2(@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw") == "70");
        }
    }
}
