using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day12 : PuzzleWithObjectInput<(Dictionary<(int, int), Day12.Position> grid, (int,int) start, (int,int) end)>
    {
        public Day12() : base(12)
        {
        }

        public override string SolvePart1((Dictionary<(int, int), Position> grid, (int, int) start, (int, int) end) input)
        {
            Func<Position, int> Cost = x=> 1;
            var path = Astar<Position>(input.grid[(input.start.Item1, input.start.Item2)], input.grid[(input.end.Item1, input.end.Item2)],Cost);
            return (path.Count()-1).ToString();
        }

        public override string SolvePart2((Dictionary<(int, int), Position> grid, (int, int) start, (int, int) end) input)
        {
            var PossibleStarts = input.grid.Values.Where(x => x.Value == 0);
            Func<Position, int> Cost = x => 1;
            var min = PossibleStarts.Select(x => Astar<Position>(x, input.grid[(input.end.Item1, input.end.Item2)], Cost).Count()-1).Where(x=>x>0).Min();
            return min.ToString();
        }

        public List<T> Astar<T>(T start, T goal, Func<T,int> CostFunction) where T: Position
        {
            Queue<T> openSet = new();
            openSet.Enqueue(start);
            Dictionary<T, T> cameFrom = new();

            Dictionary<T, int> gScore = new();
            gScore[start] = 0;

            while (openSet.TryDequeue(out T cur))
            {
                if (cur.Equals(goal))
                {
                    return reconstruct_path(cameFrom, cur);
                }

                foreach (T item in cur.positionsAround)
                {
                    var cost = CostFunction(cur);
                    var tentGScore = gScore[cur] + cost;
                    if (tentGScore < gScore.GetValueOrDefault(item, int.MaxValue))
                    {
                        cameFrom[item] = cur;
                        gScore[item] = tentGScore;
                        openSet.Enqueue(item);
                    }
                }

            }

            return new List<T>();
        }

        public List<T> reconstruct_path<T>(Dictionary<T, T> cameFrom, T current)
        {
            List<T> totalPath = new() { current };
            while (cameFrom.TryGetValue(current, out current))
            {
                totalPath.Insert(0,current);
            }
            return totalPath;

        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi") == "31");

            Debug.Assert(SolvePart2(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi") == "29");
        }

        protected override (Dictionary<(int, int), Position> grid, (int, int) start, (int, int) end) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            Dictionary<(int, int), Position> grid = new Dictionary<(int, int), Position>();
            (int, int) start = (0,0);
            (int,int) end = (0, 0);

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid.Add((j, i),new Position(j, i, lines[i][j] - 'a'));
                    if (lines[i][j] == 'E')
                    {
                        grid[(j, i)] = new Position(j, i, 'z' - 'a');
                        end = (j, i);
                            }
                    if (lines[i][j] == 'S')
                    {
                        grid[(j, i)] = new Position(j, i, 'a' - 'a');
                        start = (j, i);
                    }
                }
            }

            List<Position> positions = grid.Values.ToList();

            positions.ForEach(x => x.FindPositionsAround(grid));

            return (grid,start,end);
        }

        public class Position
        {
            public override string ToString()
            {
                return $"({X},{Y}) {Value}";
            }
            public Position(int x, int y, int value)
            {
                X = x;
                Y = y;
                Value = value;
            }

            public int manhattan(Position other)
            {
                return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
            }

            public void FindPositionsAround(Dictionary<(int, int), Position> grid)
            {
                positionsAround = new List<Position>();
                if (grid.TryGetValue((X - 1, Y), out Position left) && left.Value-Value<=1) positionsAround.Add(left);
                if (grid.TryGetValue((X, Y - 1), out Position above) && above.Value - Value <= 1) positionsAround.Add(above);
                if (grid.TryGetValue((X + 1, Y), out Position right) && right.Value - Value <= 1) positionsAround.Add(right);
                if (grid.TryGetValue((X, Y + 1), out Position below) && below.Value - Value <= 1) positionsAround.Add(below);
            }

            public int X { get; private set; }
            public int Y { get; private set; }
            public int Value { get; private set; }

            public List<Position> positionsAround { get; set; }
        }
    }
}
