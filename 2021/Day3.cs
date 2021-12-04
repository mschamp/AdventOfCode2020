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
        public override string SolvePart1(string[] input)
        {
            int[] CountOnes = new int[input[0].Length];
           
            for (int i = 0; i < input[0].Length; i++)
            {
                CountOnes[i] = input.Count(x => x[i] == '1');
            }

            string result = new string(CountOnes.Select(x => x >= input.Length / 2 ? '1':'0').ToArray()); 
            string resultinv = new string(CountOnes.Select(x => x < input.Length / 2 ? '1' : '0').ToArray());


            int gammaRate = Convert.ToInt32(result, 2);
            int epsilonRate = Convert.ToInt32(resultinv, 2);

            return (gammaRate * epsilonRate).ToString();
        }

        public override string SolvePart2(string[] input)
        {

            //           List<string> possibleOx = input.ToList();

            //int k = 0;
            //while (possibleOx.Count>1)
            //{
            //    int[] CountOnes = new int[input[0].Length];

            //    for (int i = 0; i < input[0].Length; i++)
            //    {
            //        CountOnes[i] = possibleOx.Count(x => x[i] == '1');
            //    }

            //    string result = new string(CountOnes.Select(x => x >= possibleOx.Count / 2 ? '1' : '0').ToArray());
            //    possibleOx = possibleOx.Where(x => x[k] == result[k]).ToList();
            //    k++;
            //}

            //List<string> possibleCO = input.ToList();
            //k = 0;
            //while (possibleCO.Count > 1)
            //{
            //    int[] CountOnes = new int[input[0].Length];

            //    for (int i = 0; i < input[0].Length; i++)
            //    {
            //        CountOnes[i] = possibleCO.Count(x => x[i] == '1');
            //    }
            //    string resultinv = new string(CountOnes.Select(x => x < possibleCO.Count / 2 ? '1' : '0').ToArray());
            //    possibleCO = possibleCO.Where(x => x[k] == resultinv[k]).ToList();
            //    k++;
            //}

            //int ox = Convert.ToInt32(possibleOx[0], 2);
            //int co = Convert.ToInt32(possibleCO[0], 2);

            //return (ox * co).ToString();
            return "";

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
