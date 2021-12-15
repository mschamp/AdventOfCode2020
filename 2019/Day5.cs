using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day5 : General.IAoC
    {
        public int Day => throw new NotImplementedException();

        public string SolvePart1(string input = null)
        {
            string[] parts = input.Split(Environment.NewLine);
            IntcodeComputer computer = new();
            computer.loadProgram(parts[0]);
            computer.InputValue(int.Parse(parts[1]));
            computer.ExecuteProgram();
            return "" + computer.ReadOutputs().Last();
        }

        public string SolvePart2(string input = null)
        {
            string[] parts = input.Split(Environment.NewLine);
            IntcodeComputer computer = new();
            computer.loadProgram(parts[0]);
            computer.InputValue(int.Parse(parts[1]));
            computer.ExecuteProgram();
            return "" + computer.ReadOutputs().Last();
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"3,0,4,0,99
2") == "2");

            Debug.Assert(SolvePart2(@"3,9,8,9,10,9,4,9,99,-1,8
8") == "1");
            Debug.Assert(SolvePart2(@"3,9,8,9,10,9,4,9,99,-1,8
7") == "0");
            Debug.Assert(SolvePart2(@"3,9,7,9,10,9,4,9,99,-1,8
8") == "0");
            Debug.Assert(SolvePart2(@"3,9,7,9,10,9,4,9,99,-1,8
7") == "1");
            Debug.Assert(SolvePart2(@"3,3,1108,-1,8,3,4,3,99
8") == "1");
            Debug.Assert(SolvePart2(@"3,3,1108,-1,8,3,4,3,99
7") == "0");
            Debug.Assert(SolvePart2(@"3,3,1107,-1,8,3,4,3,99
8") == "0");
            Debug.Assert(SolvePart2(@"3,3,1107,-1,8,3,4,3,99
7") == "1");

            Debug.Assert(SolvePart2(@"3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9
7") == "1");
            Debug.Assert(SolvePart2(@"3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9
0") == "0");
            Debug.Assert(SolvePart2(@"3,3,1105,-1,9,1101,0,0,12,4,12,99,1
7") == "1");
            Debug.Assert(SolvePart2(@"3,3,1105,-1,9,1101,0,0,12,4,12,99,1
0") == "0");


            Debug.Assert(SolvePart2(@"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99
7") == "999");
            Debug.Assert(SolvePart2(@"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99
8") == "1000");
            Debug.Assert(SolvePart2(@"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99
9") == "1001");
        }
    }
}
