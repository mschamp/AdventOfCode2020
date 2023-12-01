using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day10 : General.abstractPuzzleClass
    {
        public Day10():base(10,2019)
        {

        }

        public override string SolvePart1(string input = null)
        {
            string[] lines = input.Split(Environment.NewLine);
            List<General.clsPoint> Asteroids = new();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j]=='#')
                    {
                        Asteroids.Add(new General.clsPoint(j, i));
                    }
                }
            }

            Dictionary<General.clsPoint, List<double>> PointAngles = new();
            foreach (General.clsPoint pointA in Asteroids)
            {
                PointAngles[pointA] = new List<double>();
                foreach (General.clsPoint pointB in Asteroids )
                {
                    if (!pointA.Equals(pointB))
                    {
                        PointAngles[pointA].Add(pointA.Angle(pointB));
                    }
                }
                PointAngles[pointA] = PointAngles[pointA].Distinct().ToList();
            }

            return "" + PointAngles.Values.Max(x => x.Count); ;
        }

        public override string SolvePart2(string input = null)
        {
            string[] lines = input.Split(Environment.NewLine);
            List<General.clsPoint> Asteroids = new();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        Asteroids.Add(new General.clsPoint(j, i));
                    }
                }
            }

            Dictionary<General.clsPoint, List<double>> PointAngles = new();
            foreach (General.clsPoint pointA in Asteroids)
            {
                PointAngles[pointA] = new List<double>();
                foreach (General.clsPoint pointB in Asteroids)
                {
                    if (!pointA.Equals(pointB))
                    {
                        PointAngles[pointA].Add(pointA.Angle(pointB));
                    }
                }
                PointAngles[pointA] = PointAngles[pointA].Distinct().ToList();
            }

            General.clsPoint optimalPoint = PointAngles.Keys.OrderByDescending(x => PointAngles[x].Count).First();
            List<General.clsPoint> Destroyed = new();
            List<Tuple<double, double, General.clsPoint>> SortedAsteroids = orderAsteroids(optimalPoint, Asteroids);
            Double Angle = 0.0000000000000000001;
            while (SortedAsteroids.Count>0)
            {
                Tuple<double, double, General.clsPoint> vaporized = SortedAsteroids.Where(x => x.Item1<Angle).FirstOrDefault();
                
                if (vaporized==null)
                {
                    Angle = Math.PI * 2;
                }
                else 
                {
                    Angle = vaporized.Item1;
                    Destroyed.Add(vaporized.Item3);
                    SortedAsteroids.Remove(vaporized);
                }
            }
            

            return "" + (Destroyed[199].X*100+ Destroyed[199].Y); ;
        }

        private List<Tuple<double, double, General.clsPoint>> orderAsteroids(General.clsPoint optimalPoint, List<General.clsPoint> Asteroids )
        {
            List<Tuple<double, double, General.clsPoint>> result = new();
            foreach (General.clsPoint asteroid in Asteroids)
            {
                if (!asteroid.Equals(optimalPoint))
                {
                    result.Add(new Tuple<double, double, General.clsPoint>(asteroid.AnglePos(optimalPoint), asteroid.distance(optimalPoint),asteroid));
                }
            }
            return result.OrderByDescending(x => x.Item1).ThenBy(x => x.Item2).ToList();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@".#..#
.....
#####
....#
...##") == "8");

                Debug.Assert(SolvePart1(@"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####") == "33");

            Debug.Assert(SolvePart1(@"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.") == "35");

            Debug.Assert(SolvePart1(@".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..") == "41");

            Debug.Assert(SolvePart1(@".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##") == "210");

            Debug.Assert(SolvePart2(@".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##") == "802");
        }
    }
}
