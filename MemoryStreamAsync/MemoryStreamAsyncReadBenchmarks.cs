

using BenchmarkDotNet.Attributes;
using System.Buffers.Binary;

namespace Benchmarks.MemoryStreamAsync
{
    [MemoryDiagnoser]
    [AsciiDocExporter]
    [CsvExporter]
    [MarkdownExporterAttribute.Default]
    [MarkdownExporterAttribute.GitHub]
    public class MemoryStreamAsyncReadBenchmarks
    {
        private const int BufferSize = 512;
        byte[] _buffer = new byte[BufferSize];

        public MemoryStreamAsyncReadBenchmarks()
        {
            for (int i = 0; i < BufferSize; i++)
            {
                _buffer[i] = (byte)i;
            }
        }

        [Benchmark]
        public async Task ReadInChunksAsync()
        {
            using (var stream = new MemoryStream(_buffer))
            {
                byte[] buffer = new byte[BufferSize];
                int bytesRead = await stream.ReadAsync(buffer, 0, BufferSize);
                for (int i = 0; i < bytesRead; i += sizeof(int))
                {
                    int value = BinaryPrimitives.ReadInt32LittleEndian(buffer.AsSpan());
                    // Process the value if needed
                }
            }
        }

        [Benchmark]
        [Arguments(0)]
        [Arguments(3)]
        [Arguments(5)]
        [Arguments(9)]
        [Arguments(15)]
        public async Task ReadWithDepth(int depth)
        {
            await TerminateOn(0, depth);
        }

        private async Task TerminateOn(int currentDepth, int depth)
        {
            if (currentDepth < depth)
            {
                await TerminateOn(currentDepth+1, depth);
            }
            else
            {
                await ReadInChunksAsync();
            }
        }

        [Benchmark]
        public async Task ReadInt32ValuesAsync()
        {
            using (var stream = new MemoryStream(_buffer))
            {
                byte[] buffer = new byte[sizeof(int)];
                int totalBytesRead = 0;
                while (totalBytesRead < BufferSize)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, sizeof(int));
                    if (bytesRead == 0) break;
                    totalBytesRead += bytesRead;
                    int value = BinaryPrimitives.ReadInt32LittleEndian(buffer.AsSpan());
                    // Process the value if needed
                }
            }
        }
    }

}
