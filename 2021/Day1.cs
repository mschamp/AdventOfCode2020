using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2021
{
    public class Day1 : General.PuzzleWithIntegerArrayInput
    {
        public override string SolvePart1(int[] input)
        {
            int Previous = input[0];
            int larger = 0;

            for (int i = 1; i < input.Count(); i++)
            {
                if (input[i] > Previous) larger++;
                Previous = input[i];
            }
            return larger.ToString();
        }

        public override string SolvePart2(int[] input)
        {
            int Previous = input[0]+ input[1]+ input[2];
            int larger = 0;
            for (int i = 3; i < input.Count(); i++)
            {
                int moving = input[i] + input[i - 1] + input[i - 2];
                if (moving > Previous) larger++;
                Previous = moving;
            }
            return larger.ToString();
        }

        public override void Tests()
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
