using System;

namespace КМЗИ_пр1_аффинный_рекурентный_шифр
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] text;
            char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
           
            int a1, a2, b1, b2;
            int N = 26;

            Console.WriteLine("Введите a1");
            a1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите b1");
            b1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите a2");
            a2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите b2");
            b2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите текст");
            text = Console.ReadLine().ToCharArray();
            int a11 = a1, b11 = b1, a22 = a2, b22 = b2;

            for (int i = 0; i < text.Length; i++)
            {
                char T;
                int A, B;
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

                    if (i == 0)
                    {
                        A = a11;
                        B = b11;
                    }
                    else if (i == 1)
                    {
                        A = a22;
                        B = b22;
                    }
                    else
                    {
                        A = (a11 * a22) % N;
                        B = (b11 + b22) % N;
                        a11 = a22;
                        a22 = A;
                        b11 = b22;
                        b22 = B;
                    }

                    int E = Encrypt(x, A, B, N); //encrypted letter

                    if (IsUpperFlag)
                    {
                        text[i] = Char.ToUpper(letters[E]);
                    }
                    else text[i] = letters[E];
                }
            }
            Console.WriteLine("\nЗашифрованный текст:");
            Console.WriteLine(text);


            //Расшифровка---------------------------
            a11 = a1;
            b11 = b1;
            a22 = a2;
            b22 = b2;
            for (int i = 0; i < text.Length; i++)
            {
                char T;
                int A, B;
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

                    if (i == 0)
                    {
                        A = a11;
                        B = b11;
                    }
                    else if (i == 1)
                    {
                        A = a22;
                        B = b22;
                    }
                    else
                    {
                        A = (a11 * a22) % N;
                        B = (b11 + b22) % N;
                        a11 = a22;
                        a22 = A;
                        b11 = b22;
                        b22 = B;
                    }

                    int D = Decrypt(x, A, B, N); //decrypted letter

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
            int a_obr = ExtentedEuclAlg(a, N);
            int D = a_obr * (y - b);//----------------------------
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
