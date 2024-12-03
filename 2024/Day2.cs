using General;
using System.Linq;

namespace _2024
{
    public class Day2 : PuzzleWithObjectArrayInput<int[]>
    {
        public Day2():base(2,2024)
        { }

        public override string SolvePart1(int[][] input)
        {
            int count = input.Count(x => Safe(x));

            return count.ToString();
        }

        private bool Safe(int[] row)
        {
            int Sign = Math.Sign(row[0] - row[1]);
            if (Sign == 0 || Math.Abs(row[0] - row[1]) > 3) return false;

            for (int i = 1; i < row.Length-1; i++)
            {
                if (Sign != Math.Sign(row[i] - row[i+1]) || Math.Abs(row[i] - row[i+1]) > 3) return false;
            }

            return true;
        }

        public bool SafeWithDampener(int[] row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                List<int> newRow = row.ToList();
                newRow.RemoveAt(i);

                if (Safe(newRow.ToArray())) return true;
            }
            return false;
        }

        public override string SolvePart2(int[][] input)
        {
            int count = input.Count(x=> Safe(x)||SafeWithDampener(x));
            return count.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9") == "2");
            Debug.Assert(SolvePart2(@"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9") == "4");
        }

        protected override int[] CastToObject(string RawData)
        {
            return RawData.Split(' ').Select(x=> int.Parse(x)).ToArray();
        }
    }
}
