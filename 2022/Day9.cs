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
        public Day9() : base(9)
        {
        }

        public override string SolvePart1(string[] input)
        {
            HashSet<General.clsPoint> VisitedPositions= new HashSet<General.clsPoint> ();
            General.clsPoint Head=new General.clsPoint(0,0);
            General.clsPoint Tail= new General.clsPoint(0, 0);
            VisitedPositions.Add(Tail);

            foreach (var instruction in input)
            {
                var parts = instruction.Split(' ');
                //Move head
                for (int i = 0; i < int.Parse(parts[1]); i++)
                {
                    Head = Head.Move(parts[0], 1);
                    if (Head.distance(Tail)>=2)
                    {
                        Tail=MoveTail(Head,Tail);
                    }
                    VisitedPositions.Add(Tail);
                }
            }

            return VisitedPositions.Count().ToString();
        }

        private clsPoint MoveTail(clsPoint head, clsPoint tail)
        {
            clsPoint difference = head - tail;
                return tail.plus(Math.Max(Math.Min(difference.X,1),-1),Math.Max( Math.Min(difference.Y,1),-1));
        }

        public override string SolvePart2(string[] input)
        {
            HashSet<General.clsPoint> VisitedPositions = new HashSet<General.clsPoint>();
            General.clsPoint[] knots = new clsPoint[10];
            for (int i = 0; i < 10; i++)
            {
                knots[i]= new General.clsPoint(0, 0);
            }
            VisitedPositions.Add(knots.Last());

            foreach (var instruction in input)
            {
                var parts = instruction.Split(' ');
                //Move head
                for (int i = 0; i < int.Parse(parts[1]); i++)
                {
                    knots[0] = knots[0].Move(parts[0], 1);
                    for (int j = 1; j < 10; j++)
                    {
                        if(knots[j-1].distance(knots[j]) >= 2)
                    {
                            knots[j] = MoveTail(knots[j-1], knots[j]);
                        }
                    }
                    
                    VisitedPositions.Add(knots.Last());
                }
            }

            return VisitedPositions.Count().ToString();
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
        }
    }
}
