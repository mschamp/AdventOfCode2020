using System.Collections.Generic;

namespace _2020
{
	public class Day15 : General.abstractPuzzleClass
    {
        public Day15() : base(15, 2020)
        {

        }
        public override string SolvePart1(string input = null)
        {
            
            return PlayGame(2020,input).ToString();
        }

        public override string SolvePart2(string input = null)
        {
            return PlayGame(30000000, input).ToString();
        }

        private int PlayGame(int NumberOfTurns, string input)
        {
            int Counter = 0;
            int LastNumber = 0;
            Dictionary<int, int> Numbers = [];
            foreach (var item in input.Split(","))
            {
                Counter++;
                LastNumber = int.Parse(item);
                Numbers[LastNumber] = Counter;
            }

            for (int i = Counter; i < NumberOfTurns; i++)
            {
                if (Numbers.ContainsKey(LastNumber))
                {
                    int next = i - Numbers[LastNumber];
                    Numbers[LastNumber] = i;
                    LastNumber = next;
                    continue;
                }
                else
                {
                    Numbers[LastNumber] = i;
                    LastNumber = 0;
                }
            }
            return LastNumber;
        }

        public override void Tests()
        {
            //Debug.Assert(SolvePart1("0,3,6") == "436");
            //Debug.Assert(SolvePart1("1,3,2") == "1");
            //Debug.Assert(SolvePart1("2,1,3") == "10");
            //Debug.Assert(SolvePart1("1,2,3") == "27");
            //Debug.Assert(SolvePart1("2,3,1") == "78");
            //Debug.Assert(SolvePart1("3,2,1") == "438");
            //Debug.Assert(SolvePart1("3,1,2") == "1836");

            //Debug.Assert(SolvePart2("0,3,6") == "175594");
            //Debug.Assert(SolvePart2("1,3,2") == "2578");
            //Debug.Assert(SolvePart2("2,1,3") == "3544142");
            //Debug.Assert(SolvePart2("1,2,3") == "261214");
            //Debug.Assert(SolvePart2("2,3,1") == "6895259");
            //Debug.Assert(SolvePart2("3,2,1") == "18");
            //Debug.Assert(SolvePart2("3,1,2") == "362");
        }
    }
}
