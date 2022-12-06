using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day6 : General.abstractPuzzleClass
    {
        public Day6() : base(6)
        {
        }

        public override string SolvePart1(string input = null)
        {
            for (int i = 3; i < input.Length; i++)
            {
                if ( input.Substring(i-3,4).Distinct().Count()==4)                 
                    return (i+1).ToString();
            }
            return "0";
        }

        public override string SolvePart2(string input = null)
        {
            for (int i = 13; i < input.Length; i++)
            {
                if (input.Substring(i - 13, 14).Distinct().Count() == 14)
                    return (i + 1).ToString();
            }
            return "0";
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("mjqjpqmgbljsphdztnvjfqwrcgsmlb") == "7");
            Debug.Assert(SolvePart1("bvwbjplbgvbhsrlpgdmjqwftvncz") == "5");
            Debug.Assert(SolvePart1("nppdvjthqldpwncqszvftbrmjlhg") == "6");
            Debug.Assert(SolvePart1("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg") == "10");
            Debug.Assert(SolvePart1("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw") == "11");

            Debug.Assert(SolvePart2("mjqjpqmgbljsphdztnvjfqwrcgsmlb") == "19");
            Debug.Assert(SolvePart2("bvwbjplbgvbhsrlpgdmjqwftvncz") == "23");
            Debug.Assert(SolvePart2("nppdvjthqldpwncqszvftbrmjlhg") == "23");
            Debug.Assert(SolvePart2("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg") == "29");
            Debug.Assert(SolvePart2("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw") == "26");
        }
    }
}
