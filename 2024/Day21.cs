using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day21 : PuzzleWithStringArrayInput
    {
        public Day21():base(21,2024)
        {
        }

        private class Keypad
        {
            public static readonly Dictionary<string, (int, int)> Numpad = new()
        {
            {"7", (0, 0)},
        {"8", (1, 0)},
        {"9", (2, 0)},
        {"4", (0, 1)},
        {"5", (1, 1)},
        {"6", (2, 1)},
        {"1", (0, 2)},
        {"2", (1, 2)},
        {"3", (2, 2)},
        {" ", (0, 3)},
        {"0", (1, 3)},
        {"A", (2, 3)}
    };

            public static readonly Dictionary<string, (int, int)> Dirpad = new()
    {
        {" ", (0, 0)},
        {"^", (1, 0)},
        {"A", (2, 0)},
        {"<", (0, 1)},
        {"v", (1, 1)},
        {">", (2, 1)}
    };
        }

        private enum KeypadType
        {
            NUMPAD,
            DIRPAD
        }



        public override string SolvePart1(string[] input)
        {
            Dictionary<(string, int, KeypadType), long> cache = new();
            return input.Sum(code => int.Parse(code[..^1]) * FindShortestSequence(code, 2, cache)).ToString();
        }

        private long FindShortestSequence(string code, int proxies, Dictionary<(string, int, KeypadType), long> cache, KeypadType keypadType = KeypadType.NUMPAD)
        {
            if (cache.TryGetValue((code, proxies, keypadType), out long cachedValue))
            {
                return cachedValue;
            }

            List<List<string>> sequences = KeypadControlSequences(code, keypadType);
            long result;

            if (proxies == 0)
            {
                result = sequences.Min(sequence => sequence.Sum(part => part.Length));
            }
            else
            {
                result = sequences.Min(sequence => sequence.Sum(dircode => FindShortestSequence(dircode, proxies - 1,cache, KeypadType.DIRPAD)));
            }

            cache[(code, proxies, keypadType)] = result;
            return result;
        }

        private List<List<string>> KeypadControlSequences(string code, KeypadType keypadType, string start = "A")
        {
            if (string.IsNullOrEmpty(code))
            {
                return new List<List<string>> { new List<string>() };
            }

            Dictionary<string, (int, int)> keypadPositions = keypadType == KeypadType.NUMPAD ? Keypad.Numpad : Keypad.Dirpad;
            (int, int) position = keypadPositions[start];
            (int, int) nextPosition = keypadPositions[code[0].ToString()];
            (int, int) gap = keypadPositions[" "];

            return KeypadControlSequences(code[1..], keypadType, start: code[0].ToString())
                .SelectMany(sequence => GetMoveOptions(position, nextPosition, gap)
                    .Select(option => new List<string> { option }.Concat(sequence).ToList()))
                .ToList();
        }

        private IEnumerable<string> GetMoveOptions((int X,int Y) position, (int X, int Y) nextPosition, (int X, int Y) gap)
        {
            char horizontalArrow = nextPosition.X < position.X ? '<' : '>';
            char verticalArrow = nextPosition.Y < position.Y ? '^' : 'v';
            int horizontalDistance = Math.Abs(position.X - nextPosition.X);
            int verticalDistance = Math.Abs(position.Y - nextPosition.Y);

            if (position.Equals(nextPosition))
            {
                yield return "A";
            }
            else if (position.X == nextPosition.X)
            {
                yield return new string(verticalArrow, verticalDistance) + "A";
            }
            else if (position.Y == nextPosition.Y)
            {
                yield return new string(horizontalArrow, horizontalDistance) + "A";
            }
            else
            {
                if (!((gap.X == nextPosition.X && NonemptyRange(position.Y, nextPosition.Y).Contains(gap.Y)) ||
                      (gap.Y == position.Y && NonemptyRange(nextPosition.X, position.X).Contains(gap.X))))
                {
                    yield return new string(horizontalArrow, horizontalDistance) + new string(verticalArrow, verticalDistance) + "A";
                }
                if (!((gap.X == position.X && NonemptyRange(nextPosition.Y, position.Y).Contains(gap.Y)) ||
                      (gap.Y == nextPosition.Y && NonemptyRange(position.X, nextPosition.X).Contains(gap.X))))
                {
                    yield return new string(verticalArrow, verticalDistance) + new string(horizontalArrow, horizontalDistance) + "A";
                }
            }
        }

        private IEnumerable<int> NonemptyRange(int start, int end)
        {
            if (start < end)
            {
                for (int i = start; i < end; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = start; i > end; i--)
                {
                    yield return i;
                }
            }
        }

        public override string SolvePart2(string[] input)
        {

            Dictionary<(string, int, KeypadType), long> cache = new();
            return input.Sum(code => int.Parse(code[..^1]) * FindShortestSequence(code, 25, cache)).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"029A") == "1972"); 
            Debug.Assert(SolvePart1(@"029A
980A
179A
456A
379A") == "126384");
        }
    }
}
