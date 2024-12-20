namespace _2024
{
    public class Day19:PuzzleWithObjectInput<(HashSet<string> towels, string[] designs)>
    {
        public Day19():base(19,2024)
        {
            
        }

        public override string SolvePart1((HashSet<string> towels, string[] designs) input)
        {
            Dictionary<string, long> cache = new();
            return input.designs.Count(x => Solve(x, input.towels,cache)>0).ToString();
        }

        public override string SolvePart2((HashSet<string> towels, string[] designs) input)
        {
            Dictionary<string, long> cache = new();
            return input.designs.Sum(x => Solve(x, input.towels,cache)).ToString();
        }

        private long Solve(string configuration, HashSet<string> available, Dictionary<string, long> cache)
        {
            if (cache.TryGetValue(configuration, out long cachedValue)) return cachedValue;

            long total = 0;
            foreach (var av in available)
            {
                if (configuration.StartsWith(av))
                {
                    total += configuration.Length == av.Length ? 1 : Solve(configuration.Substring(av.Length),available,cache);
                }
            }

            cache[configuration] = total;
            return total;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb") == "6");

            Debug.Assert(SolvePart2(@"r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb") == "16");
        }

        protected override (HashSet<string> towels, string[] designs) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine+Environment.NewLine);

            return (parts[0].Split(", ").ToHashSet(), parts[1].Split(Environment.NewLine));
        }
    }
}
