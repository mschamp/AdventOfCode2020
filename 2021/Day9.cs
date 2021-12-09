using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _2021
{
    public class Day9 : General.PuzzleWithObjectInput<List<Day9.Position>>
    {
        public override List<Day9.Position> CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            Dictionary<(int, int), Position> grid = new Dictionary<(int, int), Position>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[(j, i)] = new Position(j,i, lines[i][j]-'0');
                }
            }

            List<Position> positions = grid.Values.ToList();
            positions.ForEach(x => x.FindPositionsAround(grid));

            return positions;
        }

        public override string SolvePart1(List<Day9.Position> input)
        {
            IEnumerable<Position> min = input.Where(x => x.isMin);
            return (min.Sum(x => x.Value) + min.Count()).ToString();
        }

        public override string SolvePart2(List<Day9.Position> input)
        {
            IEnumerable<Position> min = input.Where(x => x.isMin);
            IEnumerable<int> BasinSize = min.Select(x => x.WalkThroughBasin());

            long Result = 1;
            foreach (int size in BasinSize.OrderByDescending(x => x).Take(3))
            {
                Result *= size;
            }
            return Result.ToString();
        }

        public class Position
        {
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

            public void FindPositionsAround(Dictionary<(int,int),Position> grid)
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
            Debug.Assert(SolvePart1(@"2199943210
3987894921
9856789892
8767896789
9899965678") == "15");

            Debug.Assert(SolvePart2(@"2199943210
3987894921
9856789892
8767896789
9899965678") == "1134");
        }
    }
}
