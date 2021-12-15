using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day15 : General.PuzzleWithObjectInput<List<Day15.Position>>
    {
        public Day15() : base(15) { }
        public override string SolvePart1(List<Day15.Position> input)
        {
            Position start = input.Where(x => x.X == input.Max(y => y.X) && x.Y == input.Max(y => y.Y)).First();
            Position goal = input.Where(x => x.X == 0 && x.Y == 0).First();

            Func<Position, int> CostFunction = s => s.Value;

            List<Position> Path = Astar(start, goal, CostFunction);

            return (Path.Sum(x => x.Value) - start.Value).ToString();
        }

        public override string SolvePart2(List<Day15.Position> input)
        {
            return "";
        }

        public List<Position> Astar(Position start, Position goal, Func<Position,int> CostFunction)
        {
            List<Position> openSet = new() { start};
            HashSet<Position> Seen = new();
            Dictionary<Position, Position> cameFrom = new();

            Dictionary<Position, int> gScore = new();
            gScore[start] = 0;

            Dictionary<Position, int> fScore = new();
            fScore[start] = 0;

            while (openSet.Any())
            {
                Position cur = openSet[0];
                openSet.Remove(cur);
                Seen.Add(cur);

                if (cur.Equals(goal))
                {
                    return reconstruct_path(cameFrom, cur);
                }

                foreach (Position item in cur.positionsAround)
                {
                    var tentGScore = gScore[cur] + CostFunction(item);
                    if (tentGScore < gScore.GetValueOrDefault(item, int.MaxValue))
                    {
                        cameFrom[item] = cur;
                        gScore[item] = tentGScore;
                        fScore[item] = tentGScore+ item.manhattan(start);
                        openSet.Add(item);
                        openSet = openSet.Where(x => !Seen.Contains(x)).OrderBy(x => fScore[x]).ToList();
                    }
                }

            }

            return null;
        }

        public List<Position> reconstruct_path(Dictionary<Position, Position> cameFrom, Position current)
        {
            List<Position> totalPath = new() { current };
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
            public bool isMin
            {
                get
                {
                    return positionsAround.All(x => x.Value > Value);
                }
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
                if (grid.TryGetValue((X - 1, Y), out Position left)) positionsAround.Add(left);
                if (grid.TryGetValue((X, Y - 1), out Position above)) positionsAround.Add(above);
                if (grid.TryGetValue((X + 1, Y), out Position right)) positionsAround.Add(right);
                if (grid.TryGetValue((X, Y + 1), out Position below)) positionsAround.Add(below);
            }

            public bool AssignedToBasin = false;

            public int BasinSize()
            {
                int Size = 1;


                return Size;
            }

            public int WalkThroughBasin()
            {
                if (AssignedToBasin) return 0;
                if (Value == 9) return 0;

                AssignedToBasin = true;

                var positionsNotInBasin = positionsAround.Where(p => p.AssignedToBasin == false);
                return 1 + positionsNotInBasin.Sum(p => p.WalkThroughBasin());
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

            Debug.Assert(SolvePart1(@"1911191111
1119111991
9999999111
9999911199
9999119999
9999199999
9111199999
9199999111
9111911191
9991119991") == "40");

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

        public override List<Position> CastToObject(string RawData)
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

            List<Position> positions = grid.Values.ToList();
            positions.ForEach(x => x.FindPositionsAround(grid));

            return positions;
        }
    }
}
