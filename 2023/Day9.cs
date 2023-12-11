namespace _2023
{
	public class Day9 : PuzzleWithObjectArrayInput<List<int>>
	{
		public Day9() : base(9, 2023)
		{
		}

		public override string SolvePart1(List<int>[] input)
		{
			return input.Sum(getNext).ToString();
		}

		private int getNext(IEnumerable<int> sequence)
		{
			IEnumerable<int> diffs = sequence;
			List<int> lasts = new List<int> { sequence.Last() };

			while (diffs.Any(x=>x!=0))
			{
				IEnumerable<(int First, int Second)> combined = diffs.Zip(diffs.Skip(1));
				diffs = combined.Select(x=>x.Second-x.First).ToList();
				lasts.Add(diffs.Last());
			}
			return lasts.Sum();
		}

		public override string SolvePart2(List<int>[] input)
		{
			return input.Sum(x => { x.Reverse(); return getNext(x); }).ToString();
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45") == "114");

			Debug.Assert(SolvePart2(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45") == "2");
		}

		protected override List<int> CastToObject(string RawData)
		{
			return new List<int>(RawData.Split(' ').Select(int.Parse));
		}
	}
}
