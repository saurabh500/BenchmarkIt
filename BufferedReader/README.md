# How expensive is the async statemachine?

# Problem statement

Should I write code to make multiple async calls to an object with buffered data, or should I read a big chunk of data expected, and work synchronously on top of the data?

# The benchmark 

## Setup

1. `class BufferedReader` takes the underlying stream and buffers the size of data specified in the constructor. When reading from it, if the data is available in the buffer, it is returned, else it reads the stream for data.

Note: This benchmark relies on `/dev/urandom` from Linux to get a random stream of bytes. On Windows, to run this, I used WSL and mounted my WSL's root folder to network drive Z: and ran the benchmark.

1. The `ReadInChunksAsync` function reads 512 bytes of data in memory and repeatedly converts that to int32 till it has read through the 512 bytes.

2. The `ReadInt32ValuesAsync` function reads from the buffered stream on demand. To create an int32, it first reads 32 bytes from the stream and then converts it to an int32. This simulates multiple async calls to buffered data, to create an int32


# Benchmark result

|               Method |     Mean |     Error |    StdDev |   Median |
|--------------------- |---------:|----------:|----------:|---------:|
|    ReadInChunksAsync | 3.023 ms | 0.1279 ms | 0.3732 ms | 2.918 ms |
| ReadInt32ValuesAsync | 4.222 ms | 0.3234 ms | 0.9536 ms | 4.012 ms |
