using System.Diagnostics;

namespace _2021
{
	public class Day2 : General.PuzzleWithObjectArrayInput<Day2.Instruction>
    {
        public Day2() : base(2, 2021)
        {

        }
        public override string SolvePart1(Instruction[] input)
        {
            int Horizontal = 0;
            int depth = 0;
            foreach (Instruction item in input)
            {
                switch (item.direction)
                {
                    case "down":
                        depth += item.distance;
                        break;
                    case "forward":
                        Horizontal += item.distance;
                        break;
                    case "up":
                        depth -= item.distance;
                        break;
                    default:
                        break;
                }
            }

            return (Horizontal * depth).ToString();
        }

        public override string SolvePart2(Instruction[] input)
        {
            int Horizontal = 0;
            int depth = 0;
            int aim = 0;
            foreach (Instruction item in input)
            {
                switch (item.direction)
                {
                    case "down":
                        aim += item.distance;
                        break;
                    case "forward":
                        Horizontal += item.distance;
                        depth +=aim* item.distance;
                        break;
                    case "up":
                        aim -= item.distance;
                        break;
                    default:
                        break;
                }
            }

            return (Horizontal * depth).ToString();
        }

        public record Instruction(string direction, int distance); 

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"forward 5
down 5
forward 8
up 3
down 8
forward 2") == "150");

            Debug.Assert(SolvePart2(@"forward 5
down 5
forward 8
up 3
down 8
forward 2") == "900");
        }

        protected override Instruction CastToObject(string RawData)
        {
            string[] data = RawData.Split(' ');
            return new Instruction(data[0], int.Parse(data[1]));
        }
    }
}
