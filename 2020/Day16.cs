using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day16 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            string[] inputParts = input.Split(Environment.NewLine + Environment.NewLine);
            List<TicketField> fields = new List<TicketField>();
            List<string[]> Tickets = new List<string[]>();
            Regex rgxfields = new Regex(@"(\w+\s?\w*):\s(\d+)-(\d+)\sor\s(\d+)-(\d+)");

            foreach (string item in inputParts[0].Split(Environment.NewLine))
            {
               var match= rgxfields.Match(item);
                TicketField newField = new TicketField(match.Groups[1].Value, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                newField.AddRange(int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value));
                fields.Add(newField);
            }
            

            foreach (string item in inputParts[2].Split(Environment.NewLine).Skip(1))
            {
                Tickets.Add(item.Split(","));
            }

            int sum = 0;
            for (int i = 0; i < Tickets.Count; i++)
            {
                for (int j = 0; j < Tickets[i].Length; j++)
                {
                    if (!fields.Any(x => x.Valid(Tickets[i][j])))
                    {
                        sum += int.Parse(Tickets[i][j]);
                    } 
                }
            }

            return "" + sum;
        }

        public string SolvePart2(string input = null)
        {
            string[] inputParts = input.Split(Environment.NewLine + Environment.NewLine);
            List<TicketField> fields = new List<TicketField>();
            string[] YourTicket = inputParts[1].Split(Environment.NewLine)[1].Split(",");
            List<string[]> Tickets = new List<string[]>();
            Regex rgxfields = new Regex(@"(\w+\s?\w*):\s(\d+)-(\d+)\sor\s(\d+)-(\d+)");

            foreach (string item in inputParts[0].Split(Environment.NewLine))
            {
                var match = rgxfields.Match(item);
                TicketField newField = new TicketField(match.Groups[1].Value, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                newField.AddRange(int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value));
                fields.Add(newField);
            }


            foreach (string item in inputParts[2].Split(Environment.NewLine).Skip(1))
            {
                Tickets.Add(item.Split(","));
            }

            List<string[]> ValidTickets = Tickets.ToList();
            for (int i = 0; i < Tickets.Count; i++)
            {
                for (int j = 0; j < Tickets[i].Length; j++)
                {
                    if (!fields.Any(x => x.Valid(Tickets[i][j])))
                    {
                        ValidTickets.Remove(Tickets[i]);
                    }
                }
            }

            TicketField[] AssignedFiels = new TicketField[Tickets.First().Length];
            while (AssignedFiels.Contains(null))
            {
                List<TicketField> ToVerify = new List<TicketField>();
                ToVerify.AddRange(fields);
                for (int k = 0; k < AssignedFiels.Length; k++)
                {
                    ToVerify.Remove(AssignedFiels[k]);
                }

                for (int i = 0; i < Tickets.First().Length; i++)
                {
                    List<TicketField> Options = new List<TicketField>();
                    for (int k = 0; k < ToVerify.Count; k++)
                    {
                        if (ValidTickets.All(x => ToVerify[k].Valid(x[i])) && ToVerify[k].Valid(YourTicket[i]))
                        {
                            Options.Add(ToVerify[k]);
                        }
                    }

                    if (Options.Count==1)
                    {
                        AssignedFiels[i] = Options.First();
                    }
                }
            }

            long product = 1;
            for (int i = 0; i < AssignedFiels.Length; i++)
            {
                if (AssignedFiels[i].Name.StartsWith("departure"))
                {
                    product *= long.Parse(YourTicket[i]);
                }
            }
            return "" + product;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12") =="71");

            Debug.Assert(SolvePart1(@"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12") == "71");
        }
    }

    public class TicketField
    {
        List<Func<string, bool>> Validators = new List<Func<string, bool>>();
        public TicketField(string name, int min, int max)
        {
            Name = name;
            AddRange(min, max);
        }

        public void AddRange(int min, int max)
        {
            Validators.Add(General.Validators.NumberValidator(min, max));
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool Valid(string value)
        {
            return Validators.Any(x => x(value));
        }
    }
}
