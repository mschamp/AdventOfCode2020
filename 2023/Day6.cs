namespace _2023
{
	public class Day6 : PuzzleWithStringArrayInput
	{
		public Day6() : base(6, 2023)
		{
		}

		public override string SolvePart1(string[] input = null)
		{
			var Combinations = input[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse)
				.Zip(input[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse));

			return Combinations.Select(CalculateWaysToWin).Aggregate((long)1, (product,value)=>product*value).ToString();
		}

		private long CalculateWaysToWin((long Totaltime, long RecordDistance)input)
		{
			bool WaitingForWin = true;
			long CountWins = 0;
			long time = 1;
			bool LastWin;
			do
			{
				LastWin = false;
				if (CalculateDistance(input.Totaltime, time) > input.RecordDistance)
				{
					CountWins++;
					WaitingForWin = false;
					LastWin = true;
				}
				time++;
			} while (WaitingForWin || LastWin);
			return CountWins;
		}

		private long CalculateDistance(long TotalTime, long HoldButton)
		{
			return (TotalTime - HoldButton) * HoldButton;
		}

		public override string SolvePart2(string[] input = null)
		{
			var Combinations = input[0].Replace(" ", "").Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse)
				.Zip(input[1].Replace(" ", "").Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse));

			return Combinations.Select(CalculateWaysToWin).Aggregate((long)1, (product, value) => product * value).ToString();
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"Time:      7  15   30
Distance:  9  40  200") == "288");

			Debug.Assert(SolvePart2(@"Time:      7  15   30
Distance:  9  40  200") == "71503");
		}
	}
}
