using MoreLinq;

namespace _2024
{
    public class Day16:PuzzleWithObjectInput<(HashSet<(int x, int y)> pathways, (int x, int y, int o) start, (int x, int y) goal)>
    {
        public Day16() : base(16,2024)
        {
            
        }

        private IEnumerable<(int dx, int dy, int dlo, int cost)> GetPossibleMoves(int Orientatation)
        {
            yield return (0, 0, 1, 1000);
            yield return (0, 0, 3, 1000);
            switch (Orientatation)
            {
                case 0:
                    yield return (0, -1,0,1);
                    break;
                case 1:
                    yield return (1,0, 0, 1);
                    break;
                case 2:
                    yield return (0, 1, 0, 1);
                    break;
                case 3:
                    yield return (-1, 0, 0, 1);
                    break;
                default:
                    break;
            }
        }

        private IEnumerable<(int x, int y, int o, int cost)>PossiblePosition((int x, int y, int o) current, HashSet<(int x, int y)> pathways)
        {
            foreach (var item in GetPossibleMoves(current.o))
            {
                var t = (current.x + item.dx, current.y + item.dy, (current.o + item.dlo) % 4, item.cost);
                if (pathways.Contains((t.Item1, t.Item2))) yield return (t);
            }
        }

        public int AstarSolver((int x, int y, int o) start, (int x, int y) goal, HashSet<(int x, int y)> pathways, out Dictionary<(int x, int y, int o), List<(int x, int y, int o)>> cameFrom, out HashSet<(int x, int y, int o)> arrivals)  
        {
            Queue<(int x, int y, int o)> openSet = new();
            cameFrom = [];
            Dictionary<(int x, int y, int o), int> gScore = [];

            openSet.Enqueue(start);
            gScore[start] = 0;

            List<((int x, int y, int o) orient, int cost)> TempArrivals = new(); 

            while (openSet.TryDequeue(out (int x, int y, int o) cur))
            {
                if (cur.x==goal.x&&cur.y==goal.y)
                {
                    TempArrivals.Add((cur, gScore[cur]));
                }

                foreach ((int x, int y, int o, int cost) item in PossiblePosition(cur,pathways))
                {
                    (int x, int y, int o) newPos = (item.x, item.y, item.o);
                    var tentGScore = gScore[cur] + item.cost;
                    if (tentGScore < gScore.GetValueOrDefault(newPos, int.MaxValue))
                    {
                        cameFrom[newPos] = [cur];
                        gScore[newPos] = tentGScore;
                        openSet.Enqueue(newPos);
                    }
                    else if(tentGScore == gScore[newPos])
                    {
                        cameFrom[newPos].Add(cur);
                    }
                }

            }

            var min = TempArrivals.Min(x=>x.cost);
            arrivals = TempArrivals.Where(x => x.cost == min).Select(x => x.orient).ToHashSet();
            return min;
        }

        public override string SolvePart1((HashSet<(int x, int y)> pathways, (int x, int y, int o) start, (int x, int y) goal) input)
        {
            var t = AstarSolver(input.start,input.goal,input.pathways, out Dictionary<(int x, int y, int o), List<(int x, int y, int o)>> _, out HashSet < (int x, int y, int o) > _);

            return t.ToString();
        }

        public override string SolvePart2((HashSet<(int x, int y)> pathways, (int x, int y, int o) start, (int x, int y) goal) input)
        {
            var t = AstarSolver(input.start, input.goal, input.pathways, out Dictionary<(int x, int y, int o), List<(int x, int y, int o)>> paths, out HashSet<(int x, int y, int o)> arrivals);


            return GetGoodPositions(paths, arrivals, (input.start.x,input.start.y)).ToString();
        }

        private int GetGoodPositions(Dictionary<(int x, int y, int o), List<(int x, int y, int o)>> cameFrom, HashSet<(int x, int y, int o)> arrivals, (int x, int y) start)
        {
            HashSet<(int x, int y)> GoodPositions = [start];

            Queue<(int x, int y, int o)> ToAnalyse = new();
            arrivals.ForEach(x=> ToAnalyse.Enqueue(x));

            while (ToAnalyse.TryDequeue(out (int x, int y, int o) current))
            {
                if (!cameFrom.TryGetValue(current, out List<(int x, int y, int o)> previous)) continue;

                GoodPositions.Add((current.x, current.y));
                previous.ForEach(x => ToAnalyse.Enqueue(x));
            }

            return GoodPositions.Count();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############") == "7036");
            Debug.Assert(SolvePart1(@"#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################") == "11048");

            Debug.Assert(SolvePart2(@"###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############") == "45");
            Debug.Assert(SolvePart2(@"#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################") == "64");
        }

        protected override (HashSet<(int x, int y)> pathways, (int x, int y, int o) start, (int x, int y) goal) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            (int x, int y, int o) start=default;
            (int x, int y) goal=default;
            HashSet<(int x, int y)> pathways=new();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case 'S':
                            pathways.Add((x, y));
                            start = (x, y, 1);
                            break;
                        case 'E':
                            pathways.Add((x, y));
                            goal = (x, y);
                            break;
                        case '.':
                            pathways.Add((x,y));
                            break;
                        default:
                            break;
                    }
                }
            }

            return (pathways,start,goal);

        }
    }
}
