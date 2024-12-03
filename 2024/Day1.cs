namespace _2024
{
    public class Day1 : PuzzleWithObjectInput<(List<int> left, List<int> right)>
    {
        public Day1() : base(1, 2024)
        {
        }

        public override string SolvePart1((List<int> left, List<int> right) input)
        {
            input.left.Sort();
            input.right.Sort();

            int Difference = input.left.Zip(input.right).Sum(x => Math.Abs(x.First-x.Second));

            return Difference.ToString();
        }

        public override string SolvePart2((List<int> left, List<int> right) input)
        {
            Dictionary<int,int> grouped= input.right.GroupBy(x=>x).ToDictionary(x=>x.First(), x=>x.Count());

            long similarity = input.left.Aggregate(0, (sim, x) => grouped.ContainsKey(x)? sim + x * grouped[x]:sim);

            return similarity.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"3   4
4   3
2   5
1   3
3   9
3   3") == "11");
            Debug.Assert(SolvePart2(@"3   4
4   3
2   5
1   3
3   9
3   3") == "31");
        }

        protected override (List<int> left, List<int> right) CastToObject(string RawData)
        {
            List<int> left = [];
            List<int> right = [];

            foreach (var item in RawData.Split(Environment.NewLine))
            {
                int[] parts = item.Split("   ").Select(x => int.Parse(x)).ToArray();
                left.Add(parts[0]);
                right.Add(parts[1]);
            }

            return (left, right);
        }
    }
}
