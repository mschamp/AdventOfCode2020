using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day5 : General.PuzzleWithStringArrayInput
    {
        public Day5():base(5){}
        public override string SolvePart1(string[] input)
        {
            Func<string, int> vowel = General.Validators.RegexMatchCountValidator(@"a|e|i|o|u");
            Func<string, bool> doubleValidator =  General.Validators.DoubleLetterValidator();
            Func<string, bool> stringValidator =  General.Validators.RegexValidator("ab|cd|pq|xy");

            return "" + input.Where(x => doubleValidator(x) && !stringValidator(x) && vowel(x) >= 3).Count();
        }


        public override string SolvePart2(string[] input)
        {
            Func<string, bool>[] req = { General.Validators.RegexValidator(@"(\w)\w(\1)"),
            General.Validators.RegexValidator(@"(\w{2})\w*(\1)")};

            return "" + input.Where(x => req.All(y => y(x))).Count();
        }

        public override void Tests()
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
