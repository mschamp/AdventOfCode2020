using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day16 : General.PuzzleWithObjectArrayInput<Day16.Aunt>
    {
        public Day16() : base(16) { }
        public class Aunt
        {
            Dictionary<string, int> things;
            public string Name;
            public Aunt(string[] data, string name)
            {
                things = data.ToDictionary(x => x.Split(":")[0].Trim(), x => int.Parse(x.Split(":")[1].Trim()));
                Name = name;
            }

            public Aunt(string line)
            {
                int start = line.IndexOf(':');

                things = line.Substring(start+1).Split(',').ToDictionary(x => x.Split(":")[0].Trim(), x => int.Parse(x.Split(":")[1].Trim()));
                Name = line.Substring(0,start);
            }

            public bool PossibleSender(Aunt sender)
            {
                foreach (var item in things.Keys)
                {
                    if (things[item] != sender.things[item])
                    {
                        return false;
                    }
                    
                }
                return true;
            }

            public bool PossibleSender2(Aunt sender)
            {
                foreach (var item in things.Keys)
                {
                    if (item == "cats" || item == "trees")
                    {
                        if (things[item] <= sender.things[item])
                        {
                            return false;
                        }
                    }
                    else if (item == "pomeranians" || item == "goldfish")
                    {
                        if (things[item] >= sender.things[item])
                        {
                            return false;
                        }
                    }
                    else
                    {

                        if (things[item] != sender.things[item])
                        {
                            return false;
                        }
                    }

                }
                return true;
            }
        }

        public override string SolvePart1(Aunt[] aunts)
        {
            Aunt sender = new(@"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1".Split(Environment.NewLine), "Sender");

            var pos = aunts.Where(x => x.PossibleSender(sender));

            return pos.First().Name;
        }

        public override string SolvePart2(Aunt[] aunts)
        {
            Aunt sender = new(@"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1".Split(Environment.NewLine), "Sender");

            var pos = aunts.Where(x => x.PossibleSender2(sender));

            return pos.First().Name;
        }

        public override void Tests()
        {
        }

        public override Aunt CastToObject(string RawData)
        {
            return new Aunt(RawData);
        }
    }
}
