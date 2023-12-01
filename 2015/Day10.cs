using System;
using System.Collections.Generic;
using System.Text;

namespace _2015
{
    public class Day10 : General.abstractPuzzleClass
    {
        public Day10() : base(10, 2015)
        {

        }

        public override string SolvePart1(string input = null)
        {
            for (int i = 0; i < 40; i++)
            {
                input = LookAndSay(input);
            }
            return input.Length.ToString();
        }

        public override string SolvePart2(string input = null)
        {
            for (int i = 0; i < 50; i++)
            {
                input = LookAndSay(input);
            }
            return input.Length.ToString();
        }

        private string LookAndSay(string input)
        {
            StringBuilder result = new();
            char current=' ';
            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (counter>0 && current!= input[i])
                {
                    result.Append(counter.ToString());
                    result.Append(current);
                    counter = 0;
                }
                current = input[i];
                counter++;
            }
            result.Append(counter.ToString());
            result.Append(current);

            return result.ToString();
        }

        public override void Tests()
        {
            System.Diagnostics.Debug.Assert(LookAndSay("1")=="11");
            System.Diagnostics.Debug.Assert(LookAndSay("11") == "21");
            System.Diagnostics.Debug.Assert(LookAndSay("21") == "1211");
            System.Diagnostics.Debug.Assert(LookAndSay("1211") == "111221");
            System.Diagnostics.Debug.Assert(LookAndSay("111221") == "312211");
        }
    }
}
