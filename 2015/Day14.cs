using System.Collections.Generic;
using System.Linq;

namespace _2015
{
	public class Day14 : General.PuzzleWithObjectArrayInput<Day14.Deer>
    {
        public Day14():base(14, 2015)
            {
            }
        public class Deer
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

        public override string SolvePart1(Deer[] input)
        {
            List<Deer> deers = input.ToList();

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

        public override string SolvePart2(Deer[] input)
        {
            Dictionary<Deer,int> deers = input.ToDictionary(x=> x,x => 0);

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

        public override void Tests()
        {
            System.Diagnostics.Debug.Assert(Race(new Deer("Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds."), 1000) == 1120);
            System.Diagnostics.Debug.Assert(Race(new Deer("Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."), 1000) == 1056);
        }

        protected override Deer CastToObject(string RawData)
        {
            return new Deer(RawData);
        }
    }
}
