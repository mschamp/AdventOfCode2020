namespace _2023
{
	public class Day2 : PuzzleWithObjectArrayInput<Day2.game>
	{
		public Day2() : base(2, 2023)
		{
		}


		Dictionary<string, int> ValidGameDict = new() { { "red",12}, { "blue", 14 }, { "green", 13 } };

		public bool ValidGame(game game)
		{
			for (int i = 0; i < game.Turns.Length; i += 1)
			{
				string[] stones = game.Turns[i].Split(' ',',');
				for (int j = 0; j < stones.Length; j += 3)
				{
					if(ValidGameDict[stones[j + 1]]< int.Parse(stones[j])) return false;
				}
			}

			return true;
		}

		public int CalculatePower(game game)
		{

			Dictionary<string, int> map = [];
			for (int i = 0; i < game.Turns.Length; i += 1)
			{
				string[] stones = game.Turns[i].Split(' ', ',');
				for (int j = 0; j < stones.Length; j += 3)
				{
					if (!map.ContainsKey(stones[j + 1])) map[stones[j + 1]] = 0;
					map[stones[j + 1]] = Math.Max(map[stones[j + 1]], int.Parse(stones[j]));
				}
			}

			return map.Values.Aggregate(1,(product, val)=>product*val);
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

		protected override game CastToObject(string RawData)
		{
			return new game(RawData);
		}

		public override string SolvePart1(game[] input)
		{
			return $"{input.Where(ValidGame).Sum(x => x.ID)}";
		}

		public override string SolvePart2(game[] input)
		{
			return $"{input.Sum(CalculatePower)}";
		}

		public class game
		{
            public game(string input)
            {
				string[] gameArray = input.Split(':');
				ID = int.Parse(gameArray[0].Split(' ')[1]);
				Turns = gameArray[1].Split(";",StringSplitOptions.TrimEntries).ToArray();
			}
			public int ID { get; private set; }
			public string[] Turns { get; private set; }
        }
	}
}
