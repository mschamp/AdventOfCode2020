using System.Diagnostics;

namespace _2022
{
	public class Day6 : General.abstractPuzzleClass
    {
        public Day6() : base(6,2022)
        {
        }

        public override string SolvePart1(string input = null)
        {
            return LocationOfUniqueSubstring(4, input).ToString();
        }

        public override string SolvePart2(string input = null)
        {
            return LocationOfUniqueSubstring(14, input).ToString();
        }

        private int LocationOfUniqueSubstring(int length,string input)
        {
             for (int i = 0; i < input.Length-4; i++)
            {
                if (new HashSet<char>(input[i..(i+length)]).Count() == length)
                    return (i + length);
            }
            return 0;
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
