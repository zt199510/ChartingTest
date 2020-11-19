using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
       static int[] Arr = new int[50];
        static void Main(string[] args)
        {
            //排序分为平均复杂度和最坏情况            
            //排序的平均复杂度和最坏情况下的复杂度都是O（n^2）所以所有的算法都有最坏情况复杂度和平均复杂度，一般使用平均复杂度
            Stopwatch stopwatch = new Stopwatch();
            Refresh();
            stopwatch.Start();
            StartBubbleSort(Arr);
            stopwatch.Stop();  //停止Stopwatch
            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("===================================================");
            Refresh();
            stopwatch.Start();
            EndBubbleSort(Arr);
            stopwatch.Stop();  //停止Stopwatch
            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("===================================================");
            Refresh();
            stopwatch.Start();
            SelectSort(Arr);
            stopwatch.Stop();  //停止Stopwatch
            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("===================================================");
            Refresh();
            stopwatch.Start();
            SimpleSelectSort(Arr);
            stopwatch.Stop();  //停止Stopwatch
            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("===================================================");

            Refresh();
            stopwatch.Start();
            InsertSort(Arr);
            stopwatch.Stop();  //停止Stopwatch
            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("===================================================");

            Refresh();
            stopwatch.Start();
            HillSort (Arr);
            stopwatch.Stop();  //停止Stopwatch
            Console.WriteLine($"耗时{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("===================================================");

            Console.ReadKey();
        }


        static void Refresh()
        {
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                Arr[i] = random.Next(1, 1000);
            }

        }
        static void StartBubbleSort(int[] list)
        {
            
            int temp;
            for (int i = 0; i < list.Length ; i++)
            {
                for (int j = 1; j < list.Length - i ; j++)
                {
                    if (list[j - 1] > list[j])
                    {
                        temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            Console.WriteLine("从首端开始的冒泡排序:" );
            //foreach (var item in list)
            //{
            //    Console.Write(item+",");
            //}
            
        }
        static void EndBubbleSort(int[] list)
        {
            int temp;
            for (int i = list.Length-1 ; i > 0; i--)
            {
                for (int j = list.Length - 1; j > list.Length - 1 - i; j--)
                {
                    if (list[j - 1] > list[j])
                    {
                        temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            Console.WriteLine("从尾端开始的冒泡排序");
            //foreach (var item in list)
            //{
            //    Console.Write(item + ",");
            //}
        }
        /// <summary>
        /// 简单选择是从未排序的中找最小的顺序放在已排好的后面，直至完成和插入的区别是，是拿一个数据与排好序的数列比较然后插入
        /// 简单选择比较次数是n^2，移动次数是n^2   平均和最坏复杂度都是n^2，是因为操作和实现简单，所以叫简单选择
        /// </summary>
        /// <param name="list"></param>
        static void SimpleSelectSort(int[] list)
        {
            int sum = 0;
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = i; j < list.Length; j++)
                {
                    if (list[i] > list[j])
                    {
                      //  sum++;
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            Console.WriteLine("简单选择排序"+sum);

            //foreach (var item in list)
            //{
            //    Console.Write(item + ",");
            //}

        }
        /// <summary>
        /// 快排是任取一个数，以它为基准，大的放一边，小的放另一边，一次重复直至完成排序，它是对冒泡的排序，最坏的情况是n^2，平均的情况是nlog2N
        /// 
        /// </summary>
        /// <param name="list"></param>
        static void SelectSort(int[] list)
        {
            int sum = 0;
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < list.Length; j++)
                {
                    if (list[i] < list[j])
                    {
                       // sum++;
                           var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;

                    }
                }
            }

            Console.WriteLine("快速排序"+sum);
            //foreach (var item in list)
            //{
            //    Console.Write(item + ",");
            //}
        }

        static void InsertSort(int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                int t = list[i];
                int j = i;
                while ((j>0)&&list[j-1]>t)
                {
                    list[j] = list[j - 1];
                    --j;
                }
                list[j] = t;
            }

            Console.WriteLine("插入排序");
            //foreach (var item in list)
            //{
            //    Console.Write(item + ",");
            //}
        }

        static void HillSort(int[] list)
        {
            int inc;
            for (inc = 1; inc <= list.Length / 9; inc = 3 * inc + 1) ;
            for (; inc > 0; inc /= 3)
            {
                for (int i = inc + 1; i <= list.Length; i += inc)
                {
                    int t = list[i - 1];
                    int j = i;
                    while ((j > inc) && (list[j - inc - 1] > t))
                    {
                        list[j - 1] = list[j - inc - 1];
                        j -= inc;
                    }
                    list[j - 1] = t;
                }
            }
            Console.WriteLine("希尔排序");
            //foreach (var item in list)
            //{
            //    Console.Write(item + ",");
            //}
        }


    }
}
