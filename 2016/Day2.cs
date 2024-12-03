using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2016
{
    public class Day2:General.PuzzleWithStringArrayInput
    {
        public Day2():base(2,2016)
        {

        }

        public override string SolvePart1(string[] input)
        {
            (int x, int y) pos = (0, 0);
            List<int> values = [];

            foreach (var item in input)
            {
                foreach (var item2 in item)
                {
                    switch (item2)
                    {
                        case 'L':
                            pos.x = Math.Max(0, pos.x - 1);
                            break;
                        case 'R':
                            pos.x = Math.Min(2, pos.x + 1);
                            break;
                        case 'U':
                            pos.y = Math.Max(0, pos.y - 1);
                            break;
                        case 'D':
                            pos.y = Math.Min(2, pos.y + 1);
                            break;
                    }
                }

                values.Add(1+pos.x+3*pos.y);
            }

            return string.Concat(values);
        }

        public override string SolvePart2(string[] input)
        {
            (int x, int y) pos = (-2, 0);
            Dictionary<(int x, int y), char> KeyPad = new Dictionary<(int x, int y), char>() { { (0, -2), '1' },
                { (-1, -1), '2' },{ (0, -1), '3' },{ (1, -1), '4' },
            { (-2, 0), '5' },{ (-1, 0), '6' },{ (0, 0), '7' },{ (1, 0), '8' },{ (2, 0), '9' },
            { (-1, 1), 'A' },{ (0, 1), 'B' },{ (1, 1), 'C' },
            { (0, 2), 'D' }};
            List<char> values = [];

            foreach (var item in input)
            {
                foreach (var item2 in item)
                {
                    switch (item2)
                    {
                        case 'L':
                            pos.x = Math.Max(-2+Math.Abs(pos.y), pos.x - 1);
                            break;
                        case 'R':
                            pos.x = Math.Min(2 - Math.Abs(pos.y), pos.x + 1);
                            break;
                        case 'U':
                            pos.y = Math.Max(-2 + Math.Abs(pos.x), pos.y - 1);
                            break;
                        case 'D':
                            pos.y = Math.Min(2- +Math.Abs(pos.x), pos.y + 1);
                            break;
                    }
                }

                values.Add(KeyPad[pos]);
            }
            return string.Concat(values);
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"ULL
RRDDD
LURDL
UUUUD") == "1985");
            Debug.Assert(SolvePart2(@"ULL
RRDDD
LURDL
UUUUD") == "5DB3");
        }
    }
}
