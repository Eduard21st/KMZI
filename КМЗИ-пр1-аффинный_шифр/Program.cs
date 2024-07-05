using System;

namespace КМЗИ_пр1_аффинный_шифр
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] text;
            char[] letters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
            int a, b;
            int N = 33;
            Console.WriteLine("Введите a");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите b");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите текст");
            text = Console.ReadLine().ToCharArray();

            /*for (int i = 0; i < text.Length; i++)
            {
                char T;
                bool IsUpperFlag = false;
                if (Char.IsLetter(text[i]) == true)
                {
                    if (Char.IsUpper(text[i]))
                    {
                        T = Char.ToLower(text[i]);
                        IsUpperFlag = true;
                    }
                    else T = text[i];

                    int x = Array.IndexOf(letters, T);
                    int E = Encrypt(x, a, b, N); //encrypted letter

                    if (IsUpperFlag)
                    {
                        text[i] = Char.ToUpper(letters[E]);
                    }
                    else text[i] = letters[E];
                }
            }
            Console.WriteLine("\nЗашифрованный текст:");
            Console.WriteLine(text);*/

            // Расшифровка-----------------------------------------------------------------------

            for (int i = 0; i < text.Length; i++)
            {
                char T;
                bool IsUpperFlag = false;
                if (Char.IsLetter(text[i]) == true)
                {
                    if (Char.IsUpper(text[i]))
                    {
                        T = Char.ToLower(text[i]);
                        IsUpperFlag = true;
                    }
                    else T = text[i];

                    int x = Array.IndexOf(letters, T);
                    int D = Decrypt(x, a, b, N); //decrypted letter

                    if (IsUpperFlag)
                    {
                        text[i] = Char.ToUpper(letters[D]);
                    }
                    else text[i] = letters[D];
                }
            }
            Console.WriteLine("\nРасшифрованный текст:");
            Console.WriteLine(text);
        }
        static int Encrypt(int x, int a, int b, int N)
        {
            int E = (a * x + b) % N;
            return E;
        }
        static int Decrypt(int y, int a, int b, int N)
        {
            int a_obr = ExtentedEuclAlg(a,N);
            int D = a_obr * (y - b);
            if (D >= 0)
            {
                D %= N;
            }
            else while (D < 0)
                {
                    D += N;
                }
            return D;
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
