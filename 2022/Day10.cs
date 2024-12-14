using System.Diagnostics;

namespace _2022
{
	public class Day10 : General.PuzzleWithStringArrayInput
    {
        public Day10() : base(10, 2022)
        {
        }

        public override string SolvePart1(string[] input)
        {
            int X = 1;
            int Cycle = 0;
            long result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var instructions = input[i].Split(' ');
                switch (instructions[0])
                {
                    case "addx":
                        for (int j = 0; j < 2; j++)
                        {
                            Cycle++;
                            if ((Cycle - 20) % 40 == 0)
                            {
                                result += Cycle * X;
                            }
                        }

                        
                        X += int.Parse(instructions[1]);
                        break;
                    case "noop":
                        Cycle++;
                        if ((Cycle - 20) % 40 == 0)
                        {
                            result += Cycle * X;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result.ToString();
        }

        public override string SolvePart2(string[] input)
        {
            int X = 1;
            int Cycle = 0;
            bool[] screen = new bool[6* 40];
            for (int i = 0; i < input.Length; i++)
            {
                var instructions = input[i].Split(' ');
                switch (instructions[0])
                {
                    case "addx":
                        for (int j = 0; j < 2; j++)
                        {
                            if (Cycle%40==X-1||Cycle%40==X||Cycle % 40 ==X+1)
                            {
                                screen[Cycle]= true;
                            }
                            Cycle++;
                        }

                        X += int.Parse(instructions[1]);
                        break;
                    case "noop":
                        if (Cycle%40 == X - 1 || Cycle%40 == X || Cycle%40 == X + 1)
                        {
                            screen[Cycle] = true;
                        }
                        Cycle++;
                        break;
                    default:
                        break;
                }
            }

            string result = "";
            for (int i = 0; i < screen.Length; i++)
            {
                if (i>0 &&i % 40 == 0) result += Environment.NewLine;
                if (screen[i]) result += General.Constants.charConstants.White;
                else result += '.';
            }
            return result;
            
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop") == "13140");
            Debug.Assert(SolvePart2(@"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop") == @"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....".Replace('#', General.Constants.charConstants.White));
        }
    }
}
