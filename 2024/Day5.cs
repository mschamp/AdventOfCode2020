namespace _2024
{
    public class Day5:PuzzleWithObjectInput<(HashSet<(int,int)>,List<List<int>>)>
    {
        public Day5():base(5,2024)
        {

        }

        private class ComparePages(HashSet<(int, int)> rules) : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (rules.Contains((x, y))) return -1;
                if (rules.Contains((y, x))) return 1;
                return 0;
            }
        }

        private bool SortedCorrectly(List<int> page, HashSet<(int,int)> rules)
        {
            foreach (var rule in rules)
            {
                if (page.Contains(rule.Item1) && page.Contains(rule.Item2) 
                    && page.IndexOf(rule.Item1) > page.IndexOf(rule.Item2)) return false;
            }

            return true;
        }
        public override string SolvePart1((HashSet<(int, int)>, List<List<int>>) input) { 

        
            return input.Item2.Where(x=>SortedCorrectly(x,input.Item1))
                .Sum(x => x[x.Count / 2])
                .ToString();
        }

        public override string SolvePart2((HashSet<(int, int)>, List<List<int>>) input)
        {
            IComparer<int> comp = new ComparePages(input.Item1);
            return input.Item2.Where(x => !SortedCorrectly(x, input.Item1))
                .Select(x=>x.Order(comp).ToList())
                .Sum(x => x[x.Count / 2])
                .ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47") == "143");
            Debug.Assert(SolvePart2(@"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47") == "123");
        }

        protected override (HashSet<(int, int)>, List<List<int>>) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine+Environment.NewLine);
            HashSet<(int,int)> rules = parts[0].Split(Environment.NewLine).Select(x=>x.Split('|')).Select(x=> (int.Parse(x[0]), int.Parse(x[1]))).ToHashSet();

            List<List<int>> updates = parts[1].Split(Environment.NewLine).Select(x => x.Split(',')).Select(x => x.Select(int.Parse).ToList()).ToList();

            return (rules, updates);
        }
    }
}
