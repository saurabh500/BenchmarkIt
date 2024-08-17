
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Benchmarks.BufferedReaderBenchmarks;
using Benchmarks.MemoryStreamAsync;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
#else
            var summary = BenchmarkRunner.Run<MemoryStreamAsyncReadBenchmarks>();
            Console.WriteLine(summary);
#endif
        }
    }
}
