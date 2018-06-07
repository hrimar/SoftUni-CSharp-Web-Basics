using System;
using System.Threading;
using System.Threading.Tasks;

namespace SumPrimesInRange
{
    class Program
    {
        // Да се напише програма която да смята нещо, без да блокира конзолата,
        // и като се въведе "show" да покаже резултата от сметките:

        private static string result;

        static void Main(string[] args)
        {
            Console.WriteLine("Calculating...");
            Task.Run(() => CalculateSlowly());

            Console.WriteLine("Enter conmand:");

            while (true)
            {
                string line = Console.ReadLine();

                if(line == "show")
                {
                    if(result == null)
                    {
                        Console.WriteLine("Still calculating... Please wait!");
                    }
                    else
                    {
                        Console.WriteLine($"Result is: {result}");
                    }
                }

                if(line=="exit")
                {
                    break;
                }

            }
        }

        private static void CalculateSlowly() // Backbround task!
        {
            Thread.Sleep(1000);

            result = "555";
        }
    }
}
