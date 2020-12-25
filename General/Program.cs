using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace General
{
    class Program
    {
        static void Main(string[] args)
        {
            Solve2015();
            Solve2016();
            Solve2017();
            Solve2018();
            //Solve2019();
            Solve2020();
        }

        private static void Solve2020()
        {
            List<IAoC> days = new List<IAoC> { new _2020.Day1(), new _2020.Day2(), new _2020.Day3(), new _2020.Day4(), new _2020.Day5(),
            new _2020.Day6(),new _2020.Day7(),new _2020.Day8(),new _2020.Day9(),new _2020.Day10(),
            new _2020.Day11(),new _2020.Day12(),new _2020.Day13(),new _2020.Day14(),new _2020.Day15(),
            new _2020.Day16(),new _2020.Day17(),new _2020.Day18(),new _2020.Day19(), new _2020.Day20(),
            new _2020.Day21(),new _2020.Day22(),new _2020.Day23(),new _2020.Day24(),new _2020.Day25()};
            string[] inputs = new[] { _2020.inputs.D1P1, _2020.inputs.D1P1,
            _2020.inputs.D2P1, _2020.inputs.D2P1,
            _2020.inputs.D3P1, _2020.inputs.D3P1,
            _2020.inputs.D4P1, _2020.inputs.D4P1,
            _2020.inputs.D5P1, _2020.inputs.D5P1,
            _2020.inputs.D6P1, _2020.inputs.D6P1,
            _2020.inputs.D7P1, _2020.inputs.D7P1,
            _2020.inputs.D8P1, _2020.inputs.D8P1,
            _2020.inputs.D9P1, _2020.inputs.D9P1,
            _2020.inputs.D10P1, _2020.inputs.D10P1,
            _2020.inputs.D11P1, _2020.inputs.D11P1,
            _2020.inputs.D12P1, _2020.inputs.D12P1,
            _2020.inputs.D13P1, _2020.inputs.D13P1,
            _2020.inputs.D14P1, _2020.inputs.D14P1,
            _2020.inputs.D15P1, _2020.inputs.D15P1,
            _2020.inputs.D16P1, _2020.inputs.D16P1,
            _2020.inputs.D17P1, _2020.inputs.D17P1,
            _2020.inputs.D18P1, _2020.inputs.D18P1,
            _2020.inputs.D19P1, _2020.inputs.D19P1,
            _2020.inputs.D20P2, _2020.inputs.D20P2,
            _2020.inputs.D21P1, _2020.inputs.D21P1,
            _2020.inputs.D22P1, _2020.inputs.D22P1,
            _2020.inputs.D23P1, _2020.inputs.D23P1,
            _2020.inputs.D24P1, _2020.inputs.D24P1,
            _2020.inputs.D25P1, _2020.inputs.D25P1};

            Solve(days, inputs);

        }

        private static void Solve2019()
        {
            List<IAoC> days = new List<IAoC> { new _2019.Day1(), new _2019.Day2(), new _2019.Day3(), new _2019.Day4(), new _2019.Day5(),
            new _2019.Day6(), new _2019.Day7(), new _2019.Day8(), new _2019.Day9(), new _2019.Day10(),
            new _2019.Day11(), new _2019.Day12(), new _2019.Day13(), new _2019.Day14(), new _2019.Day15()};
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
            _2019.inputs.D11P1, _2019.inputs.D11P1,
            _2019.inputs.D12P1, _2019.inputs.D12P1,
            _2019.inputs.D13P1, _2019.inputs.D13P1,
            _2019.inputs.D14P1, _2019.inputs.D14P1,
            _2019.inputs.D15P1, _2019.inputs.D15P1};

            Solve(days, inputs);

        }

        private static void Solve2018()
        {
            List<IAoC> days = new List<IAoC> { };
            string[] inputs = new string[] {};

            Solve(days, inputs);
        }

        private static void Solve2017()
        {
            List<IAoC> days = new List<IAoC> { };
            string[] inputs = new string[] { };

            Solve(days, inputs);
        }

        private static void Solve2016()
        {
            List<IAoC> days = new List<IAoC> { };
            string[] inputs = new string[] { };

            Solve(days, inputs);
        }

        private static void Solve2015()
        {
            List<IAoC> days = new List<IAoC> { };
            string[] inputs = new string[] { };

            Solve(days, inputs);
        }

        private static void Solve(List<IAoC> days, string[] inputs)
        {
            Stopwatch watch = new Stopwatch();

            for (int i = 0; i < days.Count; i++)
            {
                days[i].Tests();
                watch.Reset();
                Console.WriteLine($"DAY{i + 1}");
                watch.Start();
                Console.WriteLine(days[i].SolvePart1(inputs[2 * i]) + " " + watch.Elapsed);
                watch.Restart();
                Console.WriteLine(days[i].SolvePart2(inputs[2 * i + 1]) + " " + watch.Elapsed);
                watch.Stop();
                Console.WriteLine("__________________________________");
            }
        }

    }
}
