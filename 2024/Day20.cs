using Interfaces.Extensions;

namespace _2024
{
    public class Day20 : PuzzleWithObjectInput<(HashSet<(int x, int y)> pathways, (int x, int y) start, (int x, int y) goal)>
    {
        public Day20():base(20,2024)
        {
            
        }

        public override string SolvePart1((HashSet<(int x, int y)> pathways, (int x, int y) start, (int x, int y) goal) input)
        {
            var distances = AstarSolver(input.start, input.goal, input.pathways);

            return GetSavings(distances, 2).ToString();
        }

        public override string SolvePart2((HashSet<(int x, int y)> pathways, (int x, int y) start, (int x, int y) goal) input)
        {
            var distances = AstarSolver(input.start, input.goal, input.pathways);

            return GetSavings(distances, 20).ToString();
        }

        public static int GetSavings(Dictionary<(int x, int y), int> distances, int jumpSize)
        {
            int ret = 0;
            foreach (var p in distances.Keys)
            {
                foreach (var dx in Enumerable.Range(-jumpSize, jumpSize*2 + 1))
                {
                    foreach (var dy in Enumerable.Range(-jumpSize, jumpSize*2 + 1))
                    {
                        if (dx == 0 && dy == 0 || Math.Abs(dx) + Math.Abs(dy) > jumpSize) continue;

                        (int x, int y) np = (p.x + dx, p.y + dy);
                        if (distances.TryGetValue(np, out int value))
                        {
                            int initialCost = distances[p] - value;
                            int cheatCost = Math.Abs(p.x - np.x) + Math.Abs(p.y - np.y);
                            if ((initialCost - cheatCost) >= 100)
                            {
                                ret += 1;
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public Dictionary<(int x, int y), int> AstarSolver((int x, int y) start, (int x, int y) goal, HashSet<(int x, int y)> pathways)
        {
            Queue<(int x, int y)> openSet = new();
            Dictionary<(int x, int y), int> gScore = [];

            openSet.Enqueue(start);
            gScore[start] = 0;

            while (openSet.TryDequeue(out (int x, int y) cur))
            {
                foreach ((int x, int y) newPos in cur.Neighbors())
                {
                    if (!pathways.Contains(newPos)) continue;
                    var tentGScore = gScore[cur] + 1;
                    if (tentGScore < gScore.GetValueOrDefault(newPos, int.MaxValue))
                    {
                        gScore[newPos] = tentGScore;
                        openSet.Enqueue(newPos);
                    }
                }

            }

            return gScore;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############") == "0");
        }

        protected override (HashSet<(int x, int y)> pathways, (int x, int y) start, (int x, int y) goal) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            (int x, int y) start = default;
            (int x, int y) goal = default;
            HashSet<(int x, int y)> pathways = new();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case 'S':
                            pathways.Add((x, y));
                            start = (x, y);
                            break;
                        case 'E':
                            pathways.Add((x, y));
                            goal = (x, y);
                            break;
                        case '.':
                            pathways.Add((x, y));
                            break;
                        default:
                            break;
                    }
                }
            }

            return (pathways, start, goal);
        }
    }
}
