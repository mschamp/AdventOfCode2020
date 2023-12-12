namespace _2023
{
	public class Day12 : PuzzleWithObjectArrayInput<(string line, int[] counts)>
	{
		public Day12():base(12,2023)
		{

		}

		public override string SolvePart1((string line, int[] counts)[] input)
		{
			return $"{input.AsParallel().Sum(x => CalculateOption(x.line, x.counts, 0,0,0, new Dictionary<(int pointer, int PointerCounts, int CurrentLength), long>()))}";
		}

		private long CalculateOption(string line, int[] counts, int CurrentLength, int pointerLine, int PointerCounts, Dictionary<(int pointer, int PointerCounts, int CurrentLength), long> cache)
		{
			if (cache.TryGetValue((pointerLine,PointerCounts,CurrentLength),out long result)) return result;
			if (line.Length == pointerLine)
			{ 
				if (CurrentLength == 0 && counts.Length == PointerCounts) return 1;
				if (counts.Length == PointerCounts+1 && CurrentLength == counts[PointerCounts]) return 1;
				return 0;
			}
			int Possible = 0;
			foreach (char c in line[pointerLine..])
			{
				if (c == '#' || c == '?') Possible++;
			}
			if (CurrentLength != 0 && Possible+CurrentLength < counts[PointerCounts..].Sum()) return 0;
			if (CurrentLength==0 && Possible < counts[PointerCounts..].Sum()) return 0;
			if (CurrentLength!=0 && counts.Length==PointerCounts) return 0;

			long ValidResults = 0;
			if (CurrentLength!=0)
			{
				switch (line[pointerLine])
				{
					case '.':
						if (CurrentLength < counts[PointerCounts]) return 0;
						ValidResults += CalculateOption(line, counts, 0, pointerLine + 1, PointerCounts + 1,cache);
						break;
					case '?':
                        if (CurrentLength == counts[PointerCounts]) ValidResults += CalculateOption(line, counts, 0, pointerLine + 1, PointerCounts + 1, cache);
						if (CurrentLength < counts[PointerCounts]) ValidResults += CalculateOption(line, counts, CurrentLength + 1, pointerLine + 1, PointerCounts,cache);
						break;
					case '#':
						ValidResults += CalculateOption(line, counts, CurrentLength + 1, pointerLine + 1, PointerCounts, cache);
						break;
					default:
						break;
				}
			}
			else
			{
				switch (line[pointerLine])
				{
					case '.':
						ValidResults += CalculateOption(line, counts, 0, pointerLine + 1, PointerCounts, cache); 
						break;
					case '?':
						ValidResults += CalculateOption(line, counts, 0, pointerLine + 1, PointerCounts, cache); 
						ValidResults += CalculateOption(line, counts, 1, pointerLine + 1, PointerCounts, cache); 
						break;
					case '#':
						ValidResults += CalculateOption(line, counts, 1, pointerLine + 1, PointerCounts, cache); 
						break;
					default:
						break;
				}
			}
			cache[(pointerLine, PointerCounts, CurrentLength)] = ValidResults;

			return ValidResults;
		}

		public override string SolvePart2((string line, int[] counts)[] input)
		{
			long sum = input.AsParallel().Sum(line => CalculateOption(string.Join("?", Enumerable.Repeat(line.line, 5)),
														Enumerable.Repeat(line.counts, 5).SelectMany(x => x).ToArray(), 
														0, 0, 0, 
														new Dictionary<(int pointer, int PointerCounts, int CurrentLength), long>()));
			
			return $"{sum}";
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"???.### 1,1,3") == "1");
			Debug.Assert(SolvePart1(@".??..??...?##. 1,1,3") == "4");
			Debug.Assert(SolvePart1(@"?#?#?#?#?#?#?#? 1,3,1,6") == "1");
			Debug.Assert(SolvePart1(@"????.#...#... 4,1,1") == "1");
			Debug.Assert(SolvePart1(@"????.######..#####. 1,6,5") == "4");
			Debug.Assert(SolvePart1(@"?###???????? 3,2,1") == "10");

			Debug.Assert(SolvePart2(@"???.### 1,1,3") == "1");
			Debug.Assert(SolvePart2(@".??..??...?##. 1,1,3") == "16384");
			Debug.Assert(SolvePart2(@"?#?#?#?#?#?#?#? 1,3,1,6") == "1");
			Debug.Assert(SolvePart2(@"????.#...#... 4,1,1") == "16");
			Debug.Assert(SolvePart2(@"????.######..#####. 1,6,5") == "2500");
			Debug.Assert(SolvePart2(@"?###???????? 3,2,1") == "506250");
		}

		protected override (string line, int[] counts) CastToObject(string RawData)
		{
			var parts = RawData.Split(" ");
			int[] counts = parts[1].Split(",").Select(int.Parse).ToArray();

			return (parts[0],counts);
		}
	}
}
