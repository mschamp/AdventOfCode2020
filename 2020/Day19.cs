using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day19 : General.PuzzleWithObjectInput<(Dictionary<int, List<Rule>> Rules, string[] messages)>
    {        
        public Day19() : base(19, 2020) { }
        public Dictionary<int, List<Rule>> DecodeRules(string input)
        {
            Dictionary<int, List<Rule>> result = new();
            foreach (string rule in input.Split(Environment.NewLine))
            {
                string[] parts = rule.Split(":");
                int number = int.Parse(parts[0]);
                if (parts[1].Contains('"'))
                {
                    result[number] = new List<Rule>() { new EndRule(number, parts[1]) };
                }
                else
                {
                    List<Rule> rules;
                    if (!result.TryGetValue(number, out rules))
                    {
                        rules = new List<Rule>();
                    }
                    foreach (string option in parts[1].Split("|"))
                    {
                        rules.Add(new IntermediateRule(number, option));

                    }

                    result[number] = rules;
                }
            }

            return result;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""

ababbb
bababa
abbbab
aaabbb
aaaabbb") == "2");

            Debug.Assert(SolvePart2(@"42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: ""a""
11: 42 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: ""b""
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1

abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba") == "12");

            
        }

        protected override (Dictionary<int, List<Rule>>,string[]) CastToObject(string RawData)
        {
            string[] inputParts = RawData.Split(Environment.NewLine + Environment.NewLine);
            Dictionary<int, List<Rule>> Rules = DecodeRules(inputParts[0]);
            string[] messages = inputParts[1].Split(Environment.NewLine);

            return (Rules,messages);
        }

        public override string SolvePart1((Dictionary<int, List<Rule>> Rules, string[] messages) input)
        {
            int Count = 0;
            foreach (string message in input.messages)
            {
                if (input.Rules[0][0].Match(message, input.Rules).Any(x => x.Length == 0))
                {
                    Count++;
                }
            }
            return Count.ToString();
        }

        public override string SolvePart2((Dictionary<int, List<Rule>> Rules, string[] messages) input)
        {
            Dictionary<int, List<Rule>> OverrideRules = DecodeRules(@"8: 42 | 42 8
11: 42 31 | 42 11 31");

            foreach (KeyValuePair<int, List<Rule>> item in OverrideRules)
            {
                input.Rules[item.Key] = item.Value;
            }

            int Count = 0;
            foreach (string message in input.messages)
            {
                if (input.Rules[0][0].Match(message, input.Rules).Any(x => x.Length == 0))
                {
                    Count++;
                }
            }
            return Count.ToString();
        }
    }

    public abstract class Rule
    {
        public int Number { get; set; }
        public abstract List<string> Match(string Message, Dictionary<int, List<Rule>> rules);
    }

    public class EndRule : Rule
    {
        public EndRule(int number, string text)
        {
            Number = number;
            Text = text.Trim()[1].ToString();
        }

        public string Text { get; set; }

        public override List<string> Match(string Message, Dictionary<int, List<Rule>> rules)
        {
            if (Message.StartsWith(Text))
            {
                return new List<string>() { Message.Substring(1) };
            }
            return new List<string>();
        }

        public override string ToString()
        {
            return Number + ":" + Text;
        }
    }

    public class IntermediateRule : Rule
    {
        public IntermediateRule(int number, string text)
        {
            Number = number;
            Seq = new List<int>();
            foreach (var item in text.Split(" ",StringSplitOptions.RemoveEmptyEntries))
            {
                Seq.Add(int.Parse(item));
            }
        }

        public List<int> Seq { get; set; }

        public override List<string> Match(string Message, Dictionary<int, List<Rule>> rules)
        {
            List<string> options = new() {Message};
            foreach (int id in Seq)
            {
                List<string> results = new();
                foreach (Rule rule in rules[id])
                {
                    foreach (string message in options)
                    {
                        results.AddRange(rule.Match(message, rules));
                    }    
                }
                options = results;
            }
            return options;
            
        }

        public override string ToString()
        {
            return Number + ":" + string.Join(" ",Seq);
        }
    }
}
