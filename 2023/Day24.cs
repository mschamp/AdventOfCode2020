namespace _2023
{
	public class Day24:PuzzleWithObjectArrayInput<Day24.Hailstone>
	{
        public Day24():base(24,2023)
        {
            
        }
        public override string SolvePart1(Hailstone[] input)
		{
			(float min, float max) range;
			if (input.Length<10) range = (7, 27);
			else range = (200000000000000, 400000000000000);

			List<(float, float)> intersections = [];
			for (int i = 0; i < input.Length; i++)
			{
				for(int j = i+1;j<input.Length;j++)
				{
					if (Intersection(input[i], input[j], out (float, float) intersect)) intersections.Add(intersect);
				}
			}
			intersections = intersections.Where(x => x.Item1 >= range.min && x.Item1 <= range.max && x.Item2 >= range.min && x.Item2 <= range.max).ToList();
			return $"{intersections.Count()}";
		}

		public bool Intersection(Hailstone hailstoneA, Hailstone hailstoneB, out (float,float) intersection)
		{
			intersection = (0, 0);

			if (hailstoneA.slope==hailstoneB.slope) return false; //parallel

			var tx = ((hailstoneB.slope*hailstoneB.position.px)-(hailstoneA.slope*hailstoneA.position.px)+hailstoneA.position.py-hailstoneB.position.py)/ (hailstoneB.slope - hailstoneA.slope);
			var ty = (hailstoneA.slope * (tx - hailstoneA.position.px)) + hailstoneA.position.py;
			
			intersection=(tx,ty);

			if (!IsFuture(hailstoneA,intersection)|| !IsFuture(hailstoneB, intersection)) return false;

			return true;
		}

		private bool IsFuture(Hailstone stone, (double tx,double ty) common)
		{
			if (stone.speed.vx <0 &&stone.position.px <common.tx) return false;
			if (stone.speed.vx >0 &&stone.position.px >common.tx) return false;
			if (stone.speed.vy < 0 && stone.position.py < common.ty) return false;
			if (stone.speed.vy > 0 && stone.position.py > common.ty) return false;
			return true;
		}

		public override string SolvePart2(Hailstone[] input)
		{
			long N = 0;
			while (true)
			{
				for (long X = 0; X <= N; X++) 
				{
					long Y = N - X;
                    foreach (var negX in new int[] {-1,1 })
                    {
						foreach (var negY in new int[] { -1, 1 })
						{
							long aX = X * negX;
							long aY = Y * negY;

							Hailstone H1 = input[0];
						}
					}
                }
			}
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"19, 13, 30 @ -2,  1, -2
18, 19, 22 @ -1, -1, -2
20, 25, 34 @ -2, -2, -4
12, 31, 28 @ -1, -2, -1
20, 19, 15 @  1, -5, -3") == "2");

			Debug.Assert(SolvePart2(@"19, 13, 30 @ -2,  1, -2
18, 19, 22 @ -1, -1, -2
20, 25, 34 @ -2, -2, -4
12, 31, 28 @ -1, -2, -1
20, 19, 15 @  1, -5, -3") == "47");
		}

		protected override Hailstone CastToObject(string RawData)
		{
			return new Hailstone(RawData);
		}

		public class Hailstone
		{
			public Hailstone(string rawData)
			{
				var parts = rawData.Split(new char[] { '@', ',' }, StringSplitOptions.TrimEntries).Select(float.Parse).ToArray();
				position = (parts[0], parts[1], parts[2]);
				speed = (parts[3], parts[4], parts[5]);
				slope = speed.vy / speed.vx;
			}

			public (float px, float py, float pz) position { get; private set; }
			public (float vx, float vy, float vz) speed { get; private set; }

			public float slope { get;private set; }
		}
	}
}
