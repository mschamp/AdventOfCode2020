﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day24 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            HashSet<(int x,int y)> Blacks = new HashSet<(int x, int y)>();

            foreach (string line in input.Split(Environment.NewLine))
            {
                (int x, int y) end = IdentifyTile(line);
                if (Blacks.Contains(end))
                {
                    Blacks.Remove(end);
                    continue;
                }
                Blacks.Add(end);
            }

            return "" + Blacks.Count();
        }

        private (int x, int y) IdentifyTile(string line)
        {
            line = line.Replace("ne", "1").Replace("se", "3").Replace("sw", "4").Replace("nw", "6").Replace("e", "2").Replace("w", "5");
            (int x, int y) Current = (0, 0);
            foreach (char Direction in line)
            {
                switch (Direction)
                {
                    case '1':
                        Current.x += 1;
                        Current.y += 1;
                        break;
                    case '2':
                        Current.x += 2;
                        break;
                    case '3':
                        Current.x += 1;
                        Current.y += -1;
                        break;
                    case '4':
                        Current.x += -1;
                        Current.y += -1;
                        break;
                    case '5':
                        Current.x += -2;
                        Current.y += 0;
                        break;
                    case '6':
                        Current.x += -1;
                        Current.y += 1;
                        break;
                    default:
                        break;
                }
            }
            return Current;
        }

        public string SolvePart2(string input = null)
        {
            HashSet<(int x, int y) > Blacks = new HashSet<(int x, int y)>();

            foreach (string line in input.Split(Environment.NewLine))
            {
                (int x, int y) end = IdentifyTile(line);
                if (Blacks.Contains(end))
                {
                    Blacks.Remove(end);
                    continue;
                }
                Blacks.Add(end);
            }

            for (int i = 0; i < 100; i++)
            {
                Blacks = SimulateDay(Blacks);
            }


            return "" + Blacks.Count();
        }

        private HashSet<(int x, int y)> SimulateDay(HashSet<(int x, int y)> Blacks)
        {
            Dictionary<(int x, int y), int> OnCounter = new Dictionary<(int x, int y), int>();
            HashSet<(int x, int y)> NewGrid = new HashSet<(int x, int y)>();
            foreach ((int x, int y) tile in Blacks)
            {
                NewGrid.Add(tile);
                GetNeightbours(tile, OnCounter);
            }

            foreach (KeyValuePair<(int x, int y), int> item in OnCounter)
            {
                if (Blacks.Contains(item.Key) && (item.Value==0||item.Value>2))
                {
                    NewGrid.Remove(item.Key);
                }
                else if (item.Value==2 && !Blacks.Contains(item.Key))
                {
                    NewGrid.Add(item.Key);
                }
            }
            return NewGrid;
        }

        public void GetNeightbours((int x, int y) location, Dictionary<(int x, int y), int> Neightbours)
        {
            foreach ((int x, int y) item in new[] {(1,1),(2,0),(1,-1),(-1,-1),(-2,0),(-1,1) })
            {
                (int x, int y) Neigbour = (location.x+item.x, location.y+item.y);
                Neightbours.TryGetValue(Neigbour, out int Count);
                Neightbours[Neigbour] = Count + 1;
            }

            (int x, int y) Self = (location.x, location.y);
            if (!Neightbours.ContainsKey(Self))
            {
                Neightbours[Self] = 0;
            }
            
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew") == "10");

            Debug.Assert(SolvePart2(@"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew") == "2208");
        }
    }
}
