﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020
{
	public class Day18 : General.PuzzleWithStringArrayInput
    {
        public Day18() : base(18, 2020) { }

        MatchEvaluator evaluator = new(NoBrackets);

        public override string SolvePart1(string[] input)
        {
            List<long> Results = [];
            foreach (string item in input)
            {
                Results.Add(SolveEquation1(item.Replace(" ","")));
            }
            return Results.Sum().ToString();
        }

        private long SolveEquation1(string equation)
        {
            while (equation.Contains('('))
            {
                Regex rgxfields = new(@"\(((?:\d+(\+|\*))+\d+)\)");
                Match mtch = rgxfields.Match(equation);
                string inner = mtch.Groups[1].Value;
                equation=equation.Replace(mtch.Value, SolveEquation1(inner).ToString());
            }

            while (!long.TryParse(equation, out long result))
            {
                
                equation=Regex.Replace(equation, @"^(\d+)(\+|\*)(\d+)", evaluator);                
            }
            return long.Parse(equation);
        }

        public static string NoBrackets(Match match)
        {
            switch (match.Groups[2].Value)
            {
                case "+":
                    return (long.Parse(match.Groups[1].Value) + long.Parse(match.Groups[3].Value)).ToString();
                case "*":
                    return (long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[3].Value)).ToString();
                default:
                    break;
            }
            return "";
        }

        private long SolveEquation2(string equation)
        {
            while (equation.Contains('('))
            {
                Regex rgxfields = new(@"\(((?:\d+(\+|\*))+\d+)\)");
                Match mtch = rgxfields.Match(equation);
                string inner = mtch.Groups[1].Value;
                equation = equation.Replace(mtch.Value, SolveEquation2(inner).ToString());
            }


            while (equation.Contains('+'))
            {
                equation = Regex.Replace(equation, @"(\d+)(\+)(\d+)", evaluator);
            }

            while (equation.Contains('*'))
            {
                equation = Regex.Replace(equation, @"(\d+)(\*)(\d+)", evaluator);
            }
            
            return long.Parse(equation);
        }

        public override string SolvePart2(string[] input)
        {
            List<long> Results = [];
            foreach (string item in input)
            {
                Results.Add(SolveEquation2(item.Replace(" ", "")));
            }
            return Results.Sum().ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("1 + 2 * 3 + 4 * 5 + 6") =="71");
            Debug.Assert(SolvePart1("1 + (2 * 3) + (4 * (5 + 6))") == "51");
            Debug.Assert(SolvePart1("2 * 3 + (4 * 5)") == "26");
            Debug.Assert(SolvePart1("5 + (8 * 3 + 9 + 3 * 4 * 3)") == "437");
            Debug.Assert(SolvePart1("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))") == "12240");
            Debug.Assert(SolvePart1("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2") == "13632");
            Debug.Assert(SolvePart1("(9+4*8*7*8)*((3*5*4+2*6+7)*7*3+2+3*6)+(7*7*6*8*(9+9*9*9+9+8)+3)+4*9+(6+9*5)") == "2535869082");

            Debug.Assert(SolvePart2("1 + 2 * 3 + 4 * 5 + 6") == "231");
            Debug.Assert(SolvePart2("1 + (2 * 3) + (4 * (5 + 6))") == "51");
            Debug.Assert(SolvePart2("2 * 3 + (4 * 5)") == "46");
            Debug.Assert(SolvePart2("5 + (8 * 3 + 9 + 3 * 4 * 3)") == "1445");
            Debug.Assert(SolvePart2("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))") == "669060");
            Debug.Assert(SolvePart2("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2") == "23340");
        }
    }
}
