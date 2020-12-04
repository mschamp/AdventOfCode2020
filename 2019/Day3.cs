using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day3 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] wires = input.Split(Environment.NewLine);
            string[] Wire1 = wires[0].Split(",");
            string[] Wire2 = wires[1].Split(",");

            List<General.clsPoint> Wire1Corners = new List<General.clsPoint>() { new General.clsPoint(0,0)};
            foreach (string instruction in Wire1)
            {
                Wire1Corners.AddRange(Wire1Corners.Last().Move(instruction.Trim()));
            }

            List<General.clsPoint> Wire2Corners = new List<General.clsPoint>() { new General.clsPoint(0, 0) };
            foreach (string instruction in Wire2)
            {
                Wire2Corners.AddRange(Wire2Corners.Last().Move(instruction.Trim()));
            }
            var crossings = Wire1Corners.Intersect(Wire2Corners);
            return "" + crossings.Where(x => x.X!=0||x.Y!=0 ).Min(x => x.manhattan());
        }

        public string SolvePart2(string input = null)
        {
            string[] wires = input.Split(Environment.NewLine);
            string[] Wire1 = wires[0].Split(",");
            string[] Wire2 = wires[1].Split(",");

            List<General.clsPoint> Wire1Corners = new List<General.clsPoint>() { new General.clsPoint(0, 0) };
            foreach (string instruction in Wire1)
            {
                Wire1Corners.AddRange(Wire1Corners.Last().Move(instruction.Trim()));
            }

            List<General.clsPoint> Wire2Corners = new List<General.clsPoint>() { new General.clsPoint(0, 0) };
            foreach (string instruction in Wire2)
            {
                Wire2Corners.AddRange(Wire2Corners.Last().Move(instruction.Trim()));
            }
            var crossings = Wire1Corners.Intersect(Wire2Corners);

            List<int> Distances = new List<int>();
            foreach (General.clsPoint crossing in crossings)
            {
                int Steps = 0;
                for (int i = 1; i < Wire1Corners.Count; i++)
                {
                    if (Wire1Corners[i].Equals(crossing))
                    {
                        Steps += i;
                        break;
                    }
                }
                for (int j = 1; j < Wire2Corners.Count; j++)
                {
                    if (Wire2Corners[j].Equals(crossing))
                    {
                        Distances.Add(Steps+j);
                        break;
                    }
                }
                
            }
            return "" + Distances.Min();
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"R75,D30,R83,U83,L12,D49,R71,U7,L72
U62, R66, U55, R34, D71, R55, D58, R83") == "159");

            Debug.Assert(SolvePart1(@"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7") == "135");

            Debug.Assert(SolvePart2(@"R75,D30,R83,U83,L12,D49,R71,U7,L72
U62, R66, U55, R34, D71, R55, D58, R83") == "610");

            Debug.Assert(SolvePart2(@"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7") == "410");
        }
    }
}
