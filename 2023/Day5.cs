namespace _2023
{
	public class Day5 : General.abstractPuzzleClass
	{
		public Day5() : base(5, 2023)
		{
		}

		public override string SolvePart1(string input = null)
		{
			string[] parts = input.Split(Environment.NewLine + Environment.NewLine);
			long[] seeds = parts[0].Split(' ').Skip(1).Select(long.Parse).ToArray();
			List<ProjectionFunction> functions = [];	

			for (int i = 1; i < parts.Length; i++)
			{
				functions.Add(new ProjectionFunction(parts[i]));
			}

			List<long> Positions = [];
			foreach (long seed in seeds)
			{
				long x = seed;
				foreach (ProjectionFunction projection in functions)
				{
					x = projection.CalculateMapping(x);
				}
				Positions.Add(x);
			}

			return $"{Positions.Min()}";
		}

		public override string SolvePart2(string input = null)
		{
			string[] parts = input.Split(Environment.NewLine + Environment.NewLine);
			long[] seeds = parts[0].Split(' ').Skip(1).Select(long.Parse).ToArray();
			List<(long, long)> SeedRanges = [];

			for (int i = 0; i < seeds.Length; i+=2)
			{
				SeedRanges.Add((seeds[i], seeds[i] + seeds[i + 1]));
			}

			List<ProjectionFunction> functions = [];

			for (int i = 1; i < parts.Length; i++)
			{
				functions.Add(new ProjectionFunction(parts[i]));
			}

			List<long> PossibleStartLocations = [];
			foreach ((long, long) item in SeedRanges)
			{
				List<(long, long)> x = [item];
				foreach (ProjectionFunction projection in functions)
				{
					x = projection.CalculateMappingRange(x);
				}
				PossibleStartLocations.Add(x.Select(y => y.Item1).Min());
			}

			return $"{PossibleStartLocations.Min()}";
		}

		public class ProjectionFunction
		{
            public ProjectionFunction(string InstructionBlock)
            {
				string[] Instruction = InstructionBlock.Split(Environment.NewLine);
				Instruction.Skip(1).Select(x=>x.Split(' ').ToArray()).ToList().ForEach(x=> mappings.Add((long.Parse(x[0]), long.Parse(x[1]), long.Parse(x[2]))));
            }

			public List<(long dest,long src,long count)> mappings = [];

			public long CalculateMapping(long x)
			{
				foreach ((long dest, long src, long count) item in mappings)
				{
					if (item.src<=x && x<item.src+item.count)
					{
						return x + item.dest - item.src;
					}
				}
				return x;
			}

			public List<(long,long)> CalculateMappingRange(IEnumerable<(long start,long end)> x)
			{
				Queue<(long start, long end)> Ranges = new(x);
				List<(long, long)> result = [];
				foreach ((long dest, long src, long count) item in mappings)
				{
					long end = item.src + item.count;
					List<(long, long)> NR = [];
					while (Ranges.TryDequeue(out (long start,long end) Current))
					{
						(long start, long) before = (Current.start, Math.Min(Current.end, item.src));
						(long, long) overlap = (Math.Max(Current.start, item.src), Math.Min(Current.end, end));
						(long, long end) after = (Math.Max(end, Current.start), Current.end);

						if (before.Item2>before.Item1)
						{
							NR.Add(before);
						}
						if (overlap.Item2 > overlap.Item1)
						{
							result.Add((overlap.Item1-item.src+item.dest, overlap.Item2 - item.src + item.dest));
						}
						if (after.Item2 > after.Item1)
						{
							NR.Add(after);
						}
					}
					Ranges = new Queue<(long start, long end)>(NR);
				}
				result.AddRange(Ranges);
				return  result;
			}
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4") == "35"); 
			
			Debug.Assert(SolvePart2(@"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4") == "46");
		}
	}
}
