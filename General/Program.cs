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

            //Day4
            Day = new _2020.Day4();

            Day.Tests();
            Console.WriteLine("DAY4");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2020.inputs.D4P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2020.inputs.D4P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            //Day5
            Day = new _2020.Day5();

            Day.Tests();
            Console.WriteLine("DAY5");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2020.inputs.D5P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2020.inputs.D5P1) + " " + watch.Elapsed);
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

            // Day3
            Day = new _2019.Day3();
            Day.Tests();
            Console.WriteLine("DAY3");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D3P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D3P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day4
            Day = new _2019.Day4();
            Day.Tests();
            Console.WriteLine("DAY4");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D4P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D4P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day5
            Day = new _2019.Day5();
            Day.Tests();
            Console.WriteLine("DAY5");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D5P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D5P2) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day6
            Day = new _2019.Day6();
            Day.Tests();
            Console.WriteLine("DAY6");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D6P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D6P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day7
            Day = new _2019.Day7();
            Day.Tests();
            Console.WriteLine("DAY7");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D7P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D7P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day8
            Day = new _2019.Day8();
            Day.Tests();
            Console.WriteLine("DAY8");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D8P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D8P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day9
            Day = new _2019.Day9();
            Day.Tests();
            Console.WriteLine("DAY9");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D9P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D9P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

            // Day10
            Day = new _2019.Day10();
            Day.Tests();
            Console.WriteLine("DAY10");
            watch.Start();
            Console.WriteLine(Day.SolvePart1(_2019.inputs.D10P1) + " " + watch.Elapsed);
            watch.Restart();
            Console.WriteLine(Day.SolvePart2(_2019.inputs.D10P1) + " " + watch.Elapsed);
            watch.Stop();
            Console.WriteLine("__________________________________");

        }

    }
}
