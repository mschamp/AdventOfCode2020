using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2022
{
    public class Day5 : General.abstractPuzzleClass
    {
        public Day5() : base(5)
        {
        }

        public override string SolvePart1(string input = null)
        {
            var inputParts = input.Split(Environment.NewLine+Environment.NewLine);
            var instructions = inputParts[1].Split(Environment.NewLine);
            var startPositions = inputParts[0].Split(Environment.NewLine);

            int maxPosition = int.Parse(startPositions.Last().Split("  ").Last().Replace(" ", ""));
            List<char>[] positions = new List<char>[maxPosition];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new List<char>();
            }

            for (int i = 0; i < startPositions.Length-1; i++)
            {
                for (int j = 1; j < startPositions[i].Length; j+=4)
                {
                    if (startPositions[i][j] != ' ') positions[(j-1)/4].Add(startPositions[i][j]);
                }

            }


            Regex rgx = new Regex(@"move (\d+) from (\d+) to (\d+)");
            foreach (var instruction in instructions)
            {
               Match m = rgx.Match(instruction);
                positions[int.Parse(m.Groups[3].Value)-1].InsertRange(0,positions[int.Parse(m.Groups[2].Value)-1].Take(int.Parse(m.Groups[1].Value)).Reverse());
                positions[int.Parse(m.Groups[2].Value) - 1].RemoveRange(0, int.Parse(m.Groups[1].Value));
            }



            string result = "";

            for (int i = 0; i < positions.Length; i++)
            {
                result += positions[i].First();
            }


            return result;
        }

        public override string SolvePart2(string input = null)
        {
            var inputParts = input.Split(Environment.NewLine + Environment.NewLine);
            var instructions = inputParts[1].Split(Environment.NewLine);
            var startPositions = inputParts[0].Split(Environment.NewLine);

            int maxPosition = int.Parse(startPositions.Last().Split("  ").Last().Replace(" ", ""));
            List<char>[] positions = new List<char>[maxPosition];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new List<char>();
            }

            for (int i = 0; i < startPositions.Length - 1; i++)
            {
                for (int j = 1; j < startPositions[i].Length; j += 4)
                {
                    if (startPositions[i][j] != ' ') positions[(j - 1) / 4].Add(startPositions[i][j]);
                }

            }


            Regex rgx = new Regex(@"move (\d+) from (\d+) to (\d+)");
            foreach (var instruction in instructions)
            {
                Match m = rgx.Match(instruction);
                positions[int.Parse(m.Groups[3].Value) - 1].InsertRange(0, positions[int.Parse(m.Groups[2].Value) - 1].Take(int.Parse(m.Groups[1].Value)));
                positions[int.Parse(m.Groups[2].Value) - 1].RemoveRange(0, int.Parse(m.Groups[1].Value));
            }



            string result = "";

            for (int i = 0; i < positions.Length; i++)
            {
                result += positions[i].First();
            }


            return result;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2") == "CMZ");

                Debug.Assert(SolvePart2(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2") == "MCD");
        }
    }
}
