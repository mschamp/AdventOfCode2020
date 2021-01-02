using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2015
{
    public class Day7 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            Dictionary<string, Gate> wireValues = new Dictionary<string, Gate>();
            Regex rgx = new Regex(@"(?:(?:(\d+|[a-z]+)\s)?([A-Z]+)\s)?(\d+|[a-z]+)\s->\s([a-z]+)");
            foreach (string instruction in input.Split(Environment.NewLine))
            {
                Match mtch = rgx.Match(instruction);

                wireValues[mtch.Groups[4].Value] =new Gate(mtch.Groups[1].Value, mtch.Groups[2].Value, mtch.Groups[3].Value);
                
            }
            if (wireValues.ContainsKey("a"))
            {
                return "" + wireValues["a"].Evaluate(wireValues);
            }
            return "";
        }

        public string SolvePart2(string input = null)
        {
            Dictionary<string, Gate> wireValues = new Dictionary<string, Gate>();
            Regex rgx = new Regex(@"(?:(?:(\d+|[a-z]+)\s)?([A-Z]+)\s)?(\d+|[a-z]+)\s->\s([a-z]+)");
            foreach (string instruction in input.Split(Environment.NewLine))
            {
                Match mtch = rgx.Match(instruction);

                wireValues[mtch.Groups[4].Value] = new Gate(mtch.Groups[1].Value, mtch.Groups[2].Value, mtch.Groups[3].Value);

            }
            wireValues["b"].Result= wireValues["a"].Evaluate(wireValues);
            foreach (var item in wireValues.Where(x => x.Key != "b"))
            {
                item.Value.Calculated = false;
            } 
            return "" + wireValues["a"].Evaluate(wireValues);
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i") == "");
        }
    }

    public class Gate
    {
        public Gate(string input1, string Operator, string input2)
        {
            Input1 = input1;
            this.Operator = Operator;
            Input2 = input2;
        }

        private string Input1;
        private string Operator;
        private string Input2;

        public bool Calculated;
        public ushort Result { get; set; }

        public ushort Evaluate(Dictionary<string, Gate> wireValues)
        {
            if (Calculated)
                return Result;
            switch (Operator)
            {
                case "AND":
                    Result =(ushort)(FindValue(wireValues,Input1) & FindValue(wireValues, Input2));
                    break;
                case "OR":
                    Result = (ushort)(FindValue(wireValues, Input1) | FindValue(wireValues, Input2));
                    break;
                case "LSHIFT":
                    Result = (ushort)(FindValue(wireValues, Input1) << FindValue(wireValues, Input2));
                    break;
                case "RSHIFT":
                    Result = (ushort)(FindValue(wireValues, Input1) >> FindValue(wireValues, Input2));
                    break;
                case "NOT":
                    Result = (ushort)~FindValue(wireValues, Input2);
                    break;
                case null:
                case "":
                    Result = FindValue(wireValues, Input2);
                    break;
            }
            Calculated = true;
            return Result;
        }

        private ushort FindValue(Dictionary<string, Gate> wireValues,string value)
        {
            if (ushort.TryParse(value, out ushort result))
                return result;
            return wireValues[value].Evaluate(wireValues);
        }
    }
}
