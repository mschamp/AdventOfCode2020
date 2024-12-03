using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2019
{
	public class Day7 : General.IAoC
    {
        public int Day => 7;
		public int Year => 2019;

		public string SolvePart1(string input = null)
        {
            int[] Options = new[] { 0, 1, 2, 3, 4 };
            List<long> ThrusterSignals = [];
            foreach (var PhaseSetting in Permutations<int>.AllFor(Options))
            {
                ThrusterSignals.Add(CalculateTrustSignalOpenLoop(input, PhaseSetting));
            }
            return "" + ThrusterSignals.Max();
        }

        private long CalculateTrustSignalOpenLoop(string Program, int[] inputs)
        {
            long Value = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                Value = Amplifier(Program, Value, inputs[i]);
            }
            return Value;
        }

        private long CalculateTrustSignalClosedLoop(string Program, int[] inputs)
        {
            long Value = 0;
            List<IntcodeComputer> Amplifiers = [];
            for (int i = 0; i < inputs.Length; i++)
            {
                IntcodeComputer Amplifier = new();
                Amplifier.loadProgram(Program);
                Amplifier.InputValue(inputs[i]);
                Amplifiers.Add(Amplifier);
            }

            while (!Amplifiers.Any(amp => amp.Halted))
            {
                for (int i = 0; i < Amplifiers.Count(); i++)
                {
                    Value = Amplifier(Amplifiers[i], Value);
                }
            }
            return Value;
        }

        private long Amplifier(string Program, long input, int phaseSetting)
        {
            IntcodeComputer computer = new();
            computer.loadProgram(Program);
            computer.InputValue(phaseSetting);
            computer.InputValue(input);
            computer.ExecuteProgram();
            return computer.ReadOutputs().Last();
        }

        private long Amplifier(IntcodeComputer computer, long input)
        {
            computer.InputValue(input);
            computer.ExecuteProgram();
            return computer.ReadOutputs().Last();
        }

        public string SolvePart2(string input = null)
        {
            int[] Options = new[] { 5,6,7,8,9 };
            List<long> ThrusterSignals = [];
            foreach (var PhaseSetting in Permutations<int>.AllFor(Options))
            {
                ThrusterSignals.Add(CalculateTrustSignalClosedLoop(input, PhaseSetting));
            }
            return "" + ThrusterSignals.Max();
        }

        public void Tests()
        {
            Debug.Assert(CalculateTrustSignalOpenLoop(@"3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0",
new[] { 4, 3, 2, 1, 0 }) ==43210);
            Debug.Assert(CalculateTrustSignalOpenLoop(@"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0",
new[] { 0, 1, 2, 3, 4 }) == 54321);
            Debug.Assert(CalculateTrustSignalOpenLoop(@"3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0",
new[] { 1, 0, 4, 3, 2 }) == 65210);
            Debug.Assert(SolvePart1(@"3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0") == "43210");
            Debug.Assert(SolvePart1(@"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0") == "54321");
            Debug.Assert(SolvePart1(@"3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0") == "65210");

            Debug.Assert(CalculateTrustSignalClosedLoop(@"3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5",
    new[] { 9, 8, 7, 6, 5 }) == 139629729);
            Debug.Assert(CalculateTrustSignalClosedLoop(@"3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10",
new[] { 9, 7, 8, 5, 6 }) == 18216);
        }
    }

    

    public class Permutations<T>
    {
        public static IEnumerable<T[]> AllFor(T[] array)
        {
            if (array==null ||array.Length==0)
            {
                yield return new T[0];
            }
            else
            {
                for (int pick = 0; pick < array.Length; pick++)
                {
                    T item = array[pick];
                    int i = -1;
                    T[] rest = Array.FindAll<T>(array, delegate (T p) { return ++i != pick; });
                    foreach (T[] restPermuted in AllFor(rest))
                    {
                        i = -1;
                        yield return Array.ConvertAll<T, T>(array, delegate (T p) { return ++i == 0 ? item : restPermuted[i - 1]; });
                    }
                }
            }
        }
    }
}
