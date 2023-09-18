using System;
namespace PrimeFactorsApplication
{
    class PrimeFactorsApplication
    {
        static void Main()
        {
            Console.Write("Please input an integer:");
            string s = Console.ReadLine();
            foreach (var i in s)
            {
                if(i < '0' || i > '9')
                {
                    Console.WriteLine("Invalid input: Not an integer.");
                    return;
                }
            }
            int n = Convert.ToInt32(s);
            if (n < 2)
            {
                Console.WriteLine("Numbers below 2 have no PRIME FACTOR!");
                return;
            }
            bool[] isPrime = new bool[n + 1];
            for (int i = 2; i <= n; i++)
            {
                isPrime[i] = true;
            }
            Console.WriteLine("The PRIME FACTOR(S) of {0} is(are):", n);
            for (int i = 2; i <= n; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i << 1; j <= n; j += i)
                        isPrime[j] = false;
                    if (n % i == 0)
                        Console.WriteLine("{0}", i);
                }
            }
        }

    }
}