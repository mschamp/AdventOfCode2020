using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2020
{
    public class Day3 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] rowData = input.Split(Environment.NewLine);
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
            return "" + TreesOnSlope(array, new Tuple<int, int>(3,1), Rows, Columns);


        }

        public string SolvePart2(string input = null)
        {
            string[] rowData = input.Split(Environment.NewLine);
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

            long Trees = 1;
            foreach (Tuple<int, int> slope in ( new[] { new Tuple<int, int>(1, 1), new Tuple<int, int>(3, 1), new Tuple<int, int>(5, 1), new Tuple<int, int>(7, 1), new Tuple<int, int>(1, 2) }))
            {
                Trees *= TreesOnSlope(array, slope, Rows, Columns);
            }
            return "" + Trees;
        }

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

        public void Tests()
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
    }
}
