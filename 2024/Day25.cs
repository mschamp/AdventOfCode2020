
namespace _2024
{
    public class Day25 : PuzzleWithObjectInput<(HashSet<int[]> locks, HashSet<int[]> keys) >
    {
        public Day25():base(25,2024)
        {
                
        }

        public override string SolvePart1((HashSet<int[]> locks, HashSet<int[]> keys) input)
        {
            int counter = 0;
            foreach (var l in input.locks)
            {
                foreach (var k in input.keys)
                {
                    if(IsValid(l,k)) counter++;
                }
            }
            return counter.ToString();
        }

        private bool IsValid(int[] first, int[]second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] + second[i] > 5) return false;
            }
            return true;
        }

        public override string SolvePart2((HashSet<int[]> locks, HashSet<int[]> keys) input)
        {
            return General.Constants.stringConstants.Kerst;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"#####
.####
.####
.####
.#.#.
.#...
.....

#####
##.##
.#.##
...##
...#.
...#.
.....

.....
#....
#....
#...#
#.#.#
#.###
#####

.....
.....
#.#..
###..
###.#
###.#
#####

.....
.....
.....
#....
#.#..
#.#.#
#####") =="3");
        }

        protected override (HashSet<int[]> locks, HashSet<int[]> keys) CastToObject(string RawData)
        {
            string[] codes = RawData.Split(General.Constants.stringConstants.DoubleEnter);
            HashSet<int[]> locks = new();
            HashSet<int[]> keys = new();

            foreach (string code in codes)
            {
                string[] lines = code.Split(Environment.NewLine);
                char first = code[0];
                List<int> lengts = new List<int>();
                for (int i = 0; i < lines[0].Length; i++)
                {
                    int counter = 0;
                    while (lines[counter][i]==first)
                    {
                        counter++;
                    }
                    lengts.Add(counter);
                }
                switch (first)
                {
                    case '.':
                        keys.Add(lengts.Select(x => lines.Length-x - 1).ToArray());
                        break;
                    case '#':
                        locks.Add(lengts.Select(x => x - 1).ToArray());
                        break;
                    default:
                        break;
                }
            }

            return (locks, keys);
        }
    }
}
