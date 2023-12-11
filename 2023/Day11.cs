using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
	public class Day11 : PuzzleWithObjectInput<(List<(int X, int Y)> points, List<int> emptyRows, List<int> emptyColoms)>
	{
		public Day11() : base(11, 2023)
		{
		}

		public override string SolvePart1((List<(int X, int Y)> points, List<int> emptyRows, List<int> emptyColoms) input)
		{
			ApplyScale(2, input.points, input.emptyRows, input.emptyColoms);
			return $"{DistanceAllPairs(input.points)}";
		}

		public override string SolvePart2((List<(int X, int Y)> points, List<int> emptyRows, List<int> emptyColoms) input)
		{
			ApplyScale(1000000, input.points, input.emptyRows, input.emptyColoms);
			return $"{DistanceAllPairs(input.points)}";
		}

		private void ApplyScale(int scale, List<(int X, int Y)> points, List<int> emptyRows, List<int> emptyColoms)
		{
            for (int i = 0; i < points.Count; i++)
            {
				var point = points[i];
				point.Y += (scale - 1) * emptyRows.Count(row => point.Y > row);
				point.X += (scale - 1) * emptyColoms.Count(col => point.X > col);
				points[i] = point;

			}
        }

		private int DistanceGalaxyPair((int X,int Y)g1, (int X, int Y) g2)
		{
			return Math.Abs(g1.X - g2.X)+Math.Abs(g1.Y-g2.Y);
		}

		private long DistanceAllPairs(List<(int X, int Y)> points)
		{
			long sum = 0;
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i; j < points.Count; j++)
                {
					sum += DistanceGalaxyPair(points[i], points[j]);
                }
            }
			return sum;
        }

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....") == "374");
		}

		protected override (List<(int X, int Y)> points, List<int> emptyRows, List<int> emptyColoms) CastToObject(string RawData)
		{
			List<(int X, int Y)> points = new();
			
			string[] lines = RawData.Split(Environment.NewLine);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
					if (lines[y][x] == '#') points.Add((x, y));
                }
            }

			List<int> emptyRows = new List<int>(Enumerable.Range(0, lines.Length).Except(points.Select(p => p.Y)));
			List<int> emptyColoms = new List<int>(Enumerable.Range(0, lines[0].Length).Except(points.Select(p => p.X)));

            return (points, emptyRows, emptyColoms);
		}
	}
}
