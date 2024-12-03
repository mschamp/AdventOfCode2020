using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace _2019
{
	public class Day12 : General.IAoC
    {
        public int Day => 12;
        public int Year => 2019;

        public string SolvePart1(string input = null)
        {
            List<Moon> moons = [];
            int loops = 0;
            foreach (string line in input.Split(Environment.NewLine))
            {
                if (line.StartsWith("<"))
                {
                    moons.Add(new Moon(line));
                }
                else
                {
                    loops = int.Parse(line);
                }

            }
            for (int i = 0; i < loops; i++)
            {
                foreach (Moon moon1 in moons)
                {
                    foreach (Moon moon2 in moons)
                    {
                        moon1.UpdateVelocitie(moon2);
                    }
                }

                moons.ForEach(x => x.UpdatePosition());
            }
            
            return ""+moons.Sum(x => x.Energy);
        }

        public string SolvePart2(string input = null)
        {
            List<Moon> moonsOriginal = [];
            List<Moon> moons = [];
            foreach (string line in input.Split(Environment.NewLine))
            {
                if (line.StartsWith("<"))
                {
                    moons.Add(new Moon(line));
                    moonsOriginal.Add(new Moon(line));
                }

            }
            long[] Cycles = new[] { (long)-1, (long)-1, (long)-1 };
            long loops = 0;
            while (Cycles.Any(x => x==-1))
            {
                loops++;
                foreach (Moon moon1 in moons)
                {
                    foreach (Moon moon2 in moons)
                    {
                        moon1.UpdateVelocitie(moon2);
                    }
                }

                moons.ForEach(x => x.UpdatePosition());

                if (Cycles[0]==-1&&moons.All(x => x.Velocity.X==0 ))
                {
                    Cycles[0] = loops;
                }
                if (Cycles[1] == -1 && moons.All(x => x.Velocity.Y == 0))
                {
                    Cycles[1] = loops;
                }
                if (Cycles[2] == -1 && moons.All(x => x.Velocity.Z == 0))
                {
                    Cycles[2] = loops;
                }
            }

            Func<long, long, long> lowCM = General.MathFunctions.findLCM();
            long lcm = 1;
            foreach (long period in Cycles)
            {
                lcm = lowCM(lcm, period);
            }

            return ""+ lcm*2;


        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>
10") == "179");

            Debug.Assert(SolvePart1(@"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>
100") == "1940");

            Debug.Assert(SolvePart2(@"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>") == "2772");

            Debug.Assert(SolvePart2(@"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>
100") == "4686774924");
        }
    }
    
    public class Moon
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }

        public Moon(string position)
        {
            Func<string, Vector3> posExtr = PositionExtractor();
            Velocity = new Vector3(0,0,0);
            Position = posExtr(position);
        }

        public void UpdateVelocitie(Moon other)
        {
            if (other != this)
            {
                float dx = other.Position.X - Position.X;
                float dy = other.Position.Y - Position.Y;
                float dz = other.Position.Z - Position.Z;
                Velocity += new Vector3(dx == 0 ? 0 : Math.Sign(dx),
                    dy == 0 ? 0 : Math.Sign(dy),
                    dz == 0 ? 0 : Math.Sign(dz));
            }
        }
        
        public void UpdatePosition()
        {
            Position += Velocity;
        }

        public int PotentialEnergy
        {
            get
            {
                return (int)(Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z));
            }
        }

        public int KineticEnergy
        {
            get
            {
                return (int)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z));
            }
        }

        public int Energy
        {
            get
            {
                return PotentialEnergy * KineticEnergy;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj==null)
            {
                return false;
            }
            else if (obj is Moon)
            {
                Moon moon = (Moon)obj;
                return Position.X==moon.Position.X &&
                    Position.Y == moon.Position.Y &&
                    Position.Z == moon.Position.Z &&
                    Velocity.X == moon.Velocity.X &&
                    Velocity.Y == moon.Velocity.Y &&
                    Velocity.Z == moon.Velocity.Z;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (int)(Position.Length()*Velocity.Length());
        }

        public static Func<string, Vector3> PositionExtractor()
        {
            Regex rgx = new(@"(?<x>-?\d+),\sy=(?<y>-?\d+),\sz=(?<z>-?\d+)");

            return input =>
            {
                Match mtch = rgx.Match(input);
                return new Vector3(int.Parse(mtch.Groups["x"].Value), int.Parse(mtch.Groups["y"].Value), int.Parse(mtch.Groups["z"].Value));
            };
        }
    }
}
