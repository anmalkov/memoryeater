using System;
using System.Diagnostics;
using System.Threading;

namespace MemoryEater
{
    class Program
    {
        static volatile byte[] _buffer;

        static void Main(string[] args)
        {
            Console.WriteLine($"Wait 10 sec before start");
            Trace.WriteLine($"Wait 10 sec before start");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine($"GO!");
            Trace.WriteLine($"GO!");
            var i = 1;
            while (true)
            {
                _buffer = new byte[i * 1024 * 1024];
                if (i % 10 == 0)
                {
                    Console.WriteLine($"Allocated {i} MB");
                    Trace.WriteLine($"Allocated {i} MB");
                }
                i++;
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}
