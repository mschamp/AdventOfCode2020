using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace _2015
{
	public class Day6 : General.PuzzleWithStringArrayInput
    {
        public Day6() : base(6, 2015) { }
        public override string SolvePart1(string[] input)
        {
            HashSet<(int, int)> LampsOn = [];
            Regex rgx = new(@"([\w\s]*)\s(\d+),(\d+)\sthrough\s(\d+),(\d+)");

            foreach (string instruction in input)
            {
                Match mtch = rgx.Match(instruction);
                int xStart = int.Parse(mtch.Groups[2].Value);
                int yStart = int.Parse(mtch.Groups[3].Value);
                int xEnd = int.Parse(mtch.Groups[4].Value);
                int yEnd = int.Parse(mtch.Groups[5].Value);

                switch (mtch.Groups[1].Value)
                {
                    case "turn on":
                        for (int x = xStart; x <= xEnd; x++)
                        {
                            for (int y = yStart; y <= yEnd; y++)
                            {
                                LampsOn.Add((x, y));
                            }
                        }
                        break;
                    case "turn off":
                        for (int x = xStart; x <= xEnd; x++)
                        {
                            for (int y = yStart; y <= yEnd; y++)
                            {
                                LampsOn.Remove((x, y));
                            }
                        }
                        break;
                    case "toggle":
                        for (int x = xStart; x <= xEnd; x++)
                        {
                            for (int y = yStart; y <= yEnd; y++)
                            {
                                if (LampsOn.Contains((x, y)))
                                {
                                    LampsOn.Remove((x, y));
                                    continue;
                                }
                                LampsOn.Add((x, y));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return ""+ LampsOn.Count();
        }


        public override string SolvePart2(string[] input)
        {
            Dictionary<(int, int),int> LampsOn = [];
            Regex rgx = new(@"([\w\s]*)\s(\d+),(\d+)\sthrough\s(\d+),(\d+)");

            foreach (string instruction in input)
            {
                Match mtch = rgx.Match(instruction);
                int xStart = int.Parse(mtch.Groups[2].Value);
                int yStart = int.Parse(mtch.Groups[3].Value);
                int xEnd = int.Parse(mtch.Groups[4].Value);
                int yEnd = int.Parse(mtch.Groups[5].Value);
                for (int x = xStart; x <= xEnd; x++)
                {
                    for (int y = yStart; y <= yEnd; y++)
                    {
                        LampsOn.TryGetValue((x, y), out int Brightness);

                        switch (mtch.Groups[1].Value)
                        {
                            case "turn on":
                                Brightness++;
                                break;
                            case "turn off":
                                if (Brightness>0)
                                {
                                    Brightness--;
                                }
                                
                                break;
                            case "toggle":
                                Brightness += 2;
                                break;
                            default:
                                break;
                        }

                        LampsOn[(x, y)]=Brightness;
                    }
                }
            }

            return "" + LampsOn.Values.Sum();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("turn on 0,0 through 999,999") == "1000000");
            Debug.Assert(SolvePart1("toggle 0,0 through 999,0") == "1000");
            Debug.Assert(SolvePart1("turn on 499,499 through 500,500") == "4");

        }
    }
}
