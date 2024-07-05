using System;
using System.Collections.Generic;

namespace Рекурентный_шифр_Хилла
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.Шифрование текста

            char[] text;
            char[] letters = "abcdefghijklmnopqrstuvwxyz?!#".ToCharArray();
            int n = 3; //размер блока
            int[] key = new int[n * n];
            int[] key2 = new int[n * n];
            int[] tmp_key1 = new int[n * n];
            int[] tmp_key2 = new int[n * n];
            int[] new_key = new int[n * n];
            int N = 29;
            Console.WriteLine("Введите текст");
            text = Console.ReadLine().ToCharArray();
            List<int> list = new List<int>();//численные значения букв текста
            int[] block = new int[n];
            int[] encblock = new int[n];
            int numberofblocks;
            int Determ, Determ2;
            do {
                Console.WriteLine("Введите каждый элемент 1-го ключа по Enter");
                for (int i = 0; i < n * n; i++)
                {
                    key[i] = int.Parse(Console.ReadLine());
                }
                Console.WriteLine("Введите каждый элемент 2-го ключа по Enter");
                for (int i = 0; i < n * n; i++)
                {
                    key2[i] = int.Parse(Console.ReadLine());
                }
                Determ = Find_Determ(key, N);
                Determ2 = Find_Determ(key2, N);

                if (Determ == 0)
                    Console.WriteLine("Определитель 1-го ключа равен 0! Введите другой ключ!");
                if (Determ2 == 0)
                    Console.WriteLine("Определитель 2-го ключа равен 0! Введите другой ключ!");
            } while (Determ == 0 || Determ2 == 0);
            Console.WriteLine("\nОпределитель 1-го равен: " + Determ);
            Console.WriteLine("\nОпределитель 2-го равен: " + Determ2);

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

                    list.Add(Array.IndexOf(letters, T));
                }
            }
            if (list.Count % n != 0)
            {
                int o = list.Count % 3;
                switch (o)
                {
                    case 1:
                        {
                            list.Add(27);
                            list.Add(27);
                            break;
                        }
                    case 2:
                        {
                            list.Add(27);
                            break;
                        }
                }
            }
            numberofblocks = list.Count / n;
            int k = 0, m = 0;
            for (int i = 0; i < numberofblocks; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block[j] = list[k];
                    k++;
                }
                if(i==0)
                    encblock = Encrypt(block, key, N);
                else if(i==1)
                    encblock = Encrypt(block, key2, N);
                else
                {
                    new_key = Mul_Matrix(tmp_key1, tmp_key2, N);
                    encblock = Encrypt(block, new_key, N);
                    tmp_key1 = tmp_key2;
                    tmp_key2 = new_key;
                }
                for (int w = 0; w < 3; w++)
                {
                    list[m] = encblock[w];
                    m++;
                }
            }
            k = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]) == true || text[i] == '?' || text[i] == '!' || text[i] == '#')
                {
                    text[i] = letters[list[k]];
                    k++;
                }
            }
            Console.WriteLine("\nЗашифрованный текст:");
            Console.WriteLine(text);
            

            //2.Дешифровка шифротекста
            k = 0;
            m = 0;
            int[] key_obr;
            for (int i = 0; i < numberofblocks; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    encblock[j] = list[k];
                    k++;
                }
                if (i == 0)
                    key_obr = Key_Obr_Meth(key, Determ, N);
                else if (i == 1)
                    key_obr = Key_Obr_Meth(key2, Determ2, N);
                else
                {
                    new_key = Mul_Matrix(tmp_key1, tmp_key2, N);
                    int new_determ = Find_Determ(new_key, N);
                    key_obr = Key_Obr_Meth(new_key, new_determ, N);
                    tmp_key1 = tmp_key2;
                    tmp_key2 = new_key;
                }
                block = Decrypt(encblock, key_obr, N);
                for (int w = 0; w < 3; w++)
                {
                    list[m] = block[w];
                    m++;
                }
            }
            k = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]) == true || text[i] == '?' || text[i] == '!' || text[i] == '#')
                {
                    text[i] = letters[list[k]];
                    k++;
                }
            }
            Console.WriteLine("\nРасшифрованный текст:");
            Console.WriteLine(text);
        }
        //--------------------------------------
        static int[] Encrypt(int[] block, int[] key, int N)
        {
            int[] Encblock = new int[3];
            Encblock[0] = (key[0] * block[0] + key[1] * block[1] + key[2] * block[2]) % N;
            Encblock[1] = (key[3] * block[0] + key[4] * block[1] + key[5] * block[2]) % N;
            Encblock[2] = (key[6] * block[0] + key[7] * block[1] + key[8] * block[2]) % N;
            return Encblock;
        }
        static int[] Decrypt(int[] encblock, int[] key_obr, int N)
        {
            int[] block = new int[3];
            block[0] = (key_obr[0] * encblock[0] + key_obr[1] * encblock[1] + key_obr[2] * encblock[2]) % N;
            block[1] = (key_obr[3] * encblock[0] + key_obr[4] * encblock[1] + key_obr[5] * encblock[2]) % N;
            block[2] = (key_obr[6] * encblock[0] + key_obr[7] * encblock[1] + key_obr[8] * encblock[2]) % N;
            return block;
        }
        static int[] Mul_Matrix(int[] key1, int[] key2, int N)
        {
            int[] newkey = new int[9];
            newkey[0] = (key1[0] * key2[0] + key1[1] * key2[3] + key1[2] * key2[6]) % N;
            newkey[1] = (key1[0] * key2[1] + key1[1] * key2[4] + key1[2] * key2[7]) % N;
            newkey[2] = (key1[0] * key2[2] + key1[1] * key2[5] + key1[2] * key2[8]) % N;

            newkey[3] = (key1[3] * key2[0] + key1[4] * key2[3] + key1[5] * key2[6]) % N;
            newkey[4] = (key1[3] * key2[1] + key1[4] * key2[4] + key1[5] * key2[7]) % N;
            newkey[5] = (key1[3] * key2[2] + key1[4] * key2[5] + key1[5] * key2[8]) % N;

            newkey[6] = (key1[6] * key2[0] + key1[7] * key2[3] + key1[8] * key2[6]) % N;
            newkey[7] = (key1[6] * key2[1] + key1[7] * key2[4] + key1[8] * key2[7]) % N;
            newkey[8] = (key1[6] * key2[2] + key1[7] * key2[5] + key1[8] * key2[8]) % N;
            return newkey;
        }
        static int[] Key_Obr_Meth(int[] key, int Determ, int N)
        {
            int D_obr = ExtentedEuclAlg(Determ, N);
            int[] key_obr = new int[9];
            key_obr[0] = key[4] * key[8] - key[7] * key[5];
            key_obr[1] = (-1) * (key[3] * key[8] - key[6] * key[5]);
            key_obr[2] = key[3] * key[7] - key[6] * key[4];
            key_obr[3] = (-1) * (key[1] * key[8] - key[7] * key[2]);
            key_obr[4] = key[0] * key[8] - key[6] * key[2];
            key_obr[5] = (-1) * (key[0] * key[7] - key[6] * key[1]);
            key_obr[6] = key[1] * key[5] - key[4] * key[2];
            key_obr[7] = (-1) * (key[0] * key[5] - key[3] * key[2]);
            key_obr[8] = key[0] * key[4] - key[3] * key[1];
            for (int i = 0; i < key_obr.Length; i++)
            {
                if (key_obr[i] > N)
                    key_obr[i] = key_obr[i] % N;
                else
                {
                    while (key_obr[i] < 0)
                    {
                        key_obr[i] += N;
                    }
                }
            }
            int temp = key_obr[1];
            key_obr[1] = key_obr[3];
            key_obr[3] = temp;
            temp = key_obr[2];
            key_obr[2] = key_obr[6];
            key_obr[6] = temp;
            temp = key_obr[5];
            key_obr[5] = key_obr[7];
            key_obr[7] = temp;
            for (int i = 0; i < key_obr.Length; i++)
            {
                key_obr[i] = key_obr[i] * D_obr % N;
            }
            return key_obr;
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
        static int Find_Determ(int[] key, int N)
        {
            int Determ = key[0] * key[4] * key[8] + key[1] 
            * key[5] * key[6] + key[2] * key[3] * key[7] - key[2] 
            * key[4] * key[6] - key[1] * key[3] * key[8] - key[0] * key[5] * key[7];
            if (Determ > N)
                Determ %= N;
            else
            {
                while (Determ < 0)
                {
                    Determ += N;
                }
            }
            return Determ;
        }
    }
}
