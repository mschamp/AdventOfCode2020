namespace _2023
{
	public class Day18 : PuzzleWithStringArrayInput
	{
		Dictionary<string, string> directions = new Dictionary<string, string> { { "0", "R" }, { "1", "D" }, { "2", "L" }, { "3", "U" } };
		Dictionary<string, (long,long)> motion = new Dictionary<string, (long,long)> { { "R", (0,1) }, { "L", (0,-1)  }, { "U", (-1,0) }, { "D", (1,0) } };
        public Day18():base(18,2023)
        {
			

        }
        public override string SolvePart1(string[] input)
		{
			(long x, long y) = (0, 0);
			List<(long x, long Y)> borders = new List<(long x, long Y)> { (x, y) };
			foreach (var item in input)
			{
				string[] parts= item.Split(" ");
				(long dx, long dy) = motion[parts[0]];
				y= y+long.Parse(parts[1])*dy;
				x= x + long.Parse(parts[1]) * dx;
				borders.Add((x, y));
			}
			return $"{calculateInside(borders)}";
		}

		private long calculateInside(List<(long x, long y)> borders)
		{
			long area = Math.Abs(Shoelace(borders));
			long perimeter = borders.Zip(borders.Skip(1)).Sum(x => Math.Abs(x.First.x - x.Second.x) + Math.Abs(x.First.y - x.Second.y));
			return area / 2 + perimeter / 2 + 1;
		}

		private long Shoelace(List<(long x, long y)>borders)
		{
			long area = borders.Zip(borders.Skip(1)).Select(x=>x.First.x*x.Second.y-x.Second.x*x.First.y).Sum();
			return area;
		}

		public override string SolvePart2(string[] input)
		{
			(long x, long y) = (0, 0);
			List<(long x, long Y)> borders = new List<(long x, long Y)> { (x, y) };
			foreach (var item in input)
			{
				string color = item.Split(" ")[2];
				long distance = long.Parse(color[2..^2], System.Globalization.NumberStyles.HexNumber);
				(long dx, long dy) = motion[directions[new string(color[^2..^1])]];
				y = y + distance * dy;
				x = x + distance * dx;
				borders.Add((x, y));
			}
			return $"{calculateInside(borders)}";
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)") == "62");

			Debug.Assert(SolvePart2(@"R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)") == "952408144115");
		}
	}
}
