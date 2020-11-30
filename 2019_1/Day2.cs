using System;
using System.Diagnostics;

namespace _2019
{
    class Day2:IAoC
    {
       
        public string SolvePart1(string input)
        {
            int[] values = Array.ConvertAll(input.Split(","), s => int.Parse(s));
            int position = 0;
            while (true)
            {
                int opCode = values[position];
                if (opCode == 99)
                {
                    return values[0].ToString();
                }
                else
                {
                    int value1loc = values[position + 1];
                    int value2loc = values[position + 2];
                    int resultloc = values[position + 3];

                    if (opCode == 1)
                    {
                        values[resultloc] = values[value1loc] + values[value2loc];
                    }
                    else if (opCode == 2)
                    {
                        values[resultloc] = values[value1loc] * values[value2loc];
                    }
                    else
                    {
                        Console.WriteLine("unknown code");
                    }
                }

                position += 4;
            }
        }

        public string SolvePart2(string input)
        {
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    int[] values = Array.ConvertAll(input.Split(","), s => int.Parse(s));
                    values[1] = noun;
                    values[2] = verb;
                    int position = 0;
                    while (true)
                    {
                        int opCode = values[position];
                        if (opCode == 99)
                        {
                            break;
                        }
                        else
                        {
                            int value1loc = values[position + 1];
                            int value2loc = values[position + 2];
                            int resultloc = values[position + 3];

                            if (opCode == 1)
                            {
                                values[resultloc] = values[value1loc] + values[value2loc];
                            }
                            else if (opCode == 2)
                            {
                                values[resultloc] = values[value1loc] * values[value2loc];
                            }
                            else
                            {
                                Console.WriteLine("unknown code");
                            }
                        }

                        position += 4;
                    }
                    if (values[0] == 19690720)
                    {
                         return "Noun:" + noun.ToString() + " verb:" + verb.ToString() + "result:" + values[0];
                    }
                }
            }
            return "";
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1("1,0,0,0,99") == "2");
            Debug.Assert(SolvePart1("2,3,0,3,99") == "2");
            Debug.Assert(SolvePart1("2,4,4,5,99,0") == "2");
            Debug.Assert(SolvePart1("1,1,1,4,99,5,6,0,99") == "30");
            Debug.Assert(SolvePart1("1,9,10,3,2,3,11,0,99,30,40,50") == "3500");
        }
    }
}


