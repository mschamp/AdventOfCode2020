using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day10 : General.PuzzleWithLongArrayInput
    {
        Func<long, long> Tribonnaci = General.MathFunctions.Tribonnaci();

        public override string SolvePart1(long[] joltages)
        {
            int Difference1 = 0;
            int Difference3 = 1; //1 because of built-in adapter

            List<long> joltSteps = new();
            long prev = 0;
            
            foreach (var item in joltages.OrderBy(x => x))
            {
                if (item - prev == 1)
                {
                    Difference1++;
                }
                else if (item - prev == 3)
                {
                    Difference3++;
                }
                prev = item;
            }
            return (Difference1 * Difference3).ToString();
        }

        public override string SolvePart2(long[] joltages)
        {
            return Combinations(joltages.Distinct().OrderBy(x => x).ToList()).ToString();
        }
        
        private long Combinations(List<long> input)
        {
            Dictionary<int, int> _values = new();
            long last = 0;
            int OneCounter = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                if (input[i]-last==1)
                {
                    OneCounter++;
                }
                else
                {
                    if (_values.ContainsKey(OneCounter))
                    {
                        _values[OneCounter]++;
                    }
                    else
                    {
                        _values[OneCounter] = 1;
                    }
                    OneCounter = 0;
                }
                last = input[i];
            }

            if (_values.ContainsKey(OneCounter))
            {
                _values[OneCounter]++;
            }
            else
            {
                _values[OneCounter] = 1;
            }

            long product = 1;
            foreach (KeyValuePair<int,int> item in _values)
            {
                product *= (long)(Math.Pow(Tribonnaci(item.Key), item.Value));
            }
            return product;
            }

        //private long Combinations(long curValue, List<long> input)
        //{
        //Nice solution from woy
        //if (!input.Any())
        //    return 1;
        //if (_values.ContainsKey(curValue))
        //    return _values[curValue];
        //_values[curValue] = input
        //    .TakeWhile(x => x > curValue && x <= curValue + 3).Select((x, idx) => Combinations(x, input.Skip(idx + 1))).Sum();
        //return _values[curValue];
        //}

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3")=="220");

            Debug.Assert(SolvePart1(@"16
10
15
5
1
11
7
19
6
12
4") == "35");

            Debug.Assert(SolvePart2(@"16
10
15
5
1
11
7
19
6
12
4") == "8");

            Debug.Assert(SolvePart2(@"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3") == "19208");


        }
    }
}
