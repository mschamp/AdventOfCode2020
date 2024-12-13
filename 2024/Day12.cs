using Interfaces.Extensions;

namespace _2024
{
    public class Day12: PuzzleWithObjectInput<(Dictionary<(int x, int y),char> grid, (int x, int y) size)>
    {
        public Day12():base (12,2024)
        {
            
        }

        public override string SolvePart1((Dictionary<(int x, int y), char> grid, (int x, int y) size) input)
        {
            HashSet<(int x, int y)> seen = new();

            long result = 0;

            for (int y = 0; y < input.size.y; y++)
            {
                for (int x = 0; x < input.size.x; x++)
                {
                    if (seen.Contains((x, y))) continue;
                    var group = GetGroup((x,y),input.grid,seen).ToList();

                    result += area(group) * perimeter(group.ToHashSet(), input.grid);
                }
            }

            return result.ToString();
        }

        private int perimeter(HashSet<(int x, int y)> group, Dictionary<(int x, int y), char> grid)
        {
            int result = 0;
            foreach (var item in group)
            {
                var t = grid.Neighbors(item).Intersect(group).ToList();
                result += 4 - grid.Neighbors(item).Intersect(group).Count();
            }
            return result;
        }

        private int area(List<(int x, int y)> group)
        {
            return group.Count;
        }

        private IEnumerable<(int x, int y)> GetGroup((int x, int y) start, Dictionary<(int x, int y),char> grid, HashSet<(int x, int y)> seen)
        {
            Queue<(int x, int y)> ToAnalyse = new();
            ToAnalyse.Enqueue(start);
            seen.Add(start);
            yield return start;
            while (ToAnalyse.TryDequeue(out (int x, int y)current))
            {
                foreach (var item in grid.Neighbors(current))
                {
                    if (grid[current]==grid[item] && seen.Add(item))
                    {
                        ToAnalyse.Enqueue(item);
                        yield return item;
                    }
                }
                
            }
        }

        public override string SolvePart2((Dictionary<(int x, int y), char> grid, (int x, int y) size) input)
        {
            HashSet<(int x, int y)> seen = new();

            long result = 0;

            for (int y = 0; y < input.size.y; y++)
            {
                for (int x = 0; x < input.size.x; x++)
                {
                    if (seen.Contains((x, y))) continue;
                    var group = GetGroup((x, y), input.grid, seen).ToList();

                    result += area(group) * sides(group.ToHashSet(), [(-1,0),(1,0),(0,1),(0,-1)] );
                }
            }

            return result.ToString();
        }

        private int sides(HashSet<(int, int)> grp, List<(int, int)> moves)
        {
            HashSet<(int, int, int, int)> sseen = new HashSet<(int, int, int, int)>();
            int ccs = 0;

            foreach (var (y, x) in grp)
            {
                foreach (var (dy, dx) in moves)
                {
                    if (grp.Contains((y + dy, x + dx)))
                    {
                        continue;
                    }
                    int cy = y, cx = x;
                    while (grp.Contains((cy + dx, cx + dy)) && !grp.Contains((cy + dy, cx + dx)))
                    {
                        cy += dx;
                        cx += dy;
                    }
                    if (!sseen.Contains((cy, cx, dy, dx)))
                    {
                        sseen.Add((cy, cx, dy, dx));
                        ccs++;
                    }
                }
            }
            return ccs;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"AAAA
BBCD
BBCC
EEEC") == "140"); 
            Debug.Assert(SolvePart1(@"OOOOO
OXOXO
OOOOO
OXOXO
OOOOO") == "772");

            Debug.Assert(SolvePart2(@"AAAA
BBCD
BBCC
EEEC") == "80");

            Debug.Assert(SolvePart2(@"EEEEE
EXXXX
EEEEE
EXXXX
EEEEE") == "236");
            Debug.Assert(SolvePart2(@"AAAAAA
AAABBA
AAABBA
ABBAAA
ABBAAA
AAAAAA") == "368");
        }

        protected override (Dictionary<(int x, int y), char> grid, (int x, int y) size) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            (int x, int y) size = (lines[0].Length, lines.Length);

            Dictionary<(int x, int y), char> grid = new();
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    grid[(x,y)]= lines[y][x];
                }
            }

            return (grid,size);
        }
    }
}
