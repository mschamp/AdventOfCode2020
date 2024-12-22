using General;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2016
{
    public class Day4 : PuzzleWithObjectArrayInput<Room>
    {
        public Day4() : base(4, 2016)
        {
        }

        public override string SolvePart1(Room[] input)
        {
            return input.Where(x=>x.Valid).Sum(x=>x.ID).ToString();
        }

        public override string SolvePart2(Room[] input)
        {
            var t = input.First(x => x.decypher().Equals("northpole object storage",StringComparison.Ordinal));
            return t.ID.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"aaaaa-bbb-z-y-x-123[abxyz]
a-b-c-d-e-f-g-h-987[abcde]
not-a-real-room-404[oarel]
totally-real-room-200[decoy]") == "1514");
        }

        Regex rgx = new Regex(@"^(.+)-(\d+)\[(\w+)\]",RegexOptions.Compiled);
        protected override Room CastToObject(string RawData)
        {
            Match mt= rgx.Match(RawData);
            return new Room(mt.Groups[1].Value, int.Parse(mt.Groups[2].Value), mt.Groups[3].Value);
        }
    }

    public class Room
    {
        public Room(string roomname, int iD, string checksum)
        {
            Roomname = roomname;
            ID= iD;
            Checksum = checksum;
        }

        public string Roomname{get;private set;}
        public int ID { get; private set; }
        public string Checksum { get; private set; }

        public string CalculateChecksum()
        {
            return new string(Roomname.Replace("-","").GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Take(5).Select(x=>x.Key).ToArray());
        }

        public bool Valid => CalculateChecksum() == Checksum;

        public string decypher()
        {
            int rotateCount = ID % 26;
            return new string(Roomname.Select(x => rotate(x, rotateCount)).ToArray());
        }


        public char rotate(char c, int iterations )
        {
            if (c == '-') return ' ';
            else
            {
                int cN = (c + iterations);
                if (cN > 'z') cN -= 'z'-'a'+1;
                return (char)cN;
            }
        }
    }
}
