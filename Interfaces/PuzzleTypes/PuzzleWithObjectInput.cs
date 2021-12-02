using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public abstract class PuzzleWithObjectInput<ObjectClass> : IAoC
    {
        public string SolvePart1(string input = null)
        {
            return SolvePart1(CastToObject(input));
        }

        public string SolvePart2(string input = null)
        {
            return SolvePart2(CastToObject(input));
        }

        public abstract ObjectClass CastToObject(string RawData);

        public abstract void Tests();

        public abstract string SolvePart1(ObjectClass input);
        public abstract string SolvePart2(ObjectClass input);
    }
}
