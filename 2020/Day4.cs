using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace _2020
{

    //Met dank aan rversteeg: https://github.com/rversteeg/AdventOfCode2020/blob/cb8dbf0162f22b134862a7b2133042e4d05d9e85/Day04
    //Heel veel uitgeleerd en mijn code kunnen optimaliseren
    public class Day4 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            List<string> Needed = new List<string> { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" };
            IEnumerable<Passport> Passports = ReadPassports(input);
            return "" + Passports.Count(Passport => Needed.All(item => Passport.Content.ContainsKey(item)));
        }

        public string SolvePart2(string input = null)
        {
            IEnumerable<Passport> Passports = ReadPassports(input);
            var Requirements = new Dictionary<string, Func<string, bool>>()
            {
                {"eyr",Validators.NumberValidator(2020,2030) },
                {"byr",Validators.NumberValidator(1920,2002) },
                {"iyr",Validators.NumberValidator(2010,2020) },
                {"ecl",Validators.ElementOfListValidator(new [] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" })},
                {"pid",Validators.RegexValidator(@"^\d{9}$") },
                {"hcl",Validators.RegexValidator(@"^#[0-9|a-f]{6}$") },
                {"hgt",Validators.HeightValidator()} 
            };

            return ""+Passports.Count(x => x.IsValid(Requirements));
        }

        private IEnumerable<Passport> ReadPassports(string input)
        {
            List<Passport> passports = new List<Passport>();
            foreach (string item in input.Split(Environment.NewLine + Environment.NewLine))
            {
                passports.Add(new Passport(item));
            }
            return passports;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in ") == "2");

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

    public class Passport
    {
        public Passport(string content)
        {
            Content = new Dictionary<string, string>();
            foreach (string item in content.Split())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] parts = item.Split(':');
                    Content[parts[0]] = parts[1];
                }

            } ;
        }

        public Dictionary<string, string> Content { get; private set; } 

        public bool IsValid(Dictionary<string, Func<string, bool>> Requirements)
        {
            return Requirements.All(req => this.Content.ContainsKey(req.Key) && req.Value(this.Content[req.Key]));
        }
    }

    public static class Validators
    {
        public static Func<string,bool> NumberValidator(int min, int max)
        {
            return input => int.TryParse(input, out int number) && (number >= min && number <= max);
        }

        public static Func<string,bool> ElementOfListValidator(string[] List)
        {
            return input => List.Contains(input);
        }

        public static Func<string, bool> RegexValidator(string Regex)
        {
            Regex rgx = new Regex(Regex);

            return input => rgx.IsMatch(input);
        }

        public static Func<string, bool> HeightValidator()
        {
            Func<string, bool> cmValidator = NumberValidator(150, 193);
            Func<string, bool> inValidator = NumberValidator(59, 76);

            Regex hgt = new Regex(@"^(\d+)(cm|in)$");

            return input =>
            {
                Match value = hgt.Match(input);
                return value.Success && ((value.Groups[2].Value == "cm" && cmValidator(value.Groups[1].Value)) || (value.Groups[2].Value == "in" && inValidator(value.Groups[1].Value)));
            };
        }
    }
}
