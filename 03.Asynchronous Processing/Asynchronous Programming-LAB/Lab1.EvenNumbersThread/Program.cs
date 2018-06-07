using System;
using System.Threading;

namespace Lab1.EvenNumbersThread
{
    class Program
    {
        static void Main(string[] args)
        {
            int min = int.Parse(Console.ReadLine());
            int max = int.Parse(Console.ReadLine());

            //      Thread.Sleep(1000); // - приспива Main метода!!!

            Thread evenThread = new Thread(() => PrintEvenNumbers(min, max));
            evenThread.Start();
            evenThread.Join();

            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int min, int max)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 == 0)
                    Console.WriteLine(i);
            }
            // Thread.Sleep(1000); // - приспива EvenNumbersThread метода!!!
        }
    }
}
