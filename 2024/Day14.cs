using System.Text;
using System.Text.RegularExpressions;

namespace _2024
{
    public class Day14:PuzzleWithObjectArrayInput<(int x, int y, int vx, int vy)>
    {
        public Day14():base(14,2024)
        {
            
        }

        public override string SolvePart1((int x, int y, int vx, int vy)[] input)
        {
            int W = 101;
            int H = 103;

            if (input.Length < 20)
            {
                W = 11;
                H = 7;
            }

            Dictionary<(bool top, bool left), int> counters = new Dictionary<(bool top, bool left), int>{ { (false, false),0},{ (false, true), 0 },
            { (true, false),0},{ (true, true), 0 }};

            for (int i = 0; i<input.Length;i++)
            {
                input[i].x = (input[i].x + 100 * (input[i].vx+W)) % W;
                input[i].y = (input[i].y + 100 * (input[i].vy+H)) % H;

                if (input[i].y == H / 2 || input[i].x == W / 2) continue;
                counters[(input[i].y > H / 2, input[i].x > W / 2)]++;
            }

            return counters.Values.Aggregate(1, (acc, v) => acc * v).ToString();
        }

        public override string SolvePart2((int x, int y, int vx, int vy)[] input)
        {

            int W = 101;
            int H = 103;

            int counter = 0;

            do
            {
                counter++;
                for (int i = 0; i < input.Length; i++)
                {
                    input[i].x = (input[i].x + input[i].vx + W) % W;
                    input[i].y = (input[i].y +input[i].vy + H) % H;

                }
            }
            while (input.GroupBy(x => (x.x, x.y)).Any(x => x.Count() > 1));

            HashSet<(int x, int y)> used = input.Select(x=>(x.x, x.y)).ToHashSet();
            StringBuilder bld = new();
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    bld.Append(used.Contains((x, y)) ? General.Constants.charConstants.White : " ");
                }
                bld.AppendLine();
            }
            return bld.ToString() + counter.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3") == "12");
        }

        Regex rgx = new Regex(@"=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)", RegexOptions.Compiled);

        protected override (int x, int y, int vx, int vy) CastToObject(string RawData)
        {
            var mt = rgx.Match(RawData);
            return (int.Parse(mt.Groups[1].Value), int.Parse(mt.Groups[2].Value), int.Parse(mt.Groups[3].Value), int.Parse(mt.Groups[4].Value));
        }
    }
}
