using General;
using System.Diagnostics;

namespace _2023
{
	public class Day3 : PuzzleWithObjectInput<(List<Day3.Number> numbers, List<Day3.Symbol> symbols)>
	{
		public Day3() : base(3, 2023)
		{
		}

		public struct Number
		{
			public int Value { get; set; }
			public (int Row, int Column) Start { get; set; }
			public (int Row, int Column) End { get; set; }
		}

		public struct Symbol
		{
			public char Value { get; set; }
			public (int Row, int Column) Position { get; set; }
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..") == "4361");

			Debug.Assert(SolvePart2(@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..") == "467835");
		}

		protected override (List<Number> numbers, List<Symbol> symbols) CastToObject(string RawData)
		{
			List<Number> numbers = new List<Number>();
			List<Symbol> symbols = new List<Symbol>();
			string[] input = RawData.Split(Environment.NewLine);

			for (int row = 0; row < input.Length; row++)
			{
				Number currentNumber = new Number();
				List<char> digits = new List<char>();

				for (int col = 0; col < input[row].Length; col++)
				{
					if (input[row][col] == '.')
						continue;

					if (char.IsDigit(input[row][col]))
					{
						char digit = input[row][col];
						digits.Add(digit);
						if (digits.Count == 1)
						{
							currentNumber.Start = (row, col);
						}

						while (col < input[row].Length - 1 && char.IsDigit(input[row][col + 1]))
						{
							digit = input[row][col+1];
							digits.Add(digit);
							col++;
						}

						currentNumber.End = (row, col);
						currentNumber.Value = int.Parse(new string(digits.ToArray()));
						numbers.Add(currentNumber);
						currentNumber = new Number();
						digits.Clear();
					}
					else
					{
						symbols.Add(new Symbol
						{
							Value = input[row][col],
							Position = (row, col)
						});
					}
				}
			}

			return (numbers, symbols);
		}

		public override string SolvePart1((List<Number> numbers, List<Symbol> symbols) input)
		{
			int part1 = input.numbers
				.Where(number => input.symbols.Any(symbol =>
					Math.Abs(symbol.Position.Row - number.Start.Row) <= 1
					&& symbol.Position.Column >= number.Start.Column - 1
					&& symbol.Position.Column <= number.End.Column + 1))
				.Sum(number => number.Value);
			return $"{part1}";
		}

		public override string SolvePart2((List<Number> numbers, List<Symbol> symbols) input)
		{
			int part2 = input.symbols
				.Where(symbol => symbol.Value == '*')
				.Select(symbol => input.numbers.Where(number =>
					Math.Abs(symbol.Position.Row - number.Start.Row) <= 1
					&& symbol.Position.Column >= number.Start.Column - 1
					&& symbol.Position.Column <= number.End.Column + 1)
					.ToArray())
				.Where(gears => gears.Length == 2)
				.Sum(gears => gears[0].Value * gears[1].Value);

			return $"{part2}";
		}
	}
}
