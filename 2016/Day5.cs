using General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _2016
{
    public class Day5:abstractPuzzleClass
    {
        public Day5():base(5,2016)
        {
            
        }

        public override string SolvePart1(string input = null)
        {
            char[] pasword = new char[8];
            int charCount = 0;
            int i = 0;
            while (true)
            {
                string r = CreateMD5(input + i.ToString());
                if (r.StartsWith("00000"))
                {
                    pasword[charCount]= r[5];
                    charCount++;
                    if (charCount == 8)
                        return new string(pasword).ToLower();
                }
                i++;
            }
        }

        public override string SolvePart2(string input = null)
        {
            char[] pasword = new char[8];
            int charCount = 0;
            int i = 0;
            while (true)
            {
                string r = CreateMD5(input + i.ToString());
                if (r.StartsWith("00000"))
                {
                    int index = r[5] - '0';
                    if (index >= 0 && index < pasword.Length && pasword[index]==default) 
                    {
                        pasword[index] = r[6];
                    }
                    if (pasword.All(x=>x!=default))
                        return new string(pasword).ToLower();
                }
                i++;
            }
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("abc") == "18f47a30");
            Debug.Assert(SolvePart2("abc") == "05ace8e3");
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); 
            }
        }
    }
}
