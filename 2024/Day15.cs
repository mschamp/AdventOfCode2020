using MoreLinq;

namespace _2024
{
    public class Day15:PuzzleWithObjectInput<(HashSet<clsPoint> walls, HashSet<Day15.box> boxes, clsPoint robot, List<char> Instructions, (int x, int y) size )>
    {
        public Day15():base(15,2024)
        {
            
        }

        public override string SolvePart1((HashSet<clsPoint> walls, HashSet<box> boxes, clsPoint robot, List<char> Instructions, (int x, int y) size) input)
        {
            foreach (var instruction in input.Instructions)
            {
                if (TryMove(input.walls, input.boxes, input.robot, instruction, out HashSet<box> NewBoxes, out clsPoint newRobot))
                {
                    input.robot = newRobot;
                    input.boxes = NewBoxes;
                }
            }

            return input.boxes.Sum(x=> CalculatePosition(x,input.size)).ToString();
        }

        public override string SolvePart2((HashSet<clsPoint> walls, HashSet<box> boxes, clsPoint robot, List<char> Instructions, (int x, int y) size) input)
        {
            HashSet<clsPoint> scaledWalls = new();
            foreach (var item in input.walls)
            {
                scaledWalls.Add(new clsPoint(item.X * 2, item.Y));
                scaledWalls.Add(new clsPoint(item.X * 2+1, item.Y));
            }

            HashSet<box> scaledBoxes = new();
            foreach (var item in input.boxes)
            {
                scaledBoxes.Add(new box(item.X * 2, item.Y,2,1));
            }

            input.robot = new clsPoint(2 * input.robot.X, input.robot.Y);

            foreach (var instruction in input.Instructions)
            {
                if (TryMove2(scaledWalls, scaledBoxes, input.robot, instruction, out HashSet<box> NewBoxes, out clsPoint newRobot))
                {
                    input.robot = newRobot;
                    scaledBoxes = NewBoxes;
                }
            }

            return scaledBoxes.Sum(x => CalculatePosition(x, input.size)).ToString();
        }

        private bool TryMove(HashSet<clsPoint> walls, HashSet<box> boxes, clsPoint robot, char Instruction,
            out HashSet<box> NewBoxes,out clsPoint newRobot)
        {
            newRobot = robot.Move(Instruction,1).First();
            NewBoxes = boxes.ToHashSet();

            if(walls.Contains(newRobot)) return false;
            Queue<box> boxesToMove = new Queue<box>();
            boxes.Intersect([newRobot]).ForEach(x=> boxesToMove.Enqueue((box)x));

            HashSet<box> ToRemove = new();
            HashSet<box> ToAdd = new();

            while(boxesToMove.TryDequeue(out box box))
            {
               box newb = box.Move(Instruction, 1).Select(x=> new box(x.X,x.Y, box.Sx,box.Sy)).First();
               if (walls.Contains(newb)) return false;
               ToRemove.Add(box);
               ToAdd.Add(newb);
               boxes.Intersect([newb]).ForEach(x => boxesToMove.Enqueue(x));
            }

            foreach (box newb in ToRemove)
            {
                NewBoxes.Remove(newb);
            }

            foreach (box newb in ToAdd)
            {
                NewBoxes.Add(newb);
            }

            return true;
        }

        private bool TryMove2(HashSet<clsPoint> walls, HashSet<box> boxes, clsPoint robot, char Instruction,
           out HashSet<box> NewBoxes, out clsPoint newRobot)
        {
            newRobot = robot.Move(Instruction, 1).First();
            NewBoxes = boxes.ToHashSet();

            if (walls.Contains(newRobot)) return false;
            Queue<box> boxesToMove = new Queue<box>();

            Dictionary<clsPoint, box> lookup = new();
            foreach (var box1 in boxes)
            {
                box1.UsedLocations().ForEach(x => lookup.Add(x, box1));               
            }

            if (lookup.TryGetValue(newRobot, out box collision)) boxesToMove.Enqueue(collision);

            HashSet<box> ToRemove = new();
            HashSet<box> ToAdd = new();
            HashSet<box> Seen = new();

            while (boxesToMove.TryDequeue(out box box))
            {
                if (Seen.Contains(box)) continue;
                Seen.Add(box);
                box newb = box.Move(Instruction, 1).Select(x => new box(x.X, x.Y, box.Sx, box.Sy)).First();
                if (walls.Intersect(newb.UsedLocations()).Any()) return false;
                ToRemove.Add(box);
                ToAdd.Add(newb);

                HashSet<box> TempToMove = new();
                foreach(var loc in newb.UsedLocations())
                {
                    if (lookup.TryGetValue(loc,out box collision2)&&collision2!=box) TempToMove.Add(collision2);
                }

                TempToMove.ForEach(x=>boxesToMove.Enqueue(x));
            }

            foreach (box newb in ToRemove)
            {
                NewBoxes.Remove(newb);
            }

            foreach (box newb in ToAdd)
            {
                NewBoxes.Add(newb);
            }

            return true;
        }


        public int CalculatePosition (clsPoint box, (int x, int y) sizes)
        {
            return (int)(100*(sizes.y-box.Y)+box.X);
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"########
#..O.O.#
##@.O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

<^^>>>vv<v>>v<<") == "2028");

            Debug.Assert(SolvePart1(@"##########
#..O..O.O#
#......O.#
#.OO..O.O#
#..O@..O.#
#O#..O...#
#O..O..O.#
#.OO.O.OO#
#....O...#
##########

<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^") == "10092");

            Debug.Assert(SolvePart2(@"#######
#...#.#
#.....#
#..OO@#
#..O..#
#.....#
#######

<vv<<^^<<^^") == "618");

            Debug.Assert(SolvePart2(@"##########
#..O..O.O#
#......O.#
#.OO..O.O#
#..O@..O.#
#O#..O...#
#O..O..O.#
#.OO.O.OO#
#....O...#
##########

<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^") == "9021");
        }

        protected override (HashSet<clsPoint> walls, HashSet<box> boxes, clsPoint robot, List<char> Instructions, (int x, int y) size) CastToObject(string RawData)
        {
            string[] parts = RawData.Split(Environment.NewLine+Environment.NewLine);

            HashSet<clsPoint> walls = new();
            HashSet<box> boxes = new();
            clsPoint robot=null;

            string[] grid = parts[0].Split(Environment.NewLine);
            (int x, int y) size = (grid[0].Length, grid.Length);
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    switch (grid[y][x])
                    {
                        case '#':
                            walls.Add(new clsPoint(x, grid.Length - y));
                            break;
                        case 'O':
                            boxes.Add(new box(x, grid.Length - y,1,1));
                            break;
                        case '@':
                            robot = new clsPoint(x, grid.Length - y);
                            break;
                        default:
                            break;
                    }
                }
            }
            
            List<char>instructions = parts[1].Replace(Environment.NewLine, "").ToList();

            return (walls, boxes, robot, instructions,size);
        }

    public class box:clsPoint
        {

            public box(double x, double y, int Sx, int Sy):base(x,y)
            {
                this.Sx = Sx;
                this.Sy = Sy;
            }

            public int Sx { get; }
            public int Sy { get; }

            public IEnumerable<clsPoint> UsedLocations()
            {
                int scX = 1;
                while (scX <= Sx)
                {
                    int scY = 1;
                    while (scY <= Sy)
                    {
                        yield return new clsPoint(X + scX - 1, Y + scY - 1);
                        scY++;
                    }
                    scX++;
                }
            }
        }
    }
}
