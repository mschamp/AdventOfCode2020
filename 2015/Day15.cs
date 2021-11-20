using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day15 : General.IAoC
    {
        private class ingredient
        {
            public ingredient()
            {

            }
            public ingredient(string data)
                {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(data, @"^(\w+).+?(-?\d+).+?(-?\d+).+?(-?\d+).+?(-?\d+).+?(-?\d+)");
                Name = match.Groups[1].Value;
                capacity = int.Parse(match.Groups[2].Value);
                durability = int.Parse(match.Groups[3].Value);
                flavor = int.Parse(match.Groups[4].Value);
                texture = int.Parse(match.Groups[5].Value);
                calories = int.Parse(match.Groups[6].Value);
            }

            string Name;
            public int capacity;
            public int durability;
            public int flavor;
            public int texture;
            public int calories;

            public static ingredient operator +(ingredient x, ingredient y)
            {
                return new ingredient
                {
                    capacity = x.capacity + y.capacity,
                    durability = x.durability + y.durability,
                    flavor = x.flavor + y.flavor,
                    texture = x.texture + y.texture,
                    calories = x.calories + y.calories
                };
            }

            public static ingredient operator *(ingredient x, int n)
            {
                return new ingredient
                {
                    capacity = x.capacity * n,
                    durability = x.durability * n,
                    flavor = x.flavor * n,
                    texture = x.texture * n,
                    calories = x.calories * n
                };
            }

            public long Score
            {
                get { return Math.Max(0, capacity) * Math.Max(0, texture) * Math.Max(0, flavor) * Math.Max(0, durability); }
            }
        }

        public string SolvePart1(string input = null)
        {
            List<ingredient> ingredients = input.Split(Environment.NewLine)
   .Select(s => new ingredient(s)).ToList();

            var ValidRecepts = Distribute4(100);
            var scores = ValidRecepts.Select(rec => CalculateScore(rec, ingredients));

            return scores.Max(x => x.Item1).ToString();
        }

        private Tuple<long,int> CalculateScore(int[] amount, List<ingredient> ingredients)
        {
            ingredient result = ingredients.Zip(amount, (ing, amo) => ing * amo).Aggregate((a, b) => a + b);

            return Tuple.Create(result.Score, result.calories);
        }

        public string SolvePart2(string input = null)
        {
            List<ingredient> ingredients = input.Split(Environment.NewLine)
    .Select(s => new ingredient(s)).ToList();

            var ValidRecepts = Distribute4(100);
            var scores = ValidRecepts.Select(rec => CalculateScore(rec, ingredients));

            return scores.Where(x => x.Item2==500).Max(x => x.Item1).ToString();
        }

        public void Tests()
        {
            System.Diagnostics.Debug.Assert(SolvePart1(@"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3") == "62842880");
        }

        IEnumerable<int[]> Distribute4(int max)
        {
            for (int a = 0; a <= max; a++)
                for (int b = 0; b <= max - a; b++)
                    for (int c = 0; c <= max - a - b; c++)
                        yield return new[] { a, b, c, max - a - b - c };
        }
    }
}
