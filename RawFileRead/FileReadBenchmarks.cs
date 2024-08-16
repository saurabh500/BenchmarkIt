using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.RawFileRead
{
    public class FileReadBenchmarks
    {
        private const string FilePath = "Z:\\dev\\urandom";
        private const int BufferSize = 512;

        [Benchmark]
        public async Task ReadInChunksAsync()
        {
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, useAsync: true))
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
        public async Task ReadInt32ValuesAsync()
        {
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, sizeof(int), useAsync: true))
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