﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace _2020
{
    public class Day12 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] instructions = input.Split(Environment.NewLine);
            General.clsPoint position = new General.clsPoint(0, 0);
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


        public string SolvePart2(string input = null)
        {
            string[] instructions = input.Split(Environment.NewLine);
            General.clsPoint Waypoint = new General.clsPoint(10, 1);
            General.clsPoint Ship = new General.clsPoint(0, 0);
            General.Direction direction = General.Direction.Right;

            foreach (string instruction in instructions)
            {
                switch (instruction[0])
                {
                    case 'L':
                        for (int i = 0; i < (int.Parse(instruction.Substring(1)) / 90); i++)
                        {
                            Waypoint = Waypoint.rotate90();
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < (int.Parse(instruction.Substring(1)) / 90*3); i++)
                        {
                            Waypoint = Waypoint.rotate90();
                        }
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

        public void Tests()
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