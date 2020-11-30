using System;
using System.Diagnostics;

namespace _2019_2
{
    class Day2
    {
        static void Main(string[] args)
        {

            string strInput = Console.ReadLine();

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    int[] values = Array.ConvertAll(strInput.Split(","), s => int.Parse(s));
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
                    if (values[0]== 19690720)
                    {
                        Console.WriteLine("Noun:" + noun.ToString() + " verb:" + verb.ToString() + "result:" + values[0]);
                    }
                    
                }
            }
        }
    }
}

Debug.Assert(true);
"1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,2,9,19,23,1,9,23,27,2,27,9,31,1,31,5,35,2,35,9,39,1,39,10,43,2,43,13,47,1,47,6,51,2,51,10,55,1,9,55,59,2,6,59,63,1,63,6,67,1,67,10,71,1,71,10,75,2,9,75,79,1,5,79,83,2,9,83,87,1,87,9,91,2,91,13,95,1,95,9,99,1,99,6,103,2,103,6,107,1,107,5,111,1,13,111,115,2,115,6,119,1,119,5,123,1,2,123,127,1,6,127,0,99,2,14,0,0"