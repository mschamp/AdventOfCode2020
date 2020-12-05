using System;
using System.Collections.Generic;
using System.Text;

namespace _2019
{
    public class IntcodeComputer
    {
        IntCodeProgram program;
        long positionPointer;
        long relativeBase;
        Queue<long> inputs ;
        List<long> outputs;

        public void loadProgram(string program)
        {
            this.program = new IntCodeProgram(program);
            positionPointer = 0;
            relativeBase = 0;
            inputs = new Queue<long>();
            outputs = new List<long>();
            WaitingForInput = false;
        }

        public void ExecuteProgram()
        {
            while (!Halted && !WaitingForInput)
            {
                positionPointer += ExecuteInstruction();
            }
        }

        private long ExecuteInstruction()
        {
            int[] opCode = decodeOpcode(program.GetAddress(positionPointer));

            switch ((Instruction)opCode[0])
            {
                case Instruction.Add:
                    program.SetAddress(LoadParameter(opCode[3], positionPointer + 3), program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)) + program.GetAddress(LoadParameter(opCode[2], positionPointer + 2)));
                    return 4;
                case Instruction.Mult:
                    program.SetAddress(LoadParameter(opCode[3], positionPointer + 3), program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)) * program.GetAddress(LoadParameter(opCode[2], positionPointer + 2)));
                    return 4;
                case Instruction.Input:
                    if (inputs.Count == 0)
                    {
                        WaitingForInput = true;
                        return 0;
                    }
                    program.SetAddress(LoadParameter(opCode[1], positionPointer + 1), inputs.Dequeue());
                    return 2;
                case Instruction.Output:
                    outputs.Add(program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)));
                    return 2;
                case Instruction.JumpIfTrue:
                    if (program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)) != 0)
                    {
                        return program.GetAddress(LoadParameter(opCode[2], positionPointer + 2)) - positionPointer;
                    }
                    return 3;
                case Instruction.JumpIfFalse:
                    if (program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)) == 0)
                    {
                        return program.GetAddress(LoadParameter(opCode[2], positionPointer + 2)) - positionPointer;
                    }
                    return 3;
                case Instruction.LessThan:
                    if (program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)) < program.GetAddress(LoadParameter(opCode[2], positionPointer + 2)))
                    {
                        program.SetAddress(LoadParameter(opCode[3], positionPointer + 3),1);
                    }
                    else
                    {
                        program.SetAddress(LoadParameter(opCode[3], positionPointer + 3), 0);
                    }
                    return 4;
                case Instruction.Equals:
                    if (program.GetAddress(LoadParameter(opCode[1], positionPointer + 1)) == program.GetAddress(LoadParameter(opCode[2], positionPointer + 2)))
                    {
                        program.SetAddress(LoadParameter(opCode[3], positionPointer + 3), 1);
                    }
                    else
                    {
                        program.SetAddress(LoadParameter(opCode[3], positionPointer + 3),0);
                    }
                    return 4;
                case Instruction.AdjRel: //adjust relative base
                    relativeBase += program.GetAddress(LoadParameter(opCode[1], positionPointer + 1));
                    return 2;
                default:
                    Console.WriteLine("unknown code");
                    return 1000;
            }
        }

        private long LoadParameter(int mode, long value)
        {
            switch ((Modes)mode)
            {
                case Modes.Parameter:
                    return program.GetAddress(value);
                case Modes.Immediate:
                    return value;
                case Modes.Relative:
                    return relativeBase+program.GetAddress(value);
                default:
                    Console.WriteLine("unknown code");
                    return 0;
            }
        }

        private int[] decodeOpcode(long opCode)
        {
            int[] codes = new int[4];
            codes[0]= (int)(opCode % 100);
            codes[1] = (int)((opCode % 1000- codes[0])/100);
            codes[2] = (int)((opCode % 10000 - codes[0]-codes[1]*100) / 1000);
            codes[3] = (int)((opCode - codes[0] - codes[1] * 100 - codes[2] * 1000) / 10000);
            return codes;
        }

        public long GetMemoryContent(int address)
        {
            return program.GetAddress(address);
        }

        public void SetMemoryContent(int address, int value)
        {
            program.SetAddress(address, value);
        }
    
        public void InputValue(long value)
        {
            inputs.Enqueue(value);
            WaitingForInput = false;
        }

        public List<long> ReadOutputs()
        {
            return outputs;
        }

        public bool WaitingForInput
        {
            get; private set;
        }

        public bool Halted
        {
            get
            {
                return (Instruction)program.GetAddress(positionPointer) == Instruction.Halt;
            }
        }
    }

    public class IntCodeProgram
    {
        public IntCodeProgram(List<long> intCode)
        {
            _oProgram = intCode;
        }
        public IntCodeProgram(string intCode)
        {
            _oProgram = new List<long>();
            foreach (string item in intCode.Split(","))
            {
                _oProgram.Add(long.Parse(item));
            }
        }

        private List<long> _oProgram;
        public long GetAddress(long address)
        {
            CreateMemoryTillAddress(address);
            return _oProgram[(int)address];
        }

        public void SetAddress(long address, long Value)
        {
            CreateMemoryTillAddress(address);
            _oProgram[(int)address] = Value;
        }
        
        private void CreateMemoryTillAddress(long address)
        {
            while (_oProgram.Count<address+1)
            {
                _oProgram.Add(0);
            }
        }
    }
}
