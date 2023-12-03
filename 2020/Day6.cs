using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2020
{
	public class Day6 : General.PuzzleWithObjectArrayInput<Group>
    {
        public Day6() : base(6, 2020) { }
        protected override Group CastToObject(string RawData)
        {
            return new Group(RawData);
        }

        public override string SolvePart1(Group[] input)
        {
            return input.Sum(x => x.uniqueQuestionsAnyone).ToString();
        }

        public override string SolvePart2(Group[] input)
        {
            return input.Sum(x => x.uniqueQuestionsEveryone()).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"abc")=="3");
            Debug.Assert(SolvePart1(@"a
b
c

") == "3");
            Debug.Assert(SolvePart1(new Group[] { new Group(@"ab
ac") }) == "3");

            Debug.Assert(SolvePart1(new Group[] { new Group(@"a
a
a
a") }) == "1");

            Debug.Assert(SolvePart1(@"b") == "1");

            Debug.Assert(SolvePart1(@"abc

a
b
c

ab
ac

a
a
a
a

b") == "11");

            Debug.Assert(SolvePart2(@"abc

a
b
c

ab
ac

a
a
a
a

b") == "6");
        }
    }

    public class Group
    {
        List<char> answers;
        int people = 0;
        public Group(string input)
        {
            answers = new List<char>();
            foreach (var item in input.Split(Environment.NewLine))
            {
                answers.AddRange(item.ToCharArray());
                people++;
            }
            
        }

        public int uniqueQuestionsAnyone
        {
            get
            {
                return answers.Distinct().Count();
            }

        }

        public int uniqueQuestionsEveryone()
        {
            Dictionary<char, int> LetterCounts = new();
            foreach (char ch in answers)
            {
                int Count;
                LetterCounts.TryGetValue(ch, out Count);
                Count++;
                LetterCounts[ch] = Count;
            }
            return LetterCounts.Where(x => x.Value == people).Count();
        }
    }
}
