using MoreLinq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace _2022
{
	public class Day13 : General.PuzzleWithObjectArrayInput<(Day13.Packet left, Day13.Packet right)>
    {
        public Day13() : base(13, 2022)
        {
        }

        protected List<Packet> ParsePacket(string input)
        {
            List<Packet> packets = [];
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '[':
                        int counter = 0;
                        int j;
                        for (j = i; j < input.Length; j++)
                        {
                            if (input[j] == '[') counter++;
                            if (input[j] == ']') { counter--; }
                            if (counter == 0) break;
                        }
                        packets.Add(new Packet() { Parts = ParsePacket(input[(i + 1)..j]), StringRepresentation = input[(i + 1)..j] });
                        i += j - i;
                        break;
                    case ']':
                        break;
                    case ',':
                        break;
                    default:
                        var stringDigits = string.Concat(input.Skip(i).TakeWhile(char.IsDigit));
                        i += stringDigits.Length;
                        packets.Add(new Packet { List = false, Value = int.Parse(stringDigits), StringRepresentation=stringDigits });
                        break;
                }
            }
            return packets;
        }


        public class Packet:IComparable<Packet>, IEqualityComparer<Packet>
        {
            public bool List { get; set; } = true;
            public List<Packet> Parts { get; set; } = [];
            public int Value { get; set; } = 0;
            public string StringRepresentation { get; set; }

            public int CompareTo(Packet other)
            {

                var t =  ComparePackets(this.Parts, other.Parts, out bool keepOn);
                if (keepOn) return 0;
                else if (t) return -1;
                else return 1;
            }

            public override int GetHashCode()
            {
                return GetHashCode(this);
            }

            public override bool Equals(object? obj)
            {
                return Equals(this,obj);
            }
            public bool Equals(Packet? x, Packet? y)
            {
                return x.StringRepresentation == y.StringRepresentation;
            }

            public int GetHashCode([DisallowNull] Packet obj)
            {
                return obj.StringRepresentation.Length;
            }

            public override string ToString()
            {
                return StringRepresentation;
            }
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]
") == "13");

            Debug.Assert(SolvePart2(@"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]
") == "140");
        }

        protected override (Packet left, Packet right) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine);

            var left =  ParsePacket(parts[0]);
            var right = ParsePacket(parts[1]);

            return (left.First(), right.First());
        }

        public override string SolvePart1((Packet left, Packet right)[] input)
        {
            int Sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (ComparePackets(input[i].left.Parts, input[i].right.Parts, out _)) Sum += i + 1;
            }
            return Sum.ToString();
        }

        public override string SolvePart2((Packet left, Packet right)[] input)
        {
            List<Packet> AllPackets = [];

            Packet p1 = ParsePacket("[[2]]").First();
            Packet p2 = ParsePacket("[[6]]").First();

            for (int i = 0; i < input.Length; i++)
            {
                AllPackets.Add(input[i].left);
                AllPackets.Add(input[i].right);
            }

            AllPackets.Add(p1);
            AllPackets.Add(p2);

            AllPackets.Sort();

            int result = 1;

            for (int i = 0; i < AllPackets.Count; i++)
            {
                if (AllPackets[i] == p1) result *= (i+1);
                if (AllPackets[i] == p2) result *= (i+1);

            }

            return result.ToString();
        }

        private static bool ComparePackets(List<Packet> left, List<Packet> right, out bool keepOn)
        {

            for (int i = 0; i < Math.Max(left.Count, right.Count); i++)
            {
                keepOn = false;
                if (i >= left.Count) return true;
                if (i >= right.Count) return false;
                if (!left[i].List && !right[i].List) //Both ints
                {
                    if (left[i].Value < right[i].Value) return true;
                    else if (left[i].Value == right[i].Value) continue;
                    else return false;
                }
                else if (left[i].List && right[i].List)
                {
                    var t = ComparePackets(left[i].Parts, right[i].Parts, out keepOn);
                    if (keepOn) continue;
                    else return t;
                }
                else
                {
                    List<Packet> tmp = [];
                    bool res;
                    if (!left[i].List)
                    {
                        tmp.Add(left[i]);
                        res = ComparePackets(tmp, right[i].Parts, out keepOn);
                        if (keepOn) continue;
                        else return res;
                    }
                    else
                    {
                        {
                            tmp.Add(right[i]);
                            res = ComparePackets(left[i].Parts, tmp, out keepOn);
                            if (keepOn) continue;
                            else return res;
                        }
                    }
                }
            }

            keepOn = true;
            return false;
        }
    }
}
