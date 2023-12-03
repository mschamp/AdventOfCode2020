using System;

namespace General
{
	public static class MathFunctions
    {
        public static Func<long, long, long> findGCD()
        {
            return (a, b) =>
            {
                if (a % b == 0) return b;
                Func<long, long, long> GCD = findGCD();
                return GCD(b, a % b);
            };
        }
        public static Func<long, long, long> findLCM()
        {
            Func<long, long, long> GCD = findGCD();
            return (a, b) => a * b / GCD(a, b);
        }

        public static Func<long,long> Tribonnaci()
        {
            return n =>
            {
                if (n < 0)
                    return 0;

                if (n == 0)
                    return 1;
                else
                {
                    Func<long, long> Trib = Tribonnaci();
                    return Trib(n - 1) +
                           Trib(n - 2) +
                           Trib(n - 3);
                }
            };
        }

        public static Func<long, long> Finonnaci()
        {
            return n =>
            {
                if (n < 0)
                    return 0;

                if (n == 0)
                    return 1;
                else
                {
                    Func<long, long> Fib = Finonnaci();
                    return Fib(n - 1) +
                           Fib(n - 2);
                }
            };
        }

    }
}
