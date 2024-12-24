using Interfaces.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2024
{
    public class Day24:PuzzleWithObjectInput<(Dictionary<string, int>inputs, List<(string,string,string,string)> gates)>
    {
        public Day24():base(24,2024)
        {
            
        }

        public override string SolvePart1((Dictionary<string, int> inputs, List<(string, string, string, string)> gates) input)
        {
            Dictionary<string, (string, string)> parents = new();
            Dictionary<string, string> operations = new();
            foreach (var item in input.gates)
            {
                parents[item.Item3] = (item.Item1, item.Item2);
                operations[item.Item3] = item.Item4;
            }

            Dictionary<string, int> KnownValues = input.inputs.ToDictionary();

            var variables = input.gates.Select(x => (x.Item3, CalculateDepth(x.Item3, parents, input.inputs))).OrderBy(x => x.Item2).ToList();

            foreach (var item in variables)
            {
                (string register1, string register2) = parents[item.Item1];
                int V1 = KnownValues[register1];
                int V2 = KnownValues[register2];
                switch (operations[item.Item1])
                {
                    case "XOR":
                        KnownValues[item.Item1] = V1 ^ V2;
                        break;
                    case "OR":
                        KnownValues[item.Item1] = V1 | V2;
                        break;
                    case "AND":
                        KnownValues[item.Item1] = V1 & V2;
                        break;
                }
            }

            var resultList = KnownValues.Where(x => x.Key.StartsWith('z')).OrderByDescending(x=>x.Key).ToList();
            string binString = string.Concat(resultList.Select(x=>x.Value));

            var result = Convert.ToInt64(binString, 2);
            return result.ToString();
            throw new NotImplementedException();
        }

        private int CalculateDepth(string register, Dictionary<string, (string, string)> parents, Dictionary<string, int> inputs)
        {
            if (inputs.ContainsKey(register)) return 0;

            return 1+ Math.Max(CalculateDepth(parents[register].Item1, parents, inputs), CalculateDepth(parents[register].Item2, parents, inputs));
        }

        public override string SolvePart2((Dictionary<string, int> inputs, List<(string, string, string, string)> gates) input)
        {
            var swaps = new HashSet<(string, string)>();

            var (baseValue, baseUsed) = FurthestMade(input.gates);

            for (int _ = 0; _ < 4; _++)
            {
                var combinations = input.gates.DifferentCombinations(2).Select(x=>x.ToArray());
                // try swapping
                for (int i = 0; i < input.gates.Count; i++)
                {
                    for (int j = i + 1; j < input.gates.Count; j++)
                    {
                        var (x1_i, x2_i, res_i, op_i) = input.gates[i];
                        var (x1_j, x2_j, res_j, op_j) = input.gates[j];

                        if (baseUsed.Contains(res_i) || baseUsed.Contains(res_j)) continue;

                        // Switch output wires
                        input.gates[i] = (x1_i, x2_i, res_j, op_i);
                        input.gates[j] = (x1_j, x2_j, res_i, op_j);
                        var (attempt, attemptUsed) = FurthestMade(input.gates);
                        if (attempt > baseValue)
                        {
                            swaps.Add((res_i, res_j));
                            baseValue = attempt;
                            baseUsed = attemptUsed;
                            break;
                        }
                        // Switch output wires back
                        input.gates[i] = (x1_i, x2_i, res_i, op_i);
                        input.gates[j] = (x1_j, x2_j, res_j, op_j);
                    }
                }
            }
            var t = swaps.Select((x1, x2) => new string[] { x1.Item1, x1.Item2 }).SelectMany(x => x).Order().ToList();
            return string.Join(",", t);

        }

        private (int, HashSet<string>) FurthestMade(List<(string, string, string, string)> opList)
        {
            var ops = new Dictionary<(string,string, string), string>();
            foreach (var (x1, x2, res, op) in opList)
            {
                ops[(x1, x2, op)] = res; // hashability reason
            }

            string GetRes(string x1, string x2, string op)
            {
                if (ops.TryGetValue((x1, x2, op), out var res)) return res;
                if (ops.TryGetValue((x2, x1, op), out res)) return res;
                return  null;
            }

            var carries = new Dictionary<int, string>();
            var correct = new HashSet<string>();
            var prevIntermediates = new HashSet<string>();
            for (int i = 0; i < 45; i++)
            {
                var pos = i < 10 ? $"0{i}" : i.ToString();
                var predigit = GetRes($"x{pos}", $"y{pos}", "XOR");
                var precarry1 = GetRes($"x{pos}", $"y{pos}", "AND");
                if (i == 0)
                {
                    // only two, XOR and AND
                    //if (predigit != "z00") continue;
                    carries[i] = precarry1;
                    continue;
                }
                var digit = GetRes(carries[i - 1], predigit, "XOR");
                if (digit != $"z{pos}")
                {
                    return (i - 1, correct);
                }

                // If it DID work, we know carries[i-1] and predigit were correct
                correct.Add(carries[i - 1]);
                correct.Add(predigit);
                // Also add variables from previous position's ripple-carry adder module
                foreach (var wire in prevIntermediates)
                {
                    correct.Add(wire);
                }

                // Next, we compute the carries
                var precarry2 = GetRes(carries[i - 1], predigit, "AND");
                var carryOut = GetRes(precarry1, precarry2, "OR");
                carries[i] = carryOut;
                prevIntermediates = new HashSet<string> { precarry1, precarry2 };
            }

            return (45, correct);
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"x00: 1
x01: 1
x02: 1
y00: 0
y01: 1
y02: 0

x00 AND y00 -> z00
x01 XOR y01 -> z01
x02 OR y02 -> z02") == "4");
            Debug.Assert(SolvePart1(@"x00: 1
x01: 0
x02: 1
x03: 1
x04: 0
y00: 1
y01: 1
y02: 1
y03: 1
y04: 1

ntg XOR fgs -> mjb
y02 OR x01 -> tnw
kwq OR kpj -> z05
x00 OR x03 -> fst
tgd XOR rvg -> z01
vdt OR tnw -> bfw
bfw AND frj -> z10
ffh OR nrd -> bqk
y00 AND y03 -> djm
y03 OR y00 -> psh
bqk OR frj -> z08
tnw OR fst -> frj
gnj AND tgd -> z11
bfw XOR mjb -> z00
x03 OR x00 -> vdt
gnj AND wpb -> z02
x04 AND y00 -> kjc
djm OR pbm -> qhw
nrd AND vdt -> hwm
kjc AND fst -> rvg
y04 OR y02 -> fgs
y01 AND x02 -> pbm
ntg OR kjc -> kwq
psh XOR fgs -> tgd
qhw XOR tgd -> z09
pbm OR djm -> kpj
x03 XOR y03 -> ffh
x00 XOR y04 -> ntg
bfw OR bqk -> z06
nrd XOR fgs -> wpb
frj XOR qhw -> z04
bqk OR frj -> z07
y03 OR x01 -> nrd
hwm AND bqk -> z03
tgd XOR rvg -> z12
tnw OR pbm -> gnj") == "2024");

        }

        protected override (Dictionary<string, int> inputs, List<(string, string, string, string)> gates) CastToObject(string RawData)
        {
            Regex rgxinput = new Regex(@"([xy]\d\d): ([10])");
            Regex rgxGate = new Regex(@"([a-z0-9]{3}) ([XORAND]+) ([a-z0-9]{3}) -> ([a-z0-9]{3})");

            string[] parts = RawData.Split(Environment.NewLine+ Environment.NewLine);

            Dictionary<string, int> inputs = new();
            foreach (string part in parts[0].Split(Environment.NewLine))
            {
                Match match = rgxinput.Match(part);
                inputs[match.Groups[1].Value]=int.Parse(match.Groups[2].Value);
            }

            List<(string, string, string, string)> gates = new();
            foreach (string part in parts[1].Split(Environment.NewLine))
            {
                Match match = rgxGate.Match(part);
                gates.Add((match.Groups[1].Value, match.Groups[3].Value, match.Groups[4].Value, match.Groups[2].Value));
            }
           return (inputs, gates);
        }
    }
}
