using System;

namespace ExtentedEucAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int a, N;
            a = int.Parse(Console.ReadLine());
            N = int.Parse(Console.ReadLine());
            int a_obr = ExtentedEuclAlg(a, N);
            Console.WriteLine(a_obr);
        }
        static int ExtentedEuclAlg(int a, int N)
        {
            int q, r, y, y2 = 0, y1 = 1, n = N;
            do
            {
                q = n / a;
                r = n % a;
                y = y2 - q * y1;
                n = a;
                a = r;
                y2 = y1;
                y1 = y;
            } while (r != 0);
            while (y2 < 0)
            {
                y2 += N;
            }
            return y2;
        }
    }
}
