using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day9 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] inputvalues = input.Split(Environment.NewLine);
            int store = int.Parse(inputvalues[0]);
            List<long> values = new List<long>();
            List<long> valueslong = new List<long>();

            for (int i = 1; i < inputvalues.Length; i++)
            {
                valueslong.Add(long.Parse(inputvalues[i]));
            }
            return "" + findInvalidNumber(ref values, store, valueslong);
            
        }

        public long findInvalidNumber(ref List<long> values, int store, List<long> inputvalues)
        {
            int pointer = 0;
            while (values.Count < store)
            {
                values.Add(inputvalues[pointer]);
                pointer++;
            }

            while (true)
            {
                long value = inputvalues[pointer];
                if (!CheckIfSum(ref values, value))
                {
                    return value;
                }
                values.RemoveAt(0);
                values.Add(value);
                pointer++;
            }
        }
        public bool CheckIfSum(ref List<long> values, long value)
        {
            foreach (long n1 in values)
            {
                foreach (long n2 in values)
                {
                    if (n1+n2==value && n1 != n2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string SolvePart2(string input = null)
        {
            string[] inputvalues = input.Split(Environment.NewLine);
            int store = int.Parse(inputvalues[0]);
            List<long> values = new List<long>();
            List<long> valueslong = new List<long>();

            for (int i = 1; i < inputvalues.Length; i++)
            {
                valueslong.Add(long.Parse(inputvalues[i]));
            }
           
            List<long> set = FindContinguousSet(findInvalidNumber(ref values, store, valueslong), valueslong);

            return "" + (set.Min()+set.Max());
        }

        public List<long> FindContinguousSet(long sum, List<long> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                List<long> Values = new List<long> { inputs[i] };
                while (Values.Sum()<sum)
                {
                    for (int j=1; j< inputs.Count-i; j++)
                    {
                        Values.Add(inputs[i + j]);
                        if (Values.Sum() == sum)
                        {
                            return Values;
                        }
                    }
                }
            }
            return null;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"5
35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576") == "127");

            Debug.Assert(SolvePart2(@"5
35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576") == "62");
        }
    }
}
