using General;
using System;
using System.Diagnostics;
using System.Linq;
namespace _2016
{
    public class Day6:PuzzleWithStringArrayInput
    {
        public Day6():base(6,2016)
        {
            
        }

        public override string SolvePart1(string[] input)
        {
         var a=   Enumerable.Range(0,input[0].Length).Select(i=> input.Select(x => x[i]).GroupBy(x => x).MaxBy(x => x.Count())).Select(x=>x.Key).ToArray();
            return new string(a);
        }

        public override string SolvePart2(string[] input)
        {
            var a = Enumerable.Range(0, input[0].Length).Select(i => input.Select(x => x[i]).GroupBy(x => x).MinBy(x => x.Count())).Select(x => x.Key).ToArray();
            return new string(a);
            throw new System.NotImplementedException();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar") == "easter");
            Debug.Assert(SolvePart2(@"eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar") == "advent");
        }
    }
}
