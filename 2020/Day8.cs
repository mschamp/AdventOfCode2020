using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2020
{
    public class Day8 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            handheld hand = new handheld();
            hand.LoadProgram(input);
            hand.ExecuteProgram();

            return "" + hand.Accumulator;
        }

        public string SolvePart2(string input = null)
        {
            handheld hand = new handheld();
            hand.LoadProgram(input);
            hand.TryFixing();

            return "" + hand.Accumulator;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6") == "5");

            Debug.Assert(SolvePart2(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6") == "8");
        }
    }

    public class handheld
    {
        string[] instructions;
        public long Accumulator { get; private set; }

        public handheld()
        {

        }

        public void LoadProgram(string program)
        {
            instructions = program.Split(Environment.NewLine);
            Instructionpointer = 0;
            Accumulator = 0;
        }

        public void TryFixing()
        {
            string[] Originalinstructions = (string[])instructions.Clone();
            for (int i = 0; i < Originalinstructions.Length; i++)
            {
                instructions = (string[])Originalinstructions.Clone();
                if (instructions[i].StartsWith("acc"))
                {
                    continue;
                }
                else if (instructions[i].StartsWith("jmp"))
                {
                    instructions[i]=instructions[i].Replace("jmp","nop");
                }
                else if (instructions[i].StartsWith("nop"))
                {
                    instructions[i]=instructions[i].Replace("nop", "jmp");
                }
                ExecuteProgram();
                if (ReachedEnd)
                {
                    break;
                }
            }
        }

        public void ExecuteProgram()
        {
            Instructionpointer = 0;
            Accumulator = 0;
            List<long> ExecutedInstruction = new List<long>();
            while (!ExecutedInstruction.Contains(Instructionpointer) && instructions.Length> Instructionpointer)
            {
                ExecutedInstruction.Add(Instructionpointer);
                Instructionpointer+=ExecuteInstruction(instructions[Instructionpointer]);
            }
        }

        public long Instructionpointer { get; private set; }

        public bool ReachedEnd
        {
            get
            {
                return (instructions.Length == Instructionpointer);
            }
        }

        public long ExecuteInstruction(string instruction)
        {
            string[] parts = instruction.Split();
            long Argmument = long.Parse(parts[1]);
            switch (parts[0])
            {
                case "nop":
                    return 1;
                case "acc":
                    Accumulator += Argmument;
                    return 1;
                case "jmp":
                    return Argmument;
                default:
                    break;
            }
            return 100;
        }
    }
}
