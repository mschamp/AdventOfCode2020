using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
	public class Day14 : PuzzleWithObjectInput<Dictionary<(int, int), char>>
	{
		public Day14():base(14,2023)
		{

		}

		public override string SolvePart1(Dictionary<(int, int), char> input)
		{
			(int limitXMin,int limitXMax) = (0, input.Max(x => x.Key.Item1));
			(int limitYMin,int limitYMax) = (0, input.Max(x => x.Key.Item2));

			Move(input,(0,-1),(limitXMin,limitXMax),(limitYMin,limitYMax));

			return $"{GetScore(input,limitYMax)}";
		}

		private void Move(Dictionary<(int x, int y), char> grid, (int dx, int dy) direction, (int limitXMin, int limitXMax)LimitX, (int limitYMin, int limitYMax)LimitY)
		{
			bool Moved = false;
			do
			{
				Moved = false;
				List<KeyValuePair<(int x, int y), char>> stones = grid.Where(x=>x.Value=='O').ToList();
				foreach (var stone in stones)
				{
					if (stone.Key.x + direction.dx < LimitX.limitXMin || (stone.Key.x + direction.dx > LimitX.limitXMax)) continue;
					if (stone.Key.y + direction.dy < LimitY.limitYMin || (stone.Key.y + direction.dy > LimitY.limitYMax)) continue;
					if (!grid.ContainsKey((stone.Key.x + direction.dx,stone.Key.y + direction.dy)))
					{
						grid.Remove(stone.Key);
						grid[(stone.Key.x + direction.dx, stone.Key.y + direction.dy)] = stone.Value;
						Moved = true;
					}
				}
				

			} while (Moved);
		}

		public override string SolvePart2(Dictionary<(int, int), char> input)
		{
			(int limitXMin, int limitXMax) = (0, input.Max(x => x.Key.Item1));
			(int limitYMin, int limitYMax) = (0, input.Max(x => x.Key.Item2));

			int loop = 0;
			Dictionary <string,int> cache = new();
			bool loopDetected = false;

            for (int i = 0; i < 1000000000; i++)
            {

				string newValues = getID(input.Keys.Select(x => x.Item1*1000+x.Item2).Order());
				if (!loopDetected&cache.ContainsKey(newValues))
				{
					loopDetected = true;
					int startloop = cache[newValues];
					int loopSize = i - startloop;

					i = 1000000000 - ((1000000000 - startloop) % loopSize);
				}
					cache[newValues] = i;

					Move(input, (0, -1), (limitXMin, limitXMax), (limitYMin, limitYMax));
					Move(input, (-1, 0), (limitXMin, limitXMax), (limitYMin, limitYMax));
					Move(input, (0, 1), (limitXMin, limitXMax), (limitYMin, limitYMax));
					Move(input, (1, 0), (limitXMin, limitXMax), (limitYMin, limitYMax));
			}			

			return $"{GetScore(input, limitYMax)}";
		}

		private int GetScore(Dictionary<(int x, int y), char> grid, int MaxY)
		{

			return grid.Where(x => x.Value == 'O').GroupBy(x => x.Key.y).Sum(x => (MaxY - x.Key + 1) * x.Count());
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....") == "136");

			Debug.Assert(SolvePart2(@"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....") == "64");
		}

		protected override Dictionary<(int, int), char> CastToObject(string RawData)
		{
			Dictionary<(int, int), char> result = new Dictionary<(int, int), char>();
			string[] lines = RawData.Split(Environment.NewLine);
            for (int y = 0; y < lines.Length; y++)
            {
				for (int x = 0; x < lines.Length; x++)
				{
					if (lines[y][x]=='O'|| lines[y][x] == '#') result[(x,y)] = lines[y][x];
				}
			}
			return result;
        }

		public string getID(IEnumerable<int> arr)
		{
			return string.Join(",", arr);
		}
	}
}
