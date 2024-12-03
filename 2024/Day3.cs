using System.Text.RegularExpressions;

namespace _2024
{
    public class Day3 : abstractPuzzleClass
    {
        public Day3():base(3,2024)
        {

        }
        public override string SolvePart1(string input = null)
        {
            Regex rgx = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled);
            var mtchs = rgx.Matches(input);

            long result = mtchs.Aggregate(0, (v, m) => v + int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));

            return result.ToString();
        }

        public override string SolvePart2(string input = null)
        {
            Regex rgx = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)", RegexOptions.Compiled);
            
            var mtchs = rgx.Matches(input);

            bool enabled = true;

            long result = 0;


            foreach (var item in mtchs.AsEnumerable())
            {
                if (item.Value == "don't()")
                {
                    enabled = false;
                }
                else if (item.Value == "do()")
                {
                    enabled = true;
                }
                else if (enabled)
                {
                    result += (int.Parse(item.Groups[1].Value) * int.Parse(item.Groups[2].Value));
                }
            }

            return result.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))") == "161");
            Debug.Assert(SolvePart2("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))") == "48");
        }
    }
}
