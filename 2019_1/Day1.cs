using System;
using System.Diagnostics;

namespace _2019
{
    class Day1:IAoC
    {

        private  int GetFuelForModule(int mass, int divider, int substracter)
        {
            double sub = mass / divider;
            int partialResult = (int)Math.Ceiling(sub);
            return partialResult - substracter;
        }

        private  int GetFuelForFuel(int mass, int divider, int substracter)
        {
            double sub = mass / divider;
            int partialResult = (int)Math.Floor(sub);
            return partialResult - substracter;
        }

        public string SolvePart1(string input)
        {
            int sum = 0;
                foreach (string str in input.Split(Environment.NewLine))
                {
                    int Fuel = GetFuelForModule(int.Parse(str), 3, 2);
                    sum += Fuel;
            }
            return sum.ToString();
        }

        public string SolvePart2(string input)
        {
            int sum = 0;
                foreach (string str in input.Split(Environment.NewLine))
                {
                    int Fuel = GetFuelForModule(int.Parse(str), 3, 2);


                    while (Fuel > 0)
                    {
                        sum += Fuel;
                        Fuel = GetFuelForFuel(Fuel, 3, 2);
                    }
                }
            return sum.ToString();
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1("12") == "2");
            Debug.Assert(SolvePart1("14") == "2");
            Debug.Assert(SolvePart1("1969") == "654");
            Debug.Assert(SolvePart1("100756") == "33583");

            // part 02
            Debug.Assert(SolvePart2("12") == "2");
            Debug.Assert(SolvePart2("14") == "2");
            Debug.Assert(SolvePart2("1969") == "966");
            Debug.Assert(SolvePart2("100756") == "50346");
        }
    }
}

