using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2020
{
    public class Day3 : General.PuzzleWithObjectInput<char[,]>
    {

        private int TreesOnSlope(char[,] array, Tuple<int,int> slope, int Rows, int Columns)
        {
            int Trees = 0;
            for (int i = 0; i < Rows; i=i+slope.Item2)
            {
                int j = (slope.Item1 * i/ slope.Item2) % Columns;
                if (array[j, i] == '#')
                {
                    Trees++;
                }
            }
            return  Trees;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#") == "7");

            Debug.Assert(SolvePart2(@"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#") == "336");
        }

        public override char[,] CastToObject(string RawData)
        {
            string[] rowData = RawData.Split(Environment.NewLine);
            int Rows = rowData.Length;
            int Columns = rowData[0].Length;

            char[,] array = new char[Columns, Rows];
            for (int i = 0; i < Rows; i++)
            {
                char[] chars = rowData[i].ToCharArray();
                for (int j = 0; j < Columns; j++)
                {
                    array[j, i] = chars[j];
                }
            }
            return array;
        }

        public override string SolvePart1(char[,] input)
        {
            return TreesOnSlope(input, new Tuple<int, int>(3, 1), input.GetLength(1), input.GetLength(0)).ToString();
        }

        public override string SolvePart2(char[,] input)
        {
            long Trees = 1;
            foreach (Tuple<int, int> slope in (new[] { new Tuple<int, int>(1, 1), new Tuple<int, int>(3, 1), new Tuple<int, int>(5, 1), new Tuple<int, int>(7, 1), new Tuple<int, int>(1, 2) }))
            {
                Trees *= TreesOnSlope(input, slope, input.GetLength(1), input.GetLength(0));
            }
            return  Trees.ToString();
        }
    }
}
