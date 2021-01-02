using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace _2015
{
    public class Day4 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            int Counter = 0;
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash;
            while (true)
            {
                Counter++;
                hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + Counter));
                if (hash[0]==0&&hash[1]==0&&hash[2]<16)
                {
                    return "" + Counter;
                }
            }
        }

        public string SolvePart2(string input = null)
        {
            int Counter = 0;
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash;
            while (true)
            {
                Counter++;
                hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input + Counter));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] ==0)
                {
                    return "" + Counter;
                }
            }
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1("abcdef") == "609043");
        }
    }
}
