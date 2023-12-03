using System.Diagnostics;

namespace _2022
{
	public class Day2 : General.PuzzleWithStringArrayInput
    {
        public Day2() : base(2, 2022)
        {
        }

        public override string SolvePart1(string[] input)
        {
            int scores = 0;
            foreach (string item in input)
            {
                hand p1 = ConvertChart(item[0], 0);
                hand p2 = ConvertChart(item[2], 1);

                scores += GetScore(p1, p2);
            }

            return scores.ToString();
        }

        private int GetScore(hand p1, hand p2)
        {
            return ScoreChoose(p2) + ScoreResult(p1, p2);
        }

        private int ScoreResult(hand p1, hand p2)
        {
            if (p1 == p2) return 3;
            if (p1 == hand.Rock && p2 == hand.Paper) return 6;
            if (p1 == hand.Paper && p2 == hand.Scissors) return 6;
            if (p1 == hand.Scissors && p2 == hand.Rock) return 6;
            return 0;
        }

        private int ScoreChoose(hand p2)
        {
            return (int)p2+1;
        }

        private hand ConvertChart(char value, int offset)
        {
            return (hand)((value - 'A' + offset) % 3);
        }

        public override string SolvePart2(string[] input)
        {
            int scores = 0;
            foreach (string item in input)
            {
                hand p1 = ConvertChart(item[0], 0);
                hand p2 = GetHandBasedOnResult(item[2], p1);
                scores += GetScore(p1, p2);
            }

            return scores.ToString();
        }

        private hand GetHandBasedOnResult(char value, hand OtherHand)
        {
            int Difference = (value - 'Y');
            return (hand)((3+(int)OtherHand + Difference)%3);
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"A Y
B X
C Z") == "15");

            Debug.Assert(SolvePart2(@"A Y
B X
C Z") == "12");
        }


        private enum hand
        {
            Rock = 0,
            Paper = 1,
            Scissors = 2
        }
    }
}
