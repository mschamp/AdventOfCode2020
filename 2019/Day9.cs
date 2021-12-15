using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day9 : General.IAoC
    {
        public int Day => throw new NotImplementedException();

        public string SolvePart1(string input = null)
        {
            var outputTestmode = RunComputer(input, 1);
            if (outputTestmode.Count==1)
            {
                return "" + outputTestmode[0].ToString();
            }
            return "";
        }

        public string SolvePart2(string input = null)
        {
            var outputTestmode = RunComputer(input, 2);
            return "" + string.Join(",", outputTestmode);
        }

        private List<long> RunComputer(string program)
        {
            IntcodeComputer computer = new();
            computer.loadProgram(program);
            computer.ExecuteProgram();
            return computer.ReadOutputs();
        }

        private List<long> RunComputer(string program, long input)
        {
            IntcodeComputer computer = new();
            computer.loadProgram(program);
            computer.InputValue(input);
            computer.ExecuteProgram();
            return computer.ReadOutputs();
        }

        public void Tests()
        {
            Debug.Assert(string.Join(",", RunComputer("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99"))== "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            Debug.Assert(RunComputer("1102,34915192,34915192,7,4,7,99,0").Last().ToString().Length==16);
            Debug.Assert(RunComputer("104,1125899906842624,99").Last().ToString()== "1125899906842624");
        }
    }
}
