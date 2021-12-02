using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public abstract class PuzzleWithLongArrayInput:IAoC
    {
        public string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(Environment.NewLine).Select(x => long.Parse(x)).ToArray());
        }

        public string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(Environment.NewLine).Select(x => long.Parse(x)).ToArray());
        }

        public abstract string SolvePart1(long[] input);
        public abstract string SolvePart2(long[] input);

        public abstract void Tests();
    }
}
