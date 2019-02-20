using System;
using System.Diagnostics;
using System.Threading;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Bank.Bank<string , string , string> bank = new Core.Bank.Bank<string, string, string>("achou", "hello");
            bank.AddAgent(1);
       
            int number = Process.GetCurrentProcess().Threads.Count;

            Console.WriteLine(Thread.CurrentThread.Name + "current thread +++" + number);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
