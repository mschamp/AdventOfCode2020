﻿using General;
using System.Diagnostics;

namespace _2022
{
	public class Day12 : PuzzleWithObjectInput<(Dictionary<(int, int), Astar.Position> grid, (int,int) start, (int,int) end)>
    {
        public Day12() : base(12,2022)
        {
        }

        public override string SolvePart1((Dictionary<(int, int), Astar.Position> grid, (int, int) start, (int, int) end) input)
        {
            Func<Astar.Position, int> Cost = x=> 1;
            var path = Astar.AstarSolver(input.grid[(input.start.Item1, input.start.Item2)], input.grid[(input.end.Item1, input.end.Item2)],Cost);
            return (path.Count-1).ToString();
        }

        public override string SolvePart2((Dictionary<(int, int), Astar.Position> grid, (int, int) start, (int, int) end) input)
        {
            var PossibleStarts = input.grid.Values.Where(x => x.Value == 0);
            Func<Astar.Position, int> Cost = x => 1;
            var min = Astar.AstarSolver(PossibleStarts, input.grid[(input.end.Item1, input.end.Item2)], Cost).Count - 1;
            return min.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"Sabqponm
abcryxxl
accszExk}
acctuvwj
abdefghi") == "31");

            Debug.Assert(SolvePart2(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi") == "29");
        }

        protected override (Dictionary<(int, int), Astar.Position> grid, (int, int) start, (int, int) end) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            Dictionary<(int, int), Astar.Position> grid = [];
            (int, int) start = (0,0);
            (int,int) end = (0, 0);

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid.Add((j, i), new Astar.Position(j, i, lines[i][j] - 'a'));
                    if (lines[i][j] == 'E')
                    {
                        grid[(j, i)] = new Astar.Position(j, i, 'z' - 'a');
                        end = (j, i);
                            }
                    if (lines[i][j] == 'S')
                    {
                        grid[(j, i)] = new Astar.Position(j, i, 'a' - 'a');
                        start = (j, i);
                    }
                }
            }

            List<Astar.Position> positions = grid.Values.ToList();

            Func<Astar.Position, Astar.Position, bool> filterFunct = (me,other)=>other.Value-me.Value<=1;
            positions.ForEach(x => x.FindPositionsAround(grid,filterFunct));

            return (grid,start,end);
        }
    }
}
