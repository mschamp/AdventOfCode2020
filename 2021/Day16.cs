using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day16 : General.PuzzleWithObjectInput<Day16.Packet>
    {
        public Day16() : base(16, 2021) { }

        protected override Packet CastToObject(string RawData)
        {
            string binaryString = string.Join(string.Empty, RawData.Select(c => HexCharToBitString(c,4)));
            int increment = 0;
            Packet Outer = GetNextPacket(binaryString, 0, ref increment);

            return Outer;
        }

        public string HexCharToBitString(char hex)
        {
            return Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2);
        }

        public string HexCharToBitString(char hex, int Length)
        {
            //Convert to bitstring, does not add leading 0, fill to a fixed length of "Length"
            return HexCharToBitString(hex).PadLeft(Length, '0');
        }

        public override string SolvePart1(Packet Input)
        {
            return Input.VersionSum.ToString();
        }

        public override string SolvePart2(Packet input)
        {
            return input.Value.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("8A004A801A8002F478") == "16");
            Debug.Assert(SolvePart1("620080001611562C8802118E34") == "12");
            Debug.Assert(SolvePart1("C0015000016115A2E0802F182340") == "23");
            Debug.Assert(SolvePart1("A0016C880162017C3686B18A3D4780") == "31");

            Debug.Assert(SolvePart2("C200B40A82") =="3");
            Debug.Assert(SolvePart2("04005AC33890") == "54");
            Debug.Assert(SolvePart2("880086C3E88112") == "7");
            Debug.Assert(SolvePart2("CE00C43D881120") == "9");
            Debug.Assert(SolvePart2("D8005AC2A8F0") == "1");
            Debug.Assert(SolvePart2("F600BC2D8F") == "0");
            Debug.Assert(SolvePart2("9C005AC2F8F0") == "0");
            Debug.Assert(SolvePart2("9C0141080250320F1802104A08") == "1");
        }

        private Packet GetNextPacket(string binaryString, int startPoint, ref int incrementBy)
        {
            int tmpInc = 0;
            Packet res = new();
            res.Version = Convert.ToInt32(binaryString.Substring(startPoint, 3), 2);
            tmpInc += 3;
            
            res.TypeID = Convert.ToInt32(binaryString.Substring(startPoint + tmpInc, 3), 2);
            tmpInc += 3;


            if (res.TypeID == 4)
            {
                StringBuilder ValueBuilder = new();
                do
                {
                    tmpInc++;
                    ValueBuilder.Append(binaryString.AsSpan(startPoint + tmpInc, 4));
                    tmpInc += 4;
                } while (binaryString[startPoint + tmpInc-5] == '1');//First bit == 1 means more values to add

                res.LiteralValue = Convert.ToInt64(ValueBuilder.ToString(), 2);
            }
            else
            {
                int subTmpInc = 0;
                if (binaryString[startPoint + tmpInc] == '0') //Next 15 bits encode total length in bits
                {
                    tmpInc++;
                    int totalLengthOfSubs = Convert.ToInt32(binaryString.Substring(startPoint + tmpInc, 15), 2);
                    tmpInc += 15;
                    while (subTmpInc < totalLengthOfSubs)
                    {
                        res.SubPackets.Add(GetNextPacket(binaryString, startPoint + tmpInc + subTmpInc, ref subTmpInc));
                    }
                }
                else //next 11 encode total number of subpackets
                {
                    tmpInc++;
                    int totalCountOfSubs = Convert.ToInt32(binaryString.Substring(startPoint + tmpInc, 11), 2);
                    tmpInc += 11;
                    for (int i = 0; i < totalCountOfSubs; i++)
                    {
                        res.SubPackets.Add(GetNextPacket(binaryString, startPoint + tmpInc + subTmpInc, ref subTmpInc));
                    } 
                }
                tmpInc += subTmpInc;
            }

            incrementBy += tmpInc;
            return res;
        }

        public class Packet
            {
            public int TypeID { get; set; }
            public int Version { get; set; }
            public long LiteralValue { get; set; }
            public List<Packet> SubPackets = new();
            public long VersionSum => Version + SubPackets.Sum(x => x.VersionSum);
            public long Value => (TypeID) switch
            {
                0 => SubPackets.Sum(x => x.Value),
                1 => SubPackets.Aggregate(1L, (acc, val) => (acc * val.Value)),
                2 => SubPackets.Min(x => x.Value),
                3 => SubPackets.Max(x => x.Value),
                4 => LiteralValue,
                5 => SubPackets[0].Value > SubPackets[1].Value ? 1 : 0,
                6 => SubPackets[0].Value < SubPackets[1].Value ? 1 : 0,
                7 => SubPackets[0].Value == SubPackets[1].Value ? 1 : 0,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
