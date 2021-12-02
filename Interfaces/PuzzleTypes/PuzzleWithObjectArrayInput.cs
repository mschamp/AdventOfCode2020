using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General
{
    public abstract class PuzzleWithObjectArrayInput<ObjectClass> : IAoC
    {
        public string SolvePart1(string input = null)
        {
            return SolvePart1(input.Split(Environment.NewLine).Select(x => CastToObject(x)).ToArray());
        }

        public string SolvePart2(string input = null)
        {
            return SolvePart2(input.Split(Environment.NewLine).Select(x => CastToObject(x)).ToArray());
        }

        public abstract ObjectClass CastToObject(string RawData);

        public abstract void Tests();

        public abstract string SolvePart1(ObjectClass[] input);
        public abstract string SolvePart2(ObjectClass[] input);
    }
}
