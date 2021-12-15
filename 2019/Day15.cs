using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day15 : General.IAoC
    {
        public int Day => throw new NotImplementedException();

        public string SolvePart1(string input = null)
        {
            Dictionary<General.clsPoint, char> VisitedPlaces = new();
            IntcodeComputer robot = new();
            robot.loadProgram(input);
            General.clsPoint location = new(0, 0);

            while (true)
            {
                switch (robot.ReadOutputs().Last())
                {
                    case 0:
                        VisitedPlaces.Add(location, '.');
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }
            }
            if (robot.ReadOutputs().Last()==2)
            {
                return "";
            }
        }

        public string SolvePart2(string input = null)
        {
            throw new NotImplementedException();
        }

        public void Tests()
        {
            throw new NotImplementedException();
        }
    }
}
