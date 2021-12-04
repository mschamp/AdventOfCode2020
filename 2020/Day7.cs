using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day7 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            Dictionary<string, Bag> bags = new();
            List<Bag> Outer = new();
            Regex rgx = new(@"(\d+)?\s?(\w+\s\w+)\sbags?");
            foreach  (string outer in input.Split(Environment.NewLine))
            {
                MatchCollection mtchs = rgx.Matches(outer);
                Bag Outbag;
                if (!bags.TryGetValue(mtchs[0].Groups[2].Value, out Outbag))
                {
                    Outbag = new Bag(mtchs[0].Groups[2].Value);
                    bags[mtchs[0].Groups[2].Value] = Outbag;
                }
                Outer.Add(Outbag);
                for (int i = 1; i < mtchs.Count; i++)
                {
                    if (mtchs[i].Groups[2].Value != "no other")
                    {
                        Bag inBag;
                        if (!bags.TryGetValue(mtchs[i].Groups[2].Value, out inBag))
                        {
                            inBag = new Bag(mtchs[i].Groups[2].Value);
                            bags[mtchs[i].Groups[2].Value] = inBag;
                        }
                        Outbag.Content[inBag] = int.Parse(mtchs[i].Groups[1].Value);
                    }
                }
            }

            return "" + (Outer.Count(x => x.canContain("shiny gold")) -1);
        }

        public string SolvePart2(string input = null)
        {
            Dictionary<string, Bag> bags = new();
            List<Bag> Outer = new();
            Regex rgx = new(@"(\d+)?\s?(\w+\s\w+)\sbags?");
            foreach (string outer in input.Split(Environment.NewLine))
            {
                MatchCollection mtchs = rgx.Matches(outer);
                Bag Outbag;
                if (!bags.TryGetValue(mtchs[0].Groups[2].Value, out Outbag))
                {
                    Outbag = new Bag(mtchs[0].Groups[2].Value);
                    bags[mtchs[0].Groups[2].Value] = Outbag;
                }
                Outer.Add(Outbag);
                for (int i = 1; i < mtchs.Count; i++)
                {
                    if (mtchs[i].Groups[2].Value != "no other")
                    {
                        Bag inBag;
                        if (!bags.TryGetValue(mtchs[i].Groups[2].Value, out inBag))
                        {
                            inBag = new Bag(mtchs[i].Groups[2].Value);
                            bags[mtchs[i].Groups[2].Value] = inBag;
                        }
                        Outbag.Content[inBag] = int.Parse(mtchs[i].Groups[1].Value);
                    }
                }
            }

            return "" + (bags["shiny gold"].canContainAmount() - 1);
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.") == "4");

            Debug.Assert(SolvePart2(@"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.") == "126");
        }

        public class Bag
        {
            public Bag(string color)
            {
                Content = new Dictionary<Bag, int>();
                Color = color;
            }

            public Dictionary<Bag,int> Content { get; set; }

            public string Color { get; set; }

            public override string ToString()
            {
                return Color;
            }

            public bool canContain(string color)
            {
                if (Color == color)
                {
                    return true;
                }
                return Content.Keys.Any(x => x.canContain(color));
            }

           public int canContainAmount()
            {
                int i = 1;
                foreach (KeyValuePair<Bag,int> item in Content)
                {
                    i += item.Value * item.Key.canContainAmount();
                }
                return i;
            }
        }
    }
}
