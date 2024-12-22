using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2024
{
    public class Day22 : PuzzleWithLongArrayInput
    {
        public Day22():base(22,2024)
        {
            
        }
        public override string SolvePart1(long[] input)
        {
            return input.AsParallel().Sum(x => Enumerable.Range(0,2000).Aggregate(x, (x, y) => nextSecret(x))).ToString();
        }

        private long nextSecret(long n)
        {
            n ^= n * 64;
            n &= 0xFFFFFF;
            //n %= 16777216;

            n ^= n / 32;
            n &= 0xFFFFFF;
            //n %= 16777216;

            n ^= n * 2048;
            n &= 0xFFFFFF;
            //n %= 16777216;

            return n;
        }

        public override string SolvePart2(long[] input)
        {
            Dictionary<(long, long, long, long), long> patternsRoi = new();

            foreach (long init in input)
            {
                List<(long, long)> deltas = GetDeltas(init);
                HashSet<(long, long, long, long)> added = new();

                for (int idx = 0; idx < deltas.Count - 4; idx++)
                {
                    (long, long, long, long) pat = (deltas[idx].Item1, deltas[idx + 1].Item1, deltas[idx + 2].Item1, deltas[idx + 3].Item1);
                    if (!added.Contains(pat))
                    {
                        patternsRoi[pat] = patternsRoi.GetValueOrDefault(pat) + deltas[idx + 3].Item2;
                        added.Add(pat);
                    }
                }
            }
            return patternsRoi.Values.Max().ToString();
        }

        private List<(long, long)> GetDeltas(long n)
        {
            List<(long, long)> deltas = new List<(long, long)>();

            for (int i = 0; i < 2000; i++)
            {
                long b = n % 10;
                n = nextSecret(n);
                long nb = n % 10;

                deltas.Add((nb - b, nb));

            }

            return deltas;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"1
10
100
2024") == "37327623");

            Debug.Assert(SolvePart2(@"1
2
3
2024") == "23");
        }
    }
}
