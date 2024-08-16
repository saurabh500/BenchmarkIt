using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarks.BufferedReaderBenchmarks
{
    public class BufferedReader : Stream
    {
        private readonly FileStream _fileStream;
        private readonly Memory<byte> _buffer;
        private int _bufferPosition;
        private int _bufferLength;

        public BufferedReader(FileStream fileStream, int bufferSize)
        {
            _fileStream = fileStream ?? throw new ArgumentNullException(nameof(fileStream));
            _buffer = new byte[bufferSize];
            _bufferPosition = 0;
            _bufferLength = 0;
        }

        public override bool CanRead => _fileStream.CanRead;
        public override bool CanSeek => _fileStream.CanSeek;
        public override bool CanWrite => false;
        public override long Length => _fileStream.Length;
        public override long Position
        {
            get => _fileStream.Position - _bufferLength + _bufferPosition;
            set => throw new NotSupportedException();
        }

        public override void Flush() => _fileStream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fileStream?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken ct)
        {
            int count = buffer.Length;
            int bytesRead = 0;
            while (count >0)
            {
                if (_bufferPosition >= _bufferLength)
                {
                    _bufferLength = await _fileStream.ReadAsync(_buffer, ct).ConfigureAwait(false);
                    _bufferPosition = 0;
                    if (_bufferLength == 0)
                    {
                        break;
                    }
                }
                int bytesToCopy = Math.Min(count, _bufferLength - _bufferPosition);
                _buffer.Slice(_bufferPosition, bytesToCopy).CopyTo(buffer.Slice(0, bytesToCopy));
                _bufferPosition += bytesToCopy;
                count -= bytesToCopy;
                buffer = buffer.Slice(bytesToCopy);
                bytesRead += bytesToCopy;
            }
            return bytesRead;
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
