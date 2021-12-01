using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2021
{
    public class Day1 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            List<int> depths = input.Split(Environment.NewLine).Select(x => int.Parse(x)).ToList();

            int Previous = depths.First();
            int larger = 0;

            for (int i = 1; i < depths.Count(); i++)
            {
                if (depths[i] > Previous) larger++;
                Previous = depths[i];
            }
            return larger.ToString();
        }

        public string SolvePart2(string input = null)
        {
            List<int> depths = input.Split(Environment.NewLine).Select(x => int.Parse(x)).ToList();

            int Previous = depths[0]+ depths[1]+ depths[2];
            int larger = 0;

            for (int i = 3; i < depths.Count(); i++)
            {
                int moving = depths[i] + depths[i - 1] + depths[i - 2];
                if (moving > Previous) larger++;
                Previous = moving;
            }
            return larger.ToString();
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"199
200
208
210
200
207
240
269
260
263") == "7");
            Debug.Assert(SolvePart2(@"199
200
208
210
200
207
240
269
260
263") == "5");
        }
    }
}
