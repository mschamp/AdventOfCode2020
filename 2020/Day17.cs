using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2020
{
	public class Day17 : General.abstractPuzzleClass
    {
        public Day17() : base(17, 2020)
        {

        }
        public override string SolvePart1(string input = null)
        {
            Cubes3D cubes = new(input);
            cubes.ExecuteCycles(6);
            return cubes.CubesOnCount.ToString();
        }

        public override string SolvePart2(string input = null)
        {
            Cubes4D cubes = new(input);
            cubes.ExecuteCycles(6);
            return cubes.CubesOnCount.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@".#.
..#
###") == "112");

            Debug.Assert(SolvePart2(@".#.
..#
###") == "848");
        }
    }

    public class Cubes3D
    {
        public HashSet<(int x, int y, int z)> CubesOn;
        public Cubes3D(string input)
        {

            string[] rows = input.Split(Environment.NewLine);
            CubesOn = [];
            for (int x = 0; x < rows.Length; x++)
            {
                for (int y = 0; y < rows[x].Length; y++)
                {
                    if (rows[x][y]=='#')
                    {
                        CubesOn.Add((x, y, 0));
                    }
                }
            }
        }

        public void GetNeightbours((int x, int y, int z) location, Dictionary<(int x, int y, int z), int> Neightbours)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (dx!=0||dy!=0||dz!=0)
                        {
                            (int x, int y, int z) Nposition = (location.x + dx, location.y + dy, location.z + dz);
                            Neightbours.TryGetValue(Nposition, out int Current);
                            Neightbours[Nposition] = Current + 1;
                        }
                    }
                }
            }
        }

        public HashSet<(int x, int y, int z)> ExecuteCycle()
        {
            Dictionary<(int x, int y, int z), int> OnCounter = [];
            foreach ((int x, int y, int z) cube in CubesOn)
            {
                GetNeightbours(cube, OnCounter);
            }

            HashSet<(int x, int y, int z)> NewCubesOn = [];
            foreach (KeyValuePair<(int x, int y, int z), int> item in OnCounter)
            {
                if (item.Value==3 ||
                    (item.Value==2 && CubesOn.Contains(item.Key)))
                {
                    NewCubesOn.Add(item.Key);
                }
            }
            return NewCubesOn;
        }
    
        public void ExecuteCycles(int numberOfCycles)
        {
            for (int i = 0; i < numberOfCycles; i++)
            {
                CubesOn = ExecuteCycle();
            }
        }

        public int CubesOnCount
        {
            get
            { return CubesOn.Count; }
        }
    }

    public class Cubes4D
    {
        public HashSet<(int x, int y, int z, int w)> CubesOn;
        public Cubes4D(string input)
        {

            string[] rows = input.Split(Environment.NewLine);
            CubesOn = [];
            for (int x = 0; x < rows.Length; x++)
            {
                for (int y = 0; y < rows[x].Length; y++)
                {
                    if (rows[x][y] == '#')
                    {
                        CubesOn.Add((x, y, 0, 0));
                    }
                }
            }
        }

        public void GetNeightbours((int x, int y, int z, int w) location, Dictionary<(int x, int y, int z, int w), int> Neightbours)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        for (int dw = -1; dw <= 1; dw++)
                        {
                            if (dx != 0 || dy != 0 || dz != 0 || dw != 0)
                            {
                                (int x, int y, int z, int w) Nposition = (location.x + dx, location.y + dy, location.z + dz, location.w + dw);
                                Neightbours.TryGetValue(Nposition, out int Current);
                                Neightbours[Nposition] = Current + 1;
                            }
                        }
                        
                    }
                }
            }
        }

        public HashSet<(int x, int y, int z, int w)> ExecuteCycle()
        {
            Dictionary<(int x, int y, int z, int w), int> OnCounter = [];
            foreach ((int x, int y, int z, int w) cube in CubesOn)
            {
                GetNeightbours(cube, OnCounter);
            }

            HashSet<(int x, int y, int z, int w)> NewCubesOn = [];
            foreach (KeyValuePair<(int x, int y, int z, int w), int> item in OnCounter)
            {
                if (item.Value == 3 ||
                    (item.Value == 2 && CubesOn.Contains(item.Key)))
                {
                    NewCubesOn.Add(item.Key);
                }
            }
            return NewCubesOn;
        }

        public void ExecuteCycles(int numberOfCycles)
        {
            for (int i = 0; i < numberOfCycles; i++)
            {
                CubesOn = ExecuteCycle();
            }
        }

        public int CubesOnCount
        {
            get
            { return CubesOn.Count; }
        }
    }
}
