using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day14 : General.IAoC
    {
        private class Deer
        {
            public Deer(string data)
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(data, @"^(\w*).+?(\d+)\skm/s.+?(\d+).+?(\d+).*");
                Name = match.Groups[1].Value;
                Speed = int.Parse(match.Groups[2].Value);
                Duration = int.Parse(match.Groups[3].Value);
                Waiting = int.Parse(match.Groups[4].Value);
            }

            public string Name;
            public int Speed;
            public int Duration;
            public int Waiting;
            public int CurrentLocation;

            private int location;

            public void Step()
            {
                if (location<Duration)
                {
                    CurrentLocation += Speed;
                }
                location = (location + 1) % (Duration + Waiting);

            }

        }

        public string SolvePart1(string input = null)
        {
            List<Deer> deers = input.Split(Environment.NewLine)
    .Select(s => new Deer(s)).ToList();

            for (int i = 0; i < 2503; i++)
            {
                deers.ForEach(x=> x.Step());
            }

            return deers.Max(x => x.CurrentLocation).ToString();
        }

        private int Race(Deer deer, int Duration)
        {
            for (int i = 0; i < Duration; i++)
            {
                deer.Step();
            }
            

            return deer.CurrentLocation;
        }

        public string SolvePart2(string input = null)
        {
            Dictionary<Deer,int> deers = input.Split(Environment.NewLine)
    .Select(s => new Deer(s)).ToDictionary(x=> x,x => 0);

            for (int i = 0; i < 2503; i++)
            {
                foreach (Deer item in deers.Keys)
                {
                    item.Step();
                }

                List<Deer> winning = deers.Keys.Where(x => x.CurrentLocation == deers.Keys.Max(y => y.CurrentLocation)).ToList();
                winning.ForEach(x => deers[x]++);             

            }


            return deers.Max(x => x.Value).ToString();
        }

        public void Tests()
        {
            System.Diagnostics.Debug.Assert(Race(new Deer("Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds."), 1000) == 1120);
            System.Diagnostics.Debug.Assert(Race(new Deer("Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."), 1000) == 1056);
        }
    }
}
