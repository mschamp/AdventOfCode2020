namespace _2024
{
    public class Day4 : PuzzleWithObjectInput<Dictionary<(int, int), char>>
    {
        public Day4() : base(4, 2024)
        {

        }

        public override string SolvePart1(Dictionary<(int, int), char> grid)
        {
            string goal = "XMAS";
            int counter = 0;

            var DELTAS = new List<(int dy, int dx)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            ( 0, -1),          ( 0, 1),
            ( 1, -1), ( 1, 0), ( 1, 1)
        };

            foreach (var (y, x) in grid.Keys)
            {
                if (grid[(y, x)] != 'X') continue;
                foreach (var (dy, dx) in DELTAS)
                {
                    string candidate = new string(Enumerable.Range(0, goal.Length)
                        .Select(i => grid.ContainsKey((y + dy * i, x + dx * i)) ? grid[(y + dy * i, x + dx * i)] : '.')
                        .ToArray());
                    if (candidate == goal) counter++;
                }
            }

            return counter.ToString();
        }

        public override string SolvePart2(Dictionary<(int, int), char> grid)
        {
            int counter = 0;
            HashSet<string> options = new HashSet<string> { "MS", "SM" };
            foreach (var (y, x) in grid.Keys)
            {
                if (grid[(y, x)] != 'A') continue;
                HashSet<string> foundValues = new HashSet<string>
                {
                    ""+
                    (grid.ContainsKey((y - 1, x - 1)) ? grid[(y - 1, x - 1)] : '.') +
                    (grid.ContainsKey((y + 1, x + 1)) ? grid[(y + 1, x + 1)] : '.'),
                    ""+
                    (grid.ContainsKey((y - 1, x + 1)) ? grid[(y - 1, x + 1)] : '.') +
                    (grid.ContainsKey((y + 1, x - 1)) ? grid[(y + 1, x - 1)] : '.')
                };

                if (foundValues.IsSubsetOf(options))
                {
                    counter++;
                }
            }

            return counter.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX") == "18");
            Debug.Assert(SolvePart2(@"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX") == "9");
        }

        protected override Dictionary<(int, int), char> CastToObject(string RawData)
        {
            string[] input = RawData.Split(Environment.NewLine).ToArray();
            Dictionary<(int, int), char> grid = new Dictionary<(int, int), char>();

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    grid[(y, x)] = input[y][x];
                }
            }
            return grid;

        }
    }
}
