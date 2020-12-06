using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day6 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            List<Group> groups = new List<Group>();
            foreach (string group in input.Split(Environment.NewLine + Environment.NewLine))
            {
                groups.Add(new Group(group));
            }
            return "" + groups.Sum(x => x.uniqueQuestionsAnyone);
        }

        public string SolvePart2(string input = null)
        {
            List<Group> groups = new List<Group>();
            foreach (string group in input.Split(Environment.NewLine + Environment.NewLine))
            {
                groups.Add(new Group(group));
            }
            return "" + groups.Sum(x => x.uniqueQuestionsEveryone());
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"abc")=="3");
            Debug.Assert(SolvePart1(@"a
b
c") == "3");
            Debug.Assert(SolvePart1(@"ab
ac") == "3");

            Debug.Assert(SolvePart1(@"a
a
a
a") == "1");

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
            Dictionary<char, int> LetterCounts = new Dictionary<char, int>();
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
