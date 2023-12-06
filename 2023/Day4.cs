namespace _2023
{
	public class Day4 : General.PuzzleWithStringArrayInput
	{
        public Day4():base(4,2023)
        {
				
        }
        public override string SolvePart1(string[] input)
		{
			int sum = input.Select(x => x.Split(':', '|')).Sum(x => ScoreOfCard(x[1].Trim(), x[2].Trim()));
			return sum.ToString();
		}

		private int ScoreOfCard(string YourNumbers, string AllNumbers)
		{
			int Matches = NumberOfMatches(YourNumbers, AllNumbers);
			if (Matches > 0)
			{
				return (int)Math.Pow(2, Matches) / 2;
			}
			return 0;
		}

		private int NumberOfMatches(string YourNumbers, string AllNumbers)
		{
			int[] YourNumberInt = YourNumbers.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			int[] AllNInt = AllNumbers.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

			int[] Matches = YourNumberInt.Intersect(AllNInt).ToArray();
			return Matches.Length;
		}

		public override string SolvePart2(string[] input)
		{
			Dictionary<int, int>CardWins = new Dictionary<int, int>();
			int[] CardCount = new int[input.Length+1];
			for (int i = 1; i <= input.Length; i++)
			{
				string[] cardParts = input[i-1].Split(':', '|');
				CardWins[i] = NumberOfMatches(cardParts[1].Trim(), cardParts[2].Trim());
				CardCount[i] ++;
				AddWinningCards(CardWins, ref CardCount, i);
			}



			return $"{CardCount.Sum()}";
        }

		private void AddWinningCards(Dictionary<int, int> CardWins, ref int[] CardCount, int CardID)
		{
			for (int i = CardID+1; i < CardID + 1 + CardWins[CardID]; i++)
			{
				CardCount[i]+= CardCount[CardID];
			}
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11") == "13");

			Debug.Assert(SolvePart2(@"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11") == "30");
		}
	}
}
