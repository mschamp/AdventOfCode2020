using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Extensions;

namespace _2021
{
    public class Day4 : General.PuzzleWithObjectInput<(List<int>,List<Day4.Board>, Dictionary<int, Day4.BingoNumber>)>
    {
        public Day4() : base(4)
        {

        }
        public override (List<int>, List<Board>, Dictionary<int, BingoNumber>) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine + Environment.NewLine);
            List<int> Numbers = parts[0].Split(",").Select(x => int.Parse(x)).ToList();
            Dictionary<int, BingoNumber> dict = new();
            var boards = parts.Skip(1).Select(x => new Board(x, ref dict)).ToList();

            return (Numbers, boards, dict);
        }

        public override string SolvePart1((List<int> , List<Board>, Dictionary<int, BingoNumber>) input)
        {
            List<int> Numbers = input.Item1;
            Dictionary<int, BingoNumber> dict = input.Item3;
            List<Board> boards = input.Item2;

            int draw = -1;
            while (!boards.Any(x => x.bingo()))
            {
                draw++;
                dict[Numbers[draw]].Drawn = true;
            }

            Board winning = boards.Where(x => x.bingo()).First();

            return (winning.SumUnmarked() * Numbers[draw]).ToString();
        }

        public override string SolvePart2((List<int>, List<Board>, Dictionary<int, BingoNumber>) input)
        {
            List<int> Numbers = input.Item1;
            Dictionary<int, BingoNumber> dict = input.Item3;
            List<Board> boards = input.Item2;

            int draw = -1;
            while (boards.Count(x => !x.bingo()) > 1)
            {
                draw++;
                dict[Numbers[draw]].Drawn = true;
            }

            Board losing = boards.Where(x => !x.bingo()).First();

            while (!losing.bingo())
            {
                draw++;
                dict[Numbers[draw]].Drawn = true;
            }

            return (losing.SumUnmarked() * Numbers[draw]).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7") == "4512");

            Debug.Assert(SolvePart2(@"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7") == "1924");
        }

        public class BingoNumber
        {
            public BingoNumber(int number)
            {
                Number = number;
            }

            public int Number { get; private set; }
            public bool Drawn { get; set; }
        }

        public class Board
        {
            public Board(string input, ref Dictionary<int,BingoNumber> dictNumbers)
            {
                string[] lines = input.Split(Environment.NewLine);
                numbers = new BingoNumber[lines.Length, lines.Length]; 
                for (int i = 0; i < lines.Length; i++)
                {
                    int[] options = lines[i].Split(" ",StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                    for (int j = 0; j < options.Length; j++)
                    {
                        if (!dictNumbers.TryGetValue(options[j], out BingoNumber value))
                        {
                            value = new BingoNumber(options[j]);
                            dictNumbers.Add(options[j], value);
                        }
                        numbers[i, j] = value;

                    }
                }
            }

            public BingoNumber[,] numbers { get; set; }

            public bool bingo()
            {
                for (int i = 0; i < numbers.GetLength(1); i++)
                {
                    if (numbers.getRow(i).All(x => x.Drawn))
                    {
                        return true;
                    }
                    
                }

                for (int i = 0; i < numbers.GetLength(0); i++)
                {
                    if (numbers.getColumn(i).All(x => x.Drawn))
                    {
                        return true;
                    }

                }

                return false;
            }
        
            public int SumUnmarked()
            {
                int sum = 0;
                for (int i = 0; i < numbers.GetLength(0); i++)
                {
                    sum += numbers.getColumn(i).Where(x=> !x.Drawn).Sum(x => x.Number);
                }
                return sum;
            }
        }
    }
}
