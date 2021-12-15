using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day8 : General.PuzzleWithStringArrayInput
    {
        public Day8() : base(8) { }

        public override string SolvePart1(string[] input)
        {
           return  input.Select(s => new
            {
                Escaped = s,
                Unescaped = System.Text.RegularExpressions.Regex.Replace(
                s.Substring(1, s.Length - 2)
                .Replace("\\\"", "\"")
                .Replace("\\\\", "?"),
                @"\\x[0-9a-f]{2}", "?")
            })
            .Sum(s => s.Escaped.Length - s.Unescaped.Length).ToString();
        }



        public override string SolvePart2(string[] input)
        {
            return input.Select(s => new
            {
                Unescaped = s,
                Exploded = "\"" + s.Replace("\\", "\\\\")
                .Replace("\"", "\\\"") +"\""
            })
             .Sum(s => s.Exploded.Length - s.Unescaped.Length).ToString();
        }

        public override void Tests()
        {
            System.Diagnostics.Debug.Assert(SolvePart1("\"\"") == "2");
            System.Diagnostics.Debug.Assert(SolvePart1("\"abc\"") == "2");
            System.Diagnostics.Debug.Assert(SolvePart1("\"aaa\\\"aaa\"") == "3");
            System.Diagnostics.Debug.Assert(SolvePart2("\"\"") == "4");
            System.Diagnostics.Debug.Assert(SolvePart2("\"abc\"") == "4");
            System.Diagnostics.Debug.Assert(SolvePart2("\"aaa\\\"aaa\"") == "6");
        }
    }
}
