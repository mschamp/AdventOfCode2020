using System;

namespace _2019_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            while (true)
            {
                string strInput = Console.ReadLine();
                foreach (string str in strInput.Split(Environment.NewLine))
                {
                    if (str == "exit")
                    {
                        Console.WriteLine("Total:" + sum.ToString());
                        break;
                    }
                    int Fuel = GetFuelForModule(int.Parse(str), 3, 2);
                    

                    while (Fuel >0)
                    {
                        sum += Fuel;
                        Fuel = GetFuelForFuel(Fuel, 3, 2);
                    }

                    Console.WriteLine(Fuel);
                }
            }
          
        }

        private static int GetFuelForModule(int mass, int divider, int substracter)
        {
            double sub = mass / divider;
            int partialResult = (int)Math.Ceiling(sub);
            return partialResult - substracter;
        }

        private static int GetFuelForFuel(int mass, int divider, int substracter)
        {
            double sub = mass / divider;
            int partialResult = (int)Math.Floor(sub);
            return partialResult - substracter;
        }
    }
}
