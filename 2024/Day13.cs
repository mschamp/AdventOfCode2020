using System.Text.RegularExpressions;

namespace _2024
{
    public class Day13:PuzzleWithObjectArrayInput<(double x1, double y1, double x2, double y2, double rx, double ry)>

    {
        public Day13():base(13,2024)
        {
            
        }

        public override string SolvePart1((double x1, double y1, double x2, double y2, double rx, double ry)[] input)
        {
            return input.Sum(x=> CalculateScore(x.x1,x.y1, x.x2, x.y2, x.rx, x.ry)).ToString();
        }

        private decimal CalculateScore(double x1, double y1, double x2, double y2, double rx, double ry)
        {
            double btnA = (rx* y2 - ry * x2) / (x1 * y2 - y1 * x2);
            double btnB = (ry * x1 - rx * y1) / (x1 * y2 - y1 * x2);

            if (double.IsInteger(btnA) && double.IsInteger(btnB)) return (decimal)(3 * btnA + btnB);
                return 0;
        }

        public override string SolvePart2((double x1, double y1, double x2, double y2, double rx, double ry)[] input)
        {
            return input.Sum(x => CalculateScore(x.x1, x.y1, x.x2, x.y2, 10_000_000_000_000 + x.rx, 10_000_000_000_000 + x.ry)).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

") == "280");

            Debug.Assert(SolvePart1(@"Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279") == "480");
        }

        Regex rgx = new Regex(@".*X\+(\d+).*Y\+(\d+)\r\n.*X\+(\d+).*Y\+(\d+)\r\n.*X=(\d+).*Y=(\d+)", RegexOptions.Multiline | RegexOptions.Compiled);

        protected override (double x1, double y1, double x2, double y2, double rx, double ry) CastToObject(string RawData)
        {
           var mt = rgx.Match(RawData);

            return (double.Parse(mt.Groups[1].Value), double.Parse(mt.Groups[2].Value),
                double.Parse(mt.Groups[3].Value), double.Parse(mt.Groups[4].Value),
                double.Parse(mt.Groups[5].Value), double.Parse(mt.Groups[6].Value));
        }
    }
}
