using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day3 : General.PuzzleWithStringArrayInput
    {
        public Day3() : base(3)
        {

        }
        public override string SolvePart1(string[] input)
        {
            int[] CountOnes = new int[input[0].Length];
           
            for (int i = 0; i < input[0].Length; i++)
            {
                CountOnes[i] = input.Count(x => x[i] == '1');
            }

            string result = new(CountOnes.Select(x => x >= input.Length / 2 ? '1':'0').ToArray()); 
            string resultinv = new(CountOnes.Select(x => x < input.Length / 2 ? '1' : '0').ToArray());


            int gammaRate = Convert.ToInt32(result, 2);
            int epsilonRate = Convert.ToInt32(resultinv, 2);

            return (gammaRate * epsilonRate).ToString();
        }

        public override string SolvePart2(string[] input)
        {

            List<string> possibleOx = input.ToList();
            string OX = ReduceList(possibleOx, 0, false);

            List<string> possibleCO = input.ToList();
            string CO = ReduceList(possibleOx, 0, true);

            int ox = Convert.ToInt32(OX, 2);
            int co = Convert.ToInt32(CO, 2);

            return (ox * co).ToString();

        }

        private string ReduceList(List<string> possibleOx, int index, bool invert)
        {
            char MostCommon = FindMostCommon(possibleOx, index);
            if (invert)
            {
            MostCommon= MostCommon == '1' ? '0' : '1';
            }
            possibleOx = possibleOx.Where(x => x[index] == MostCommon).ToList();
            if (possibleOx.Count==1)
            {
                return possibleOx[0];
            }
            return ReduceList(possibleOx, index + 1, invert);
        }

        private char FindMostCommon(List<string> possibleOx, int index)
        {
            int CounterOnes = possibleOx.Count(x => x[index] == '1');
            if (CounterOnes*2>=possibleOx.Count)
            {
                return '1';
            }
            return '0';
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010") =="198");

            Debug.Assert(SolvePart2(@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010") == "230");
        }
    }
}
