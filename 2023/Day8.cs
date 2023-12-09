namespace _2023
{
	public class Day8 : PuzzleWithObjectInput<(char[] instructions, Dictionary<string, Day8.NetworkNode> network)>
	{
		public Day8() : base(8, 2023)
		{
		}

		public override string SolvePart1((char[] instructions, Dictionary<string, NetworkNode> network) input)
		{
			return StepsToDestination(input.instructions, input.network, "AAA", ["ZZZ"]).ToString();
		}

		private int StepsToDestination(char[] instructions, Dictionary<string, NetworkNode> network, string startLocation, HashSet<string> PossibbleDestinations)
		{
			string CurrentLocation = startLocation;
			int StepCounter = 0;
			while (!PossibbleDestinations.Contains(CurrentLocation))
			{
				switch (instructions[StepCounter%instructions.Length])
				{
					case 'R':
						CurrentLocation = network[CurrentLocation].Right;
						break;
					case 'L':
						CurrentLocation = network[CurrentLocation].Left;
						break;
					default:
						break;
				}
				StepCounter++;
			}
			return StepCounter;
		}

		public override string SolvePart2((char[] instructions, Dictionary<string, NetworkNode> network) input)
		{
			IEnumerable<string> startlocationGhosts = input.network.Keys.Where(x => x[2] == 'A');
			HashSet<string> possibleDestinations = new HashSet<string>(input.network.Keys.Where(x => x[2] == 'Z') );

			Func<long, long, long> lcm = MathFunctions.findLCM();
			long result = startlocationGhosts.Select(x => (long)StepsToDestination(input.instructions, input.network, x, possibleDestinations))
									.AsParallel()
									.Aggregate(1L, (product, value) => product = lcm(product, value));
			return result.ToString();
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)") == "2");
			Debug.Assert(SolvePart1(@"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)") == "6");
			Debug.Assert(SolvePart2(@"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)") == "6");
		}

		protected override (char[] instructions, Dictionary<string, NetworkNode> network) CastToObject(string RawData)
		{
			string[] parts = RawData.Split(Environment.NewLine + Environment.NewLine);
			Dictionary<string, NetworkNode> network = parts[1].Split(Environment.NewLine).Select(x => new NetworkNode(x)).ToDictionary(x=>x.CurrentName);

			return (parts[0].ToCharArray(), network);

		}

		public class NetworkNode
		{
            public NetworkNode(string description)
            {
				string[] parts = description.Split(" ", StringSplitOptions.TrimEntries);
				CurrentName = new string( parts[0].Take(3).ToArray());
				Left = new string(parts[2].Skip(1).Take(3).ToArray());
				Right = new string(parts[3].Take(3).ToArray());
			}

			public string CurrentName { get; private set; }
			public string Left { get; private set; }
			public string Right { get; private set; }
        }
	}
}
