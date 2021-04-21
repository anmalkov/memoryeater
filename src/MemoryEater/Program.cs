using System;
using System.Diagnostics;
using System.Threading;

namespace MemoryEater
{
    class Program
    {
        private const string EatMegabytesEnvVariableName = "MEMORYEATER_EATMEGABYTES";
        private const string DelayInSecondsEnvVariableName = "MEMORYEATER_DELAYINSECONDS";
        private const int DafaultEatMegabytes = 1;
        private const int DafaultDelayInSeconds = 1;

        static volatile byte[] _buffer;

        static void Main(string[] args)
        {
            var eatMegabytes = DafaultEatMegabytes;
            var eatMegabyterFromEnvVariable = Environment.GetEnvironmentVariable(EatMegabytesEnvVariableName);
            if (!string.IsNullOrEmpty(eatMegabyterFromEnvVariable)) {
                eatMegabytes = int.Parse(eatMegabyterFromEnvVariable);
            }
            var delayInSeconds = DafaultDelayInSeconds;
            var delayInSecondsFromEnvVariable = Environment.GetEnvironmentVariable(DelayInSecondsEnvVariableName);
            if (!string.IsNullOrEmpty(delayInSecondsFromEnvVariable)) {
                delayInSeconds = int.Parse(delayInSecondsFromEnvVariable);
            }
            Console.WriteLine($"I'm going to eat {eatMegabytes} MB each {delayInSeconds} seconds");
            Console.WriteLine();

            Console.WriteLine($"Wait 10 seconds before start");
            Console.WriteLine();
            Thread.Sleep(TimeSpan.FromSeconds(10));
            
            Console.WriteLine($"GO!");
            var i = eatMegabytes;
            while (true)
            {
                _buffer = new byte[i * 1024 * 1024];
                Console.WriteLine($"Allocated {i} MB");
                i = i + eatMegabytes;
                Thread.Sleep(TimeSpan.FromSeconds(delayInSeconds));
            }
        }
    }
}
