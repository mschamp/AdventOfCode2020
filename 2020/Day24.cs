using System;
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
            Dictionary<General.clsPoint, bool> Grid = new Dictionary<General.clsPoint, bool>();

            foreach (string line in input.Split(Environment.NewLine))
            {
                General.clsPoint end = IdentifyTile(line);
                Grid.TryGetValue(end, out bool State);
                Grid[end] = !State;
            }

            return "" + Grid.Values.Count(x => x);
        }

        private General.clsPoint IdentifyTile(string line)
        {
            line = line.Replace("ne", "1").Replace("se", "3").Replace("sw", "4").Replace("nw", "6").Replace("e", "2").Replace("w", "5");
            General.clsPoint Current = new General.clsPoint(0, 0);
            foreach (char Direction in line)
            {
                switch (Direction)
                {
                    case '1':
                        Current = Current.plus(1,1);
                        break;
                    case '2':
                        Current = Current.plus(2,0);
                        break;
                    case '3':
                        Current = Current.plus(1, -1);
                        break;
                    case '4':
                        Current = Current.plus(-1, -1);
                        break;
                    case '5':
                        Current = Current.plus(-2,0);
                        break;
                    case '6':
                        Current = Current.plus(-1, 1);
                        break;
                    default:
                        break;
                }
            }
            return Current;
        }

        public string SolvePart2(string input = null)
        {
            Dictionary<General.clsPoint, bool> Grid = new Dictionary<General.clsPoint, bool>();

            foreach (string line in input.Split(Environment.NewLine))
            {
                General.clsPoint end = IdentifyTile(line);
                Grid.TryGetValue(end, out bool State);
                Grid[end] = !State;
            }

            return "" + Grid.Values.Count(x => x);
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
