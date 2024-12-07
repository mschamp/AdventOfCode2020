using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day7 : PuzzleWithObjectArrayInput<long[]>
    {
        public Day7() : base(7,2024)
        {
            
        }

        public override string SolvePart1(long[][] input)
        {
            return input.Where(x => IsPossible(x[1..], x[0],true,true,false)).Sum(x => x[0]).ToString();
        }

        public override string SolvePart2(long[][] input)
        {
            return input.Where(x => IsPossible(x[1..], x[0], true, true, true)).Sum(x => x[0]).ToString();
        }

        private bool IsPossible(long[] terms, long goal, bool Multiply, bool Sum, bool Concat)
        {
            if (terms.Length == 1) return terms[0] == goal;
            long last = terms[terms.Length - 1];
            long[] otherTerms = terms[0..(terms.Length - 1)];
            if (Multiply&& goal%last==0 && IsPossible(otherTerms,goal/last,Multiply,Sum,Concat))return true;
            if (Sum&&goal-last>0 && IsPossible(otherTerms,goal-last, Multiply, Sum, Concat))return true;
            if (Concat)
            {
                string slast=last.ToString();
                string sGoal = goal.ToString();
                if (sGoal.Length > slast.Length
                    && sGoal.EndsWith(slast)
                    && IsPossible(otherTerms,long.Parse(sGoal.Substring(0,sGoal.Length-slast.Length)),Multiply,Sum,Concat))return true;
            }
            return false;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20") == "3749");

            Debug.Assert(SolvePart2(@"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20") == "11387");
        }

        protected override long[] CastToObject(string RawData)
        {
            return RawData.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        }
    }
}
