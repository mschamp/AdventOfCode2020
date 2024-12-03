using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2021
{
	public class Day10 : General.PuzzleWithStringArrayInput
    {
        public Day10():base(10, 2021)
        {

        }

        private Dictionary<char, char> brackets = new() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };

        public override string SolvePart1(string[] input)
        {
            Dictionary<char, int> points = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            Stack<char> symbols = new();
            int score = 0;
            foreach (string line in input)
            {
                foreach (char symbol in line)
                {
                    if (brackets.ContainsKey(symbol)) symbols.Push(symbol);
                    else
                    {
                        char topStack = symbols.Pop();
                        if (symbol != brackets[topStack])
                        {
                           score += points[symbol];
                        }
                    }
                }
            }
            return score.ToString();
        }

        public override string SolvePart2(string[] input)
        {
            Dictionary<char, int> points = new() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
            List<long> scores = [];
            foreach (string line in input)
            {
                Stack<char> symbols = new ();
                long score = 0;
                bool corrupted = false;
                foreach (char symbol in line)
                {
                    if (brackets.ContainsKey(symbol)) symbols.Push(symbol);
                    else
                    {
                        char topStack = symbols.Pop();
                        if (symbol != brackets[topStack])
                        {
                            corrupted = true;
                            break;
                        }
                    }
                }
                while (!corrupted && symbols.Any())
                {
                    char symbol = symbols.Pop();
                    if (points.ContainsKey(symbol)) score = score * 5 + points[symbol];
                }
                if (score > 0) scores.Add(score);
            }
            scores.Sort();
            return scores[scores.Count / 2].ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]") == "26397");

            Debug.Assert(SolvePart2(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]") == "288957");
        }
    }
}
