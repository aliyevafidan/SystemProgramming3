using System;
using System.Threading;

namespace SystemProgramming3
{
    internal class Program
    {
        static object lockObject = new object();

        static string cardNumber = "4169 1212 1212 1212";
        static string fullName = "Fidan Aliyeva";
        static decimal balance = 150.0m;

        static void ProcessTransaction(string source, decimal amount)
        {
            lock (lockObject)
            {
                Console.WriteLine($"\n--- {source} Muracieti ---");
                Console.WriteLine($"Kart No: {cardNumber} | Kartin Yiyesi: {fullName}");
                Console.WriteLine($"Teleb olunan mebleg: {amount} AZN | Movcud Balans: {balance} AZN");

                if (balance >= amount)
                {
                    balance -= amount;
                    Console.WriteLine($"[UGURLU] {source} terefinden {amount} AZN cixarildi. Qaliq balans: {balance} AZN");
                }
                else
                {
                    Console.WriteLine($"[XETA] {source} ucun balans kifayet etmedi! Movcud balans: {balance} AZN");
                }
            }
        }

        static void Main(string[] args)
        {
            Thread marketThread = new Thread(() => ProcessTransaction("Market", 100.0m));
            Thread atmThread = new Thread(() => ProcessTransaction("ATM", 80.0m));

            marketThread.Start();
            atmThread.Start();

            marketThread.Join();
            atmThread.Join();

            Console.WriteLine($"\nEmeliyyatlar bitdi. Yekun balans: {balance} AZN");
        }
    }
}