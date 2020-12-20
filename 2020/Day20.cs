using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2020
{
    public class Day20 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (var item in input.Split(Environment.NewLine+Environment.NewLine))
            {
                tiles.Add(new Tile(item));
            }

            foreach (var item in tiles)
            {
                tiles.ForEach(x => x.Touch(item));
            }

            long product = 1;
            foreach (var item in tiles.Where(x => x.TouchingTiles.Count==2))
            {
                product *= item.ID;
            }
            return "" + product;
        }

        public string SolvePart2(string input = null)
        {
            string result="";
            List<Tile> tiles = new List<Tile>();
            foreach (var item in input.Split(Environment.NewLine + Environment.NewLine))
            {
                tiles.Add(new Tile(item));
            }

            foreach (var item in tiles)
            {
                tiles.ForEach(x => x.Touch(item));
            }

            return "";
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"Tile 2311:
..##.#..#.
##..#.....
#...##..#.
####.#...#
##.##.###.
##...#.###
.#.#.#..##
..#....#..
###...#.#.
..###..###

Tile 1951:
#.##...##.
#.####...#
.....#..##
#...######
.##.#....#
.###.#####
###.##.##.
.###....#.
..#.#..#.#
#...##.#..

Tile 1171:
####...##.
#..##.#..#
##.#..#.#.
.###.####.
..###.####
.##....##.
.#...####.
#.##.####.
####..#...
.....##...

Tile 1427:
###.##.#..
.#..#.##..
.#.##.#..#
#.#.#.##.#
....#...##
...##..##.
...#.#####
.#.####.#.
..#..###.#
..##.#..#.

Tile 1489:
##.#.#....
..##...#..
.##..##...
..#...#...
#####...#.
#..#.#.#.#
...#.#.#..
##.#...##.
..##.##.##
###.##.#..

Tile 2473:
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.

Tile 2971:
..#.#....#
#...###...
#.#.###...
##.##..#..
.#####..##
.#..####.#
#..#.#..#.
..####.###
..#.#.###.
...#.#.#.#

Tile 2729:
...#.#.#.#
####.#....
..#.#.....
....#..#.#
.##..##.#.
.#.####...
####.#.#..
##.####...
##..#.##..
#.##...##.

Tile 3079:
#.#.#####.
.#..######
..#.......
######....
####.#..#.
.#...#.##.
#.#####.##
..#.###...
..#.......
..#.###...")== "20899048083289");

            Debug.Assert(SolvePart2(@"Tile 2311:
..##.#..#.
##..#.....
#...##..#.
####.#...#
##.##.###.
##...#.###
.#.#.#..##
..#....#..
###...#.#.
..###..###

Tile 1951:
#.##...##.
#.####...#
.....#..##
#...######
.##.#....#
.###.#####
###.##.##.
.###....#.
..#.#..#.#
#...##.#..

Tile 1171:
####...##.
#..##.#..#
##.#..#.#.
.###.####.
..###.####
.##....##.
.#...####.
#.##.####.
####..#...
.....##...

Tile 1427:
###.##.#..
.#..#.##..
.#.##.#..#
#.#.#.##.#
....#...##
...##..##.
...#.#####
.#.####.#.
..#..###.#
..##.#..#.

Tile 1489:
##.#.#....
..##...#..
.##..##...
..#...#...
#####...#.
#..#.#.#.#
...#.#.#..
##.#...##.
..##.##.##
###.##.#..

Tile 2473:
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.

Tile 2971:
..#.#....#
#...###...
#.#.###...
##.##..#..
.#####..##
.#..####.#
#..#.#..#.
..####.###
..#.#.###.
...#.#.#.#

Tile 2729:
...#.#.#.#
####.#....
..#.#.....
....#..#.#
.##..##.#.
.#.####...
####.#.#..
##.####...
##..#.##..
#.##...##.

Tile 3079:
#.#.#####.
.#..######
..#.......
######....
####.#..#.
.#...#.##.
#.#####.##
..#.###...
..#.......
..#.###...") == "273");
        }
    }

    public class Tile
    {
        public Tile(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            TouchingTiles = new HashSet<Tile>();
            ID = int.Parse(lines[0].Substring(5, 4));
            List<string> content = new List<string>();
            foreach (var item in lines.Skip(1))
            {
                content.Add(item);
            }

            options = new List<string>();
            options.Add(content.First());
            options.Add(content.Last());

            string start = "";
            string end = "";
            foreach (string item in content)
            {
                start += item.First();
                end += item.Last();
            }

            options.Add(start);
            options.Add(end);

        }

        public List<string> options { get; set; }

        public string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public int ID { get; set; }

        public HashSet<Tile> TouchingTiles { get; set; }

        public override string ToString()
        {
            return ID.ToString();
        }

        public bool Touch(Tile other)
        {
            if (other != this)
            {
                foreach (string thisOptions in options)
                {
                    if (other.options.Any(x => x==thisOptions || x==Reverse(thisOptions)))
                    {
                        this.TouchingTiles.Add(other);
                        other.TouchingTiles.Add(this);
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
