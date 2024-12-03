using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2016
{
    public class Day3:abstractPuzzleClass
    {
        public Day3() : base(3, 2016)
        { }

        public override string SolvePart1(string input)
        {
            int counter = 0;

            foreach (var item in input.Split(Environment.NewLine))
            {
                IEnumerable<int> sides = item.Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

                if (sides.Max() < sides.Order().Take(2).Sum()) counter++;
            }

            return counter.ToString();
        }

        public override string SolvePart2(string input)
        {
            int counter = 0;
            List<int[]> lines = input.Split(Environment.NewLine).Select(x=>x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray()).ToList();

            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < lines.Count; i = i + 3)
                {
                    List<int> sides = [lines[i][k], lines[i + 1][k], lines[i + 2][k]];
                    if (sides.Max() < sides.Order().Take(2).Sum()) counter++;
                }
            }

            return counter.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("5 10 25") == "0");
        }
    }
}
