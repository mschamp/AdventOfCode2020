using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2019
{
    public class Day14 : General.IAoC
    {
        public int Day => 14;
        public int Year => 2019;

        public string SolvePart1(string input = null)
        {
            Dictionary<string, reaction> reactions = new();
            Regex rgx = new(@"(\d+)\s(\w+)");
            foreach (string outer in input.Split(Environment.NewLine))
            {
                MatchCollection mtchs = rgx.Matches(outer);
                reaction result;
                if (!reactions.TryGetValue(mtchs.Last().Groups[2].Value, out result))
                {
                    result = new reaction(mtchs.Last().Groups[2].Value);
                    reactions[result.Result] = result;
                }
                result.Amount = int.Parse(mtchs.Last().Groups[1].Value);

                for (int i = 0; i < mtchs.Count-1; i++)
                {
                    reaction reagense;
                    if (!reactions.TryGetValue(mtchs[i].Groups[2].Value, out reagense))
                    {
                        reagense = new reaction(mtchs[i].Groups[2].Value);
                        reactions[reagense.Result] = reagense;
                    }
                    result.Reagentia.Add(reagense, int.Parse(mtchs[i].Groups[1].Value));
                }
            }
            Dictionary<reaction, long> chem_needed = new() { { reactions["FUEL"], 1 } };
            return "" + requiredOres(chem_needed);
        }

        public long requiredOres(Dictionary<reaction, long> chem_needed)
        {
            long ore = 0;
            Dictionary<reaction, long> chem_available = new();
            while (chem_needed.Count>0)
            {
                KeyValuePair<reaction, long> Processing = chem_needed.First();
                long available;
                long needed = Processing.Value;
                if (chem_available.TryGetValue(Processing.Key, out available))
                {
                    if (available > Processing.Value)
                    {
                        chem_available[Processing.Key] -= needed;
                        chem_needed.Remove(Processing.Key);
                        continue;
                    }
                    else if (available == Processing.Value)
                    {
                        chem_available.Remove(Processing.Key);
                        chem_needed.Remove(Processing.Key);
                        continue;
                    }
                    else if (available < Processing.Value)
                    {
                        needed -= available;
                        chem_available.Remove(Processing.Key);

                    }
                }

                long num_produced = Processing.Key.Amount;
                chem_needed.Remove(Processing.Key);
                long num_Reactions = (long)Math.Ceiling((float)needed / (float)num_produced);
                chem_available[Processing.Key] = (num_Reactions * num_produced) - needed;
                foreach (KeyValuePair<reaction,int> chem in Processing.Key.Reagentia)
                {
                    if (chem.Key.Result=="ORE")
                    {
                        ore += chem.Value*num_Reactions;
                    }
                    else
                    {
                        if (chem_needed.ContainsKey(chem.Key))
                        {
                            chem_needed[chem.Key]+= chem.Value * num_Reactions;
                        }
                        else
                        {
                            chem_needed[chem.Key] = chem.Value * num_Reactions;
                        }
                    }
                }

            }
            return ore;
        }

        public string SolvePart2(string input = null)
        {
            Dictionary<string, reaction> reactions = new();
            Regex rgx = new(@"(\d+)\s(\w+)");
            foreach (string outer in input.Split(Environment.NewLine))
            {
                MatchCollection mtchs = rgx.Matches(outer);
                reaction result;
                if (!reactions.TryGetValue(mtchs.Last().Groups[2].Value, out result))
                {
                    result = new reaction(mtchs.Last().Groups[2].Value);
                    reactions[result.Result] = result;
                }
                result.Amount = int.Parse(mtchs.Last().Groups[1].Value);

                for (int i = 0; i < mtchs.Count - 1; i++)
                {
                    reaction reagense;
                    if (!reactions.TryGetValue(mtchs[i].Groups[2].Value, out reagense))
                    {
                        reagense = new reaction(mtchs[i].Groups[2].Value);
                        reactions[reagense.Result] = reagense;
                    }
                    result.Reagentia.Add(reagense, int.Parse(mtchs[i].Groups[1].Value));
                }
            }
            Dictionary<reaction, long> chem_needed = new() { { reactions["FUEL"], 1 } };

             long low = (long)(1e12 / requiredOres(chem_needed));
            long high = 10 * low;

            while (requiredOres(new Dictionary<reaction, long> { { reactions["FUEL"], high } })<1e12)
            {
                low = high;
                high = 2 * low;
            }
            long mid = 0;
            while (low<high-1)
            {
                mid = (int)((low + high) / 2);
                long ore = requiredOres(new Dictionary<reaction, long> { { reactions["FUEL"], mid } });
                if (ore < 1e12)
                {
                    low = mid;
                }
                else if (ore > 1e12)
                {
                    high = mid;
                }
                else
                    break;
            }
            return "" + (mid-1);


        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL") == "31");

            Debug.Assert(SolvePart1(@"9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL") == "165");

            Debug.Assert(SolvePart1(@"157 ORE => 5 NZVS
165 ORE => 6 DCFZ
44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
179 ORE => 7 PSHF
177 ORE => 5 HKGWZ
7 DCFZ, 7 PSHF => 2 XJWVT
165 ORE => 2 GPVTF
3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT") == "13312");

            Debug.Assert(SolvePart1(@"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
17 NVRVD, 3 JNWZP => 8 VPVL
53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
22 VJHF, 37 MNCFX => 5 FWMGM
139 ORE => 4 NVRVD
144 ORE => 7 JNWZP
5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
145 ORE => 6 MNCFX
1 NVRVD => 8 CXFTF
1 VJHF, 6 MNCFX => 4 RFSQX
176 ORE => 6 VJHF") == "180697");

            Debug.Assert(SolvePart1(@"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX") == "2210736");

            Debug.Assert(SolvePart2(@"157 ORE => 5 NZVS
165 ORE => 6 DCFZ
44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
179 ORE => 7 PSHF
177 ORE => 5 HKGWZ
7 DCFZ, 7 PSHF => 2 XJWVT
165 ORE => 2 GPVTF
3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT") == "82892753");

            Debug.Assert(SolvePart2(@"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
17 NVRVD, 3 JNWZP => 8 VPVL
53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
22 VJHF, 37 MNCFX => 5 FWMGM
139 ORE => 4 NVRVD
144 ORE => 7 JNWZP
5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
145 ORE => 6 MNCFX
1 NVRVD => 8 CXFTF
1 VJHF, 6 MNCFX => 4 RFSQX
176 ORE => 6 VJHF") == "5586022");

            Debug.Assert(SolvePart2(@"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX") == "460664");
        }
    }

    public class reaction
     {
        public reaction(string result)
        {
            Reagentia = new Dictionary<reaction, int>();
            Result = result;

        }
        public Dictionary<reaction, int> Reagentia { get; set; }

        public string Result { get; private set; }
        public int Amount { get; set; }

        public override string ToString()
        {
            return Amount + " " + Result;
        }

        //public long requiredOres()
        //{
        //    long amount = 0;
        //    foreach (KeyValuePair<reaction, int> item in Reagentia)
        //    {
        //        amount += (int)Math.Ceiling((float)item.Value/(float)Amount) * item.Key.requiredOres();
        //    }
        //    return Math.Max(1, amount);
        //}
    }
}
