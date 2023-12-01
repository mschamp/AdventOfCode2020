using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace General
{
    class Program
    {
        private static DataAccess.IDB _DB = new DataAccess.SQLiteDB();
		private static DataAccess.IHTMLReader _HTMLReader = new DataAccess.HTMLReader();
        static void Main(string[] args)
        {
			Dictionary<string, Action> years = new() {
				{ "2015",Solve2015},
				{ "2016",Solve2016},
				{ "2017",Solve2017},
				{ "2018",Solve2018},
				{ "2019",Solve2019},
				{ "2020",Solve2020},
				{ "2021",Solve2021},
				{ "2022",Solve2022},
				{ "2023",Solve2023}
			};

            while (true)
            {
                Console.WriteLine("Year to solve: (exit to close)");
                string Year = Console.ReadLine();

                if (Year == "exit") break;

                if (years.TryGetValue(Year, out Action YearClass))
                {
                    YearClass();
                }
                else
                {
                    years.Values.ToList().ForEach(x => x());
                }
            }
            
        }

        private static void Solve2020()
        {
            List<IAoC> days = new()
            { new _2020.Day1(), new _2020.Day2(), new _2020.Day3(), new _2020.Day4(), new _2020.Day5(),
            new _2020.Day6(),new _2020.Day7(),new _2020.Day8(),new _2020.Day9(),new _2020.Day10(),
            new _2020.Day11(),new _2020.Day12(),new _2020.Day13(),new _2020.Day14(),new _2020.Day15(),
            new _2020.Day16(),new _2020.Day17(),new _2020.Day18(),new _2020.Day19(), new _2020.Day20(),
            new _2020.Day21(),new _2020.Day22(),new _2020.Day23(),new _2020.Day24(),new _2020.Day25()};

            Solve(days);

        }

        private static void Solve2019()
        {
            List<IAoC> days = new()
            { new _2019.Day1(), new _2019.Day2(), new _2019.Day3(), new _2019.Day4(), new _2019.Day5(),
            new _2019.Day6(), new _2019.Day7(), new _2019.Day8(), new _2019.Day9(), new _2019.Day10(),
            new _2019.Day11(), new _2019.Day12(), new _2019.Day13(), new _2019.Day14(), new _2019.Day15()};

            Solve(days);

        }

        private static void Solve2018()
        {
            List<IAoC> days = new() { };

            Solve(days);
        }

        private static void Solve2017()
        {
            List<IAoC> days = new() { };

            Solve(days);
        }

        private static void Solve2016()
        {
            List<IAoC> days = new() { };

            Solve(days);
        }

        private static void Solve2015()
        {
            List<IAoC> days = new()
            { new _2015.Day1(), new _2015.Day2(), new _2015.Day3(), new _2015.Day4(), new _2015.Day5(),
            new _2015.Day6(),new _2015.Day7(),new _2015.Day8(),new _2015.Day9(),new _2015.Day10(),
            new _2015.Day11(),new _2015.Day12(),new _2015.Day13(),new _2015.Day14(),new _2015.Day15(),
            new _2015.Day16()};

            Solve(days);
        }

        private static void Solve2021()
        {
            List<IAoC> days = new() { new _2021.Day1(), new _2021.Day2(), new _2021.Day3(), new _2021.Day4(), new _2021.Day5(),
                new _2021.Day6(), new _2021.Day7(), new _2021.Day8(), new _2021.Day9(),
                new _2021.Day10(),new _2021.Day11(),new _2021.Day12(),new _2021.Day13(),new _2021.Day14(),
                new _2021.Day15(),new _2021.Day16(),
                new _2021.Day18()
            };

            Solve(days);
        }

        private static void Solve2022()
        {
            List<IAoC> days = new() { new _2022.Day1(), new _2022.Day2(), new _2022.Day3(), new _2022.Day4(), new _2022.Day5(),
            new _2022.Day6(), new _2022.Day7(), new _2022.Day8(), new _2022.Day9(), new _2022.Day10(),
            new _2022.Day11(),new _2022.Day11(),new _2022.Day12(),new _2022.Day12(),new _2022.Day13()};

            Solve(days);
        }

		private static void Solve2023()
		{
			List<IAoC> days = new() { new _2023.Day1(), new _2023.Day2()};

			Solve(days);
		}

		private static void Solve(List<IAoC> days)
        {
            Stopwatch watch = new();

            for (int i = 0; i < days.Count; i++)
            {
                days[i].Tests();
                watch.Reset();
                Console.WriteLine($"DAY{days[i].Day}");
                string input = GetInput(days[i]);
                watch.Start();
                Console.WriteLine(days[i].SolvePart1(input) + " " + watch.Elapsed);
                watch.Restart();
                Console.WriteLine(days[i].SolvePart2(input) + " " + watch.Elapsed);
                watch.Stop();
                Console.WriteLine("__________________________________");
            }
        }

        private static string GetInput(IAoC puzzle)
        {
            string DefaultUser = ConfigurationManager.AppSettings["DefaultUser"];
           if (_DB.LoadPuzzleInput(puzzle.Year, puzzle.Day, DefaultUser, out string PuzzleInput)) return PuzzleInput;

           PuzzleInput = _HTMLReader.GetInputData(puzzle.Day, puzzle.Year);
            _DB.StorePuzzleInput(new Interfaces.PuzzleData()
			{
				Day = puzzle.Day,
				Year = puzzle.Year,
				input = PuzzleInput,
				user = DefaultUser
			});

           return PuzzleInput;
        }

    }
}
