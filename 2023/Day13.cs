using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _2023
{
	public class Day13 : PuzzleWithObjectArrayInput<Day13.Grid>
	{
		public Day13() : base(13, 2023)
		{

		}

		private (int y, int x) GetMirror(Grid grid, int difference)
		{

			List<int> verts = new List<int>();
			for (int i = 1; i < grid.Width; i++)
			{
				List<string> left = new List<string>();
				List<string> right = new List<string>();
				for (int j = i - 1; j >= 0; j--)
				{
					left.Add(grid.getCol(j));
				}
				for (int j = i; j < grid.Width; j++)
				{
					right.Add(grid.getCol(j));
				}
				int n = Math.Min(left.Count, right.Count);
				left = left.GetRange(0, n);
				right = right.GetRange(0, n);
				if (GetDifference(left.Zip(right)) == difference)
				{
					verts.Add(i);
					break;
				}
			}

				List<int> horizontals = new List<int>();
				for (int i = 1; i < grid.Height; i++)
				{
					List<string> top = new List<string>();
					List<string> bottom = new List<string>();
					for (int j = i - 1; j >= 0; j--)
					{
						top.Add(grid.getRow(j));
					}
					for (int j = i; j < grid.Height; j++)
					{
						bottom.Add(grid.getRow(j));
					}
					int n = Math.Min(top.Count, bottom.Count);
					top = top.GetRange(0, n);
					bottom = bottom.GetRange(0, n);
					if (GetDifference(top.Zip(bottom)) == difference)
					{
						horizontals.Add(i);
						break;
					}


					
				}
				return (horizontals.FirstOrDefault(), verts.FirstOrDefault());
			
		}

		private int GetDifference(IEnumerable<(string First, string Second)> list)
		{
			int count = 0;
            foreach (var pair in list)
            {
                for (int i = 0; i < pair.First.Length; i++)
                {
					if (pair.First[i] != pair.Second[i]) count++;
                }
            }
			return count;
        }

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#") == "405");

			Debug.Assert(SolvePart2(@"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#") == "400");
		}

		protected override Grid CastToObject(string RawData)
		{
			return new Grid(RawData);
		}

		public override string SolvePart1(Grid[] input)
		{
			return $"{input.Select(x=>GetMirror(x,0)).Sum(m=>m.x+m.y*100)}";
		}

		public override string SolvePart2(Grid[] input)
		{
			return $"{input.Select(x=> GetMirror(x,1)).Sum(m => m.x + m.y * 100)}";
		}

		private void FixSmudge(Grid x)
		{
			
		}

		public class Grid
		{
			public Grid(string gridData)
			{
				Rows = gridData.Split(Environment.NewLine);
			}
			public int Width => Rows[0].Length;
			public int Height => Rows.Length;

			public string[] Rows { get; set; }
			public string getRow(int i)
			{ return Rows[i]; }

			public string getCol(int i)
			{
				char[] col = new char[Height];
                for (int j = 0; j < Height; j++)
				{
					col[j] = Rows[j][i];
                }
				return new string(col);
            }
		}
	}
}
