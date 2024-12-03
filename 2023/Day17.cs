namespace _2023
{
    public class Day17 : PuzzleWithObjectArrayInput<int[]>
	{
        public Day17():base(17,2023)
        {
            
        }
        public override string SolvePart1(int[][] input)
		{
			return $"{AstarSpecial(input,3,1)}";
		}

		int[] directions = [0,1,2,3];
		List<(int, int)> dirMovement = [(0, 1), (1, 0), (0, -1), (-1, 0)];
		private int AstarSpecial(int[][] input,int MaxDistance, int MinDistance)
		{
			Queue<(int, int, int, int)> q = new();
			q.Enqueue((0, 0, 0, -1));
			HashSet<( int, int, int)> seen = [];
			Dictionary<(int,int,int),int> costs = [];
			while (q.TryDequeue(out (int cost, int x, int y, int dd) c))
			{
				//if (c.x==input.Length-1 && c.y == input[0].Length-1)return c.cost;
				if (seen.Contains((c.x, c.y, c.dd))) continue;
				foreach (int direction in directions)
				{
					int costIncrease = 0;
					if (direction == c.dd) continue;
					if ((direction + 2)%2 == c.dd) continue;

					for (int distance = 1; distance <= MaxDistance; distance++)
					{
						int newX = c.x + dirMovement[direction].Item1 * distance;
						int newY = c.y + dirMovement[direction].Item2 * distance;
						if (newY<0|| newX <0) break;
						if (newY>=input.Length|| newX >= input[newY].Length) break;

						costIncrease += input[newY][newX];
						if (distance < MinDistance) continue;
						var nc = c.cost + costIncrease;
						if (!costs.TryGetValue((newX,newY,direction),out int stored)||stored>nc)
						{
							costs[(newX, newY, direction)] = nc;
							q.Enqueue((nc, newX, newY, direction));
						}
					}
				}

			}

			return costs.Where(x => x.Key.Item2 == input.Length - 1 && x.Key.Item1 == input[0].Length - 1).Min(x => x.Value);
		}

		public override string SolvePart2(int[][] input)
		{
			return $"{AstarSpecial(input, 10,4)}";
		}

		public override void Tests()
		{
			Debug.Assert(SolvePart1(@"2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533") == "102");

			Debug.Assert(SolvePart2(@"2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533") == "94");

			Debug.Assert(SolvePart2(@"111111111111
999999999991
999999999991
999999999991
999999999991") == "71");
		}

		protected override int[] CastToObject(string RawData)
		{
			return RawData.Select(x => x-'0').ToArray();
		}
	}
}
