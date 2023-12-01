using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022
{
    public class Day8 : General.PuzzleWithObjectArrayInput<int[]>
    {
        public Day8() : base(8, 2022)
        {
        }

        public override string SolvePart1(int[][] input)
        {
            int count = 0;
            for (int i = 1; i < input.Length-1; i++)
            {
                for (int j = 1; j < input[i].Length-1; j++)
                {
                    if (Visible(i, j, input)) count++;
                }

            }

            count += 2 * input.Length + (2 * (input[0].Length - 2));
            return count.ToString();
        }

        public bool Visible (int x, int y, int[][] trees)
        {
            int Direction = 4;
            for (int i = x-1; i >=0; i--)
            {
                if (trees[i][y] >= trees[x][y])
                {
                    Direction--;
                    break;
                }
            }

            for (int i = y - 1; i >= 0; i--)
            {
                if (trees[x][i] >= trees[x][y])
                {
                    Direction--;
                    break;
                }
            }

            for (int i = x+1; i<trees.Length; i++)
            {
                if (trees[i][y] >= trees[x][y])
                {
                    Direction--;
                    break;
                }
            }

            for (int i = y + 1; i < trees[0].Length; i++)
            {
                if (trees[x][i] >= trees[x][y])
                {
                    Direction--;
                    break;
                }
            }

            return Direction>0;
        }
        public int GetScore(int x, int y, int[][] trees)
        {
            int scorexmin = 0;
            int scoreymin  =0;
            int scorexplus = 0;
            int scoreyplus = 0;
            for (int i = x - 1; i >= 0; i--)
            {
                scorexmin++;
                if (trees[i][y] >= trees[x][y]) break;
                
            }

            for (int i = y - 1; i >= 0; i--)
            {
                scoreymin++;
                if (trees[x][i] >= trees[x][y]) break;
                
            }

            for (int i = x + 1; i < trees.Length; i++)
            {
                scorexplus++; 
                if (trees[i][y] >= trees[x][y]) break;
               
            }

            for (int i = y + 1; i < trees[0].Length; i++)
            {
                scoreyplus++;
                if (trees[x][i] >= trees[x][y]) break;
                
            }

            return scorexmin * scorexplus * scoreymin * scoreyplus;
        }

        public override string SolvePart2(int[][] input)
        {
            int max = 0;
            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    max = Math.Max(max, GetScore(i, j, input));
                }

            }
            return max.ToString();

        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"30373
25512
65332
33549
35390") == "21");

            Debug.Assert(SolvePart2(@"30373
25512
65332
33549
35390") == "8");
        }

        protected override int[] CastToObject(string RawData)
        {
            return RawData.Select(x => int.Parse(x.ToString())).ToArray();
        }
    }
}
