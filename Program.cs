using System;

namespace RSA
{
    internal class Program
    {
        private static Random random = new Random();

        public static bool IsNumberPrime(long x)
        {
            if (x <= 1) 
                return false;

            if (x == 2) 
                return true;

            if (x % 2 == 0) 
                return false;

            for (int d = 3; d * d < x; d += 2)
                if (x % d == 0)
                    return false;

            return true;
        }


        public static long GCD(long x, long y)
        {
            if (y == 0) return x;
            return GCD(y, x % y);
        }

        public static bool Coprime(long x, long y)
        {
            return GCD(x, y) == 1;
        }


        public static long GeneratePrimeNumber()
        {
            var generatedNumber = random.Next() % 5000;
            return IsNumberPrime(generatedNumber) ? generatedNumber : GeneratePrimeNumber();
        }


        public static long Encrypt(long m, long n, long e)
        {
            long c = 1;
            for (long i = 0; i < e; i++)
                c = (c * m) % n;
            return c % n;
        }


        public static long Decrypt(long c, long n, long d)
        {
            long x = 1;
            for(long i = 0; i < d; i++)
                x = (x * c) % n;
            return x % n;
        }



        static void Main(string[] args)
        {
            long p = GeneratePrimeNumber();
            long q = GeneratePrimeNumber();
            Console.WriteLine($"p = {p} | q = {q}");

            long n = p * q;
            Console.WriteLine($"n = {n}");

            long f = (p - 1) * (q - 1);
            Console.WriteLine($"fi(n) = {f}");

            long e = -1;
            bool coPrime = false;
            while(!coPrime)
            {
                Console.WriteLine($"Choose the value of e for which is coprime w/ fi(n) - {f}");
                e = Convert.ToInt32(Console.ReadLine());
                coPrime = Coprime(e, f);
                string isCoPrime = coPrime ? "coprime" : "necoprime";
                Console.WriteLine($"The value of e is {e} and it is {isCoPrime}.");
            }

            long d = 1;
            while(true)
            {
                if ((d * e) % f == 1) break;
                d++;
            }

            Console.WriteLine($"Decryption exponent: {d}");

            Console.WriteLine($"Public Key (e, n): {e}, {n}");
            Console.WriteLine($"Private Key (d, n): {d}, {n}");

            Console.WriteLine("Enter the message clearly:");
          
            long m = Convert.ToInt32(Console.ReadLine());
            long c = Encrypt(m, n, e);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Crypted message: {c}");
            long x = Decrypt(c, n, d);
            Console.WriteLine($"Decrypted message: {x}");

            Console.ResetColor();
        }
    }
}