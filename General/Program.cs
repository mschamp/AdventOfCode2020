using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace General
{
	class Program
    {
        private static DataAccess.ICachedInput _Cache;
		private static DataAccess.IHTMLReader _HTMLReader = new DataAccess.HTMLReader();
        private static bool AllUsers = false;
        static void Main(string[] args)
        {
            switch (ConfigurationManager.AppSettings["UsedCachingMethod"])
            {
                case "DB":
                    _Cache = new DataAccess.SQLiteDB();
					break;
                case "file":
                    _Cache = new DataAccess.FileStorage();
                    break;
                default:
                    break;
            }


            Dictionary<string, Action> years = new() {
				{ "2015",Solve2015},
				{ "2016",Solve2016},
				{ "2017",Solve2017},
				{ "2018",Solve2018},
				{ "2019",Solve2019},
				{ "2020",Solve2020},
				{ "2021",Solve2021},
				{ "2022",Solve2022},
				{ "2023",Solve2023},
                { "2024",Solve2024}
            };

            while (true)
            {
                Console.WriteLine("Year to solve: (exit to close, all to toggle all user inputs)");
                if (AllUsers) { Console.WriteLine("All user inputs currently active"); }
                string Year = Console.ReadLine();

                if (Year == "exit") break;
                if (Year == "all") { AllUsers = !AllUsers; continue; };
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
            List<IAoC> days =
			[new _2020.Day1(), new _2020.Day2(), new _2020.Day3(), new _2020.Day4(), new _2020.Day5(),
            new _2020.Day6(),new _2020.Day7(),new _2020.Day8(),new _2020.Day9(),new _2020.Day10(),
            new _2020.Day11(),new _2020.Day12(),new _2020.Day13(),new _2020.Day14(),new _2020.Day15(),
            new _2020.Day16(),new _2020.Day17(),new _2020.Day18(),new _2020.Day19(), new _2020.Day20(),
            new _2020.Day21(),new _2020.Day22(),new _2020.Day23(),new _2020.Day24(),new _2020.Day25()];

            Solve(days);

        }

        private static void Solve2019()
        {
            List<IAoC> days =
			[new _2019.Day1(), new _2019.Day2(), new _2019.Day3(), new _2019.Day4(), new _2019.Day5(),
            new _2019.Day6(), new _2019.Day7(), new _2019.Day8(), new _2019.Day9(), new _2019.Day10(),
            new _2019.Day11(), new _2019.Day12(), new _2019.Day13(), new _2019.Day14(), new _2019.Day15()];

            Solve(days);

        }

        private static void Solve2018()
        {
            List<IAoC> days = [];

            Solve(days);
        }

        private static void Solve2017()
        {
            List<IAoC> days = [];

            Solve(days);
        }

        private static void Solve2016()
        {
            List<IAoC> days = [new _2016.Day3(),
                new _2016.Day1(), new _2016.Day2(),new _2016.Day3()];

            Solve(days);
        }

        private static void Solve2024()
        {
            List<IAoC> days = [new _2024.Day14(),
                new _2024.Day1(), new _2024.Day2(),new _2024.Day3(),new _2024.Day4(),new _2024.Day5(),
            new _2024.Day6(),new _2024.Day7(),new _2024.Day8(),new _2024.Day9(),new _2024.Day10(),
            new _2024.Day11(),new _2024.Day12(),new _2024.Day13(),new _2024.Day14()];

            Solve(days);
        }

        private static void Solve2015()
        {
            List<IAoC> days =
			[new _2015.Day1(), new _2015.Day2(), new _2015.Day3(), new _2015.Day4(), new _2015.Day5(),
            new _2015.Day6(),new _2015.Day7(),new _2015.Day8(),new _2015.Day9(),new _2015.Day10(),
            new _2015.Day11(),new _2015.Day12(),new _2015.Day13(),new _2015.Day14(),new _2015.Day15(),
            new _2015.Day16()];

            Solve(days);
        }

        private static void Solve2021()
        {
            List<IAoC> days = [ new _2021.Day1(), new _2021.Day2(), new _2021.Day3(), new _2021.Day4(), new _2021.Day5(),
                new _2021.Day6(), new _2021.Day7(), new _2021.Day8(), new _2021.Day9(),
                new _2021.Day10(),new _2021.Day11(),new _2021.Day12(),new _2021.Day13(),new _2021.Day14(),
                new _2021.Day15(),new _2021.Day16(),
                new _2021.Day18()
            ];

            Solve(days);
        }

        private static void Solve2022()
        {
            List<IAoC> days = [new _2022.Day12(), new _2022.Day1(), new _2022.Day2(), new _2022.Day3(), new _2022.Day4(), new _2022.Day5(),
            new _2022.Day6(), new _2022.Day7(), new _2022.Day8(), new _2022.Day9(), new _2022.Day10(),
            new _2022.Day11(),new _2022.Day11(),new _2022.Day12(),new _2022.Day12(),new _2022.Day13()];

            Solve(days);
        }

		private static void Solve2023()
		{
			List<IAoC> days = [new _2023.Day1(), new _2023.Day2(), new _2023.Day3(), new _2023.Day4(), new _2023.Day5(), new _2023.Day6(),
			new _2023.Day7(), new _2023.Day8(),	new _2023.Day9(), new _2023.Day10(),new _2023.Day11(), new _2023.Day12(),
			new _2023.Day13(),	new _2023.Day14(),	new _2023.Day15(), new _2023.Day16(),	new _2023.Day17() ,new _2023.Day18(), 
                new _2023.Day19(), new _2023.Day20(), new _2023.Day21(), new _2023.Day22(), new _2023.Day24()
            ];

			Solve(days);
		}

		private static void Solve(List<IAoC> days)
        {
            Stopwatch watch = new();
			string DefaultUser = ConfigurationManager.AppSettings["DefaultUser"];

			for (int i = 0; i < days.Count; i++)
            {
                days[i].Tests();
                foreach ((string user, string data) in GetInput(days[i]))
                {
					Console.WriteLine($"DAY{days[i].Day}{(user == DefaultUser ? "" : $"({user})")}");
					watch.Reset();
					watch.Start();
					Console.WriteLine(days[i].SolvePart1(data) + " " + watch.Elapsed);
					watch.Restart();
					Console.WriteLine(days[i].SolvePart2(data) + " " + watch.Elapsed);
					watch.Stop();
					Console.WriteLine("__________________________________");
				}
				
            }
        }

        private static IEnumerable<(string,string)> GetInput(IAoC puzzle)
        {
            string DefaultUser = ConfigurationManager.AppSettings["DefaultUser"];
            IList<(string, string)> PuzzleInput;

			if (AllUsers)
            {
				if (_Cache.TryLoadPuzzleInputAllUsers(puzzle.Year, puzzle.Day, out PuzzleInput)) return PuzzleInput;
			}
            else
            {
				if (_Cache.TryLoadPuzzleInput(puzzle.Year, puzzle.Day, DefaultUser, out PuzzleInput)) return PuzzleInput;
			}
           

           PuzzleInput = _HTMLReader.GetInputData(puzzle.Day, puzzle.Year);
            if (PuzzleInput.Any())
            {
                _Cache.StorePuzzleInput(new Interfaces.PuzzleData()
                {
                    Day = puzzle.Day,
                    Year = puzzle.Year,
                    input = PuzzleInput.First().Item2,
                    user = DefaultUser
                });
            }

           return PuzzleInput;
        }

    }
}
