using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2021
{
    public class Day2 : General.PuzzleWithStringArrayInput
    {
        public override string SolvePart1(string[] input)
        {
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"(\w*)\s(\d*)");
            int Horizontal = 0;
            int depth = 0;
            foreach (string item in input)
            {
                var mtch = rgx.Match(item);
                switch (mtch.Groups[1].Value)
                {
                    case "down":
                        depth += int.Parse(mtch.Groups[2].Value);
                        break;
                    case "forward":
                        Horizontal += int.Parse(mtch.Groups[2].Value);
                        break;
                    case "up":
                        depth -= int.Parse(mtch.Groups[2].Value);
                        break;
                    default:
                        break;
                }
            }

            return (Horizontal * depth).ToString();
        }

        public override string SolvePart2(string[] input)
        {
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"(\w*)\s(\d*)");
            int Horizontal = 0;
            int depth = 0;
            int aim = 0;
            foreach (string item in input)
            {
                var mtch = rgx.Match(item);
                switch (mtch.Groups[1].Value)
                {
                    case "down":
                        aim += int.Parse(mtch.Groups[2].Value);
                        break;
                    case "forward":
                        Horizontal += int.Parse(mtch.Groups[2].Value);
                        depth+=aim* int.Parse(mtch.Groups[2].Value);
                        break;
                    case "up":
                        aim -= int.Parse(mtch.Groups[2].Value);
                        break;
                    default:
                        break;
                }
            }

            return (Horizontal * depth).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"forward 5
down 5
forward 8
up 3
down 8
forward 2") == "150");

            Debug.Assert(SolvePart2(@"forward 5
down 5
forward 8
up 3
down 8
forward 2") == "900");
        }
    }
}
