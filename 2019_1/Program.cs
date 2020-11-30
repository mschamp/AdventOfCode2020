using System;
using System.Collections.Generic;
using System.Text;

namespace _2019
{
    class Program
    {
        static void Main(string[] args)
        {
            IAoC Day;
            //Day1
            Day = new Day1();

            Day.Tests();
            Console.WriteLine("DAY1");
            Console.WriteLine(Day.SolvePart1(inputs.D1P1));
            Console.WriteLine(Day.SolvePart2(inputs.D1P1));
            Console.WriteLine("__________________________________");






            // Day2
            Day = new Day2();
            Day.Tests();
            Console.WriteLine("DAY2");
            Console.WriteLine(Day.SolvePart1(inputs.D2P1));
            Console.WriteLine(Day.SolvePart2(inputs.D2P2));
            Console.WriteLine("__________________________________");
        }
    }
}
