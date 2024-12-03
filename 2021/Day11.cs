using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2021
{
	public class Day11 : General.PuzzleWithObjectInput<List<Day11.Octopus>>
    {
        public Day11() : base(11, 2021)
        {

        }
        protected override List<Day11.Octopus> CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);
            Dictionary<(int, int), Octopus> grid = [];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[(j, i)] = new Octopus(j, i, lines[i][j] - '0');
                }
            }

            List<Octopus> octos = grid.Values.ToList();
            octos.ForEach(x => x.FindPositionsAround(grid));

            return octos;
        }

        public override string SolvePart1(List<Day11.Octopus> input)
        {
            int Result = 0;
            for (int i = 0; i < 100; i++)
            {
                Result += RunStep(input);
            }
            return Result.ToString();
        }

        private int RunStep(List<Octopus> octopi)
        {
            octopi.ForEach(x => x.IncreaseEnergy());
            HashSet<Octopus> Flashed = [];

            while(octopi.Where(x => x.EnergyLevel>9).Except(Flashed).Any())
            {
                foreach (var item in octopi.Where(x => x.EnergyLevel > 9).Except(Flashed))
                {
                    item.Flash();
                    Flashed.Add(item);
                } 
            }

            Flashed.ToList().ForEach(x => x.ResetEnergyLevel());
            return Flashed.Count();
        }

        public override string SolvePart2(List<Day11.Octopus> input)
        {
            int Result = 1;
            while (RunStep(input)< input.Count)
            {
                Result++;
            }
            return Result.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526") == "1656");

            Debug.Assert(SolvePart2(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526") == "195");
        }

        public class Octopus
        {
            private int X;
            private int Y;
            
            public int EnergyLevel { get; set; }

            public Octopus(int X, int Y, int EnergyLevel)
            {
                this.X = X;
                this.Y = Y;
                this.EnergyLevel = EnergyLevel;
            }

            public void IncreaseEnergy()
            {
                EnergyLevel++;
            }

            public void Flash()
            {
                positionsAround.ForEach(x => x.IncreaseEnergy());
            }

            public void ResetEnergyLevel()
            {
                EnergyLevel = 0;
            }

            internal void FindPositionsAround(Dictionary<(int, int), Octopus> grid)
            {
                positionsAround = [];
                if (grid.TryGetValue((X - 1, Y), out Octopus left)) positionsAround.Add(left);
                if (grid.TryGetValue((X, Y - 1), out Octopus above)) positionsAround.Add(above);
                if (grid.TryGetValue((X + 1, Y), out Octopus right)) positionsAround.Add(right);
                if (grid.TryGetValue((X, Y + 1), out Octopus below)) positionsAround.Add(below);

                if (grid.TryGetValue((X - 1, Y-1), out Octopus lefttp)) positionsAround.Add(lefttp);
                if (grid.TryGetValue((X+1, Y - 1), out Octopus rigtTp)) positionsAround.Add(rigtTp);
                if (grid.TryGetValue((X + 1, Y+1), out Octopus rightbt)) positionsAround.Add(rightbt);
                if (grid.TryGetValue((X-1, Y + 1), out Octopus leftbt)) positionsAround.Add(leftbt);
            }

            public List<Octopus> positionsAround { get; set; }

            public override string ToString()
            {
                return EnergyLevel.ToString();
            }
        }
    }
}
