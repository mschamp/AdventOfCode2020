using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public abstract class abstractPuzzleClass : General.IAoC
    {
        public abstractPuzzleClass(int Day)
        {
            this.Day = Day;
        }

        public int Day { get; private set; }

        public abstract string SolvePart1(string input = null);

        public abstract string SolvePart2(string input = null);

        public abstract void Tests();
    }
}
