using System;
using System.Linq;

namespace General
{
	public abstract class PuzzleWithIntegerArrayInput :abstractPuzzleClass 
    {
        public PuzzleWithIntegerArrayInput(int Day, int year)
            : base(Day,year)
        {
           
        }

        public override string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(input.Contains(Environment.NewLine)?Environment.NewLine:",",StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray());
        }

        public override string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(input.Contains(Environment.NewLine) ? Environment.NewLine : ",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray());
        }

        public abstract string SolvePart1(int[] input);
        public abstract string SolvePart2(int[] input);

        public override abstract void Tests();
    }
}
