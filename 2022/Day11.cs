using System.Diagnostics;

namespace _2022
{
	public class Day11:General.PuzzleWithObjectArrayInput<Day11.Monkey>
    {
        public Day11() : base(11, 2022)
        {
        }

        public override string SolvePart1(Monkey[] monkeys)
        {
            return RunInspections(monkeys, 20, x => x /3).ToString();
        }

        private long RunInspections(Monkey[] monkeys, int Rounds, Func<long,long> ReduceWorryLevelFunction)
        {
            for (long round = 0; round < Rounds; round++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    while (monkey.Items.TryDequeue(out long Item))
                    {
                        monkey.Inspections++;
                        Item = ReduceWorryLevelFunction(monkey.Operation(Item));
                        long Target = monkey.ThrowToMonkey[Item % monkey.TestDivide == 0];
                        Monkey targetMonkey = monkeys[Target];
                        targetMonkey.Items.Enqueue(Item);
                    }
                }
            }

            var TopMonkeys = monkeys.OrderByDescending(x => x.Inspections).Take(2).ToArray();
            return (TopMonkeys[0].Inspections * TopMonkeys[1].Inspections);
        }

        public override string SolvePart2(Monkey[] monkeys)
        {
            long ProductOfDivisionTest=monkeys.Select(x => x.TestDivide).Aggregate((result, newValue) => result * newValue);
            return RunInspections(monkeys,10000, x => x% ProductOfDivisionTest).ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1") == "10605");

            Debug.Assert(SolvePart2(@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1") == "2713310158");
        }

        protected override Monkey CastToObject(string RawData)
        {
            return new Monkey(RawData);
        }

        public class Monkey
        {
            public Monkey(string rawData)
            {
                var lines = rawData.Split(Environment.NewLine).Select(x => x.Trim()).ToArray();
                Number = long.Parse(lines[0].Split(new char[] { ' ', ':' })[1]);
                Items = new Queue<long>();
                foreach (var item in lines[1].Split(": ")[1].Split(", ").Select(x => long.Parse(x)))
                {
                    Items.Enqueue(item);
                }
                Operation = GetOperation(lines[2].Split(": ")[1]);
                TestDivide = long.Parse(lines[3].Split(' ')[3]);
                ThrowToMonkey = new Dictionary<bool, long> { 
                    { true, long.Parse(lines[4].Split(' ')[5]) }, 
                    { false,long.Parse(lines[5].Split(' ')[5]) } };
                Inspections = 0;
            }

            public long Number { get; private set; }
            public Queue<long> Items { get; private set; }
            public Func<long,long> Operation { get; private set; }
            public long TestDivide { get;private set; }
            public Dictionary<bool,long> ThrowToMonkey { get;private set; }

            public long Inspections { get; set; }

            private Func<long, long> GetOperation(string operation)
            {
                var parts = operation.Split(' ');
                switch (parts[3])
                {
                    case "+":
                        return x => x + long.Parse(parts[4]);
                    case "*":
                        if (parts[4] == "old") return x => x * x;
                        return x => x * long.Parse(parts[4]);
                    default:
                        return x => x;
                }
            }
        }
    }
}
