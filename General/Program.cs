using System;
using System.Diagnostics;

namespace General
{
    class Program
    {
        static void Main(string[] args)
        {
            Solve2019();
            Solve2020();
        }

        private static void Solve2020()
        {
            IAoC Day;
            //Day1
            Day = new _2020.Day1();

            Day.Tests();
            Console.WriteLine("DAY1");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2020.inputs.D1P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2020.inputs.D1P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            //Day2
            Day = new _2020.Day2();

            Day.Tests();
            Console.WriteLine("DAY2");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2020.inputs.D2P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2020.inputs.D2P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            //Day3
            Day = new _2020.Day3();

            Day.Tests();
            Console.WriteLine("DAY3");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2020.inputs.D3P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2020.inputs.D3P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");
        }


        private static void Solve2019()
        {
            IAoC Day;
            //Day1
            Day = new _2019.Day1();
            Stopwatch watch = new Stopwatch();

            Day.Tests();
            Console.WriteLine("DAY1");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D1P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D1P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day2
            Day = new _2019.Day2();
            Day.Tests();
            Console.WriteLine("DAY2");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D2P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D2P2) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");
        }

    }
}
