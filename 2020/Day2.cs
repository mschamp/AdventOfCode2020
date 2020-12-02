using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day2 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            Regex reg = new Regex(@"(\d+)-(\d+)\s(.):\s(.+)");
            string[] entries = input.Split(Environment.NewLine);
            int OKCounter = 0;
            foreach (string entry in entries)
            {
                MatchCollection matches = reg.Matches(entry);
                if (PassWordOK1(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value), matches[0].Groups[3].Value.First(), matches[0].Groups[4].Value))
                {
                    OKCounter++;
                }
            }
            return "" + OKCounter;
        }

        public string SolvePart2(string input = null)
        {
            Regex reg = new Regex(@"(\d+)-(\d+)\s(.):\s(.+)");
            string[] entries = input.Split(Environment.NewLine);
            int OKCounter = 0;
            foreach (string entry in entries)
            {
                MatchCollection matches = reg.Matches(entry);
                if (PassWordOK2(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value), matches[0].Groups[3].Value.First(), matches[0].Groups[4].Value))
                {
                    OKCounter++;
                }
            }
            return "" + OKCounter;
        }


        private bool PassWordOK1(int min, int max, char Letter, string passw)
        {
            int number = passw.ToCharArray().Count(c => c == Letter);
            if (number >= min &&  number <= max)
            {
                return true;
            }
            return false;
        }

        private bool PassWordOK2(int p1, int p2, char Letter, string passw)
        {
            char[] passchar = passw.ToCharArray();
            if (passchar[p1-1]==Letter && passchar[p2 - 1] != Letter)
            {
                return true;
            }
            if (passchar[p2 - 1] == Letter && passchar[p1 - 1] != Letter)
            {
                return true;
            }
            return false;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc") == "2");

            Debug.Assert(SolvePart2("1-3 a: abcde") == "1");
            Debug.Assert(SolvePart2("1-3 b: cdefg") == "0");
            Debug.Assert(SolvePart2("2-9 c: ccccccccc") == "0");
        }
    }
}
