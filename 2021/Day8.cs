using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day8 : General.PuzzleWithStringArrayInput
    {
        public Day8() : base(8, 2021)
        {

        }
        public override string SolvePart1(string[] input)
        {
            int[] UniqueLength = new int[] { 2, 3, 4, 7 };
            var outputs = input.Select(x => x.Split(" | ")[1]);
            return outputs.Select(x => x.Split(' ').Where(x => UniqueLength.Contains(x.Length)).Count()).Sum().ToString();
        }

        public override string SolvePart2(string[] input)
        {
            long sum = 0;
            foreach (var item in input)
            {
                string[] parts = item.Split(" | ");
                sum += CalculateValue(parts[1], detectConnections(parts[0]));
            }
            return sum.ToString();
        }

        private long CalculateValue(string output, string[] connections)
        {
            string[] digits = output.Split(' ');
            int number = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                number *= 10;
                number += FindDigit(digits[i], connections);
            }
            return number;
        }

        private int FindDigit(string Digit, string[] connections)
        {
            for (int i = 0; i < connections.Length; i++)
            {
                if (connections[i].Except(Digit).Count() == 0 && Digit.Except(connections[i]).Count()==0) return i;
            }
            return 0;
        }

        public string[] detectConnections(string input)
        {
            string[] numberConfig = new string[10];

            List<string> inputSignals = input.Split(' ').ToList();

            numberConfig[1] = inputSignals.First(s => s.Length == 2);
            inputSignals.Remove(numberConfig[1]);
            numberConfig[7] = inputSignals.First(s => s.Length == 3);
            inputSignals.Remove(numberConfig[7]);
            numberConfig[4] = inputSignals.First(s => s.Length == 4);
            inputSignals.Remove(numberConfig[4]);
            numberConfig[8] = inputSignals.First(s => s.Length == 7);
            inputSignals.Remove(numberConfig[8]);
            numberConfig[9] = inputSignals.First(s => s.Length == 6 && s.Except(numberConfig[4].Union(numberConfig[7])).Count() == 1);
            inputSignals.Remove(numberConfig[9]);
            numberConfig[0] = inputSignals.First(s => s.Length == 6 && s.Except(numberConfig[1]).Count() == 4);
            inputSignals.Remove(numberConfig[0]);
            numberConfig[3] = inputSignals.First(s => s.Length == 5 && s.Except(numberConfig[1]).Count() == 3);
            inputSignals.Remove(numberConfig[3]);
            numberConfig[5] = inputSignals.First(s => s.Length == 5 && s.Union(numberConfig[1]).Except(numberConfig[9]).Count() == 0);
            inputSignals.Remove(numberConfig[5]);
            numberConfig[2] = inputSignals.First(s => s.Length == 5);
            inputSignals.Remove(numberConfig[2]);
            numberConfig[6] = inputSignals.First();

            return numberConfig;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf") == "0");
            Debug.Assert(SolvePart1(@"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce") == "26");

            Debug.Assert(SolvePart2("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf") == "5353");
            Debug.Assert(SolvePart2(@"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce") == "61229");
        }
    }
}
