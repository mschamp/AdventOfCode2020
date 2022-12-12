using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
     public class Day15 : General.PuzzleWithObjectInput<Dictionary<(int, int), General.Astar.Position>>
    {
        public Day15() : base(15) { }
        public override string SolvePart1(Dictionary<(int, int), General.Astar.Position> input)
        {
            List<General.Astar.Position> positions = input.Values.ToList();

            Func<General.Astar.Position, General.Astar.Position, bool> filterFunct = (me, other) => true;
            positions.ForEach(x => x.FindPositionsAround(input, filterFunct));

            General.Astar.Position goal = positions.Where(x => x.X == positions.Max(y => y.X) && x.Y == positions.Max(y => y.Y)).First();
            General.Astar.Position start = positions.Where(x => x.X == 0 && x.Y == 0).First();

            Func<General.Astar.Position, int> CostFunction = s => s.Value;

            List<General.Astar.Position> Path = General.Astar.AstarSolver < General.Astar.Position>(start, goal, CostFunction);

            return (Path.Sum(x => x.Value) - start.Value).ToString();
        }

        public override string SolvePart2(Dictionary<(int, int), General.Astar.Position> input)
        {
            List<General.Astar.Position> positions = new();
            
            int MaxX = input.Values.Select(x => x.X).Distinct().Count();
            int MaxY = input.Values.Select(x => x.Y).Distinct().Count();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    positions.AddRange(Scale(i, j, MaxX, MaxY, input));
                }
            }

            Dictionary<(int,int),General.Astar.Position> positionsd = positions.ToDictionary(x => (x.X,x.Y), x => x);
            Func<General.Astar.Position, General.Astar.Position, bool> filterFunct = (me, other) => true;
            positions.ForEach(x => x.FindPositionsAround(positionsd, filterFunct));


            General.Astar.Position goal = positions.Where(x => x.X == 5*MaxX-1 && x.Y == 5 * MaxY - 1).First();
            General.Astar.Position start = positions.Where(x => x.X == 0 && x.Y == 0).First();

            Func<General.Astar.Position, int> CostFunction = s => s.Value;

            List<General.Astar.Position> Path = General.Astar.AstarSolver(start, goal, CostFunction);

            return (Path.Sum(x => x.Value) - start.Value).ToString();
        }

        public List<General.Astar.Position> Scale(int dx, int dy, int MaxX, int MaxY, Dictionary<(int, int), General.Astar.Position> input)
        {
            Func<int, int> Reduce = Value =>
            {
                while (Value > 9)
                {
                    Value -= 9;
                }
                return Value;
            };
            return input.Values.Select(x => new General.Astar.Position(x.X + dx * MaxX, x.Y + dy * MaxY, Reduce(x.Value + dx + dy))).ToList();
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

        protected override Dictionary<(int, int), General.Astar.Position> CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            Dictionary<(int, int), General.Astar.Position> grid = new Dictionary<(int, int), General.Astar.Position>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    int Value = lines[i][j] - '0';
                    grid[(j, i)] = new General.Astar.Position(j, i, Value);
                }
            }
            return grid;
        }
    }
}
