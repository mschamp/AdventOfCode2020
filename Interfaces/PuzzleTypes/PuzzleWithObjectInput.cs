using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public abstract class PuzzleWithObjectInput<ObjectClass> : abstractPuzzleClass
    {
        public PuzzleWithObjectInput(int Day, int year) : base(Day, year)
        {

        }

        public override string SolvePart1(string input = null)
        {
            return SolvePart1(CastToObject(input));
        }

        public override string SolvePart2(string input = null)
        {
            return SolvePart2(CastToObject(input));
        }

        protected abstract ObjectClass CastToObject(string RawData);

        public override abstract void Tests();

        public abstract string SolvePart1(ObjectClass input);
        public abstract string SolvePart2(ObjectClass input);
    }
}
