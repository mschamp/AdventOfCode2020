﻿using System;
using System.Linq;

namespace General
{
	public abstract class PuzzleWithObjectArrayInputMultiLine<ObjectClass> : abstractPuzzleClass
    {
        public PuzzleWithObjectArrayInputMultiLine(int Day, int year):base(Day, year)
        {

        }

        public override string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(input.Contains(Environment.NewLine+Environment.NewLine)? Environment.NewLine + Environment.NewLine : Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => CastToObject(x)).ToArray());
        }

        public override string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(input.Contains(Environment.NewLine + Environment.NewLine) ? Environment.NewLine + Environment.NewLine : Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => CastToObject(x)).ToArray());
        }

        protected abstract ObjectClass CastToObject(string RawData);

        public override abstract void Tests();

        public abstract string SolvePart1(ObjectClass[] input);
        public abstract string SolvePart2(ObjectClass[] input);
    }
}
