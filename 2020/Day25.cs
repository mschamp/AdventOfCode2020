using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace _2020
{
    public class Day25 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] parts = input.Split(Environment.NewLine);
            int loopsCard = CalculateLoopSize(int.Parse(parts[0]));
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
                value = value % 20201227;
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
                value = value % 20201227;
            }
            return value;
        }

        public string SolvePart2(string input = null)
        {
            return "";
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"5764801
17807724") == "14897079");
        }
    }
}
