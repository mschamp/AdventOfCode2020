using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public abstract class PuzzleWithStringArrayInput:IAoC
    {
        public string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(Environment.NewLine));
        }

        public string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(Environment.NewLine));
        }

        public abstract string SolvePart1(string[] input);
        public abstract string SolvePart2(string[] input);

        public abstract void Tests();
    }
}
