using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public interface IAoC
    {
        public string SolvePart1(string input = null);
        public string SolvePart2(string input = null);
        public void Tests();
    }
}
