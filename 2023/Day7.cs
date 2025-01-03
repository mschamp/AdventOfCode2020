﻿namespace _2023
{
	public class Day7:PuzzleWithObjectArrayInput<Day7.Hand>
	{
		public Day7() : base(7, 2023)
		{
		}

		public override string SolvePart1(Hand[] input)
		{
			var orderedList = input.ToList();
			cardOrder['J'] = 3;
			orderedList.ForEach(x => x.DetectType(false));
			orderedList.Sort();
			int j = input.Length;
			long product = orderedList.Sum(x => x.Bid * j--);
			return product.ToString();
		}

		public override string SolvePart2(Hand[] input)
		{
			var orderedList = input.ToList();
			cardOrder['J'] = 100;
			orderedList.ForEach(x => x.DetectType(true));
			orderedList.Sort();
			int j = input.Length;
			long product = orderedList.Sum(x => x.Bid * j--); 
			return product.ToString();
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483") == "6440");
			Debug.Assert(SolvePart2(@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483") == "5905");
		}

		protected override Hand CastToObject(string RawData)
		{
			return new Hand(RawData);
		}

		public enum HandType
		{
			Fiveofakind,
			Fourofakind,
			Fullhouse,
			Threeofakind,
			Twopair,
			Onepair,
			Highcard
		}

		static Dictionary<char, int> cardOrder = new()
        { { 'A', 0 }, { 'K', 1 }, { 'Q', 2 }, { 'J', 3 }, { 'T',4 },
			{'9',5 }, {'8',6 }, {'7',7 }, {'6',8 }, {'5',9 },{'4',10 }, {'3',11 },{'2',12 } };
		
		public class Hand: IComparable<Hand>
		{
            public Hand(string input)
            {
				var parts = input.Split(" ");
				Bid = long.Parse(parts[1]);
				Cards = parts[0].ToArray();
			}

			public void DetectType(bool JisJoker)
			{
				var counted = Cards.GroupBy(c => c).Select(g => new { g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList();
				int countJ = 0;
				if (JisJoker && Cards.Contains('J'))
				{
					countJ = counted.First(x => x.Key == 'J').Count;
					counted.Remove(counted.FirstOrDefault(x => x.Key == 'J'));
				}
				if (countJ == 5 || counted[0].Count + countJ == 5) HandType = HandType.Fiveofakind;
				else if (counted[0].Count + countJ == 4) HandType = HandType.Fourofakind;
				else if (counted[0].Count + countJ == 3 && counted[1].Count == 2) HandType = HandType.Fullhouse;
				else if (counted[0].Count + countJ == 3) HandType = HandType.Threeofakind;
				else if (counted[0].Count + countJ == 2 && counted[1].Count == 2) HandType = HandType.Twopair;
				else if (counted[0].Count + countJ == 2) HandType = HandType.Onepair;
				else HandType = HandType.Highcard;
			}

			public char[] Cards { get; private set; }

			public long Bid { get; private set; }

			public HandType HandType { get; private set; }

			public int CompareTo(Hand? other)
			{
				if (other.HandType> HandType) return -1;
				if (other.HandType< HandType) return 1;
				for (int i = 0; i < Cards.Length; i++)
				{
					if (cardOrder[other.Cards[i]] > cardOrder[Cards[i]]) return -1;
					if (cardOrder[other.Cards[i]] < cardOrder[Cards[i]]) return 1;
				}
				return 0;
			}

		}
	}
}
