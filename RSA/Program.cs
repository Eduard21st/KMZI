using System;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] text;
            char[] letters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
            ulong q, p, n, e, d, mod;// E;
            bool flag;
            Console.WriteLine("Введите текст");
            text = Console.ReadLine().ToCharArray();
            double[] encrypted = new double[text.Length];
            Console.WriteLine("Введите \"p\" и \"q\"  по Enter");
            p = ulong.Parse(Console.ReadLine());
            q = ulong.Parse(Console.ReadLine());
            n = q * p;
            Console.WriteLine("\nn равен " + n + "\n");
            mod = (p - 1) * (q - 1);
            Console.WriteLine("(p-1)*(q-1) равен " + mod + "\n");
            Console.WriteLine("Введите ключ \"e\" по Enter");
            do
            {
                try
                {
                    e = ulong.Parse(Console.ReadLine());
                    d = ExtentedEuclAlg(e, mod);
                    Console.WriteLine("d равен " + d + "\n");
                    flag = true;
                }
                catch
                {
                    Console.WriteLine("Ключ е не удовлетворяет требованию. Повторите ввод");
                    e = 0;
                    d = 0;
                    flag = false;
                }
            } while (flag != true);
            Console.WriteLine("\nЗашифрованный текст:");
            for (int i = 0; i < text.Length; i++)
            {
                char T;
                if (Char.IsLetter(text[i]) == true)
                {
                    if (Char.IsUpper(text[i]))
                    {
                        T = Char.ToLower(text[i]);
                    }
                    else T = text[i];

                    int x = Array.IndexOf(letters, T);
                    ulong x1 = (ulong)x;
                    double E1 = Math.Pow(x1, e);
                    double E = E1 % n;
                    encrypted[i] = E;
                    Console.WriteLine(E);
                    /*if (IsUpperFlag)
                    {
                        text[i] = Char.ToUpper(letters[E]);
                    }
                    else text[i] = letters[E];*/
                }
            }
            Console.WriteLine("\nРасшифрованный текст:");
            for (int i = 0; i < text.Length; i++)
            {
                double D1 = Math.Pow(encrypted[i], d);
                double D = D1 % n;
                Console.WriteLine(letters[(int)D]);
            }
        }
        static ulong ExtentedEuclAlg(ulong a, ulong N)
        {
            ulong  r, n = N;
            long q, y2 = 0, y1 = 1, y;
            do
            {
                q = (long)(n / a);
                r = n % a;
                y = y2 - (q * y1);
                n = a;
                a = r;
                y2 = y1;
                y1 = y;
            } while (r != 1);
            y2 = y1;
            while (y2 < 0)
            {
                y2 = y2 + (long)N;
            }
            return (ulong)y2;
        }
    }
}
