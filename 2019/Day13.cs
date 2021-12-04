using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day13 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            IntcodeComputer computer = new();
            computer.loadProgram(input);
            computer.ExecuteProgram();
            List<long> output = computer.ReadOutputs();
            List<Tuple<long, long, long>> screen = new();
            for (int i = 0; i < output.Count; i+=3)
            {
                screen.Add(new Tuple<long, long, long>(output[i], output[i + 1], output[i + 2]));
            }
            return "" + screen.Count(x => x.Item3 == 2);
        }

        public string SolvePart2(string input = null)
        {
            IntcodeComputer computer = new();
            computer.loadProgram(input);
            computer.SetMemoryContent(0, 2);
            computer.ExecuteProgram();
            List<long> output;
            long xBall =0;
            long xPaddle = 0;
            Dictionary<General.clsPoint, long> screen = new();

            while (computer.WaitingForInput)
            {
                
                output = computer.ReadOutputs();
                for (int i = 0; i < output.Count; i += 3)
                {
                    screen[new General.clsPoint((int)output[i], (int)output[i + 1])]= output[i + 2];
                    if (output[i + 2]==3)
                    {
                        xPaddle = output[i];
                    }
                    else if (output[i + 2] == 4)
                    {
                        xBall = output[i];
                    }
                }
                computer.ClearOutputs();

                printScreen(screen);
                computer.InputValue(DetermingInput(xBall, xPaddle));
                computer.ExecuteProgram();
            }

            output = computer.ReadOutputs();
            for (int i = 0; i < output.Count; i += 3)
            {
                screen[new General.clsPoint((int)output[i], (int)output[i + 1])] = output[i + 2];
            }

            return "" + screen[new General.clsPoint(-1, 0)];
        }

        private void printScreen(Dictionary<General.clsPoint, long> screen)
        {
            int row = -1;
            string text = "";
            foreach (KeyValuePair< General.clsPoint, long> item in screen.OrderBy(x => x.Key))
            {
                if (item.Key.Y>row)
                {
                    text += Environment.NewLine;
                    row = (int)item.Key.Y;
                }
                switch (item.Value)
                {
                    case 0:
                        text += (" ");
                        break;
                    case 1:
                        text += ("B");
                        break;
                    case 2:
                        text += (".");
                        break;
                    case 3:
                        text += ("_");
                        break;
                    case 4:
                        text += ("O");
                        break;
                    default:
                        break;
                } 
            }
            //Console.Clear();
            //Console.WriteLine(text);
        }

        private int DetermingInput(long ball, long paddle)
        {
            long delta = ball - paddle;
            return (paddle > ball) ? -1 : (paddle < ball) ? 1 : 0;

        }

        public void Tests()
        {
            
        }
    }
}
