using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day13 : General.abstractPuzzleClass
    {
        public Day13() : base(13)
        {

        }

        public override string SolvePart1(string input = null)
        {
            long ArrivalTime = long.Parse(input.Split(Environment.NewLine)[0]);
            Dictionary<long, long> BusDeparts = new();
            foreach (string bus in input.Split(Environment.NewLine)[1].Split(","))
            {
                if (bus == "x")
                {
                    continue;
                }

                long ibus = long.Parse(bus);
                BusDeparts[ibus] = EarliestDepart(ibus, ArrivalTime);
            }

            KeyValuePair<long, long> earliest = BusDeparts.Where(x => x.Value== BusDeparts.Values.Min()).First();

            return ""+(earliest.Value-ArrivalTime)*earliest.Key;
        }

        private long EarliestDepart(long BusID, long ArrivalTime)
        {
            return (long)(Math.Ceiling((float)ArrivalTime / (float)BusID))*BusID;
        }

        public override string SolvePart2(string input = null)
        {
            Func<long, long,long> lmc = General.MathFunctions.findLCM();
            List<long> busses = new();
            foreach (string bus in input.Split(Environment.NewLine).Last().Split(","))
            {
                if (bus == "x")
                {
                    busses.Add(1);
                    continue;
                }
                busses.Add(long.Parse(bus));
            }

            long timestamp = 0;
            int BussesInShedule = 1;
            long TimeJump = busses[0]; //first bus always in schedule, jump in multiples of his interval
            while (BussesInShedule != busses.Count)
            {
                if (BusDepartsAdd(busses[BussesInShedule], timestamp + BussesInShedule))
                {
                    TimeJump = lmc(busses[BussesInShedule], TimeJump); //Next occurance of this situation is lmc of previous jump and current bus interval
                    BussesInShedule++;
                    continue;
                }

                timestamp += TimeJump;
                

            }

            return "" + timestamp;
        }

        private bool BusDepartsAdd(long busID, long Timestamp)
        {
            return Timestamp % busID == 0;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"939
7,13,x,x,59,x,31,19") == "295");

            Debug.Assert(SolvePart2(@"939
7,13,x,x,59,x,31,19") == "1068781");

            Debug.Assert(SolvePart2(@"17,x,13,19") == "3417");
            Debug.Assert(SolvePart2(@"67,7,59,61") == "754018");
            Debug.Assert(SolvePart2(@"67,x,7,59,61") == "779210");
            Debug.Assert(SolvePart2(@"67,7,x,59,61") == "1261476");
            Debug.Assert(SolvePart2(@"1789,37,47,1889") == "1202161486");
        }
    }
}
