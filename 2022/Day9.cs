using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day9 : General.PuzzleWithStringArrayInput
    {
        public Day9() : base(9, 2022)
        {
        }

        public override string SolvePart1(string[] input)
        {
            FollowInstructions(input, 2, out HashSet<clsPoint> VisitedPositions);

            return VisitedPositions.Count().ToString();
        }

        private clsPoint MoveTail(clsPoint head, clsPoint tail)
        {
            clsPoint difference = head - tail;
                return tail.plus(Math.Max(Math.Min(difference.X,1),-1),Math.Max( Math.Min(difference.Y,1),-1));
        }

        public override string SolvePart2(string[] input)
        {

            FollowInstructions(input, 10, out HashSet<clsPoint> VisitedPositions);
            return VisitedPositions.Count().ToString();
        }

        private void FollowInstructions(string[] input, int length, out HashSet<General.clsPoint> VisitedPositions)
        {
            VisitedPositions = new HashSet<General.clsPoint>();
            General.clsPoint[] knots = new clsPoint[length];
            for (int i = 0; i < length; i++)
            {
                knots[i] = new General.clsPoint(0, 0);
            }
            VisitedPositions.Add(knots.Last());

            foreach (var instruction in input)
            {
                var parts = instruction.Split(' ');
                for (int i = 0; i < int.Parse(parts[1]); i++)
                {
                    knots[0] = knots[0].Move(parts[0], 1);
                    for (int j = 1; j < length; j++)
                    {
                        if (knots[j - 1].distance(knots[j]) >= 2)
                        {
                            knots[j] = MoveTail(knots[j - 1], knots[j]);
                        }
                    }

                    VisitedPositions.Add(knots.Last());
                }
            }
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2") == "13");
            Debug.Assert(SolvePart2(@"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2") == "1");

            Debug.Assert(SolvePart2(@"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20") == "36");

        }
    }
}
