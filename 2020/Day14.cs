using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day14 : General.PuzzleWithStringArrayInput
    {
        public Day14() : base(14) { }
        public override string SolvePart1(string[] input)
        {
            Func<string, long> FindMemory = GetMemoryAddress();
            Dictionary<long, long> Memory = new();
            string Mask="";
            foreach (var item in input)
            {
                if (item.StartsWith("mask"))
                {
                    Mask = item.Split("=").Last().Trim();
                    continue;
                }

                long Address = FindMemory(item);
                long Value = long.Parse(item.Split("=").Last().Trim());
                Memory[Address] = ApplyMask1(Mask, Value);

            }

            return Memory.Values.Sum().ToString();
        }

        private long ApplyMask1(string Mask, long Value)
        {
            string BinValue = (string)Convert.ToString(Value, 2);
            string Result = "";
            for (int i = 0; i < Mask.Length; i++)
            {
                if (Mask[Mask.Length - i - 1] == 'X')
                {
                    if (i < BinValue.Length)
                    {
                        Result += BinValue[BinValue.Length - i - 1];
                        continue;
                    }
                    Result += '0';
                    continue;
                }
                Result += Mask[Mask.Length - i - 1];

            }

            string End = "";
            for (int i = Result.Length - 1; i >= 0; i--)
            {
                End += Result[i];
            }

            return Convert.ToInt64(End, 2);
        }

        private string ApplyMask2(string Mask, long Value)
        {
            string BinValue = (string)Convert.ToString(Value, 2);
            string Result = "";
            for (int i = 0; i < Mask.Length; i++)
            {
                if (Mask[Mask.Length - i - 1] == '0')
                {
                    if (i < BinValue.Length)
                    {
                        Result += BinValue[BinValue.Length - i - 1];
                        continue;
                    }
                    Result += '0';
                    continue;
                }
                else if (Mask[Mask.Length - i - 1] == '1')
                {
                    Result += '1';
                }
                else if (Mask[Mask.Length - i - 1] == 'X')
                {
                    Result += 'X';
                }
            }

            string End = "";
            for (int i = Result.Length - 1; i >= 0; i--)
            {
                End += Result[i];
            }

            return End;
        }

        private Func<string, long> GetMemoryAddress()
        {
            Regex match = new(@"\[(\d+)\]");

            return input =>
            {
                return long.Parse(match.Match(input).Groups[1].Value);
            };

        }


        public override string SolvePart2(string[] input)
        {
            Func<string, long> FindMemory = GetMemoryAddress();
            Dictionary<long, long> Memory = new();
            string Mask = "";
            foreach (var item in input)
            {
                if (item.StartsWith("mask"))
                {
                    Mask = item.Split("=").Last().Trim();
                    continue;
                }

                long Address = FindMemory(item);
                long Value = long.Parse(item.Split("=").Last().Trim());

                long[] Addresses = GetAddresses(ApplyMask2(Mask, Address));
                foreach (long addr in Addresses)
                {
                    Memory[addr] = Value;
                }
                

            }

            return "" + Memory.Values.Sum();
        }

        private long[] GetAddresses(string Address)
        {
            List<long> addresses = new();
            foreach (var item in MakeCombinations(Address))
            {
                addresses.Add(Convert.ToInt64(item, 2));
            }
            return addresses.ToArray();
        }

        private string[] MakeCombinations(string Addresses)
        {
            if (!Addresses.Contains('X'))
            {
                return new[] { Addresses };
            }


            List<string> NewAddresses = new();
            int loc = Addresses.IndexOf("X");
            NewAddresses.AddRange(MakeCombinations(new StringBuilder(Addresses) { [loc] = '1' }.ToString()));
            NewAddresses.AddRange(MakeCombinations(new StringBuilder(Addresses) { [loc] = '0' }.ToString()));
            return NewAddresses.ToArray();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0") == "165");

            Debug.Assert(SolvePart2(@"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1") == "208");
        }
    }
}
