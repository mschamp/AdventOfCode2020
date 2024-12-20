
using Interfaces.Extensions;
using System.Linq;

namespace _2024
{
    public class Day18:PuzzleWithObjectArrayInput<(int,int)>
    {
        public Day18():base(18,2024)
        {
            
        }

        public override string SolvePart1((int, int)[] input)
        {
            (int, int) start = (0, 0);
            (int, int) goal = (input.Length < 1024) ? (6, 6):(70,70);
            int Corrupted = (input.Length < 1024) ? 12 :1024;

            var r = AstarSolver(start, goal, input.Take(Corrupted).ToHashSet(), out HashSet<(int, int)> _);

            return (r-1).ToString();
        }


        public int AstarSolver((int x, int y) start, (int x, int y) goal, HashSet<(int x, int y)> obstructions, out HashSet<(int x, int y)> Path)
        {
            Queue<(int x, int y)> openSet = new();
            Dictionary<(int,int), (int,int)>  cameFrom = [];
            Dictionary<(int x, int y), int> gScore = [];

            openSet.Enqueue(start);
            gScore[start] = 0;

            while (openSet.TryDequeue(out (int x, int y) cur))
            {
                if (cur.x == goal.x && cur.y == goal.y)
                {
                    Path = reconstruct_path(cameFrom, cur).ToHashSet();
                    return Path.Count();
                }

                foreach ((int x, int y) newPos in cur.Neighbors((0,0), goal))
                {
                    if (obstructions.Contains(newPos)) continue;
                    var tentGScore = gScore[cur] + 1;
                    if (tentGScore < gScore.GetValueOrDefault(newPos, int.MaxValue))
                    {
                        cameFrom[newPos] = cur;
                        gScore[newPos] = tentGScore;
                        openSet.Enqueue(newPos);
                    }
                }

            }
            Path = default;
            return 0;
        }

        private static List<T> reconstruct_path<T>(Dictionary<T, T> cameFrom, T current)
        {
            List<T> totalPath = [current];
            while (cameFrom.TryGetValue(current, out current))
            {
                totalPath.Insert(0, current);
            }
            return totalPath;

        }

        public override string SolvePart2((int, int)[] input)
        {
            (int, int) start = (0, 0);
            (int, int) goal = (input.Length < 1024) ? (6, 6) : (70, 70);
            int Corrupted = (input.Length < 1024) ? 12 : 1024;

            HashSet<(int, int)> LastPath = default;
            

            for (int i = Corrupted; i < input.Length; i++)
            {
                if (LastPath == default || LastPath.Contains(input[i-1]))
                {
                    var r = AstarSolver(start, goal, input.Take(i).ToHashSet(), out LastPath);
                    if (r == 0) return input[i-1].Item1 + "," + input[i-1].Item2;
                }
                
            }

            return "error";
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0") == "22");

            Debug.Assert(SolvePart2(@"5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0") == "6,1");
        }

        protected override (int, int) CastToObject(string RawData)
        {
            var t = RawData.Split(',').Select(int.Parse).ToArray();
            return (t[0], t[1]);
        }
    }
}
