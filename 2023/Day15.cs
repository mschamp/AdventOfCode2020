using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2023
{
	public class Day15 : PuzzleWithObjectInput<Day15.instruction[]>
	{
		public Day15() : base(15, 2023)
		{
		}

		private int GetHash(string instruction)
		{
			int hash = 0;

            foreach (char c in instruction)
            {
				hash += c;
				hash *= 17;
				hash=hash%256;
            }

            return hash;
		}

		public override string SolvePart1(Day15.instruction[] input)
		{
			return $"{input.Sum(x=>GetHash(x.TotalInstruction))}";
		}

		public override string SolvePart2(Day15.instruction[] input)
		{
			List<List<instruction>> boxes = new(256);
			
			for (int i = 0; i<256; i++)
			{
				boxes.Add(new List<instruction>());
			}
			foreach (instruction b in input)
			{
				int labelHash = GetHash(b.label);
				switch (b.operatorC)
				{
					case '=':
						List < instruction> Boxlist = boxes[labelHash];
						if (Boxlist == null)
						{
							Boxlist = new List<instruction>();
							boxes[labelHash]=Boxlist;
						}
						if (Boxlist.Any(x => x.label == b.label))
						{
							Boxlist.Where(x => x.label == b.label).First().value= b.value;
						}
						else
						{
							Boxlist.Add(b);
						}

						break;
					case '-':
						boxes[labelHash].Remove(boxes[labelHash].Where(x => x.label == b.label).FirstOrDefault());
						break;
					default:
						break;
				}
			}

			int result = 0;
			for (int i = 0;i<256;i++)
			{
				for (int j = 0; j<boxes[i].Count;j++)
				{
					result += (i+1) * (j+1) * boxes[i][j].value;
				}
			}

			return $"{result}";
		}

		public override void Tests()
		{
			Debug.Assert(GetHash(@"HASH") == 52);
			Debug.Assert(SolvePart1(@"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7") == "1320");
			Debug.Assert(SolvePart2(@"rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7") == "145");
		}

		protected override Day15.instruction[] CastToObject(string RawData)
		{
			return RawData.Split(',').Select(x=>new instruction(x)).ToArray();
		}

		public class instruction
		{
			public instruction(string input)
			{
				TotalInstruction += input;
				label = new string(input.TakeWhile(x=>x!='='&& x != '-').ToArray());
				operatorC = input[label.Length];
				if (operatorC == '=') value = int.Parse(input.AsSpan(label.Length+1));
			}

			public string label { get; set; }
			public char operatorC { get; set; }
			public int value { get; set; }

			public string TotalInstruction { get; set; }
		}
	}
}
