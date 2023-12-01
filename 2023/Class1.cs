using System.Diagnostics;
using System.Text.Json.Serialization;

namespace _2023
{
	public class Day1 : General.PuzzleWithStringArrayInput
	{
		Dictionary<string, char> text = new Dictionary<string, char> {
				{ "one", '1' },
				{ "two", '2' },
				{ "three", '3' },
				{ "four", '4' },
				{ "five", '5' },
				{ "six", '6' },
				{ "seven", '7' },
				{ "eight", '8' },
				{ "nine", '9' },
			};

		public Day1():base(1,2023)
        {
            
        }
        public override string SolvePart1(string[] input)
		{
			int sum = input.Sum(x=>ExtractNumber(x,false));
			return sum.ToString();

		}

		public override string SolvePart2(string[] input)
		{
			int sum = input.Sum(x => ExtractNumber(x, true));
			return sum.ToString();
		}

		private int ExtractNumber(string line, bool includetext)
		{
			List<char> numbers = new List<char>();
			for (int i = 0; i < line.Length; i++)
			{
				if (char.IsDigit(line[i])) numbers.Add(line[i]);
				else if (includetext && dectectTextNumber(text, line, i, out char number)) numbers.Add(number);
			}

			return int.Parse(new string(new char[] { numbers[0], numbers.Last() }));
		}


		private bool dectectTextNumber(Dictionary<string, char> numbers, string line, int i, out char number)
		{
			number = '0';
			foreach (var item in numbers)
			{
				if ( line.AsSpan(i).StartsWith(item.Key))
				{
					number = item.Value;
					return true;
				}
			}
			return false;
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet") == "142");

			Debug.Assert(SolvePart2(@"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen") == "281");
		}
	}
}
