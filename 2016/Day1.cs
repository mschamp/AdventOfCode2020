using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2016
{
    public class Day1 : General.abstractPuzzleClass
    {
        public Day1() : base(1, 2016)
        {
        }

        public override string SolvePart1(string input)
        {
            General.clsPoint position = new(0,0);
            General.Direction orientation = Direction.Up;

            foreach (var instruction in input.Split(",",StringSplitOptions.TrimEntries))
            {
                switch (instruction[0])
                {
                    case 'R':
                        orientation = (General.Direction)(((int)(orientation-1+4)) % 4);
                        break;
                    case 'L':
                        orientation = (General.Direction)(((int)(orientation + 1)) % 4);
                        break;
                    default:
                        break;
                }

                position = position.Move(orientation, int.Parse(instruction[1..].ToString()));
            }
            return position.manhattan().ToString();
        }

        public override string SolvePart2(string input)
        {
            General.clsPoint position = new(0, 0);
            General.Direction orientation = Direction.Up;
            HashSet<clsPoint> visited = [];

            foreach (var instruction in input.Split(",", StringSplitOptions.TrimEntries))
            {
                switch (instruction[0])
                {
                    case 'R':
                        orientation = (General.Direction)(((int)(orientation - 1 + 4)) % 4);
                        break;
                    case 'L':
                        orientation = (General.Direction)(((int)(orientation + 1)) % 4);
                        break;
                    default:
                        break;
                }

                for (int i = 0; i < int.Parse(instruction[1..].ToString()); i++)
                {
                    position = position.Move(orientation);
                    if (visited.Contains(position))
                    {
                        return position.manhattan().ToString();
                    }
                    visited.Add(position);
                }                
            }

            return "0";
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("R2, L3") == "5");
            Debug.Assert(SolvePart1("R2, R2, R2") == "2");
            Debug.Assert(SolvePart1("R5, L5, R5, R3") == "12");
            Debug.Assert(SolvePart2("R8, R4, R4, R8") == "4");
        }
    }
}
