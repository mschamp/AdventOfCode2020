using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public abstract class PuzzleWithIntegerArrayInput : IAoC
    {
        public string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(input.Contains(Environment.NewLine)?Environment.NewLine:",").Select(x => int.Parse(x)).ToArray());
        }

        public string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(input.Contains(Environment.NewLine) ? Environment.NewLine : ",").Select(x => int.Parse(x)).ToArray());
        }

        public abstract string SolvePart1(int[] input);
        public abstract string SolvePart2(int[] input);

        public abstract void Tests();
    }
}
