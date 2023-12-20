using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023
{
	public class Day20 : PuzzleWithObjectArrayInput<Day20.Module>
	{
        public Day20():base(20,2023)
        {
            
        }
       public override void Tests()
		{
			Debug.Assert(SolvePart1(@"broadcaster -> a, b, c
%a -> b
%b -> c
%c -> inv
&inv -> a") == "32000000");
        }

		protected override Module CastToObject(string RawData)
		{
			switch (RawData[0])
			{
				case '%':
					return new FlipFlop(RawData);
				case '&':
					return new Conjunction(RawData);
				default:
					return new Broadcaster(RawData);
			}
		}

		public override string SolvePart1(Module[] input)
		{
			input.Where(x => x is Conjunction conj).Cast<Conjunction>().ForEach(x => x.SetInputs(input.Where(inp => inp.destinations.Contains(x.label)).Select(y=>y.label)));
			int low_pulses = 0;
			int high_pulses = 0;
			Dictionary<string, Module> modules = input.ToDictionary(x => x.label);
			for (int presses = 0; presses < 1000; presses++)
			{
				Pulse(ref low_pulses, ref high_pulses, modules, presses);
			}
			return $"{low_pulses*high_pulses}";
		}

		public override string SolvePart2(Module[] input)
		{
			input.Where(x => x is Conjunction conj).Cast<Conjunction>().ForEach(x => x.SetInputs(input.Where(inp => inp.destinations.Contains(x.label)).Select(y => y.label)));
			int low_pulses = 0;
			int high_pulses = 0;
			Dictionary<string, Module> modules = input.ToDictionary(x => x.label);
			Dictionary<string, int> counts = ((Conjunction)modules["ft"]).inputModules.ToDictionary(x => x, _ => 0);
			int pulse = 0;
			while (counts.Values.Any(x=>x==0))
			{
				pulse++;
				Pulse(ref low_pulses, ref high_pulses, modules, pulse,counts);

				
			}
			Func<long, long, long> LCM = MathFunctions.findLCM();

			return $"{counts.Values.Aggregate(1l,(lcm,x)=>LCM(lcm,x))}";
		}

		private void Pulse(ref int low_pulses,
						   ref int high_pulses,
						   Dictionary<string, Module> modules,
						   int pulse,
						   Dictionary<string, int> counts = null
						   )
		{

			Queue<(bool, string, string)> signalQueue = new Queue<(bool, string, string)>();
			signalQueue.Enqueue((false, "button", "broadcaster"));
			low_pulses += 1;


			while (signalQueue.TryDequeue(out (bool signal, string source, string destination) signal ))
            {
				if (signal.destination == "rx") continue;

				
				Module destination = modules[signal.destination];
				bool? newOutput = destination.ProcessSignal(signal.source, signal.signal);
				if (newOutput is not bool newBOutput) continue;
				
				foreach (var dest in destination.destinations)
                {
					if (newBOutput) high_pulses++;
					else low_pulses++;
					signalQueue.Enqueue((newBOutput, destination.label, dest));
                }

				if (counts == null) continue;
				foreach (var item in counts.Keys)
				{
					if (counts[item] == 0 && modules[item].output) counts[item] = pulse;
				}

			}
        }

		public abstract class Module
		{
            public Module(string data)
            {
                string[] parts = data.Split(" -> ");
				destinations = parts[1].Split(", ").ToList();
				label = parts[0];
            }

			public abstract bool? ProcessSignal(string src, bool Value);

            public string label { get; protected set; }
			public List<string> destinations { get; protected set; } = new();
			public bool output { get; protected set; } = false;
		}

		public class FlipFlop : Module
		{
			public FlipFlop(string rawData):base(rawData.Substring(1))
			{

			}

			private bool Memory = false;
			public override bool? ProcessSignal(string src, bool Value)
			{
				if (Value) return null;

				Memory = !Memory;
				output = Memory;
				return output;
			}
		}

		public class Conjunction : Module
		{
			public Conjunction(string rawData) : base(rawData.Substring(1))
			{
			}

			public void SetInputs(IEnumerable<string> Inputs)
			{
				inputs = Inputs.ToDictionary(x => x, x=>false);
			}

			private Dictionary<string, bool> inputs;

			public IEnumerable<string> inputModules => inputs.Keys;

			public override bool? ProcessSignal(string src, bool Value)
			{
				inputs[src] = Value;
				output = !inputs.Values.All(x => x == true);
				return output;
			}
		}

		public class Broadcaster : Module
		{
			public Broadcaster(string rawData) : base(rawData)
			{
			}

			public override bool? ProcessSignal(string src, bool Value)
			{
				output = Value;
				return Value;
			}
		}
	}
}
