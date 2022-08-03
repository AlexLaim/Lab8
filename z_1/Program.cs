using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;



namespace z_1
{
    public struct Item
    {
        public Item(string Name, TimeSpan Time, long Switch, long Compare)
        {
            this.Name = Name;
            this.Time = Time;
            this.Switch = Switch;
            this.Compare = Compare;
        }
        public String Name;
        public TimeSpan Time;
        public long Switch;
        public long Compare;
        public void Print()
        {
            Console.WriteLine($"{this.Name,-24} \nВремя:{this.Time,-12} \nОбменов:{this.Switch,-20} \nСравнений:{this.Compare,-15}");
        }
    }
    class Program
    {
        static string filename = Directory.GetCurrentDirectory() + "\\sorted.dat";
        static public Stopwatch myStopwatch = new Stopwatch();
        static string poisk = string.Empty;
        static List<Item> list = new List<Item>();
        static int gues = 0;
        static void Swap(int[] array, int i, int j)
        {
            int glass = array[i];
            array[i] = array[j];
            array[j] = glass;
        }
        static public void ShakerSort(int[] array)
        {
            myStopwatch.Restart();
            string g = new string('_', 80);
            Console.WriteLine("\n" + g);
            Console.WriteLine("\nСортировка шейкером: ");
            int left = 0,
                right = array.Length - 1;
            long count_switch = 0, //счетчик обменов
                count_compare = 0;//счетчик сравнений    

            while (left <= right)
            {
                for (int i = left; i < right; i++)
                {
                    count_compare++;
                    if (array[i] < array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        count_switch++;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    count_compare++;
                    if (array[i - 1] < array[i])
                    {
                        Swap(array, i - 1, i);
                        count_switch++;
                    }
                }
                left++;
            }
            foreach (int value in array)
            {
                Console.Write($"{value} ");
            }
            myStopwatch.Stop();
            TimeSpan first3 = myStopwatch.Elapsed;
            using (StreamWriter streamwriter = new StreamWriter(filename, true))
            {
                streamwriter.Write("Шейкерная: ");
                streamwriter.Write($"Время: {first3} обменов: {count_switch} сравнений: {count_compare} .\n");
            }
            Console.WriteLine("\nДанные о сортировке шейкером: ");
            Console.WriteLine($"\nВремя работы: {first3}");
            Console.WriteLine($"\nКол-во обменов: {count_switch} Кол-во сравнений: {count_compare}");
            Console.WriteLine("\n" + g);
        }
        static public void BubbleSort(int[] array)
        {
            myStopwatch.Restart();
            string g = new string('_', 80);
            Console.WriteLine("\n" + g);
            Console.WriteLine("\nСортировка пузырьком: ");
            long count_switch = 0, //счетчик обменов
                count_compare = 0;//счетчик сравнений         
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    count_compare++;
                    if (array[j] < array[j + 1])
                    {
                        count_switch++;
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                    }

                }

            }
            foreach (int value in array)
            {
                Console.Write($"{value} ");
            }
            myStopwatch.Stop();
            TimeSpan first2 = myStopwatch.Elapsed;
            using (StreamWriter streamwriter = new StreamWriter(filename, true))
            {
                streamwriter.Write("Пузырьковая: ");
                streamwriter.Write($"Время: {first2} обменов: {count_switch} сравнений: {count_compare} .\n");
            }
            Console.WriteLine("\nДанные о сортировке пузырьком: ");
            Console.WriteLine($"\nВремя работы: {first2}");
            Console.WriteLine($"\nКол-во обменов: {count_switch} Кол-во сравнений: {count_compare}");
            Console.WriteLine("\n" + g);
        }
        static public void InsertionSort(int[] array)
        {
            myStopwatch.Start();
            string g = new string('_', 80);
            Console.WriteLine("\n" + g);
            Console.WriteLine("\nСортировка вставками: ");
            long count_switch = 0, //счетчик обменов
                count_compare = 0;//счетчик сравнений

            for (int i = 1; i < array.Length; i++)
            {
                int cur = array[i];
                int j = i;
                count_compare++;
                while (j > 0 && cur > array[j - 1])
                {
                    array[j] = array[j - 1];
                    j--;
                    count_switch++;
                }
                array[j] = cur;
            }

            foreach (int value in array)
            {
                Console.Write($"{value} ");
            }
            myStopwatch.Stop();
            TimeSpan first = myStopwatch.Elapsed;
            using (StreamWriter streamwriter = new StreamWriter(filename, false))
            {
                streamwriter.Write("Вставки: ");
                streamwriter.Write($"Время: {first} обменов: {count_switch} сравнений: {count_compare} .\n");
            }
            Console.WriteLine("\nДанные о сортировке вставками: ");
            Console.WriteLine($"\nВремя работы: {first}");
            Console.WriteLine($"\nКол-во обменов: {count_switch} Кол-во сравнений: {count_compare}");
            Console.WriteLine("\n" + g);
        }
        static public void ShellSort(int[] array)
        {
            myStopwatch.Restart();
            string g = new string('_', 80);
            Console.WriteLine("\n" + g);
            Console.WriteLine("\nСортировка Шелла: ");
            long count_switch = 0, //счетчик обменов
                count_compare = 0;//счетчик сравнений
            int step = array.Length / 2;
            while (step > 0)
            {
                int i, j;
                for (i = step; i < array.Length; i++)
                {
                    count_compare++;
                    int value = array[i];
                    for (j = i - step; (j >= 0) && (array[j] < value); j -= step)
                    {
                        array[j + step] = array[j];
                        count_switch++;
                    }
                    array[j + step] = value;
                }
                step /= 2;
            }
            foreach (int value in array)
            {
                Console.Write($"{value} ");
            }
            myStopwatch.Stop();
            TimeSpan first4 = myStopwatch.Elapsed;
            using (StreamWriter streamwriter = new StreamWriter(filename, true))
            {
                streamwriter.Write("Шелла: ");
                streamwriter.Write($"Время: {first4} обменов: {count_switch} сравнений: {count_compare} .\n");
            }
            Console.WriteLine("\nДанные о сортировке Шелла: ");
            Console.WriteLine($"\nВремя работы: {first4}");
            Console.WriteLine($"\nКол-во обменов: {count_switch} Кол-во сравнений: {count_compare}");
            Console.WriteLine("\n" + g);
        }
        static public void SelectionSort(int[] array)
        {

            myStopwatch.Restart();
            string g = new string('_', 80);
            Console.WriteLine("\n" + g);
            Console.WriteLine("\nСортировка выбором: ");
            long count_switch = 0, //счетчик обменов
                count_compare = 0;//счетчик сравнений

            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] > array[min])
                    {
                        count_switch++;
                        min = j;
                    }
                }
                count_compare++;
                int dummy = array[i];
                array[i] = array[min];
                array[min] = dummy;
                min = i;
            }
            foreach (int value in array)
            {
                Console.Write($"{value} ");
            }
            myStopwatch.Stop();
            TimeSpan first5 = myStopwatch.Elapsed;
            using (StreamWriter streamwriter = new StreamWriter(filename, true))
            {
                streamwriter.Write("Выбором: ");
                streamwriter.Write($"Время: {first5} обменов: {count_switch} сравнений: {count_compare} .");
            }
            Console.WriteLine("\nДанные о сортировке выбором: ");
            Console.WriteLine($"\nВремя работы: {first5}");
            Console.WriteLine($"\nКол-во обменов: {count_switch} Кол-во сравнений: {count_compare}");
            Console.WriteLine("\n" + g);
        }
        static public void InsertionSortForList(List<Item> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int j;
                var buf = list[i];
                for (j = i - 1; j >= 0; j--)
                {
                    if (list[i].Name.CompareTo(list[j].Name) == 1)
                    {

                        break;
                    }

                    list[j + 1] = list[j];
                }
                list[j + 1] = buf;
            }
        }
        static void BinarySearch()

        {
            using (var streamreader = new StreamReader(File.Open(filename, FileMode.OpenOrCreate)))
            {
                while (streamreader.Peek() > -1)
                {
                    string[] Array2 = streamreader.ReadLine().Split(); //0 - название. 2 - время. 4 - обмены. 6 - сравнения.
                    string Name = Array2[0];
                    TimeSpan Time = TimeSpan.Parse(Array2[2]);
                    long Switch = long.Parse(Array2[4]);
                    long Compare = long.Parse(Array2[6]);
                    list.Add(new Item(Name, Time, Switch, Compare));
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                InsertionSortForList(list);
            }
            List<Item> list2 = new List<Item>();
            int low = 0, y =0;
            int high = list.Count;
            bool flag = true;
            while (flag)
            {
                y++;
                gues = low + (high - low) / 2;
                if (y == 5)
                {
                    flag = false;
                    Console.WriteLine("Данных не найдено");
                }
                if (poisk.CompareTo(list[gues].Name) == -1)
                {
                    high = gues - 1;

                }
                else if (poisk.CompareTo(list[gues].Name) == 1)
                {
                    low = gues + 1;
                }
                else if (poisk.CompareTo(list[gues].Name) == 0)
                {
                    flag = false;
                }
            }
            if (y < 5)
            {
                list2.Add(list[gues]);
                //(Позиция будет отличается от массива, т.к. лист перепрыгивает через ненужные строки)
                Console.WriteLine($"Позиция элемента в листе: {gues}, Данные о сортировке через бинарный поиск: ");
                foreach (Item item in list2)
                {
                    item.Print();
                }
            }
        }
        static void LineSearch()
        {
            Console.WriteLine("Информация о каком методе вам нужена?\n(Вставки, Выбором, Шейкерная, Шелла, Пузырьковая): ");
            poisk = Console.ReadLine() + ":";
            using (var streamreader = new StreamReader(File.Open(filename, FileMode.OpenOrCreate)))
            {
                int p = 0;
                int k = 0;
                int z = 0;
                string action = string.Empty;
                while (streamreader.Peek() > -1)
                {
                    string[] Array2 = streamreader.ReadLine().Split();
                    for (int i = 0; i < Array2.Length; i++)
                    {
                        z++;
                        if (Array2[i] == poisk)
                        {
                            Console.WriteLine($"Позиция элемента в массиве: {z}");
                            p++;
                            k++;
                        }
                        else if (k == 1)
                        {
                            Console.WriteLine(Array2[i]);
                            if (Array2[i].EndsWith('.'))
                            {
                                k++;
                                break;
                            }
                        }
                        else if (streamreader.Peek() == -1 & p == 0)
                        {
                            p++;
                            Console.WriteLine("Данных не найдено");
                        }
                    }
                }

            }
        }
        //На этом мои полномочия все ¯\_(ツ)_/¯

        //static void InterpolationSearch()
        //{
        //    int low = 0, y = 0;
        //    int high = list.Count;
        //    int find;
        //        while (list[low].Name.CompareTo(poisk) == -1 && list[high].Name.CompareTo(poisk) == 1)
        //        {
        //            find = low + (list[gues] - list[low]) * (high - low) / (list[high] - list[low]);

        //        }
        //}
        static void Main(string[] args)
        {

            int n = 100;
            int[] array = new int[n];
            int[] array2 = new int[n];
            int[] array3 = new int[n];
            int[] array4 = new int[n];
            int[] array5 = new int[n];
            int[] array6 = new int[n];
            Random r = new Random();
            Console.WriteLine("Исходный массив: ");
            for (int i = 0; i < n; i++) // массив заполнения случ. числами
            {
                array[i] = r.Next(100000);
                array2[i] = array[i];
                array3[i] = array[i];
                array4[i] = array[i];
                array5[i] = array[i];
                array6[i] = array[i];
                Console.Write(array[i] + " ");
            }
            InsertionSort(array2);
            BubbleSort(array3);
            ShakerSort(array4);
            ShellSort(array5);
            SelectionSort(array6);
            LineSearch();
            BinarySearch();
        }
    }
}
