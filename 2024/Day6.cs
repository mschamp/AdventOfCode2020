using System.Collections.Immutable;

namespace _2024
{
    public class Day6: PuzzleWithObjectInput<(ImmutableHashSet<clsPoint> obstacles, clsPoint start, (int x,int y)size)>
    {
        public Day6():base(6,2024)
        {

        }

        public override string SolvePart1((ImmutableHashSet<clsPoint> obstacles, clsPoint start, (int x, int y) size) input)
        {
            return getPath(input.obstacles,input.start,input.size).Count.ToString();
        }

        private HashSet<clsPoint>? getPath (ImmutableHashSet<clsPoint> obstacles, clsPoint start, (int x, int y) size)
        {
            clsPoint position = start;
            Direction direction = Direction.Up;
            HashSet<clsPoint> visited = new HashSet<clsPoint>();
            HashSet<(clsPoint,Direction)> loopDetection = new();

            while (position.X >= 0 && position.X <= size.x &&
                position.Y >= 0 && position.Y <= size.y)
            {
                visited.Add(position);
                if (loopDetection.Contains((position, direction))) return null;
                loopDetection.Add((position, direction));
                clsPoint tryNext = position.Move(direction);
                if (obstacles.Contains(tryNext))
                {
                    direction = (Direction)(((int)(direction + 3)) % 4);
                    continue;
                }
                position = tryNext;

            }
            return visited;
        }

        public override string SolvePart2((ImmutableHashSet<clsPoint> obstacles, clsPoint start, (int x, int y) size) input)
        {
            HashSet<clsPoint> orignalPath = getPath(input.obstacles, input.start, input.size);
            orignalPath.Remove(input.start);
            int counter = orignalPath.AsParallel().Count(item => getPath(input.obstacles.Add(item), input.start, input.size) == null) ;

            return counter.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...") == "42");

            Debug.Assert(SolvePart2(@"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...") == "6");
        }

        protected override (ImmutableHashSet<clsPoint> obstacles, clsPoint start, (int x, int y) size) CastToObject(string RawData)
        {
            string[] lines = RawData.Split(Environment.NewLine);

            clsPoint start = null;
            (int,int) size = (lines[0].Length, lines.Length);
            HashSet<clsPoint> obstacles = new HashSet<clsPoint> ();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            obstacles.Add(new clsPoint(x, lines.Length- y));
                            continue;
                        case '^':
                            start = new clsPoint(x, lines.Length-y);
                            continue;
                        default:
                            break;
                    }
                }
            }

            
            return (obstacles.ToImmutableHashSet(), start,size);
        }
    }
}
