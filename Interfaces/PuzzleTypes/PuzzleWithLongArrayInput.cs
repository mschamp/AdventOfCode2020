using System;
using System.Linq;

namespace General
{
	public abstract class PuzzleWithLongArrayInput:abstractPuzzleClass
    {
        public PuzzleWithLongArrayInput(int Day, int year)
            : base(Day, year)
        {

        }

        public override string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray());
        }

        public override string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray());
        }

        public abstract string SolvePart1(long[] input);
        public abstract string SolvePart2(long[] input);

        public override abstract void Tests();
    }
}
