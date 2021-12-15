using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day7 : General.PuzzleWithIntegerArrayInput
    {
        public Day7() : base(7)
        {

        }
        public override string SolvePart1(int[] input)
        {
            int min = input.Min();
            int max = input.Max();

            int[] costs = new int[max - min];

            for (int i = 0; i < costs.Length; i++)
            {
                costs[i] = input.Select(x => Math.Abs(min + i - x)).Sum();
            }

            return costs.Min().ToString();
        }

        public override string SolvePart2(int[] input)
        {
            int min = input.Min();
            int max = input.Max();

            int[] costs = new int[max - min];

            for (int i = 0; i < costs.Length; i++)
            {
                costs[i] = input.Select(x => CalculateCost(min + i, x)).Sum();
            }

            return costs.Min().ToString();
        }

        public int CalculateCost(int Position, int Goal)
        {
            int distance = Math.Abs(Goal - Position);
            int Cost = distance * (distance + 1)/2 ;
            return Cost;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("16,1,2,0,4,2,7,1,2,14") == "37");
            Debug.Assert(SolvePart2("16,1,2,0,4,2,7,1,2,14") == "168");
        }
    }
}
