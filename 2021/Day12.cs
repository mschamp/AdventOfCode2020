using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021
{
    public class Day12 : General.PuzzleWithObjectInput<Dictionary<string, Day12.Cave>>
    {
        public Day12() : base(12, 2021)
        {

        }
        protected override Dictionary<string, Cave> CastToObject(string RawData)
        {
            Dictionary<string, Cave> caves = new();
            foreach (string line in RawData.Split(Environment.NewLine))
            {
                IEnumerable< Cave> parts = line.Split("-").Select(x => GetOrCreateCave(x, caves));
                parts.First().AddConnection(parts.Last());
            }

            return caves;
        }

        private Cave GetOrCreateCave(string Name, Dictionary<string, Cave> caves)
        {
            if (caves.TryGetValue(Name, out Cave cave)) return cave;
            Cave newCave = new(Name);
            caves.Add(Name, newCave);
            return newCave;
        }

        public override string SolvePart1(Dictionary<string, Cave> input)
        {
            Cave startCave = input["start"];
            Cave endCave = input["end"];

            return CreatePaths(startCave, endCave).Count().ToString();
        }

        public override string SolvePart2(Dictionary<string, Cave> input)
        {
            Cave startCave = input["start"];
            Cave endCave = input["end"];

            return CreatePathsWithOneDouble(startCave, endCave).Count().ToString();
        }

        private IEnumerable<string> CreatePaths(Cave start, Cave end)
        {
            var queue = new Queue<(Cave c, string path, bool twice)>();
            queue.Enqueue((start, start.Name, false));

            while (queue.Any())
            {
                var c = queue.Dequeue();
                if (c.c == end)
                    yield return c.path;

                foreach (var p in c.c.ConnectedCaves)
                {

                        if (p.IsBigCave || !c.path.Contains(p.Name))
                            queue.Enqueue((p, c.path + "-" + p.Name, false));
                   
                }
            }
        }

        private IEnumerable<string> CreatePathsWithOneDouble(Cave start, Cave end)
        {
            var queue = new Queue<(Cave c, string path, bool twice)>();
            queue.Enqueue((start, start.Name, false));

            while (queue.Any())
            {
                var c = queue.Dequeue();
                if (c.c == end)
                    yield return c.path;

                foreach (var p in c.c.ConnectedCaves)
                {

                    if (p.IsBigCave)
                    {
                        queue.Enqueue((p, c.path + "-" + p.Name, c.twice));
                    }
                    else if (!c.twice)
                    {
                        if (!c.path.Contains(p.Name))
                            queue.Enqueue((p, c.path + "-" + p.Name, false));
                        else if (p != start && p != end)
                            queue.Enqueue((p, c.path + "-" + p.Name, true));
                    }
                    else
                    {
                        if (!c.path.Contains(p.Name))
                            queue.Enqueue((p, c.path + "-" + p.Name, true));
                    }

                }
            }
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end") == "10"); 
            Debug.Assert(SolvePart1(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc") == "19"); 
            Debug.Assert(SolvePart1(@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW") == "226");

            Debug.Assert(SolvePart2(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end") == "36");
            Debug.Assert(SolvePart2(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc") == "103");
            Debug.Assert(SolvePart2(@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW") == "3509");
        }

        public class Cave
        {
            public Cave(string Name)
            {
                this.Name = Name;
                IsBigCave = char.IsUpper(Name[0]);
                ConnectedCaves = new List<Cave>();
            }

            public string Name { get; set; }
            public bool IsBigCave { get; set; }

            public List<Cave> ConnectedCaves { get; set; }

            public void AddConnection(Cave otherCave)
            {
                ConnectedCaves.Add(otherCave);
                otherCave.ConnectedCaves.Add(this);
            }
        }
    }
}
