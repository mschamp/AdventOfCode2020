using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day17 : General.PuzzleWithObjectInput<int[]>
    {
        public Day17() : base(17) { }

        public override int[] CastToObject(string RawData)
        {
            System.Text.RegularExpressions.Regex rgx = new(@"-?\d+");
            return rgx.Matches(RawData).Select(x => x.Value).Select(i => int.Parse(i)).ToArray();
        }

        public override string SolvePart1(int[] input)
        {
            int dy = input[2];
            int dx = 1;
            int MaxH = 0;

            for (int i = 0; i < 75000; i++)
            {
                (bool InTarget, int height) result = Shoot(dx, dy, input);
                if (result.InTarget)
                {
                    MaxH = Math.Max(MaxH, result.height);
                }

                dx++;
                if (dx > input[1])
                {
                    dx = 1;
                    dy++;
                }
            }

            return MaxH.ToString();
        }

        private (bool InTarget, int height) Shoot(int speedX, int SpeedY, int[] goal)
        {
            int xPos = 0;
            int yPos = 0;
            int CurrentMax = yPos;

            while (true)
            {
                xPos += speedX;
                yPos += SpeedY;

                CurrentMax = Math.Max(CurrentMax, yPos);

                if (InTargetArea(xPos, yPos)) return (true, CurrentMax);
                if (GoneForever(xPos, yPos)) return (false, CurrentMax);

                if (speedX>0)
                {
                    speedX--;
                }
                else if (speedX<0)
                {
                    speedX++;
                }

                SpeedY--;
            }

            bool InTargetArea(int x, int y) => x >= goal[0]
                                            && x <= goal[1]
                                            && y >= goal[2]
                                            && y <= goal[3];

            bool GoneForever(int x, int y) => x > goal[1]
                                            || y < goal[2];
        }

        public override string SolvePart2(int[] input)
        {
            int dy = input[2];
            int dx = 1;
            int Hit = 0;

            for (int i = 0; i < 75000; i++)
            {
                (bool InTarget, int height) result = Shoot(dx, dy, input);
                if (result.InTarget) Hit++;

                dx++;
                if (dx > input[1])
                {
                    dx = 1;
                    dy++;
                }
            }

            return Hit.ToString(); 
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("target area: x=20..30, y=-10..-5") == "45");

            Debug.Assert(SolvePart2("target area: x=20..30, y=-10..-5") == "112");
        }
    }
}
