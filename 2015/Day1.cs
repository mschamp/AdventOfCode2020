using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2015
{
    public class Day1 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            int floor = 0;
            foreach (char item in input)
            {
                switch (item)
                {
                    case '(':
                        floor++;
                        break;
                    case ')':
                        floor--;
                        break;
                    default:
                        break;
                }
            }
            return ""+floor;
        }

        public string SolvePart2(string input = null)
        {
            int count = 0;
            int floor = 0;
            foreach (char item in input)
            {
                count++;
                switch (item)
                {
                    case '(':
                        floor++;
                        break;
                    case ')':
                        floor--;
                        if (floor<0)
                        {
                            return ""+count;
                        }
                        break;
                    default:
                        break;
                }
            }
            return "";
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"(())") == "0");
            Debug.Assert(SolvePart1(@"()()") == "0");
            Debug.Assert(SolvePart1(@"(((") == "3");
            Debug.Assert(SolvePart1(@"(()(()(") == "3");
            Debug.Assert(SolvePart1(@"))(((((") == "3");
            Debug.Assert(SolvePart1(@"())") == "-1");
            Debug.Assert(SolvePart1(@"))(") == "-1");
            Debug.Assert(SolvePart1(@")))") == "-3");
            Debug.Assert(SolvePart1(@")())())") == "-3");
        }
    }
}
