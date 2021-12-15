using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day6 : General.IAoC
    {
        public int Day => throw new NotImplementedException();

        public string SolvePart1(string input = null)
        {
            string[] textOrbits = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Orbit> orbits = new();

            foreach (string text in textOrbits)
            {
                string[] parts = text.Split(')');
                if (!orbits.ContainsKey(parts[0])) { orbits.Add(parts[0], new Orbit(parts[0], null)); } // Save the first orbit
                if (!orbits.ContainsKey(parts[1]))
                { orbits.Add(parts[1], new Orbit(parts[1], orbits[parts[0]])); } //Save orbiting object
                else
                {
                    orbits[parts[1]].Parent = orbits[parts[0]]; // update parent if object already exist
                }
            }

            return "" + orbits.Values.Sum(x => x.Orbits());
        }

        public string SolvePart2(string input = null)
        {
            string[] textOrbits = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Orbit> orbits = new();

            foreach (string text in textOrbits)
            {
                string[] parts = text.Split(')');
                if (!orbits.ContainsKey(parts[0])) { orbits.Add(parts[0], new Orbit(parts[0], null)); } // Save the first orbit
                if (!orbits.ContainsKey(parts[1])) 
                    { orbits.Add(parts[1], new Orbit(parts[1], orbits[parts[0]])); } //Save orbiting object
                else
                {
                    orbits[parts[1]].Parent = orbits[parts[0]]; // update parent if object already exist
                }
                
            }

            List<Orbit> YouToOrigin = new();
            Orbit orbit = orbits["YOU"].Parent;
            while(orbit.Parent != null) //While not in origin
            {
                YouToOrigin.Add(orbit);
                orbit = orbit.Parent;
            }

            Orbit SantaOrbit = orbits["SAN"].Parent;
            while (SantaOrbit.Parent != null) //While not in origin 
            {
                if (YouToOrigin.Contains(SantaOrbit))
                {
                    YouToOrigin.Remove(SantaOrbit);
                }
                else
                {
                    YouToOrigin.Add(SantaOrbit);
                }
                SantaOrbit = SantaOrbit.Parent;
            }

            return "" + YouToOrigin.Count();

        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L") == "42");

            Debug.Assert(SolvePart2(@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L
K)YOU
I)SAN") == "4");
        }
    }

    public class Orbit
    {
        public Orbit(string name, Orbit parent = null)
        {
            Name = name;
            Parent = parent;
        }
        public string Name { get; private set; }
        public Orbit Parent { get; set; }
        public int Orbits()
        {
            if (Parent==null)
            {
                return 0;
            }
            return Parent.Orbits() + 1;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return this.Name.Length;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is Orbit)
            {
                return ((Orbit)obj).Name == this.Name;
            }
            return base.Equals(obj);
        }

    }
}
