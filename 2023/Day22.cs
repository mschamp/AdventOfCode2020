using MoreLinq;

namespace _2023
{
    public class Day22 : PuzzleWithObjectArrayInput<Day22.Brick>
	{
        public Day22():base(22,2023)
        {
				
        }

		public override string SolvePart1(Brick[] input)
		{
			List<Brick> OrderByZ = input.OrderBy(x => x.start.Z).ToList();

			var bricks = new List<HashSet<(int,int,int)>>();
            foreach (var brick in OrderByZ)
            {
				HashSet<(int, int, int)> b = [];
                foreach (int x in Enumerable.Range(brick.start.X, brick.end.X-brick.start.X+1))
                {
					foreach (int y in Enumerable.Range(brick.start.Y, brick.end.Y - brick.start.Y + 1))
					{
						foreach (int z in new int[]{ brick.start.Z, brick.end.Z})
						{
							b.Add((x, y, z));
						}
					}
				}
				bricks.Add(b);
            }

			HashSet<(int, int, int)> settledPositions = [];
			HashSet<(int,int,int)> unsettledPositions = [];

            foreach (var item in bricks)
            {
				unsettledPositions.UnionWith(item);
            }

            for (int i = 0; i < bricks.Count; i++)
            {
				unsettledPositions.ExceptWith(bricks[i]);
				bricks[i] = fall(bricks[i], unsettledPositions, settledPositions);
				settledPositions.UnionWith(bricks[i]);
            }

            int answer = 0;

			for (int idx = 0; idx < bricks.Count; idx++)
			{
				settledPositions.ExceptWith(bricks[idx]);


				bool wasSafe = true;
				var tested = new List<int>();
				for (int i2 = idx + 1; i2 < bricks.Count; i2++)
				{
					tested.Add(idx + 1 + i2);
					settledPositions.ExceptWith(bricks[i2]);
					if (fall(bricks[i2],unsettledPositions,settledPositions) != bricks[i2])
					{
						wasSafe = false;
						settledPositions.UnionWith(bricks[i2]);
						break;
					}
					settledPositions.UnionWith(bricks[i2]);
				}

				if (wasSafe)
				{
					answer++;
				}

				settledPositions.UnionWith(bricks[idx]);
			}

			return $"{answer}";
		}

		private HashSet<(int,int,int)> fall(HashSet<(int,int,int)> brick, HashSet<(int, int, int)> unsettledPositions, HashSet<(int, int, int)> settledPositions)
		{
			while (true)
			{
				HashSet<(int, int, int)> newB = [];
                foreach (var item in brick)
                {
					newB.Add((item.Item1, item.Item2, item.Item3-1));
                }
				if (newB.Overlaps(settledPositions) || newB.Min(x => x.Item3) <= 0)
				{
					return brick;
				}
				brick = newB;
            }
		}


		public override string SolvePart2(Brick[] input)
		{
			List<Brick> OrderByZ = input.OrderBy(x => x.start.Z).ToList();

			var bricks = new List<HashSet<(int, int, int)>>();
			foreach (var brick in OrderByZ)
			{
				HashSet<(int, int, int)> b = [];
				foreach (int x in Enumerable.Range(brick.start.X, brick.end.X - brick.start.X + 1))
				{
					foreach (int y in Enumerable.Range(brick.start.Y, brick.end.Y - brick.start.Y + 1))
					{
						foreach (int z in new int[] { brick.start.Z, brick.end.Z })
						{
							b.Add((x, y, z));
						}
					}
				}
				bricks.Add(b);
			}

			HashSet<(int, int, int)> settledPositions = [];
			HashSet<(int, int, int)> unsettledPositions = [];

			foreach (var item in bricks)
			{
				unsettledPositions.UnionWith(item);
			}
			int answer = 0;

			for (int idx = 0; idx < bricks.Count; idx++)
			{
				HashSet<(int, int, int)> oldSettled = new(settledPositions);

				settledPositions.ExceptWith(bricks[idx]);

				for (int i2 = idx + 1; i2 < bricks.Count; i2++)
				{
					settledPositions.ExceptWith(bricks[i2]);
					HashSet<(int, int, int)> newB2 = fall(bricks[i2], unsettledPositions, settledPositions);
					if (newB2 != bricks[i2])
					{
						answer++;
					}
					settledPositions.UnionWith(newB2);
				}

				settledPositions = new HashSet<(int, int, int)>(oldSettled);
			}

			return $"{answer}";
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"1,0,1~1,2,1
0,0,2~2,0,2
0,2,3~2,2,3
0,0,4~0,2,4
2,0,5~2,2,5
0,1,6~2,1,6
1,1,8~1,1,9") == "5");

			Debug.Assert(SolvePart2(@"1,0,1~1,2,1
0,0,2~2,0,2
0,2,3~2,2,3
0,0,4~0,2,4
2,0,5~2,2,5
0,1,6~2,1,6
1,1,8~1,1,9") == "7");
		}

		protected override Brick CastToObject(string RawData)
		{
			return new Brick(RawData);

		}

		public class Brick
		{
            public Brick(string input)
            {
				int[] parts = input.Split('~',',').Select(x=> int.Parse(x)).ToArray();

				start = (Math.Min(parts[0], parts[3]), Math.Min(parts[1], parts[4]), Math.Min(parts[2], parts[5]));
				end = (Math.Max(parts[0], parts[3]), Math.Max(parts[1], parts[4]), Math.Max(parts[2], parts[5]));
			}

			public (int X,int Y,int Z) start { get; set; }
			public (int X,int Y,int Z) end { get; set; }
        }
	}
}
