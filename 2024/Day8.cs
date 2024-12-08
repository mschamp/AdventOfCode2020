using General;
using Interfaces.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day8: PuzzleWithObjectInput<(Dictionary<char,List<(int x,int y)>> grid,(int x, int y) size)>
    {
        public Day8():base(8,2024)
        {
            
        }

        public override string SolvePart1((Dictionary<char, List<(int x, int y)>> grid, (int x, int y) size) input)
        {
            HashSet<(int x, int y)> positions = new();
            foreach (var item in input.grid)
            {
                var possibleComb = item.Value.DifferentCombinations(2);
                foreach (var combination in possibleComb)
                {
                    var t = combination.ToArray();
                    positions.Add((2 * t[0].x - t[1].x, 2 * t[0].y - t[1].y));
                    positions.Add((2 * t[1].x - t[0].x, 2 * t[1].y - t[0].y));
                }
            }

            return positions.Count(x=> x.x>=0 && x.y>=0 && x.x<input.size.x && x.y < input.size.y).ToString();
        }

       

        public override string SolvePart2((Dictionary<char, List<(int x, int y)>> grid, (int x, int y) size) input)
        {
            HashSet<(int x, int y)> positions = new();
            foreach (var item in input.grid)
            {
                var possibleComb = item.Value.DifferentCombinations(2);
                foreach (var combination in possibleComb)
                {
                    var t = combination.ToArray();
                    int dx = t[0].x - t[1].x;
                    int dy = t[0].y - t[1].y;
                    int[] dirs = [-1, 1];

                    foreach (var dir in dirs)
                    {
                        int i = 0;
                        while (true)
                        {
                            (int x, int y) pos = (t[0].x + i * dir * dx, t[0].y + i * dir * dy);
                            if (pos.x < 0 || pos.y < 0 || pos.x >= input.size.x || pos.y >= input.size.y) break;
                            positions.Add(pos);
                            i++;
                        }
                    }
                }
            }

            return positions.Count().ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............") == "14");
            Debug.Assert(SolvePart2(@"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............") == "34");
        }

        protected override (Dictionary<char, List<(int x, int y)>> grid, (int x, int y) size) CastToObject(string RawData)
        {
            Dictionary<char, List<(int x, int y)>> grid = new();

            string[] lines = RawData.Split(Environment.NewLine);

            (int x, int y) size = (lines[0].Length, lines.Length);

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '.':
                            continue;
                        default:
                            if (!grid.TryGetValue(lines[y][x], out List<(int x, int y)> antennas))
                            { 
                                antennas = new();
                                grid[lines[y][x]]= antennas;
                            }
                            antennas.Add((x, y));
                            break;
                    }
                }
            }
            return (grid,size);
        }
    }
}
