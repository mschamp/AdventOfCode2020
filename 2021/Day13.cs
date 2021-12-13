using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day13 : General.PuzzleWithObjectInput<(List<General.clsPoint> dots, string[] foldinstructions)>
    {
        public override (List<clsPoint> dots, string[] foldinstructions) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine + Environment.NewLine);
            List<General.clsPoint> points = parts[0].Split(Environment.NewLine).Select(x => new clsPoint(Double.Parse(x.Split(",")[0]), Double.Parse(x.Split(",")[1]))).ToList();

            return (points, parts[1].Split(Environment.NewLine));

        }

        public override string SolvePart1((List<clsPoint> dots, string[] foldinstructions) input)
        {
            return ExecuteFold(input.dots, input.foldinstructions[0]).Count().ToString();
        }

        private HashSet<clsPoint> ExecuteFold(List<clsPoint> points, string Instruction)
        {
            HashSet<clsPoint> uniquePoints = new(points);

            direction fold = Instruction.Contains("x=") ? direction.Vertical : direction.Horizontal;
            int foldPosiion = int.Parse(Instruction.Split("=")[1]);

            switch (fold)
            {
                case direction.Horizontal:
                    foreach (clsPoint point in points.Where(x => x.Y>foldPosiion))
                    {
                        uniquePoints.Remove(point);
                        uniquePoints.Add(point.plus(0.0, 2.0 * (foldPosiion - point.Y)));
                    }
                    break;
                case direction.Vertical:
                    foreach (clsPoint point in points.Where(x => x.X > foldPosiion))
                    {
                        uniquePoints.Remove(point);
                        uniquePoints.Add(point.plus( 2.0 * (foldPosiion - point.X), 0.0));
                    }
                    break;
                default:
                    break;
            }
            return uniquePoints;
        }

        public override string SolvePart2((List<clsPoint> dots, string[] foldinstructions) input)
        {
            foreach (string instruction in input.foldinstructions)
            {
                input.dots = ExecuteFold(input.dots, instruction).ToList();
            }

            string result = "";
            int MaxX = (int)input.dots.Max(x => x.X);
            int MaxY = (int)input.dots.Max(x => x.Y);

            for (int j = 0; j <= MaxY; j++)
            {
                for (int i = 0; i <= MaxX; i++)
                {
                    result += input.dots.Contains(new clsPoint(i, j)) ? "#": " ";
                }
                result += Environment.NewLine;
            }
            
            return result;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5") == "17");
        }

        private enum direction
        {
            Horizontal,
            Vertical
        }
    }
    
}
