using System;

namespace General
{
	public abstract class PuzzleWithStringArrayInput:abstractPuzzleClass
    {
        public PuzzleWithStringArrayInput(int Day, int year) : base(Day, year)
        {

        }

        public override string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        }

        public override string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        }

        public abstract string SolvePart1(string[] input);
        public abstract string SolvePart2(string[] input);

        public override abstract void Tests();
    }
}
