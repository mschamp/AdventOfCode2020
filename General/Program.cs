﻿using System;
using System.Collections.Generic;
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
            List<IAoC> days = new List<IAoC> { new _2020.Day1(), new _2020.Day2(), new _2020.Day3(), new _2020.Day4(), new _2020.Day5(),
            new _2020.Day6()};
            string[] inputs = new[] { _2020.inputs.D1P1, _2020.inputs.D1P1,
                _2020.inputs.D2P1, _2020.inputs.D2P1,
            _2020.inputs.D3P1, _2020.inputs.D3P1,
            _2020.inputs.D4P1, _2020.inputs.D4P1,
            _2020.inputs.D5P1, _2020.inputs.D5P1,
            _2020.inputs.D6P1, _2020.inputs.D6P1};
            Stopwatch watch = new Stopwatch();

            for (int i = 0; i < days.Count; i++)
            {
                days[i].Tests();
                Console.WriteLine($"DAY{i + 1}");
                watch.Start();
                Console.WriteLine(days[i].SolvePart1(inputs[2 * i]) + " " + watch.Elapsed);
                watch.Restart();
                Console.WriteLine(days[i].SolvePart2(inputs[2 * i + 1]) + " " + watch.Elapsed);
                watch.Stop();
                Console.WriteLine("__________________________________");
            }

        }


        private static void Solve2019()
        {
            List<IAoC> days = new List<IAoC> { new _2019.Day1(), new _2019.Day2(), new _2019.Day3(), new _2019.Day4(), new _2019.Day5(),
            new _2019.Day6(), new _2019.Day7(), new _2019.Day8(), new _2019.Day9(), new _2019.Day10(),
            new _2019.Day11()};
            string[] inputs = new[] { _2019.inputs.D1P1, _2019.inputs.D1P1,
                _2019.inputs.D2P1, _2019.inputs.D2P2,
            _2019.inputs.D3P1, _2019.inputs.D3P1,
            _2019.inputs.D4P1, _2019.inputs.D4P1,
            _2019.inputs.D5P1, _2019.inputs.D5P2,
            _2019.inputs.D6P1, _2019.inputs.D6P1,
            _2019.inputs.D7P1, _2019.inputs.D7P1,
            _2019.inputs.D8P1, _2019.inputs.D8P1,
            _2019.inputs.D9P1, _2019.inputs.D9P1,
            _2019.inputs.D10P1, _2019.inputs.D10P1,
            _2019.inputs.D11P1, _2019.inputs.D11P1};
            Stopwatch watch = new Stopwatch();

            for (int i = 0; i < days.Count; i++)
            {
                days[i].Tests();
                Console.WriteLine($"DAY{i+1}");
                watch.Start();
                Console.WriteLine(days[i].SolvePart1(inputs[2*i]) + " " + watch.Elapsed);
                watch.Restart();
                Console.WriteLine(days[i].SolvePart2(inputs[2 * i+1]) + " " + watch.Elapsed);
                watch.Stop();
                Console.WriteLine("__________________________________");
            }

        }

    }
}
