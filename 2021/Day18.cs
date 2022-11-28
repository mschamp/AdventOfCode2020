using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day18:General.PuzzleWithStringArrayInput
    {
        public Day18() : base(18) { }

        public Fish CastToObject(string RawData)
        {
            return new Fish(RawData);
        }

        public override string SolvePart1(string[] input)
        {
            return SolvePart1int(input).ToString();
        }

        private long SolvePart1int(string[] input)
        {
            Fish fish = CastToObject(input[0]);
            for (int i = 1; i < input.Length; i++)
            {
                fish += CastToObject(input[i]);
                while (Reduce(fish))
                {
                }
            }
            return fish.magnitude();
        }

        public override string SolvePart2(string[] input)
        {
            long Max = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    Max = Math.Max(Max, SolvePart1int(new string[] {input[i],input[j] }));
                }
            }

            return Max.ToString();

        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"[[1,2],[[3,4],5]]") == "143");
            Debug.Assert(SolvePart1(@"[[[[0,7],4],[[7,8],[6,0]]],[8,1]]") == "1384");
            Debug.Assert(SolvePart1(@"[[[[1,1],[2,2]],[3,3]],[4,4]]") == "445");
            Debug.Assert(SolvePart1(@"[[[[3,0],[5,3]],[4,4]],[5,5]]") == "791");
            Debug.Assert(SolvePart1(@"[[[[5,0],[7,4]],[5,5]],[6,6]]") == "1137");
            Debug.Assert(SolvePart1(@"[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]") == "3488");
            Debug.Assert(SolvePart1(@"[[[[4,3],4],4],[7,[[8,4],9]]]
[1,1]") == "1384");
            Debug.Assert(SolvePart1(@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]") == "3488");
            Debug.Assert(SolvePart1(@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]") == "4140");

            Debug.Assert(SolvePart2(@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]") == "3993");
        }

        private void Explode(Fish fish, List<Fish> NodeList)
        {

            int idLeft = NodeList.IndexOf(fish.Left);

            if (idLeft > 0)
            {
                NodeList[idLeft - 1].Value += fish.Left.Value;
            }

            int idRight = NodeList.IndexOf(fish.Right);
            if (idRight < NodeList.Count - 1)
            {
                NodeList[idRight + 1].Value += fish.Right.Value;
            }

            fish.Left = null;
            fish.Right = null;
            fish.Value = 0;
        }

        private void Split(Fish Node)
        {
            Node.Left = new Fish((int)Math.Floor((int)Node.Value / 2.0));
            Node.Right = new Fish((int)Math.Ceiling((int)Node.Value / 2.0));
            Node.Value = null;
        }

        private bool Reduce(Fish fish)
        {
            List<Fish> NodeList = WalkThroughTree(fish).ToList();
           Fish PathToLevel4 = FindLevel4Fish(fish, 1);
            if (PathToLevel4 != null) 
            {
                Explode(PathToLevel4, NodeList);
                return true;
            }

            int idMore10 = NodeList.FindIndex(x => x.Value >= 10);
            if (idMore10>-1)
            {
                Split(NodeList[idMore10]);
                return true;
            }
            return false;
        }

       private IEnumerable<Fish> IterateTree(Fish Node)
        {
            if (Node is null) return new List<Fish>();
            List<Fish> result = new List<Fish>();
            result.AddRange(IterateTree(Node.Left));
            result.AddRange(IterateTree(Node.Right));
            result.Add(Node);
            return result;
        }

        private Fish FindLevel4Fish(Fish fish, int Level)
        {
           Fish Result;
            if (Level>4 && fish.FishWithValues)
            {
                return fish ;
            }
            else if (fish.Left!= null &&  (Result = FindLevel4Fish(fish.Left, Level+1))!=null)
            {
                return Result;
            }
            else if (fish.Right != null && (Result = FindLevel4Fish(fish.Right, Level+1)) != null)
            {
                return Result;
            }

            return null;
        }

        public List<Fish> WalkThroughTree(Fish node)
        {
            if (node==null)
            {
                return new List<Fish>();
            }
            if (node.Value != null)
            {
                return new List<Fish>() { node};
            }

            return WalkThroughTree(node.Left).Concat(WalkThroughTree(node.Right)).ToList();
        }

        public class Fish
        {
            public Fish(string RawData, Fish Parent = null)
            {
                this.Parent = Parent;

                if (int.TryParse(RawData, out int IntValue))
                {
                    Value = IntValue;
                }
                else
                {
                    int Start = 0;
                    int End = 0;
                    for (int i = 1; i < RawData.Length; i++)
                    {
                        switch (RawData[i])
                        {
                            case '[':
                                Start++;
                                break;
                            case ']':
                                End++;
                                break;
                            case ',':
                                if (Start == End)
                                {
                                    string Part1 = RawData.Substring(1, i - 1);
                                    string Part2 = RawData.Substring(i + 1, RawData.Length - 2-(i));
                                    Left = new Fish(Part1,this);
                                    Right = new Fish(Part2,this);
                                    return;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            public Fish(int Value) 
            {
                this.Value = Value;
            }

            public Fish(Fish Left, Fish Right)
            {
                this.Left = Left;
                this.Right = Right;
            }

            public Fish Left { get; set; }
            public Fish Right { get; set; }
            public int? Value { get; set; }

            public Fish Parent { get; set; }

            public override string ToString()
            {
                if (Value == null)   return $"[{Left},{Right}]";
                return Value.ToString();
            }

            public long magnitude()
            {
                if (Value == null) return 3*Left.magnitude()+2*Right.magnitude();
                return (long)Value;
            }

            public bool FishWithValues
            {
                get
                {
                    return Left !=null&& Left.Value != null && Right!=null&& Right.Value != null;
                }
            }

            public static Fish operator +(Fish self, Fish other)
            {
                return new Fish(self, other);
            }
        }
    }
}
