using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day2 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            long area = 0;
            foreach (string box in input.Split(Environment.NewLine))
            {
                int[] sizes = box.Split('x').Select(x => int.Parse(x)).ToArray();
                int a1 = sizes[0] * sizes[1];
                int a2 = sizes[0] * sizes[2];
                int a3 = sizes[2] * sizes[1];
                area += 2 * (a1 +a2  +a3 )+Math.Min(a1, Math.Min(a2,a3));

            }
            return "" + area;
            
        }

        public string SolvePart2(string input = null)
        {
            long Ribbon = 0;
            foreach (string box in input.Split(Environment.NewLine))
            {
                int[] sizes = box.Split('x').Select(x => int.Parse(x)).ToArray();
                Ribbon += sizes.Aggregate(1,(volume,next)=>volume*=next) + 2* (sizes.Sum()-sizes.Max());

            }
            return "" + Ribbon;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"2x3x4") == "58");
            Debug.Assert(SolvePart1(@"1x1x10") == "43");

            Debug.Assert(SolvePart2(@"2x3x4") == "34");
            Debug.Assert(SolvePart2(@"1x1x10") == "14");
        }
    }
}
