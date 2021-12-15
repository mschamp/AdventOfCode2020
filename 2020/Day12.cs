using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace _2020
{
    public class Day12 : General.PuzzleWithStringArrayInput
    {
        public Day12() : base(12) { }
        public override string SolvePart1(string[] instructions)
        {
            General.clsPoint position = new(0, 0);
            General.Direction direction = General.Direction.Right;

            foreach (string instruction in instructions)
            {
                switch (instruction[0])
                {
                    case 'L':
                        for (int i = 0; i < (int.Parse(instruction.Substring(1))/90); i++)
                        {
                            direction = (General.Direction)((((int)direction) + 1) % 4);
                        } 
                        break;
                    case 'R':
                        for (int i = 0; i < (int.Parse(instruction.Substring(1)) / 90); i++)
                        {
                            direction = (General.Direction)((((int)direction) + 3) % 4);
                        }    
                        break;
                    case 'F':
                        position = position.Move(direction, int.Parse(instruction.Substring(1)));
                        break;
                    default:
                        position = position.Move(instruction).Last();
                        break;
                }
            }

            return "" + position.manhattan();
        }


        public override string SolvePart2(string[] instructions)
        {
            General.clsPoint Waypoint = new(10, 1);
            General.clsPoint Ship = new(0, 0);

            foreach (string instruction in instructions)
            {
                switch (instruction[0])
                {
                    case 'L':
                        Waypoint = Waypoint.rotateDegrees(int.Parse(instruction.Substring(1)));
                        break;
                    case 'R':
                        Waypoint = Waypoint.rotateDegrees(-int.Parse(instruction.Substring(1)));
                        break;
                    case 'F':
                        for (int i = 0; i < int.Parse(instruction.Substring(1)); i++)
                        {
                            Ship = Ship.plus(Waypoint);
                        }
                        
                        break;
                    default:
                        Waypoint = Waypoint.Move(instruction).Last();
                        break;
                }
            }

            return "" + Ship.manhattan();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"F10
N3
F7
R90
F11") =="25");

            Debug.Assert(SolvePart2(@"F10
N3
F7
R90
F11") == "286");
        }
    }

}
