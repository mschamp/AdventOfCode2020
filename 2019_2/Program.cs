using System;

namespace _2019_2
{
    class Program
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
