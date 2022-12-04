using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day4 : General.PuzzleWithObjectArrayInput<((int, int), (int, int))>
    {
        public Day4() : base(4)
        {
        }

        public override ((int, int), (int, int)) CastToObject(string RawData)
        {
            var parts = RawData.Split(new char[] { ',', '-' }).Select(x=>int.Parse(x)).ToArray();
            return ((parts[0], parts[1]), (parts[2], parts[3]));

        }

        public override string SolvePart1(((int, int), (int, int))[] input)
        {
            return input.Count(x => FullOverlap(x)).ToString();
        }

        public bool FullOverlap (((int min, int max)l1, (int min , int max)l2)line)
        {
            if (line.l1.min<= line.l2.min && line.l1.max >=line.l2.max) return true;
            if (line.l2.min <= line.l1.min && line.l2.max >= line.l1.max) return true;
            return false;
        }

        public override string SolvePart2(((int, int), (int, int))[] input)
        {
            return input.Count(x => PartialOverlap(x)).ToString();
        }

        public bool PartialOverlap(((int min, int max) l1, (int min, int max) l2) line)
        {
            if (line.l1.min <= line.l2.min && line.l1.max >= line.l2.min) return true;
            if (line.l2.min <= line.l1.min && line.l2.max >= line.l1.min) return true;
            
            if (line.l1.max <= line.l2.max && line.l1.max >= line.l2.min) return true;
            if (line.l2.max <= line.l1.max && line.l2.max >= line.l1.min) return true;
            return false;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8") == "2");

            Debug.Assert(SolvePart2(@"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8") == "4");
        }
    }
}
