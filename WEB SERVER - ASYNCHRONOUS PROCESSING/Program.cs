using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WEB_SERVER___ASYNCHRONOUS_PROCESSING
{
    class Program
    {
        public static void BlockConsole()
        {
            // искам да намеря колко са простите числа от 1 до 100000 като бройка
            int count = 0;
            
            List<int> primeNumbers = new List<int>();

            for (int i = 2; i < 150000; i++)
            {
                bool isPrime = true;

                for (int p = 2; p <= i - 1; p++)
                {
                    if (i % p == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    count++;
                    primeNumbers.Add(i);
                }                
            }

            Console.WriteLine(count);
            Console.WriteLine("[" + string.Join(", ", primeNumbers) + "]");
            
        }

        static void PrintInfo()
        {
            for (int i = 0; i <4; i++)
            {
                Console.WriteLine($"value of i: {i}");
            }
            Console.WriteLine("Child Thread Completed");
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(PrintInfo);
            t1.Start();
            Console.WriteLine("Main Thread Completed");


            ;




            // Искам да направя метод който като го изпълня да блокира Console.Readline за определено време - например 5 сек. Затова си правя BlockConsole
            Console.Write("write your name:");
            Console.ReadLine();

           
            Task task = Task.Run(() => 
            {
                BlockConsole(); 
            });

            Console.Write("write your lastname:");
            Console.ReadLine();

            Console.Write("your age:");
            Console.ReadLine();

            Console.Write("your town:");
            Console.ReadLine();

            ;







            Task<long> task1 = Task<long>.Run(() =>
            {
                long sum = 0;
                for (int i = 0; i < 20000; i++) sum += i;
                return sum;
            });

            Console.WriteLine(task1.Result);




            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 10000; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {

                    }
                });
            }

            Console.WriteLine(sw.Elapsed);
            Console.ReadLine();

        }
    }
}
