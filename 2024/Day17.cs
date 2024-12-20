using MoreLinq;

namespace _2024
{
    public class Day17:PuzzleWithObjectInput<(long A, long B, long C, long[] Program)>
    {
        public Day17():base(17,2024)
        {
            
        }

        public override string SolvePart1((long A, long B, long C, long[] Program) input)
        {
            return string.Join(",", RunProgram(input.A, input.B, input.C, input.Program));
        }

        private List<long> RunProgram(long a, long b, long c, long[] program)
        {
            long ip = 0;
            var res = new List<long>();
            while (ip < program.Length - 1)
            {
                var (outValue, newA, newB, newC, newIp) = ExecuteInstruction(a, b, c, ip, program);
                a = newA;
                b = newB;
                c = newC;
                ip = newIp;
                if (outValue.HasValue)
                {
                    res.Add(outValue.Value);
                }
            }
            return res;
        }

        private long GetComboValue(long A, long B, long C, long Value)
        {
            if (Value < 4) return Value;
            if (Value == 4) return A;
            if (Value == 5) return B;
            if (Value == 6) return C;
            return 0;
        }

        private (long? outValue, long a, long b, long c, long ip) ExecuteInstruction(long a, long b, long c, long ip, long[] program)
        {
            long opcode = program[ip];
            long arg = program[ip + 1];
            long comb = GetComboValue(a, b, c, arg);

            switch (opcode)
            {
                case 0:
                    long num = a;
                    long denom = (int)Math.Pow(2, comb);
                    return (null, num / denom, b, c, ip + 2);
                case 1:
                    return (null, a, b ^ arg, c, ip + 2);
                case 2:
                    return (null, a, comb % 8, c, ip + 2);
                case 3:
                    return a == 0 ? (null, a, b, c, ip + 2) : (null, a, b, c, arg);
                case 4:
                    return (null, a, b ^ c, c, ip + 2);
                case 5:
                    return (comb % 8, a, b, c, ip + 2);
                case 6:
                    num = a;
                    denom = (int)Math.Pow(2, comb);
                    return (null, a, num / denom, c, ip + 2);
                case 7:
                    num = a;
                    denom = (int)Math.Pow(2, comb);
                    return (null, a, b, num / denom, ip + 2);
                default:
                    return (null, a, b, c, ip);
            }
        }

            

        public override string SolvePart2((long A, long B, long C, long[] Program) input)
        {
            return GetBestQuineInput(input.Program, input.Program.Length - 1, 0).ToString();
        }

        private long? GetBestQuineInput(long[] program, int cursor, long sofar)
        {
            for (int candidate = 0; candidate < 8; candidate++)
            {
                if (RunProgram(sofar * 8 + candidate, 0, 0, program).SequenceEqual(program.Skip(cursor)))
                {
                    if (cursor == 0)
                        return sofar * 8 + candidate;
                    long? ret = GetBestQuineInput(program, cursor - 1, sofar * 8 + candidate);
                    if (ret != null)
                        return ret;
                }
            }
            return null;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0") == "4,6,3,5,6,3,5,2,1,0");

            Debug.Assert(SolvePart2(@"Register A: 2024
Register B: 0
Register C: 0

Program: 0,3,5,4,3,0") == "117440");
        }

        protected override (long A, long B, long C, long[] Program) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return (long.Parse(lines[0].Split(": ")[1]),
                long.Parse(lines[1].Split(": ")[1]),
                long.Parse(lines[2].Split(": ")[1]),
                lines[3].Split(": ")[1].Split(",").Select(long.Parse).ToArray());
        }
    }
}
