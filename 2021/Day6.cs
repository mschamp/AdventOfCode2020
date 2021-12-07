using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day6 : General.PuzzleWithIntegerArrayInput
    {
        public override string SolvePart1(int[] input)
        {
            Dictionary<int,int> FishDict =input.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            long[] fish = new long[9];
            for (int i = 1; i <= FishDict.Keys.Max(); i++)
            {
                fish[i] = FishDict[i];
            }

            return Simulate(fish, 80).ToString();
        }

        private long Simulate(long[] Fishcount, int DaysToGo)
        {
            while (DaysToGo>0)
            {
                long ReproducingFish = Fishcount[0];

                for (int i = 1; i < Fishcount.Length; i++)
                {
                    Fishcount[i - 1] = Fishcount[i];
                }
                Fishcount[6] += ReproducingFish;
                Fishcount[8] = ReproducingFish;

                DaysToGo--;
            }
            
            return Fishcount.Sum();
        }

        public override string SolvePart2(int[] input)
        {
            Dictionary<int, int> FishDict = input.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            long[] fish = new long[9];
            for (int i = 1; i <= FishDict.Keys.Max(); i++)
            {
                fish[i] = FishDict[i];
            }
            return Simulate(fish, 256).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("3,4,3,1,2") == "5934");
        }
    }
}
