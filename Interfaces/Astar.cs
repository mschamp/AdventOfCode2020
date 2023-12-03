using System;
using System.Collections.Generic;

namespace General
{
	public class Astar
    {
        public static List<T> AstarSolver<T>(IEnumerable<T> start, T goal, Func<T, int> CostFunction) where T : Position
        {
            Queue<T> openSet = new();
            Dictionary<T, T> cameFrom = new();
            Dictionary<T, int> gScore = new();

            foreach (T item in start)
            {
                openSet.Enqueue(item);
                gScore[item] = 0;
            }
            

            while (openSet.TryDequeue(out T cur))
            {
                if (cur.Equals(goal))
                {
                    return reconstruct_path(cameFrom, cur);
                }

                foreach (T item in cur.positionsAround)
                {
                    var tentGScore = gScore[cur] + CostFunction(cur);
                    if (tentGScore < gScore.GetValueOrDefault(item, int.MaxValue))
                    {
                        cameFrom[item] = cur;
                        gScore[item] = tentGScore;
                        openSet.Enqueue(item);
                    }
                }

            }
            return new List<T>();
        }

        public static List<T> AstarSolver<T>(T start, T goal, Func<T, int> CostFunction) where T : Position
        {
            return AstarSolver<T>(new List<T>() { start },goal,CostFunction);
        }


            private static List<T> reconstruct_path<T>(Dictionary<T, T> cameFrom, T current)
        {
            List<T> totalPath = new() { current };
            while (cameFrom.TryGetValue(current, out current))
            {
                totalPath.Insert(0, current);
            }
            return totalPath;

        }

        public class Position
        {
            public override string ToString()
            {
                return $"({X},{Y}) {Value}";
            }
            public Position(int x, int y, int value)
            {
                X = x;
                Y = y;
                Value = value;
            }

            public int manhattan(Position other)
            {
                return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
            }

            public void FindPositionsAround(Dictionary<(int, int), Position> grid, Func<Position, Position, bool> FilterFunction)
            {
                positionsAround = new List<Position>();
                if (grid.TryGetValue((X - 1, Y), out Position left) && FilterFunction(this, left)) positionsAround.Add(left);
                if (grid.TryGetValue((X, Y - 1), out Position above) && FilterFunction(this, above)) positionsAround.Add(above);
                if (grid.TryGetValue((X + 1, Y), out Position right) && FilterFunction(this, right)) positionsAround.Add(right);
                if (grid.TryGetValue((X, Y + 1), out Position below) && FilterFunction(this, below)) positionsAround.Add(below);
            }

            public int X { get; private set; }
            public int Y { get; private set; }
            public int Value { get; private set; }

            public List<Position> positionsAround { get; set; }
        }
    }
}
