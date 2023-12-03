using System.Diagnostics;

namespace _2020
{
	public class Day1 : General.PuzzleWithIntegerArrayInput
    {
        public Day1() : base(1, 2020) { }
        public override string SolvePart1(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                    {
                        return (input[i] * input[j]).ToString();
                    }
                }
            }
            return "";
        }

        public override string SolvePart2(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    for (int k = 0; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return (input[i] * input[j] * input[k]).ToString();
                        }
                    }
                }
            }
            return "";
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"1721
979
366
299
675
1456") == "514579");
        

        Debug.Assert(SolvePart2(@"1721
979
366
299
675
1456") == "241861950");
        }
}
}
