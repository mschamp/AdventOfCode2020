using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
	public class Day10 : PuzzleWithObjectInput<(clsPoint start, Dictionary<clsPoint, clsPoint[]> path)>
	{
		public Day10() : base(10, 2023)
		{
		}

		public override string SolvePart1((clsPoint start, Dictionary<clsPoint, clsPoint[]> path) input)
		{
			return $"{Math.Ceiling(getLoop(input.start,input.path).Count/2.0)}";
		}

		private List<clsPoint> getLoop(clsPoint start, Dictionary<clsPoint, clsPoint[]> path)
		{
			clsPoint Current = start;
			HashSet<clsPoint> Checked = new HashSet<clsPoint>();
			List<clsPoint> Loop = new List<clsPoint>();

			while(!Checked.Contains(Current))
			{
				Loop.Add(Current);
				Checked.Add(Current);
                foreach (clsPoint option in path[Current])
                {
                    if (!Checked.Contains(option) && path.TryGetValue(option, out clsPoint[] optionsLinks) && optionsLinks.Contains(Current))
					{
						Current = option;
					}
                }
            }
			return Loop;
		}

		private int FillArea(List<clsPoint> Loop, Queue<clsPoint> ToCheck, double MaxX, double MaxY)
		{
			HashSet<clsPoint> Checked = new HashSet<clsPoint>(Loop);
			while (ToCheck.TryDequeue(out clsPoint point))
			{
				if (point.X < 0 || point.Y < 0) continue;
				if (point.X > MaxX || point.Y > MaxY) continue;
				if (Checked.Contains(point)) continue;

				Checked.Add(point);
                foreach (var (X, Y) in new (int X, int Y)[] { (0, 1), (0, -1), (-1, 0), (1, 0) })
                {
					ToCheck.Enqueue(new clsPoint(point.X + X, point.Y + Y));
                }
            }

			return Checked.Count - Loop.Count;
		}
		public override string SolvePart2((clsPoint start, Dictionary<clsPoint, clsPoint[]> path) input)
		{
			List<clsPoint> Loop = getLoop(input.start, input.path);
			HashSet<clsPoint> HashLoop = new HashSet<clsPoint>(Loop);
			HashSet<clsPoint> left = new HashSet<clsPoint>();
			HashSet<clsPoint> right = new HashSet<clsPoint>();

            for (int i = 0; i < Loop.Count; i++)
            {
				var points = new clsPoint[] { Loop[i], Loop[(i + 1) % Loop.Count] };
				double dx = points[1].X - points[0].X;
				double dy = points[1].Y - points[0].Y;

                foreach (var point in points)
                {
					clsPoint pl = new clsPoint(point.X + dy, point.Y -dx);
					clsPoint pr = new clsPoint(point.X -dy, point.Y + dx);
					if (!HashLoop.Contains(pr)) right.Add(pr);
					if (!HashLoop.Contains(pl)) left.Add(pl);
                }

				
            }
			double MaxX = Loop.Max(x => x.X);
			double MaxY = Loop.Max(x => x.Y);

			int Looped = Math.Min(
				FillArea(Loop, new Queue<clsPoint>(left), MaxX, MaxY),
				FillArea(Loop, new Queue<clsPoint>(right), MaxX, MaxY));
            return $"{Looped}";
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@".....
.S-7.
.|.|.
.L-J.
.....") == "4");
			Debug.Assert(SolvePart1(@"..F7.
.FJ|.
SJ.L7
|F--J
LJ...") == "8");

		Debug.Assert(SolvePart2(@".....
.S-7.
.|.|.
.L-J.
.....") == "1");
			Debug.Assert(SolvePart2(@"...........
.S-------7.
.|F-----7|.
.||.....||.
.||.....||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........") == "4");

			Debug.Assert(SolvePart2(@".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...") == "8");

			Debug.Assert(SolvePart2(@"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJ7F7FJ-
L---JF-JLJ.||-FJLJJ7
|F|F-JF---7F7-L7L|7|
|FFJF7L7F-JF7|JL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L") == "10");
		}

	protected override (clsPoint start, Dictionary<clsPoint, clsPoint[]> path) CastToObject(string RawData)
		{
			clsPoint Start = null;
			Dictionary<clsPoint, clsPoint[]> path = new Dictionary<clsPoint, clsPoint[]>(); ;
			string[] lines =RawData.Split(Environment.NewLine);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
					switch (lines[y][x])
					{
						case 'S':
							Start = new clsPoint(x, y);
							path[new clsPoint(x, y)] = [new clsPoint(x+1, y), new clsPoint(x-1, y), new clsPoint(x, y+1), new clsPoint(x, y-1)];
							break;
						case '|':
							path[new clsPoint(x, y)] = [new clsPoint(x, y + 1), new clsPoint(x, y - 1)];
							break;
						case '-':
							path[new clsPoint(x, y)] = [new clsPoint(x+1, y), new clsPoint(x-1, y)];
							break;
						case 'F':
							path[new clsPoint(x, y)] = [new clsPoint(x, y+1), new clsPoint(x + 1, y)];
							break;
						case '7':
							path[new clsPoint(x, y)] = [new clsPoint(x - 1, y), new clsPoint(x, y+1)];
							break;
						case 'L':
							path[new clsPoint(x, y)] = [new clsPoint(x + 1, y), new clsPoint(x, y-1)];
							break;
						case 'J':
							path[new clsPoint(x, y)] = [new clsPoint(x - 1, y), new clsPoint(x, y-1)];
							break;
						default:
							break;
					}
				}
            }
			return (Start, path);
		}
	}
}
