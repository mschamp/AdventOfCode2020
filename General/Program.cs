using System;

namespace General
{
    class Program
    {
        static void Main(string[] args)
        {
            //Solve2019();
            Solve2020();
        }

        private static void Solve2020()
        {
            IAoC Day;
            //Day1
            Day = new _2020.Day1();

            Day.Tests();
            Console.WriteLine("DAY1");
            Console.WriteLine(Day.SolvePart1(_2020.inputs.D1P1));
            Console.WriteLine(Day.SolvePart2(_2020.inputs.D1P1));
            Console.WriteLine("__________________________________");
        }


        private static void Solve2019()
        {
            IAoC Day;
            //Day1
            Day = new _2019.Day1();

            Day.Tests();
            Console.WriteLine("DAY1");
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D1P1));
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D1P1));
            Console.WriteLine("__________________________________");

            // Day2
            Day = new _2019.Day2();
            Day.Tests();
            Console.WriteLine("DAY2");
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D2P1));
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D2P2));
            Console.WriteLine("__________________________________");
        }

    }
}
