using System.Numerics;

namespace _2023
{
    public class Day16 : PuzzleWithObjectInput<Dictionary<Complex, char>>
	{
		Complex[] directions = new Complex[] { new(1, 0), new(-1, 0), new(0, -1), new(0, 1) };

		public Day16():base(16,2023)
        {
			
        }
        public override string SolvePart1(Dictionary<Complex, char> input)
		{
			var queue = new Queue<(Complex, Complex)>();
			queue.Enqueue((directions[1], directions[0]));
			var result = CountEnergized(queue, input);	

			return $"{result}";
		}

		public override string SolvePart2(Dictionary<Complex, char> input)
		{
			
			var result = 0 ;

			foreach (var item in input.Keys)
			{
				foreach (var key in directions)
				{
					if (!input.ContainsKey(item-key))
					{
						var queue = new Queue<(Complex, Complex)>();
						queue.Enqueue((item-key, key));
						result = Math.Max(result,CountEnergized(queue, input));
					}

				}
			}
			
			return $"{result}";
		}

		private int CountEnergized(Queue<(Complex, Complex)> todo, Dictionary<Complex, char> grid)
		{
			HashSet<(Complex, Complex)> energized = [];
			while(todo.TryDequeue(out (Complex point, Complex direction) current))
			{
				 while (!energized.Contains(current))
				{
					energized.Add(current);
					current.point += current.direction;
					if (!grid.TryGetValue(current.point, out char mirror))
					{
						break;
					}
					switch (mirror)
					{
						case '\\':
							current.direction = (current.direction * directions[2]).InvertImaginary();
							break;
						case '/':
							current.direction = (current.direction * directions[3]).InvertImaginary();
							break;
						case '|':
							current.direction = directions[3];
							todo.Enqueue((current.point, directions[2]));
							break;
						case '-':
							current.direction = directions[0];
							todo.Enqueue((current.point, directions[1]));
							break;
					}
				}
			}
			return energized.Select(x=>x.Item1).Distinct().Count()-1;
		}
		public override void Tests()
		{
			Debug.Assert(SolvePart1(@".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....") == "46");

			Debug.Assert(SolvePart2(@".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....") == "51");
		}

		protected override Dictionary<Complex, char> CastToObject(string RawData)
		{
			Dictionary<Complex, char> result = [];
			string[] lines = RawData.Split(Environment.NewLine);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
					result[new Complex(x, y)] = lines[y][x];
                }
            }

			return result;
        }

		
	}

	static class complexExtension
	{
		public static Complex InvertImaginary(this Complex comp)
		{
			return new Complex(comp.Real, -comp.Imaginary);
		}
	}
}
