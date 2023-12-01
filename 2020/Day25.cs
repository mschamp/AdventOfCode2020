using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace _2020
{
    public class Day25 : General.PuzzleWithStringArrayInput
    {
        public Day25():base(25, 2020)
        {

        }
        public override string SolvePart1(string[] parts)
        {
            int loopsDoor = CalculateLoopSize(int.Parse(parts[1]));

            return ""+ EncryptionKey(loopsDoor, int.Parse(parts[0]));
        }

        private int CalculateLoopSize(int PublicKey)
        {
            long value = 1;
            int subjectNumber = 7;
            int loop = 0;
            while (true)
            {
                loop++; 
                value *= subjectNumber;
                value %= 20201227;
                if (value==PublicKey)
                {
                    return loop;
                }
            }
        }

        private long EncryptionKey(int loopSize, int subjectNumber)
        {
            long value = 1;
            for (int i = 0; i < loopSize; i++)
            {
                value *= subjectNumber;
                value %= 20201227;
            }
            return value;
        }

        public override string SolvePart2(string[] input)
        {
            return "";
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"5764801
17807724") == "14897079");
        }
    }
}
