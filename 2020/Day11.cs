﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2020
{
    public class Day11 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] rows = input.Split(Environment.NewLine);
            string[] NewConfig = new string[rows.Length];
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = "";
                    for (int j = 0; j < rows[i].Length; j++)
                    {
                        if (rows[i][j] == 'L' && OccupiedSeats(rows, j, i) == 0)
                        {
                            row += '#';
                            changed = true;
                        }
                        else if (rows[i][j] == '#' && OccupiedSeats(rows, j, i) >= 4)
                        {
                            row += 'L';
                            changed = true;
                        }
                        else
                        {
                            row += rows[i][j];
                        }
                    }
                    NewConfig[i] = row;
                }
                rows = (string[])NewConfig.Clone();
            }
            
            return "" + NumberOccupied(rows);
        }

        public int OccupiedSeats(string[] rows, int seatx, int seaty)
        {
            int counter = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if ( seatx+x>=0 && seaty + y >= 0 &&
                        seatx + x< rows[seaty].Length && seaty + y < rows.Length &&
                        !(x==0&&y==0) && rows[seaty + y][seatx + x] == '#')
                    {
                        counter ++;
                    }
                }
            }
            return counter;
        }

        public int NumberOccupied(string[] rows)
        {
            int counter = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {

                     if (rows[i][j] == '#')
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public string SolvePart2(string input = null)
        {
            string[] rows = input.Split(Environment.NewLine);
            string[] NewConfig = new string[rows.Length];
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = "";
                    for (int j = 0; j < rows[i].Length; j++)
                    {
                        if (rows[i][j] == 'L' && VisibleOccupiedSeats(rows, j, i) == 0)
                        {
                            row += '#';
                            changed = true;
                        }
                        else if (rows[i][j] == '#' && VisibleOccupiedSeats(rows, j, i) >= 5)
                        {
                            row += 'L';
                            changed = true;
                        }
                        else
                        {
                            row += rows[i][j];
                        }
                    }
                    NewConfig[i] = row;
                }
                rows = (string[])NewConfig.Clone();
            }

            return "" + NumberOccupied(rows);
        }

        public int VisibleOccupiedSeats(string[] rows, int seatx, int seaty)
        {
            int counter = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!(x == 0 && y == 0) && VisibleChairOccupied(seatx,seaty,x,y,rows))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public bool VisibleChairOccupied(int startX, int startY, int dx, int dy,string[] rows)
        {
            int Lookx = startX + dx;
            int Looky = startY + dy;
            while (Lookx>=0 && Looky >=0 &&
                Looky < rows.Length && Lookx < rows[Looky].Length)
            {
                if (rows[Looky][Lookx] == '#')
                {
                    return true;
                }
                else if (rows[Looky][Lookx] == 'L')
                {
                    return false;
                }
                Lookx +=  dx;
                Looky += dy;
            }
            return false;
        }
        public void Tests()
        {
            Debug.Assert(SolvePart1(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL") == "37");

            Debug.Assert(SolvePart2(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL") == "26");
        }
    }
}