
using BenchmarkDotNet.Running;
using Benchmarks.BufferedReaderBenchmarks;
using Benchmarks.MemoryStreamAsync;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MemoryStreamAsyncReadBenchmarks>();
            Console.WriteLine(summary);
        }
    }
}
