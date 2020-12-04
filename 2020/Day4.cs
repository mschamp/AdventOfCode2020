using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace _2020
{
    public class Day4 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] pasports = input.Split(Environment.NewLine + Environment.NewLine);
            List<string> Valid = new List<string> {"ecl","pid","eyr","hcl","byr","iyr","hgt" };
            int PasOK = 0;
            foreach (string item in pasports)
            {
                int OKParts = 0;
                Regex reg = new Regex(@"(.{3}):(.+?)\s");
                foreach (Match part in reg.Matches(item+ " "))
                {
                    if (Valid.Contains(part.Groups[1].Value))
                    {
                        OKParts++;
                    }
                }
                if (OKParts==7)
                {
                    PasOK++;
                }
            }
            return "" + PasOK;
        }

        public string SolvePart2(string input = null)
        {
            string[] pasports = input.Split(Environment.NewLine + Environment.NewLine);
            List<string> eye = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            int PasOK = 0;
            foreach (string item in pasports)
            {
                List<string> matched = new List<string>();
                Regex reg = new Regex(@"(.{3}):(.+?)\s");
                foreach (Match part in reg.Matches(item + " "))
                {
                    switch (part.Groups[1].Value.Trim())
                    {
                        case "ecl":
                            if (eye.Contains(part.Groups[2].Value.Trim()))
                            {
                                matched.Add("ecl");
                            }
                            break;
                        case "pid":
                            Regex pid = new Regex(@"\d{9}");
                            if (pid.IsMatch(part.Groups[2].Value))
                            {
                                matched.Add("pid");
                            }
                            break;
                        case "eyr":
                            if (Enumerable.Range(2020, 2030).Contains(int.Parse(part.Groups[2].Value)))
                            {
                                matched.Add("eyr");
                            }
                            break;
                        case "hcl":
                            Regex hair = new Regex(@"#[0-9|a-f]{6}\s");
                            if (hair.IsMatch(part.Groups[2].Value+" "))
                            {
                                matched.Add("hcl");
                            }
                            break;
                        case "byr":
                            if (Enumerable.Range(1920, 2002).Contains(int.Parse(part.Groups[2].Value)))
                            {
                                matched.Add("byr");
                            }
                            break;
                        case "iyr":
                            if (Enumerable.Range(2010, 2020).Contains(int.Parse(part.Groups[2].Value)))
                            {
                                matched.Add("iyr");
                            }
                            break;
                        case "hgt":
                            Regex hgt = new Regex(@"(\d+)");
                            Match value = hgt.Match(part.Groups[2].Value);
                            if ((part.Groups[2].Value.Trim().EndsWith("cm") && Enumerable.Range(150, 193).Contains(int.Parse(value.Groups[1].Value))))
                            {
                                matched.Add("hgt");
                            }
                            else if ((part.Groups[2].Value.Trim().EndsWith("in") && Enumerable.Range(59, 76).Contains(int.Parse(value.Groups[1].Value))))
                            {
                                matched.Add("hgt");
                            }
                            break;
                    }
                }
                if (matched.Count() == 7 && matched.Distinct().Count() == 7)
                {
                    PasOK++;
                }
            }
            return "" + PasOK;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr: 1937 iyr: 2017 cid: 147 hgt: 183cm

iyr: 2013 ecl: amb cid: 350 eyr: 2023 pid: 028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr: 2024
ecl: brn pid: 760753108 byr: 1931
hgt: 179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl: brn hgt: 59in ") == "2");

            Debug.Assert(SolvePart2(@"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007") == "0");

            Debug.Assert(SolvePart2(@"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719") == "4");
        }
    }
}
