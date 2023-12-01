using System;
using System.Diagnostics;

namespace _2019
{
    public class Day1:General.PuzzleWithIntegerArrayInput
    {
        public Day1() : base(1, 2019) { }
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

        public override string SolvePart1(int[] input)
        {
            int sum = 0;
                foreach (int value in input)
                {
                    int Fuel = GetFuelForModule(value, 3, 2);
                    sum += Fuel;
            }
            return sum.ToString();
        }

        public override string SolvePart2(int[] input)
        {
            int sum = 0;
            foreach (int value in input)
            {
                    int Fuel = GetFuelForModule(value, 3, 2);


                    while (Fuel > 0)
                    {
                        sum += Fuel;
                        Fuel = GetFuelForFuel(Fuel, 3, 2);
                    }
                }
            return sum.ToString();
        }

        public override void Tests()
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

