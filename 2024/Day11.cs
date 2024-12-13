namespace _2024
{
    public class Day11:PuzzleWithObjectInput<Dictionary<long,long>>
    {
        public Day11():base(11,2024)
        {
            
        }

        public override string SolvePart1(Dictionary<long, long> input)
        {
            Dictionary<long, long> newValues = new();
            for (long i = 0; i < 25; i++)
            {
                newValues = new();
                foreach (var key in input.Keys)
                {
                    foreach (var newStone in blink(key))
                    {
                        if (!newValues.ContainsKey(newStone)) newValues[newStone] = 0;
                        newValues[newStone] += input[key];
                    }
                }
                input=newValues;
            }
            return newValues.Values.Sum().ToString();
        }

        private IEnumerable<long> blink(long rockValue)
        {
            string rock = rockValue.ToString();
            if (rockValue == 0) yield return 1;
            else if (rock.Length % 2 == 0)
            {
                yield return long.Parse(rock.Substring(0,rock.Length/2));
                yield return long.Parse(rock.Substring(rock.Length / 2, rock.Length / 2));
            }
            else
            {
                yield return 2024 * rockValue;
            }
        }

        public override string SolvePart2(Dictionary<long, long> input)
        {
            Dictionary<long, long> newValues = new();
            for (long i = 0; i < 75; i++)
            {
                newValues = new();
                foreach (var key in input.Keys)
                {
                    foreach (var newStone in blink(key))
                    {
                        if (!newValues.ContainsKey(newStone)) newValues[newStone] = 0;
                        newValues[newStone] += input[key];
                    }
                }
                input = newValues;
            }
            return newValues.Values.Sum().ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("125 17") == "55312");
        }

        protected override Dictionary<long, long> CastToObject(string RawData)
        {
           return RawData.Split(' ').GroupBy(x=>x).ToDictionary(x=>long.Parse(x.Key),x=>(long)x.Count());
        }
    }
}
