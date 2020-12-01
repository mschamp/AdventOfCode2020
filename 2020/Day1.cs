using System;
using System.Diagnostics;

namespace _2020
{
    public class Day1 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            int[] values = Array.ConvertAll(input.Split(Environment.NewLine), s => int.Parse(s));
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if (values[i]+values[j]==2020)
                    {
                        return (values[i] * values[j]).ToString();
                    }
                }
            }
            return "";
        }

        public string SolvePart2(string input = null)
        {
            int[] values = Array.ConvertAll(input.Split(Environment.NewLine), s => int.Parse(s));
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    for (int k = 0; k < values.Length; k++)
                    {
                        if (values[i] + values[j] + values[k] == 2020)
                        {
                            return (values[i] * values[j] * values[k]).ToString();
                        }
                    }  
                }
            }
            return "";
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"1721
979
366
299
675
1456") == "514579");
        

        Debug.Assert(SolvePart2(@"1721
979
366
299
675
1456") == "241861950");
        }
}
}
