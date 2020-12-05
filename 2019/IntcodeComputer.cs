using System;
using System.Collections.Generic;
using System.Text;

namespace _2019
{
    public class IntcodeComputer
    {
        int[] memory;
        int positionPointer;
        Queue<int> inputs ;
        List<int> outputs;

        public void loadProgram(string program)
        {
            memory = Array.ConvertAll(program.Split(","), s => int.Parse(s));
            positionPointer = 0;
            inputs = new Queue<int>();
            outputs = new List<int>();
            WaitingForInput = false;
            Halted = false;
        }

        public void ExecuteProgram()
        {
            while (positionPointer < memory.Length&&memory[positionPointer]!=99 && !WaitingForInput)
            {
                positionPointer += ExecuteInstruction();
            }
            if (memory[positionPointer] == 99)
            {
                Halted = true;
            }
        }

        private int ExecuteInstruction()
        {
            int[] opCode = decodeOpcode(memory[positionPointer]);

            switch (opCode[0])
            {
                case 1:
                    memory[LoadParameter(0,positionPointer + 3)] = memory[LoadParameter(opCode[1], positionPointer + 1)] + memory[LoadParameter(opCode[2], positionPointer + 2)];
                    return 4;
                case 2:
                    memory[LoadParameter(0, positionPointer + 3)] = memory[LoadParameter(opCode[1], positionPointer + 1)] * memory[LoadParameter(opCode[2], positionPointer + 2)];
                    return 4;
                case 3:
                    if (inputs.Count==0)
                    {
                        WaitingForInput = true;
                        return 0;
                    }
                    memory[LoadParameter(0, positionPointer + 1)] = inputs.Dequeue();
                    return 2;
                case 4:
                    outputs.Add(memory[LoadParameter(opCode[1], positionPointer + 1)]);
                    return 2;
                case 5:
                    if (memory[LoadParameter(opCode[1], positionPointer + 1)]!=0)
                    {
                        return memory[LoadParameter(opCode[2], positionPointer + 2)] - positionPointer;
                    }
                    return 3 ;
                case 6:
                    if (memory[LoadParameter(opCode[1], positionPointer + 1)] == 0)
                    {
                        return memory[LoadParameter(opCode[2], positionPointer + 2)] - positionPointer;
                    }
                    return 3;
                case 7:
                    if (memory[LoadParameter(opCode[1], positionPointer + 1)] < memory[LoadParameter(opCode[2], positionPointer + 2)])
                    {
                        memory[LoadParameter(0, positionPointer + 3)] = 1;
                    }
                    else
                    {
                        memory[LoadParameter(0, positionPointer + 3)] = 0;
                    }
                    return 4;
                case 8:
                    if (memory[LoadParameter(opCode[1], positionPointer + 1)] == memory[LoadParameter(opCode[2], positionPointer + 2)])
                    {
                        memory[LoadParameter(0, positionPointer + 3)] = 1;
                    }
                    else
                    {
                        memory[LoadParameter(0, positionPointer + 3)] = 0;
                    }
                    return 4;
                default:
                    Console.WriteLine("unknown code");
                    return 1000;
            }
        }

        private int LoadParameter(int mode, int value)
        {
            switch (mode)
            {
                case 0:
                    return memory[value];
                case 1:
                    return value;
                default:
                    Console.WriteLine("unknown code");
                    return 0;
            }
        }

        private int[] decodeOpcode(int opCode)
        {
            int[] codes = new int[4];
            codes[0]= opCode % 100;
            codes[1] = (opCode % 1000- codes[0])/100;
            codes[2] = (opCode % 10000 - codes[0]-codes[1]*100) / 1000;
            codes[3] = (opCode - codes[0] - codes[1] * 100 - codes[2] * 1000) / 10000;
            return codes;
        }

        public int GetMemoryContent(int address)
        {
            return memory[address];
        }

        public void SetMemoryContent(int address, int value)
        {
            memory[address] = value;
        }
    
        public void InputValue(int value)
        {
            inputs.Enqueue(value);
            WaitingForInput = false;
        }

        public List<int> ReadOutputs()
        {
            return outputs;
        }

        public bool WaitingForInput
        {
            get; private set;
        }

        public bool Halted
        {
            get; private set;
        }
    }
}
