using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day5: General.PuzzleWithObjectArrayInput<Day5.Line>
    {
        public Day5() : base(5)
        {

        }
        public override Line CastToObject(string RawData)
        {
            return new Line(RawData);
        }

        public override string SolvePart1(Line[] input)
        {
            var horAndVerLines = input.Where(x => x.IsHorizontal || x.IsVertical);
            int CountOfPointMultipleTimes =horAndVerLines.SelectMany(line => line.PointsOnLine()).GroupBy(point => point).Count(x => x.Count() > 1);
            return CountOfPointMultipleTimes.ToString();
        }

        public override string SolvePart2(Line[] input)
        {
            int CountOfPointMultipleTimes = input.SelectMany(line => line.PointsOnLine()).GroupBy(point => point).Count(x => x.Count() > 1);
            return CountOfPointMultipleTimes.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2") == "5");

            Debug.Assert(SolvePart2(@"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2") == "12");
        }

        public class Line
        {
            public Line(string rawData)
            {
                string[] points = rawData.Split(" -> ");
                start = new General.clsPoint(Double.Parse(points[0].Split(",")[0]), Double.Parse(points[0].Split(",")[1]));
                end = new General.clsPoint(Double.Parse(points[1].Split(",")[0]), Double.Parse(points[1].Split(",")[1]));
            }

            public General.clsPoint start { get; set; }
            public General.clsPoint end { get; set; }

            public bool IsVertical
            {
                get
                {
                    return start.Y == end.Y;
                }
            }

            public bool IsHorizontal
            {
                get
                {
                    return start.X == end.X;
                }
            }

            public List<General.clsPoint> PointsOnLine()
            {
                List<General.clsPoint> points = new();
                    General.clsPoint Huidig = start;
                    int DeltaX = Math.Sign(end.X - start.X);
                    int DeltaY = Math.Sign(end.Y - start.Y);


                    while (!Huidig.Equals(end))
                    {
                        points.Add(Huidig);
                        Huidig = Huidig.plus(DeltaX, DeltaY);

                    }
                    points.Add(end);

                return points;
            }
        }

    }
}
