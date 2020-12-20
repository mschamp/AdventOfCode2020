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

            Tile[,] image = RestorImage(tiles);
            string RestoredImage = "Tile 0000:" + Environment.NewLine;
            int sqrtTiles = (int)Math.Sqrt(image.Length);
            int CountLines = image[0, 0].Left().Length;
            for (int row = 0; row < sqrtTiles; row++)
            {
                for (int i = 1; i < CountLines-1; i++)
                {
                    string line = "";
                    for (int column = 0; column < sqrtTiles; column++)
                    {
                        line += image[row, column].Row(i).Substring(1, CountLines - 2);
                    }
                    RestoredImage += line +Environment.NewLine;
                }
            }
            Tile finaly = new Tile(RestoredImage);
            List<(int row, int column)> monster = LoadMonster();
            int monsterCount = 0;
            for (int i = 0; i <8; i++)
            {
                monsterCount += CountMonsters(finaly, monster);
                finaly.ChangeOrientation();
            }

            int CountChart = CountCharts(finaly, '#');

            return "" + (CountChart- monsterCount*monster.Count);
        }

        private int CountCharts(Tile finaly, char v)
        {
            int Count = 0;

            for (int i = 0; i < finaly.image.Length; i++)
            {
                Count += finaly.image[i].Count(x => x == v);
            }

            return Count;
        }

        private int CountMonsters(Tile finaly, List<(int row, int column)> monster)
        {
            int Count = 0;
            int MonsterMaxRow = monster.Max(x => x.row);
            int MonsterMaxCol = monster.Max(x => x.column);

            for (int row = 0; row < finaly.image.Length- MonsterMaxRow; row++)
            {
                for (int col = 0; col < finaly.image.Length - MonsterMaxCol; col++)
                {
                    if (monster.All(X => finaly.Pixel(row + X.row, col + X.column) == '#'))
                    {
                        Count++;
                    }
                }
            }

            return Count;
        }

        private List<(int row, int column)> LoadMonster()
        {
            List<(int row, int column)> result = new List<(int row, int column)>();
            string Monster = @"                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ";
            string[] rows = Monster.Split(Environment.NewLine);
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    if (rows[i][j]=='#')
                    {
                        result.Add((i, j));
                    }
                }
            }
            return result;
        }

        private Tile[,] RestorImage(List<Tile> tiles)
        {
            List<Tile> tilesToMatch = new List<Tile>();
            tilesToMatch.AddRange(tiles);
            int Size = (int)Math.Sqrt(tiles.Count);
            Tile[,] fullImage = new Tile[Size, Size];
            for (int Row = 0; Row < Size; Row++)
            {
                for (int Column = 0; Column < Size; Column++)
                {
                    string ReqTop = null;
                    string ReqLeft = null;
                    if (Row!=0)
                    {
                        ReqTop = fullImage[Row - 1, Column].Bottom();
                    }
                    if (Column != 0)
                    {
                        ReqLeft = fullImage[Row, Column-1].Right();
                    }

                    Tile match = findFittingTile(ReqTop, ReqLeft, tilesToMatch);
                    fullImage[Row, Column] = match;
                    tilesToMatch.Remove(match);

                }
            }
            return fullImage;
        }

        private Tile findFittingTile(string reqTop, string reqLeft, List<Tile> tilesToMatch)
        {
            foreach (Tile tile in tilesToMatch)
            {
                for (int i = 0; i < 8; i++)
                {
                    bool matchTop;
                    bool matchLeft;
                    if (reqTop==null)
                    {
                        matchTop = !tilesToMatch.Any(x => x.ID != tile.ID && x.options.Contains(tile.Top()));
                    }
                    else
                    {
                        matchTop = tile.Top() == reqTop;
                    }
                    if (reqLeft == null)
                    {
                        matchLeft = !tilesToMatch.Any(x => x.ID != tile.ID && x.options.Contains(tile.Left()));
                    }
                    else
                    {
                        matchLeft = tile.Left() == reqLeft;
                    }

                    if (matchTop&&matchLeft)
                    {
                        return tile;
                    }

                    tile.ChangeOrientation();
                }
            }
            return null;
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
            string[] lines = input.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries);
            TouchingTiles = new HashSet<Tile>();
            ID = int.Parse(lines[0].Substring(5, 4));
            image = lines.Skip(1).ToArray();
            Orientation = 0;
            options = new[] {
                edge(0,0,0,1),
                edge(0,0,1,0),
                edge(image.Length-1,0,0,1),
                edge(image.Length-1,0,-1,0),
                edge(0,image.Length-1,0,-1),
                edge(0,image.Length-1,1,0),
                edge(image.Length-1,image.Length-1,0,-1),
                edge(image.Length-1,image.Length-1,-1,0) };
        }

        public string[] image;

        public string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public string[] options { get; set; }

        public int ID { get; set; }

        public HashSet<Tile> TouchingTiles { get; set; }

        public void ChangeOrientation()
        {
            Orientation++;
            Orientation %= 8;
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public char Pixel(int row, int colom)
        {
            for (var i = 0; i < Orientation % 4; i++)
            {
                (row, colom) = (colom, image.Length - 1 - row);
            }
            string rowContent = image[row];
            if (Orientation>=4)
            {
                rowContent = Reverse(rowContent);
            }
            return rowContent[colom];
        }

        private string edge(int Row, int Col, int DeltaRow, int DeltaCol)
        {
            string result = "";
            for (var i = 0; i < image.Length; i++)
            {
                result += Pixel(Row, Col);
                Row += DeltaRow;
                Col += DeltaCol;
            }
            return result;
        }

        public string Row(int row)
        {
            return edge(row, 0, 0, 1);
        }

        public string Column(int column)
        {
            return edge(0, column, 1, 0);
        }

        public string Top()
        {
            return Row(0);
        }

        public string Bottom()
        {
            return Row(image.Length-1);
        }

        public string Left()
        {
            return Column(0);
        }

        public string Right()
        {
            return Column(image.Length - 1);
        }

        public int Orientation { get; set; }

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
