using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day4 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] values = input.Split("-");
            int min = int.Parse(values[0]);
            int max = int.Parse(values[1]);

            int OK = 0;
            for (int i = min; i <= max; i++)
            {
                if (ValidPassword(i))
                {
                    OK++;
                };
            }
            return "" + OK;
        }

        private bool ValidPassword(int password)
        {
            string spassword = password.ToString();
            bool doubleValue = false;
            int Lastvalue = -1;
            foreach (char teken in spassword)
            {
                int value = int.Parse(teken.ToString());
                if (value < Lastvalue)
                {
                    return false;
                }
                else if(value== Lastvalue)
                {
                    doubleValue = true;
                }
                Lastvalue = value;
            }
            return doubleValue;
        }

        private bool ValidPassword2(int password)
        {
            string spassword = password.ToString();
            List<char> doubles = new List<char>();
            int Lastvalue = -1;
            foreach (char teken in spassword)
            {
                doubles.Add(teken);
                int value = int.Parse(teken.ToString());
                if (value < Lastvalue)
                {
                    return false;
                }
                Lastvalue = value;
            }
            return spassword.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).Any(x => x.Count==2);
        }

        public string SolvePart2(string input = null)
        {
            string[] values = input.Split("-");
            int min = int.Parse(values[0]);
            int max = int.Parse(values[1]);

            int OK = 0;
            for (int i = min; i <= max; i++)
            {
                if (ValidPassword2(i))
                {
                    OK++;
                };
            }
            return "" + OK;
        }

        public void Tests()
        {
            Debug.Assert(ValidPassword(111111) == true);
            Debug.Assert(ValidPassword(223450) == false);
            Debug.Assert(ValidPassword(123789) == false);

            Debug.Assert(ValidPassword2(112233) == true);
            Debug.Assert(ValidPassword2(123444) == false);
            Debug.Assert(ValidPassword2(111122) == true);

        }
    }
}
