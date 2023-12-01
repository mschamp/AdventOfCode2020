using System.Diagnostics;

namespace _2022
{
    public class Day1 : General.PuzzleWithObjectArrayInput<int[]>
    {
        public Day1():base(1, 2022)
        {

        }

        protected override int[] CastToObject(string RawData)
        {
            return RawData.Split(Environment.NewLine).Select(y => int.Parse(y)).ToArray();
        }

        public override string SolvePart1(int[][] input)
        {
            return input.Max(x => x.Sum()).ToString();
        }


        public override string SolvePart2(int[][] input)
        {
            return input.Select(x => x.Sum()).OrderByDescending(x => x).Take(3).Sum().ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000") == "24000");

            Debug.Assert(SolvePart2(@"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000") == "45000");
        }
    }
}