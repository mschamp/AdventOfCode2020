﻿using System.Diagnostics;

namespace _2019
{
	public class Day2:General.abstractPuzzleClass
    {
        public Day2() : base(2,2019) { }

        public override string SolvePart1(string input)
        {
            IntcodeComputer computer = new();
            computer.loadProgram(input);
            computer.ExecuteProgram();
            return ""+computer.GetMemoryContent(0);
        }

        public override string SolvePart2(string input)
        {
            IntcodeComputer computer = new();
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    computer.loadProgram(input);
                    computer.SetMemoryContent(1, noun);
                    computer.SetMemoryContent(2, verb);
                    computer.ExecuteProgram();
                    
                    if (computer.GetMemoryContent(0) == 19690720)
                    {
                         return "Noun:" + noun.ToString() + " verb:" + verb.ToString();
                    }
                }
            }
            return "";
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("1,0,0,0,99") == "2");
            Debug.Assert(SolvePart1("2,3,0,3,99") == "2");
            Debug.Assert(SolvePart1("2,4,4,5,99,0") == "2");
            Debug.Assert(SolvePart1("1,1,1,4,99,5,6,0,99") == "30");
            Debug.Assert(SolvePart1("1,9,10,3,2,3,11,0,99,30,40,50") == "3500");
        }
    }
}


