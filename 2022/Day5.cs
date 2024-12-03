using System.Diagnostics;

namespace _2022
{
	public class Day5 : General.PuzzleWithObjectInput<(IEnumerable<string> moves, List<char>[] positions)>
    {
        public Day5() : base(5, 2022)
        {
        }

        protected override (IEnumerable<string> moves, List<char>[] positions) CastToObject(string RawData)
        {
            var inputParts = RawData.Split(Environment.NewLine + Environment.NewLine);
            var instructions = inputParts[1].Split(Environment.NewLine);
            var startPositions = inputParts[0].Split(Environment.NewLine);

            int maxPosition = int.Parse(startPositions.Last().Split("   ").Last().Replace(" ", ""));
            List<char>[] positions = new List<char>[maxPosition];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = [];
            }

            for (int i = 0; i < startPositions.Length - 1; i++)
            {
                for (int j = 1; j < startPositions[i].Length; j += 4)
                {
                    if (startPositions[i][j] != ' ') positions[(j - 1) / 4].Add(startPositions[i][j]);
                }

            }

            return (instructions, positions);
        }

        public override string SolvePart1((IEnumerable<string> moves, List<char>[] positions) input)
        {
            foreach (var instruction in input.moves)
            {
                var parts = instruction.Split(" ");
                input.positions[int.Parse(parts[5]) - 1].InsertRange(0, input.positions[int.Parse(parts[3]) - 1].Take(int.Parse(parts[1])).Reverse());
                input.positions[int.Parse(parts[3]) - 1].RemoveRange(0, int.Parse(parts[1]));
            }

            return string.Join("", input.positions.Select(x => x.First()));
        }

        public override string SolvePart2((IEnumerable<string> moves, List<char>[] positions) input)
        {
            foreach (var instruction in input.moves)
            {
                var parts = instruction.Split(" ");
                input.positions[int.Parse(parts[5]) - 1].InsertRange(0, input.positions[int.Parse(parts[3]) - 1].Take(int.Parse(parts[1])));
                input.positions[int.Parse(parts[3]) - 1].RemoveRange(0, int.Parse(parts[1]));
            }

            return string.Join("", input.positions.Select(x => x.First()));
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2") == "CMZ");

                Debug.Assert(SolvePart2(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2") == "MCD");
        }
    }
}
