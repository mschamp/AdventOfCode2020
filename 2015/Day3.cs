using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day3 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            HashSet<General.clsPoint> VisitedHouses = new();
            General.clsPoint Current = new(0, 0);
            VisitedHouses.Add(Current);

            foreach (char item in input)
            {
                Current = Current.Move(item, 1).Last();
                VisitedHouses.Add(Current);
            }
            return "" + VisitedHouses.Count;
        }

        public string SolvePart2(string input = null)
        {
            HashSet<General.clsPoint> VisitedHouses = new();
            General.clsPoint Santa = new(0, 0);
            General.clsPoint Robot = new(0, 0);
            VisitedHouses.Add(Santa);

            for (int i = 0; i < input.Length; i+=2)
            {
                Santa = Santa.Move(input[i], 1).Last();
                Robot = Robot.Move(input[i+1], 1).Last();
                VisitedHouses.Add(Santa);
                VisitedHouses.Add(Robot);
            }
            return "" + VisitedHouses.Count;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(">") == "2");
            Debug.Assert(SolvePart1("^>v<") == "4");
            Debug.Assert(SolvePart1("^v^v^v^v^v") == "2");

            Debug.Assert(SolvePart2("^v") == "3");
            Debug.Assert(SolvePart2("^>v<") == "3");
            Debug.Assert(SolvePart2("^v^v^v^v^v") == "11");
        }
    }
}
