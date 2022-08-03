using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace z_2
{
    class Program
    {
        static public Stopwatch myStopwatch = new Stopwatch();
        static int x = 0;       

        //static void SimpleSearch(string[] mas, string act)
        //{            
        //    int k = 0, z = 0;
        //    myStopwatch.Start();
        //    Console.WriteLine("Простой метод: ");
        //    foreach (string key in mas)
        //    {
        //        z++;
        //        if (key.Contains(act))
        //        {
        //            int f=key.IndexOf(act);
        //            Console.WriteLine($"Данная подстрока найдена, она находится в строке под индексом: {k}, количество сравнений: {z}, индекс подстроки: {f}");
        //        }
        //        k++;
        //    }           
        //    myStopwatch.Stop();
        //    TimeSpan simple = myStopwatch.Elapsed;
        //    Console.WriteLine($"Время затраченное на поиск подстроки: {simple}");
        //}
        public static int[] createPrefix(string pattern)
        {
            int[] pi = new int[pattern.Length];
            int j = 0;
            pi[0] = 0;
            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[j] != pattern[i])
                    j = pi[j - 1];
                if (pattern[j] == pattern[i])
                    j++;
                pi[i] = j;
            }
            return pi;
        }
        static void KMP(string[] mas, string pattern)
        {
            Console.WriteLine("\nКМП поиск:");
            myStopwatch.Restart();
            int[] prefix = createPrefix(pattern);
            int k = 0, comparisons = 0, index = -1;

            for (int i = 0; i < mas.Length; i++)
            {
                string text = mas[i];
                for (int j = 1; j <= text.Length; j++)
                {
                    while (k > 0 && pattern[k] != text[j - 1])
                    {
                        k = prefix[k - 1];
                        comparisons++;
                    }
                    if (pattern[k] == text[j - 1])
                    {
                        k++;
                        comparisons++;
                    }
                    if (k == pattern.Length)
                    {
                        index = j - pattern.Length;
                        k = 0;
                        Console.WriteLine($"Данная подстрока найдена! Она находится в строке {i}, её индекс: {index + 1}, количество сравнений: {comparisons }");
                        break;
                    }
                }
            }
            if (index == -1)
            {
                Console.WriteLine("Данной подстроки не найдено!");
            }
            myStopwatch.Stop();
            TimeSpan KMP = myStopwatch.Elapsed;
            Console.WriteLine($"Время затраченное на поиск подстроки: {KMP}");
            Console.WriteLine("");
        }
        static void SimpleSearch(string[] mas, string act)
        {
            int comparisons = 0, f = 0, z;
            Console.WriteLine("Простой метод: ");
            myStopwatch.Restart();
            for (int i = 0; i < mas.Length; i++)
            {
                string array = mas[i];
                z = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    for (int k = 0; k < act.Length; k++)
                    {
                        comparisons++;
                        if (act[k] == array[j])
                        {
                            k++;
                        }                        
                        if (k == act.Length & z == 0)
                        {
                            z++;
                            f++;
                            k = 0;
                            Console.WriteLine($"Данная подстрока найдена! Она находится в строке {i}, её индекс: {j - 2}, количество сравнений: {comparisons }");
                            break;
                        }
                    }
                }
            }
            if (f == 0)
            {
                Console.WriteLine("Данной подстроки не найдено!");
            }
            myStopwatch.Stop();
            TimeSpan simple = myStopwatch.Elapsed;
            Console.WriteLine($"Время затраченное на поиск подстроки: {simple}");
        }
        static int[] BadChar(string pattern)
        {
            int[] badcharArray = new int[256];
            for (int i = 0; i < 256; i++)
                badcharArray[i] = -1;
            for (int i = 0; i < pattern.Length - 1; i++)
                badcharArray[(int)pattern[i]] = i;
            return badcharArray;           
        }
        public static int[] suffixCreate(string pattern)
        {
            int[] suffixes = new int[pattern.Length];
            suffixes[pattern.Length - 1] = pattern.Length;
            int right = pattern.Length - 1, left = 0;
            for (int i = right - 1; i >= 0; --i)
            {
                if (i > right && suffixes[i + pattern.Length - 1 - left] < i - right)
                    suffixes[i] = suffixes[i + pattern.Length - 1 - left];
                else if (i < right)
                    right = i;
                left = i;
                while (right >= 0 && pattern[right] == pattern[right + pattern.Length - 1 - left])
                    right--;
                suffixes[i] = left - right;
            }
            return suffixes;
        }
        public static int[] GoodSuffix(string pattern)
        {           
            int[] suffixes = suffixCreate(pattern);
            int[] goodSuffixes = new int[pattern.Length];
            for (int i = 0; i < pattern.Length; i++)
                goodSuffixes[i] = pattern.Length;
            for (int i = pattern.Length - 1; i >= 0; i--)
                if (suffixes[i] == i + 1)
                    for (int j = 0; j < pattern.Length - i - 1; j++)
                        if (goodSuffixes[j] == pattern.Length)
                            goodSuffixes[j] = pattern.Length - i - 1;
            for (int i = 0; i < pattern.Length - 2; i++)
                goodSuffixes[pattern.Length - 1 - suffixes[i]] = pattern.Length - i - 1;
            return goodSuffixes;
        }
        static void BM(string[] mas, string pattern)
        {
            int comparisons = 0, index = -1, z=0, proverka = -1;
            
            Console.WriteLine("\nBM поиск:");
            myStopwatch.Restart();
            for (int i = 0; i < mas.Length; i++)
            {
                string text = mas[i];
                if(proverka == -1)
                proverka++;
                if (pattern.Length <= text.Length)
                {
                    int[] badShift = BadChar(pattern);
                    int[] goodSuffix = GoodSuffix(pattern);                  
                        while (index <= text.Length - pattern.Length)
                        {
                            int j;
                            for (j = pattern.Length - 1; j >= 0 && pattern[j] == text[j + index]; j--, comparisons++) ;
                            if (j < 0)
                            {
                                proverka = -1;
                                z++;
                                Console.WriteLine($"Данная подстрока найдена! Она находится в строке: {i}, её индекс: {index + 1}, количество сравнений: {comparisons }");                               
                                break;
                            }
                            if (proverka == 0)
                            {
                                index += Math.Max(j - badShift[(int)text[index + j]], goodSuffix[j]);
                            }
                        }                    
                }
            }
            if (z == 0)
            {
                Console.WriteLine("Данной подстроки не найдено!");
            }           
            myStopwatch.Stop();
            TimeSpan BM = myStopwatch.Elapsed;
            Console.WriteLine($"Время затраченное на поиск подстроки: {BM}");
        }
        static void Main(string[] args)
        {

            for (; ; )
            {
                try
                {
                    Console.WriteLine("Введите кол-во строк: ");
                    x = int.Parse(Console.ReadLine());
                    if (x > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Некорректные данные, введите год рождения еще раз.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректные данные, пожалуйста введите числовое значение!");
                }
            }
            string[] mas = new string[x];

            Console.WriteLine("Введите строки на английском языке: ");
            for (int i = 0; i < x; i++)
            {
                mas[i] = Console.ReadLine();
                if (Regex.IsMatch(mas[i], @"^([a-zA-Zа-яА-Я]+|\s)+$"))
                {
                    continue;
                }
                else
                {
                    i--;
                    Console.WriteLine("Некорректные данные, введите строку еще раз.");
                }
            }
            Console.WriteLine("Введите подстроку, которую нужно найти: ");
            string act = Console.ReadLine();

            KMP(mas, act);
            SimpleSearch(mas, act);
            BM(mas, act);
        }
    }
}
