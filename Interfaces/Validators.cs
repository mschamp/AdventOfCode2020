using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace General
{
	public static class Validators
    {
        public static Func<string, bool> NumberValidator(int min, int max)
        {
            return input => int.TryParse(input, out int number) && (number >= min && number <= max);
        }

        public static Func<string, int> RegexMatchCountValidator(string Regex)
        {
            Regex rgx = new(Regex);

            return input => rgx.Matches(input).Count();
        }

        public static Func<string, bool> ElementOfListValidator(string[] List)
        {
            return input => List.Contains(input);
        }

        public static Func<string, bool> DoubleLetterValidator()
        {
            return input => 
            {
                for (int i = 1; i < input.Length; i++)
                {
                    if (input[i]==input[i-1])
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        public static Func<string, bool> ContainsLetterValidator(char Letter)
        {
            return input => input.Contains(Letter);
        }

        public static Func<string, bool> ContainsStringValidator(string String)
        {
            return input => input.Contains(String);
        }

        public static Func<string, bool> RegexValidator(string Regex)
        {
            Regex rgx = new(Regex);

            return input => rgx.IsMatch(input);
        }

        public static Func<int,bool>LargerThen(int limit) 
        {
            return input => input > limit;
        }

		public static Func<int, bool> SmallerThen(int limit)
		{
			return input => input < limit;
		}

		public static Func<string, bool> HeightValidator()
        {
            Func<string, bool> cmValidator = NumberValidator(150, 193);
            Func<string, bool> inValidator = NumberValidator(59, 76);

            Regex hgt = new(@"^(\d+)(cm|in)$");

            return input =>
            {
                Match value = hgt.Match(input);
                return value.Success && ((value.Groups[2].Value == "cm" && cmValidator(value.Groups[1].Value)) || (value.Groups[2].Value == "in" && inValidator(value.Groups[1].Value)));
            };
        }
    }
}
