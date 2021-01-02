using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day5 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            Func<string, int> vowel = General.Validators.RegexMatchCountValidator(@"a|e|i|o|u");
            Func<string, bool> doubleValidator =  General.Validators.DoubleLetterValidator();
            Func<string, bool> stringValidator =  General.Validators.RegexValidator("ab|cd|pq|xy");

            return "" + input.Split(Environment.NewLine).Where(x => doubleValidator(x) && !stringValidator(x) && vowel(x) >= 3).Count();
        }


        public string SolvePart2(string input = null)
        {
            Func<string, bool>[] req = { General.Validators.RegexValidator(@"(\w)\w(\1)"),
            General.Validators.RegexValidator(@"(\w{2})\w*(\1)")};

            return "" + input.Split(Environment.NewLine).Where(x => req.All(y => y(x))).Count();
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"ugknbfddgicrmopn") == "1");
            Debug.Assert(SolvePart1(@"aaa") == "1");
            Debug.Assert(SolvePart1(@"jchzalrnumimnmhp") == "0");
            Debug.Assert(SolvePart1(@"haegwjzuvuyypxyu") == "0");
            Debug.Assert(SolvePart1(@"dvszwmarrgswjxmb") == "0");

            Debug.Assert(SolvePart2(@"qjhvhtzxzqqjkmpb") == "1");
            Debug.Assert(SolvePart2(@"xxyxx") == "1");
            Debug.Assert(SolvePart2(@"aaa") == "0");
            Debug.Assert(SolvePart2(@"uurcxstgmygtbstg") == "0");
            Debug.Assert(SolvePart2(@"ieodomkazucvgmuy") == "0");
        }
    }
}
