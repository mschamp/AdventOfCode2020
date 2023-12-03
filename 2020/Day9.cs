using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2020
{
	public class Day9 : General.PuzzleWithLongArrayInput
    {
        public Day9() : base(9, 2020) { }
        public long findInvalidNumber(ref List<long> values, int store, long[] inputvalues)
        {
            int pointer = 0;
            while (values.Count < store)
            {
                values.Add(inputvalues[pointer]);
                pointer++;
            }

            while (true)
            {
                long value = inputvalues[pointer];
                if (!CheckIfSum(values, value))
                {
                    return value;
                }
                values.RemoveAt(0);
                values.Add(value);
                pointer++;
            }
        }
        public bool CheckIfSum(List<long> values, long value)
        {
            return values.Any(x => values.Contains(value - x));
        }

        public List<long> FindContinguousSet(long sum, long[] inputs)
        {
            for (int i = 1; i < inputs.Length; i++)
            {
                List<long> Values = new() { inputs[i] };
                while (Values.Sum()<sum)
                {
                    for (int j=1; j< inputs.Length-i; j++)
                    {
                        Values.Add(inputs[i + j]);
                        if (Values.Sum() == sum)
                        {
                            return Values;
                        }
                    }
                }
            }
            return null;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576") == "127");

            Debug.Assert(SolvePart2(@"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576") == "62");
        }

        public override string SolvePart1(long[] input)
        {
			int store;

			if (input.Length > 100)
			{
				store = 25;
			}
			else
			{
				store = 5;
			}
			List<long> values = new();

            return "" + findInvalidNumber(ref values, store, input);
        }

        public override string SolvePart2(long[] input)
        {
            int store;

			if (input.Length >100)
            {
				store = 25;
			}
            else
            {
				store = 5;
			}
            
            List<long> values = new();

            List<long> set = FindContinguousSet(findInvalidNumber(ref values, store, input), input);

            return "" + (set.Min() + set.Max());
        }
    }
}
