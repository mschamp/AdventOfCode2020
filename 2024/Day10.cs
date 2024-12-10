using Interfaces.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day10:PuzzleWithObjectInput<Dictionary<(int, int), int>>
    {
        public Day10():base(10,2024)
        {
            
        }

        public override string SolvePart1(Dictionary<(int, int), int> input)
        {
            List<(int,int)> startpoints = input.Where(x=> x.Value == 0).Select(x=>x.Key).ToList();

            return startpoints.Sum(x=>ReachablePeaks(input,x,false)).ToString();
        }

        private int ReachablePeaks(Dictionary<(int, int), int> grid, (int,int) startpoint,bool CountPaths)
        {
            HashSet<(int, int)> seen = new();
            List<(int, int)> Peaks = new();
            Queue<(int, int)> ToDo = new Queue<(int, int)>();
            ToDo.Enqueue(startpoint);

            while (ToDo.TryDequeue(out var toDo))
            {
                foreach (var option in grid.Neighbors(toDo))
                {
                    if (grid[option] == grid[toDo] + 1)
                    {
                        if (CountPaths || seen.Add(option))
                        {
                            if (grid[option] == 9)
                            {
                                Peaks.Add(option);
                            }
                            else
                            {
                                ToDo.Enqueue(option);
                            }
                        }
                    }
                }
            }
            return Peaks.Count();
        }

        public override string SolvePart2(Dictionary<(int, int), int> input)
        {
            List<(int, int)> startpoints = input.Where(x => x.Value == 0).Select(x => x.Key).ToList();

            return startpoints.Sum(x => ReachablePeaks(input, x,true)).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"...0...
...1...
...2...
6543456
7.....7
8.....8
9.....9") == "2"); 
            Debug.Assert(SolvePart1(@"..90..9
...1.98
...2..7
6543456
765.987
876....
987....") == "4");
            Debug.Assert(SolvePart1(@"10..9..
2...8..
3...7..
4567654
...8..3
...9..2
.....01") == "3");
            Debug.Assert(SolvePart1(@"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732") == "36");
            Debug.Assert(SolvePart2(@".....0.
..4321.
..5..2.
..6543.
..7..4.
..8765.
..9....") == "3");
            Debug.Assert(SolvePart2(@"..90..9
...1.98
...2..7
6543456
765.987
876....
987....") == "13");
            Debug.Assert(SolvePart2(@"012345
123456
234567
345678
4.6789
56789.") == "227");
            Debug.Assert(SolvePart2(@"89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732") == "81");
        }

        protected override Dictionary<(int, int), int> CastToObject(string RawData)
        {
            Dictionary<(int,int),int> grid = new Dictionary<(int, int), int>();
            string[] lines = RawData.Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                for (int c = 0; c < lines[i].Length; c++)
                {
                    grid[(i, c)] = lines[i][c]-'0';
                }
            }

            return grid;
        }
    }
}
