using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day15 : General.PuzzleWithObjectInput<Dictionary<(int, int), Day15.Position>>
    {
        public Day15() : base(15) { }
        public override string SolvePart1(Dictionary<(int, int), Position> input)
        {
            List<Position> positions = input.Values.ToList();

            positions.ForEach(x => x.FindPositionsAround(input));
            Position goal = positions.Where(x => x.X == positions.Max(y => y.X) && x.Y == positions.Max(y => y.Y)).First();
            Position start = positions.Where(x => x.X == 0 && x.Y == 0).First();

            Func<Position, int> CostFunction = s => s.Value;

            List<Position> Path = Astar(start, goal, CostFunction);

            return (Path.Sum(x => x.Value) - start.Value).ToString();
        }

        public override string SolvePart2(Dictionary<(int, int), Position> input)
        {
            List<Position> positions = new();
            
            int MaxX = input.Values.Select(x => x.X).Distinct().Count();
            int MaxY = input.Values.Select(x => x.Y).Distinct().Count();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    positions.AddRange(Scale(i, j, MaxX, MaxY, input));
                }
            }

            Dictionary<(int,int),Position> positionsd = positions.ToDictionary(x => (x.X,x.Y), x => x);
            positions.ForEach(x => x.FindPositionsAround(positionsd));


            Position goal = positions.Where(x => x.X == 5*MaxX-1 && x.Y == 5 * MaxY - 1).First();
            Position start = positions.Where(x => x.X == 0 && x.Y == 0).First();

            Func<Position, int> CostFunction = s => s.Value;

            List<Position> Path = Astar(start, goal, CostFunction);

            return (Path.Sum(x => x.Value) - start.Value).ToString();
        }

        public List<Position> Scale(int dx, int dy, int MaxX, int MaxY, Dictionary<(int, int), Position> input)
        {
            return input.Values.Select(x => new Position(x.X + dx * MaxX, x.Y + dy * MaxY, x.Value + dx + dy)).ToList();
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
                    var tentGScore = gScore[cur] + CostFunction(item);
                    if (tentGScore < gScore.GetValueOrDefault(item, int.MaxValue))
                    {
                        cameFrom[item] = cur;
                        gScore[item] = tentGScore;
                        openSet.Enqueue(item);
                    }
                }

            }

            return null;
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

        public class Position
        {
            public override string ToString()
            {
                return $"({X},{Y})";
            }
            public Position(int x, int y, int value)
            {
                X = x;
                Y = y;
                Value = value;
                while (Value>9)
                {
                    Value -= 9;
                }
                

            }

            public int manhattan(Position other)
            {
                return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
            }

            public void FindPositionsAround(Dictionary<(int, int), Position> grid)
            {
                positionsAround = new List<Position>();
                if (grid.TryGetValue((X - 1, Y), out Position left)) positionsAround.Add(left);
                if (grid.TryGetValue((X, Y - 1), out Position above)) positionsAround.Add(above);
                if (grid.TryGetValue((X + 1, Y), out Position right)) positionsAround.Add(right);
                if (grid.TryGetValue((X, Y + 1), out Position below)) positionsAround.Add(below);
            }

            public int X { get; private set; }
            public int Y { get; private set; }
            public int Value { get; private set; }

            public List<Position> positionsAround { get; set; }
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581") == "40");

            Debug.Assert(SolvePart2(@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581") == "315");
        }

        public override Dictionary<(int, int), Position> CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            Dictionary<(int, int), Position> grid = new Dictionary<(int, int), Position>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[(j, i)] = new Position(j, i, lines[i][j] - '0');
                }
            }
            return grid;
        }
    }
}
