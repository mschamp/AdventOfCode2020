using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day11 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            PaintingRobot robot = new(input,0);
            return "" + robot.NumberPaintedCells;
        }

        public string SolvePart2(string input = null)
        {
            PaintingRobot robot = new(input,1);
            string result = "";
            var test = robot.PaintedCellsOrdered;
            for (double i = robot.maxY; i >= robot.minY; i--)
            {
                for (double j = robot.minX; j <= robot.maxX; j++)
                {
                    if (robot.GetColor((int)j,(int)i)==1)
                    {
                        result += "#";
                    }
                    else
                    {
                        result += " ";
                    }
                }
                result += Environment.NewLine;
            }
            return result;
        }

        public void Tests()
        {
            
        }
    }

    public class PaintingRobot
    {
        private IntcodeComputer computer;
        private General.Direction direction;
        General.clsPoint CurrentPosition;
        Dictionary<General.clsPoint, long> paintedPanels;
        public PaintingRobot(string sProgram,long start)
        {
            direction = General.Direction.Up;
            CurrentPosition = new General.clsPoint(0, 0);
            paintedPanels = new Dictionary<General.clsPoint, long>();
            paintedPanels[CurrentPosition]= start;
            computer = new IntcodeComputer();
            computer.loadProgram(sProgram);

            while (!computer.Halted)
            {
                Move();
            }
        }

        public int NumberPaintedCells
        {
            get
            {
                return paintedPanels.Count;
            }
        }

        public KeyValuePair<General.clsPoint, long>[] PaintedCellsOrdered
        {
            get
            {
                return paintedPanels.OrderBy(x => x.Key).ToArray();
            }
        }

        public Dictionary<General.clsPoint, long> PaintedCells
        {
            get
            {
                return paintedPanels;
            }
        }

        public double minX
        {
            get
            {
                return paintedPanels.Min(x => x.Key.X);
            }
        }

        public double maxX
        {
            get
            {
                return paintedPanels.Max(x => x.Key.X);
            }
        }

        public double minY
        {
            get
            {
                return paintedPanels.Min(x => x.Key.Y);
            }
        }

        public double maxY
        {
            get
            {
                return paintedPanels.Max(x => x.Key.Y);
            }
        }

        private void Move()
        {
            long color= GetColor(CurrentPosition);
            computer.InputValue(color);
            computer.ExecuteProgram();
            paintedPanels[CurrentPosition]=computer.ReadOutputs()[0];
            Rotate(computer.ReadOutputs()[1]);
            computer.ClearOutputs();
            CurrentPosition = CurrentPosition.Move(direction);
        }

        public long GetColor(General.clsPoint point)
        {
            long color;
            paintedPanels.TryGetValue(point, out color);
            return color;
        }

        public long GetColor(int X, int Y)
        {
            return GetColor(new General.clsPoint(X,Y));
        }

        public void Rotate(long value)
        {
            int Current = (int)direction;
            if (value==0)
            {
                direction = (General.Direction)((Current + 1) % 4);
            }
            else
            {
                direction = (General.Direction)((Current - 1+4) % 4);
            }
        }
    }
}
