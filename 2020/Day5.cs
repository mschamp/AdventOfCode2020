using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day5 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            List<int> SeatIDs = new List<int>();
            foreach (string item in input.Split(Environment.NewLine))
            {
                SeatIDs.Add(getSeatID(item));
            }
            return ""+SeatIDs.Max();
        }

        private int getSeatID(string input)
        {
            int row = ProcessTextAsBinary(input.Substring(0, 7),'B');
            int column = ProcessTextAsBinary(input.Substring(7, 3),'R');
            return  (row * 8 + column);
        }

        private int ProcessTextAsBinary(string text,char accept)
        {
            double row = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i]== accept)
                {
                    row += Math.Pow(2 ,text.Length - i-1);
                }
            }
            return (int)row;
        }

        public string SolvePart2(string input = null)
        {
            List<int> SeatIDs = new List<int>();
            foreach (string item in input.Split(Environment.NewLine))
            {
                SeatIDs.Add(getSeatID(item));
            }

            foreach (int item in SeatIDs)
            {
                if (SeatIDs.Contains(item+2) && !SeatIDs.Contains(item + 1))
                {
                    return "" + (item + 1);
                }
            }
            return "";
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1("BFFFBBFRRR") == "567");
            Debug.Assert(SolvePart1("FFFBBBFRRR") == "119");
            Debug.Assert(SolvePart1("BBFFBBFRLL") == "820");
        }
    }
}
