﻿namespace _2023
{
	public class Day6 : PuzzleWithStringArrayInput
	{
		public Day6() : base(6, 2023)
		{
		}

		public override string SolvePart1(string[] input)
		{
			IEnumerable<(long First, long Second)> Combinations = input[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse)
				.Zip(input[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse));

			return Combinations.Select(CalculateWaysToWin).Aggregate((long)1, (product, value) => product * value).ToString();
		}

		public override string SolvePart2(string[] input)
		{
			IEnumerable<(long First, long Second)> Combinations = input[0].Replace(" ", "").Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse)
				.Zip(input[1].Replace(" ", "").Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse));

			return Combinations.Select(CalculateWaysToWin).Aggregate((long)1, (product, value) => product * value).ToString();
		}

		private long CalculateWaysToWin((long Totaltime, long RecordDistance) input)
		{
			double sqrtD = Math.Sqrt(Math.Pow(input.Totaltime, 2) - 4 * input.RecordDistance);
			long R = (long)(Math.Ceiling((input.Totaltime + sqrtD) / 2) - Math.Floor((input.Totaltime - sqrtD) / 2) - 1);

			return R;
		}

		private long CalculateWaysToWinOld((long Totaltime, long RecordDistance) input)
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

		private long CalculateDistance(long TotalTime, long HoldButton) => (TotalTime - HoldButton) * HoldButton;

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"Time:      7  15   30
Distance:  9  40  200") == "288");

			Debug.Assert(SolvePart2(@"Time:      7  15   30
Distance:  9  40  200") == "71503");
		}
	}
}
