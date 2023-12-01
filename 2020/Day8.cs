using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day8 : General.PuzzleWithObjectInput<handheld>
    {
        public Day8() : base(8, 2020) { }
        protected override handheld CastToObject(string RawData)
        {
            handheld hand = new();
            hand.LoadProgram(RawData);

            return hand;
        }

        public override string SolvePart1(handheld hand)
        {
            hand.ExecuteProgram();

            return hand.Accumulator.ToString();
        }


        public override string SolvePart2(handheld hand)
        {
            hand.TryFixing();

            return hand.Accumulator.ToString();
        }

        public override void Tests()
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
        Instruction[] instructions;
        public long Accumulator { get; private set; }

        public handheld()
        {

        }

        public void LoadProgram(string program)
        {
            instructions = program.Split(Environment.NewLine).Select(x => x.Split()).Select(x => new Instruction(x[0], int.Parse(x[1]))).ToArray();
            Instructionpointer = 0;
            Accumulator = 0;
        }

        public void TryFixing()
        {
            ExecuteProgram(); //execute original program
            List<long> ExecutedInstruction = ExecutedInstructions;
            Instruction[] Originalinstructions = instructions; // instructions that are executed during original execution
            foreach (long instructionID in ExecutedInstruction)
            {
                instructions = (Instruction[])Originalinstructions.Clone();
                if (instructions[instructionID].Operation == EnuOperation.acc)
                {
                    continue;
                }
                else if (instructions[instructionID].Operation == EnuOperation.jmp)
                {
                    instructions[instructionID] = new Instruction(EnuOperation.nop, instructions[instructionID].Argument);
                }
                else if (instructions[instructionID].Operation == EnuOperation.nop)
                {
                    instructions[instructionID] = new Instruction(EnuOperation.jmp, instructions[instructionID].Argument);
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
            ExecutedInstructions = new List<long>();
            while (!ExecutedInstructions.Contains(Instructionpointer) && instructions.Length> Instructionpointer)
            {
                ExecutedInstructions.Add(Instructionpointer);
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

        public long ExecuteInstruction(Instruction instruction)
        {
            switch (instruction.Operation)
            {
                case EnuOperation.nop:
                    return 1;
                case EnuOperation.acc:
                    Accumulator += instruction.Argument;
                    return 1;
                case EnuOperation.jmp:
                    return instruction.Argument;
                default:
                    break;
            }
            return 100;
        }

        public List<long> ExecutedInstructions { get; private set; }
    }

    public class Instruction
    {
        public Instruction(string inputInstruction)
            :this(inputInstruction.Split()[0], int.Parse(inputInstruction.Split()[1]))
        {

        }

        public Instruction(string operation, int argument)
            :this((EnuOperation)Enum.Parse(typeof(EnuOperation), operation),argument)
        {

        }
        public Instruction(EnuOperation operation, long argument)
        {
            Operation =  operation;
            Argument = argument;
        }

        public EnuOperation Operation {get;set;}
        public long Argument { get; private set; }
       
    }

    public enum EnuOperation
    {
        nop = 0,
        jmp = 1,
        acc =2
    }
}
