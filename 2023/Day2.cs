using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
	public class Day2 : PuzzleWithStringArrayInput
	{
		public Day2() : base(2, 2023)
		{
		}

		public override string SolvePart1(string[] input)
		{
			int sum = 0;
            foreach (var item in input)
            {
				if (ValidGame(item, out int ID)) sum += ID;
            }
			return $"{sum}";


		}

		Dictionary<string, int> ValidGameDict = new Dictionary<string, int> { { "red",12}, { "blue", 14 }, { "green", 13 } };

		public bool ValidGame(string game, out int ID)
		{
			string[] gameArray = game.Split(':');
			ID = int.Parse(gameArray[0].Split(' ')[1]);

			string[] parts = gameArray[1].Split(';').ToArray();
			for (int i = 0; i < parts.Length; i += 1)
			{
				string[] stones = parts[i].Split(' ',',');
				for (int j = 1; j < stones.Length; j += 3)
				{
					if(ValidGameDict[stones[j + 1]]< int.Parse(stones[j])) return false;
				}
			}

			return true;
		}

		public int CalculatePower(string game)
		{

			string[] gameArray = game.Split(':');

			string[] parts = gameArray[1].Split(';').ToArray();
			Dictionary<string, int> map = new Dictionary<string, int>();
			for (int i = 0; i < parts.Length; i += 1)
			{
				string[] stones = parts[i].Split(' ', ',');
				for (int j = 1; j < stones.Length; j += 3)
				{
					if (!map.ContainsKey(stones[j + 1])) map[stones[j + 1]] = 0;
					map[stones[j + 1]] = Math.Max(map[stones[j + 1]], int.Parse(stones[j]));
				}
			}

			return map.Values.Aggregate(1,(product, val)=>product*val);
		}

		public override string SolvePart2(string[] input)
		{
			return $"{input.Sum(x =>CalculatePower(x))}";
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green") == "8");

			Debug.Assert(SolvePart2(@"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green") == "2286");
		}
	}
}
