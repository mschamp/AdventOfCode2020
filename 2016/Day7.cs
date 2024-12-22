using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2016
{
    public class Day7:PuzzleWithObjectArrayInput<IP7Address>
    {
        public Day7():base(7,2016)
        {
            
        }

        public override string SolvePart1(IP7Address[] input)
        {
            return input.Count(x => x.SupportsTLS).ToString();
        }

        public override string SolvePart2(IP7Address[] input)
        {
            return input.Count(x => x.SupportsSSL).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"abba[mnop]qrst") == "1");
            Debug.Assert(SolvePart1(@"abcd[bddb]xyyx") == "0");
            Debug.Assert(SolvePart1(@"aaaa[qwer]tyui") == "0");
            Debug.Assert(SolvePart1(@"ioxxoj[asdfgh]zxcvbn") == "1");
        }

        protected override IP7Address CastToObject(string RawData)
        {
            return new IP7Address(RawData.Split(['[', ']']));
        }
    }

    public class IP7Address
    {
        public IP7Address(string[] parts)
        {
            this.parts = parts;
        }

        private bool containsMirrored(string str)
        {
            for (int i = 0; i < str.Length-3; i++)
            {
                if (str[i] == str[i+3] && str[i+1] == str[i+2] && str[i] != str[i+1]) return true;
            }
            return false;
        }

        public bool SupportsTLS
        {
            get
            {
                for (int i = 1; i < parts.Length; i+=2)
                {
                    if (containsMirrored(parts[i])) return false;
                }
                for (int i = 0; i < parts.Length; i += 2)
                {
                    if (containsMirrored(parts[i])) return true;
                }
                return false;
            }
        }

        public bool SupportsSSL
        {
            get
            {
                List<string> lookFor = new List<string>();

                for (int i = 0; i < parts.Length; i += 2)
                {
                    lookFor.AddRange(FindABAs(parts[i]));
                }

                List<string> inside = new List<string>();

                for (int i = 1; i < parts.Length; i += 2)
                {
                    inside.AddRange(FindBABs(parts[i]));
                }

                bool result = inside.Intersect(lookFor).Any();
                return result;
            }
        }

        private List<string> FindABAs(string input)
        {
            List<string> list = new List<string>();

            for (int index = 0; index < input.Length - 2; index++)
            {
                if (input[index] == input[index + 2] && input[index] != input[index + 1])
                {
                    list.Add(new string([input[index+1], input[index], input[index + 1]]));
                }
            }

            return list;
        }

        private List<string> FindBABs(string input)
        {
            List<string> list = new List<string>();

            for (int index = 0; index < input.Length - 2; index++)
            {
                if (input[index] == input[index + 2] && input[index] != input[index + 1])
                {
                    list.Add(new string([input[index], input[index+1], input[index]]));
                }
            }

            return list;
        }

        private bool MeetsSslCriteria(List<string> outsideList, List<string> insideList)
        {
            bool matchingPairs = false;

            for (int outsideIndex = 0; outsideIndex < outsideList.Count; outsideIndex++)
            {
                string outside = outsideList[outsideIndex]; // aba
                char out1 = outside[0]; // a
                char out2 = outside[1]; // b
                char out3 = outside[2]; // a

                for (int insideIndex = 0; insideIndex < insideList.Count; insideIndex++)
                {
                    string inside = insideList[insideIndex]; // bab
                    char in1 = inside[0]; // b
                    char in2 = inside[1]; // a
                    char in3 = inside[2]; // b

                    if (out1 == in2 && out2 == in1)
                    {
                        matchingPairs = true;
                        break;
                    }
                }

                if (matchingPairs)
                {
                    break;
                }
            }

            return matchingPairs;
        }
        private readonly string[] parts;
    }
}
