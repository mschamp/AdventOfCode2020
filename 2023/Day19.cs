using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2023
{
	public class Day19:PuzzleWithObjectInput<(Dictionary<string, Day19.instruction> instruction,List<Day19.material>material)>
	{
		private Dictionary<string, int> keyLocation = new Dictionary<string, int> { { "x", 0 }, { "m", 1 }, { "a", 2 }, { "s", 3 } };
        public Day19():base(19,2023)
        {
            
        }
        public override string SolvePart1((Dictionary<string,instruction> instruction, List<material> material) input)
		{
			var valids = input.material.Where(x => IsValid(x, input.instruction));
			return $"{valids.Sum(x => x.materialValues.Values.Sum())}";
		}

		public override string SolvePart2((Dictionary<string, instruction> instruction, List<material> material) input)
		{
			int[] LowLimit = [1, 1, 1, 1];
			int[] HighLimit = [4001, 4001, 4001, 4001];



			return $"{calculate(LowLimit, HighLimit, "in", input.instruction)}";
		}

		private long calculate(int[] lowLimit, int[] highLimit, string instruction, Dictionary<string, instruction> instructions) 
		{
			long Result = 0;
			if (instruction=="A")
			{
				return CountOptions(lowLimit, highLimit);
			}
			if (instruction=="R") 
			{
				return 0;
			}
            foreach (var item in instructions[instruction].requirements)
            {
                if (item.requirementInstruction==null) Result+=calculate(lowLimit,highLimit,item.Next,instructions);
				else
				{
					switch (item.Compare)
					{
						case "<":
							int[] NewHighLimits = highLimit.ToArray();
							NewHighLimits[keyLocation[item.label]] = Math.Min(NewHighLimits[keyLocation[item.label]], item.Limit);
							if (CountOptions(lowLimit, NewHighLimits) > 0) Result += calculate(lowLimit.ToArray(), NewHighLimits, item.Next, instructions);
							lowLimit[keyLocation[item.label]] = Math.Max(lowLimit[keyLocation[item.label]], item.Limit);
							break;
						case ">":
							int[] NewLowLimits = lowLimit.ToArray();
							NewLowLimits[keyLocation[item.label]] = Math.Max(NewLowLimits[keyLocation[item.label]], item.Limit+1);
							if (CountOptions(NewLowLimits, highLimit) > 0) Result += calculate(NewLowLimits, highLimit.ToArray(), item.Next, instructions);
							highLimit[keyLocation[item.label]] = Math.Min(highLimit[keyLocation[item.label]], item.Limit+1);
							break;
					}
				}
            }
			return Result;
        }

		private long CountOptions(int[] LowLimit, int[] HighLimit)
		{
			return LowLimit.Zip(HighLimit).Aggregate(1L, (prod, x) => prod * Math.Max(0, x.Second - x.First));
		}

		private bool IsValid(material mat, Dictionary<string, instruction> instructions)
		{
			instruction current = instructions["in"];
			while (true)
			{
				foreach (var req in current.requirements)
				{
					if (req.label == null)
					{
						switch (req.Next)
						{
							case "A":
								return true;
							case "R":
								return false;
							default:
								current = instructions[req.Next];
								break;
						}
						break;
					}
					if (req.requirementInstruction(mat.materialValues[req.label]))
					{
						switch (req.Next)
						{
							case "A":
								return true;
								break;
							case "R":
								return false;
							default:
								current = instructions[req.Next];
								break;
						}
						break;
					}
				}
			}

			return false;
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}") == "19114");

			Debug.Assert(SolvePart2(@"px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}") == "167409079868000");
		}

		protected override (Dictionary<string, instruction> instruction, List<material> material) CastToObject(string RawData)
		{
			var parts = RawData.Split(Environment.NewLine+Environment.NewLine);

			List<instruction> result = parts[0].Split(Environment.NewLine).Select(x=>new instruction(x)).ToList();
			List<material> materials = parts[1].Split(Environment.NewLine).Select(x => new material(x)).ToList();

			return (result.ToDictionary(x=>x.label), materials);
		}

		public class instruction
		{
			public instruction(string input)
			{
				var parts = input.Split('{');
				label = parts[0];
				requirements = parts[1][..^1].Split(',').Select(x=>new requirement(x)).ToList();
			}

			public string label;
			public List<requirement> requirements { get; set; }
		}

		public class requirement
		{
            public requirement(string input)
            {
				Regex rgx = new Regex("(\\w)([<>])(\\d+):(\\w+)");
				Match m = rgx.Match(input);
				if (m.Success)
				{
					label = m.Groups[1].Value;
					Limit = int.Parse(m.Groups[3].Value);
					Compare = m.Groups[2].Value;
					switch (m.Groups[2].Value)
					{
						case "<":
							requirementInstruction = Validators.SmallerThen(Limit);
							break;
						case ">":
							requirementInstruction = Validators.LargerThen(Limit);
							break;
						default:
							break;
					}
					
					Next = m.Groups[4].Value;
				}
                else
                {
                    Next = input;
                }
            }
            public string label;
			public Func<int, bool> requirementInstruction { get; set; }
			public int Limit { get; set; }
			public string Next { get; set; }
			public string Compare { get; set; }
		}

        public class  material
        {
			public material(string input) 
			{
				var t = input[1..^1].Split(',');
				materialValues = new();
                foreach (var item in t)
                {
					var parts = item.Split('=');
					materialValues[parts[0]] = int.Parse(parts[1]);
                }
			}
            public Dictionary<string, int> materialValues { get; set; }
        }
    }
}
