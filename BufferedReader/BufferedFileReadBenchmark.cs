

using BenchmarkDotNet.Attributes;
using System.Buffers.Binary;

namespace Benchmarks.BufferedReaderBenchmarks
{
    [MemoryDiagnoser]
    [AsciiDocExporter]
    [CsvExporter]
    [MarkdownExporterAttribute.Default]
    [MarkdownExporterAttribute.GitHub]
    public class BufferedAsyncReadBenchamarks
    {
        private const string FilePath = "Z:\\dev\\urandom";
        private const int BufferSize = 512;

        [Benchmark]
        public async Task ReadInChunksAsync()
        {
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, useAsync: true))
            {
                using (var bStream = new BufferedReader(stream, BufferSize))
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead = await bStream.ReadAsync(buffer.AsMemory(), default);
                    for (int i = 0; i < bytesRead; i += sizeof(int))
                    {
                        int value = BinaryPrimitives.ReadInt32LittleEndian(buffer.AsSpan());
                        // Process the value if needed
                    }
                }
            }
        }

        [Benchmark]
        public async Task ReadInt32ValuesAsync()
        {
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, sizeof(int), useAsync: true))
            {
                using (var bStream = new BufferedReader(stream, BufferSize))
                {
                    byte[] buffer = new byte[sizeof(int)];
                    int totalBytesRead = 0;
                    while (totalBytesRead < BufferSize)
                    {
                        int bytesRead = await bStream.ReadAsync(buffer.AsMemory(), default);
                        if (bytesRead == 0) break;
                        totalBytesRead += bytesRead;
                        int value = BinaryPrimitives.ReadInt32LittleEndian(buffer.AsSpan());
                        // Process the value if needed
                    }
                }
            }
        }
    }
}
