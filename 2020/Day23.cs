using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day23 : General.abstractPuzzleClass
    {
        public Day23():base(23, 2020)
        {

        }

        public override string SolvePart1(string input = null)
        {
            int[] Cups = MakeList(input, input.Length);

            Cups = Simulate(Cups, int.Parse(input[0].ToString()), 100);
            
            return Answer1(Cups,1,8);
        }

        private string Answer1(int[] Cups, int Start, int Length)
        {
            string Answer = "";
            for (int i = 0; i < Length; i++)
            {
                Start = Cups[Start];
                Answer += Start;
            }
            return Answer;
        }
        private int[] Simulate (int[] Cups, int start, int NumRounds)
        {
            int max = Cups.Max();
            for (int i = 0; i < NumRounds; i++)
            {
                int i1 = Cups[start];
                int i2 = Cups[i1];
                int i3 = Cups[i2];

                int Destination = start == 1 ? max : start - 1;
                while (Destination == i1|| Destination == i2||Destination == i3)
                {
                    Destination = Destination == 1 ? max : Destination - 1;

                }

                Cups[start] = Cups[i3];
                Cups[i3] = Cups[Destination];
                Cups[Destination] = i1;

                start = Cups[start];

            }
            return Cups;
        }

        public override string SolvePart2(string input = null)
        {
            int[] Cups = MakeList(input, 1000000);

            Cups = Simulate(Cups, int.Parse(input[0].ToString()), 10000000);

            return ""+Answer2(Cups);
        }

        private long Answer2(int[] Cups)
        {
            return (long)Cups[1] * (long)Cups[Cups[1]];
        }

        private int[] MakeList(string input, int length)
        {
            int[] list = new int[length+1];
            int PreviousNumber = -1;

            foreach (char label in input)
            {
                if (PreviousNumber>0)
                {
                    list[PreviousNumber] = int.Parse(label.ToString());
                }
                PreviousNumber = int.Parse(label.ToString());
            }

            for (int i = input.Length+1; i < length+1; i++)
            {
                list[PreviousNumber] = i;
                PreviousNumber = i;
            }
            list[PreviousNumber] = int.Parse(input[0].ToString());

            return list;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("389125467") == "67384529");
            Debug.Assert(SolvePart2("389125467") == "149245887792");
        }
    }
}
