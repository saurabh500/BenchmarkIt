
using BenchmarkDotNet.Running;
using Benchmarks.BufferedReader;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BufferedAsyncReadBenchamarks>();
            Console.WriteLine(summary);
        }
    }
}
